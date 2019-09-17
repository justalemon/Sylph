using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Undine.Formats
{
    public class PlayStationPortable : Format
    {
        /// <summary>
        /// The regions available for the PS1/2/3/4 Disks, UMDs and Vita Cards.
        /// </summary>
        private static readonly Dictionary<char, RegionSet> Regions = new Dictionary<char, RegionSet>
        {
            { 'A', new RegionSet("Asia", "A") },
            { 'C', new RegionSet("China", "C") },
            { 'E', new RegionSet("Europe", "E") },
            { 'H', new RegionSet("Hong Kong", "H") },
            { 'J', new RegionSet("Japan", "J") },
            { 'K', new RegionSet("Korea", "K") },
            { 'U', new RegionSet("United States of America", "U") },
        };

        public PlayStationPortable(BinaryReader reader) : base(reader)
        {
            // Create a place to store the position and the metadata size
            long Start = -1;
            bool Huge = false;
            // Move the stream to the start just in case
            reader.BaseStream.Position = 0;

            // Read the stream in chunks of 4096
            for (int i = 0; i * 4096 < reader.BaseStream.Length; i++)
            {
                // Set the correct position of the stream
                reader.BaseStream.Position = i * 4096;

                // Grab the first 20 characters that correspond to the PARAM.SFO header
                byte[] Header = reader.ReadBytes(20);

                // If the header is a PSF of version 1.1 and the location of the table start is either 0xE4 or 0xB4
                if (Header[0] == 0x00 && Header[1] == 0x50 && Header[2] == 0x53 && Header[3] == 0x46 &&
                    Header[4] == 0x01 && Header[5] == 0x01 && Header[6] == 0x00 && Header[7] == 0x00 &&
                    (Header[8] == 0xE4 || Header[8] == 0xB4))
                {
                    // Check if we have a header bigger than normal
                    Huge = Header[8] == 0xE4;
                    // Select the correct start position based on the header size
                    Start = reader.BaseStream.Position + (Huge ? 0x164 : 0x114);
                    // And break the iterator
                    break;
                }
            }

            // For the UMD Identifier, is exactly after our start point
            reader.BaseStream.Position = Start;
            // This is a special case, because some PS1/2/3/P discs can have two characters at the end
            // So let's grab a total of 11 characters (4 region, 5 numbers and 2 possible characters)
            byte[] IDBytes = reader.ReadBytes(11);
            // Create the base of the identifier
            Identifier = Encoding.UTF8.GetString(IDBytes).Trim();

            // Set the position for the title
            reader.BaseStream.Position = Start + (Huge ? 0x34 : 0x30);
            // Grab the 80 next bytes (short PSF is 80 and long PSF is 84)
            byte[] TitleBytes = new byte[0x74];
            reader.BaseStream.Read(TitleBytes, 0, 0x74);
            // And save them as the title
            Title = Encoding.UTF8.GetString(TitleBytes).Trim();

            // For the console, the PSP does not has exclusives for certain variations
            Console = "PlayStation Portable";

            // The region on PlayStation Platforms is the 3rd Character on the Identifier
            char Character = Identifier[2];
            Region = Regions.ContainsKey(Character) ? Regions[Character].Name : $"Unknown (code {Character})";
        }

        public new static bool IsCompatible(BinaryReader reader)
        {
            // I could not find information about a UMD ISO, so I did my quick own research
            // From 0x8008 to 0x800F, you can find the phrase "PSP GAME" at the same exact position every time
            // This can be confirmed with the following UMDs: NPJH50701, UCUS98766 and ULUS10490

            // Let's start by moving onto the position of the P
            reader.BaseStream.Position = 0x8008;
            // If the following characters make "PSP GAME", this is a UMD
            if (reader.ReadByte() == 0x50 && reader.ReadByte() == 0x53 && reader.ReadByte() == 0x50 && reader.ReadByte() == 0x20 && // "PSP "
                reader.ReadByte() == 0x47 && reader.ReadByte() == 0x41 && reader.ReadByte() == 0x4D && reader.ReadByte() == 0x45) // "GAME"
            {
                return true;
            }
            // Otherwise, return false
            return false;
        }
    }
}
