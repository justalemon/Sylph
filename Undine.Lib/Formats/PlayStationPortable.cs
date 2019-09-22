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

        public PlayStationPortable(BinaryReader reader, int header = -1) : base(reader)
        {
            // If no header was sent
            if (header == -1)
            {
                // Check if is compatible and return the header
                if (!IsCompatible(reader, out header))
                {
                    // If is not, throw an exception
                    throw new InvalidOperationException("The open file is not a PSP UMD.");
                }
            }

            // Set our start position plus a couple of other bytes
            reader.BaseStream.Position = header + 8;
            // Check if the header is huge or not
            bool huge = reader.ReadByte() == 0xE4;

            // Move the stream to the location of the identifier
            reader.BaseStream.Position = header + (huge ? 0x178 : 0x128);
            // This is a special case, because some PS1/2/3/P discs can have two characters at the end
            // So we need to grab a total of 11 characters (4 region, 5 numbers and 2 possible characters)
            Identifier = Encoding.UTF8.GetString(reader.ReadBytes(11)).Trim();

            // Set the position for the title
            reader.BaseStream.Position = header + (huge ? 0x1AC : 0x158);
            // Grab the 80 next bytes (short PSF is 80 and long PSF is 84) and save them as the title
            Title = Encoding.UTF8.GetString(reader.ReadBytes(0x74)).Trim();

            // For the console, the PSP does not has exclusives for certain variations
            Console = "PlayStation Portable";

            // The region on PlayStation Platforms is the 3rd Character on the Identifier
            char Character = Identifier[2];
            Region = Regions.ContainsKey(Character) ? Regions[Character].Name : $"Unknown (code {Character})";
        }

        public static bool IsCompatible(BinaryReader reader, out int header)
        {
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
                    // Save the header and return success
                    header = i * 4096;
                    return true;
                }
            }

            // If we got here, is because we failed to find the header
            header = -1;
            return false;
        }
    }
}
