using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsThisGame.Formats
{
    public class PlayStationPortable : Format
    {
        public PlayStationPortable(Stream stream) : base(stream)
        {
            // Store the place where the header starts
            long Start = 0;
            // Reset the position of the stream just in case
            stream.Position = 0;
            // While we are not at the end of the stream
            while (stream.Position != stream.Length)
            {
                // Search for "P"
                if (stream.ReadByte() == 0x50)
                {
                    // Then "S"
                    if (stream.ReadByte() == 0x53)
                    {
                        // And finally "F"
                        if (stream.ReadByte() == 0x46)
                        {
                            // Move 4 spaces
                            stream.Position += 4;
                            // If the following character is not a "D", we are in the correct place
                            if (stream.ReadByte() != 0x44)
                            {
                                Start = stream.Position;
                                break;
                            }
                        }
                    }
                }
            }

            // Let's grab the title!
            // Go to the start of the title
            stream.Position = 0x6F28158;
            // Grab the first 12 bytes
            byte[] TitleBytes = new byte[80];
            stream.Read(TitleBytes, 0, 80);
            // And save them
            Title = Encoding.UTF8.GetString(TitleBytes).Trim();

            // For the console, the PSP does not has exclusives for certain variations
            Console = "PlayStation Portable";

            // For the CD/DVD Identifier, is located at 0x6F28128
            stream.Position = 0x6F28128;
            // TODO: There are some DVDs that have larger Identifiers
            // EXAMPLE: GTA Double Pack is SLUS-27003GH and Guitar Hero 1 and 2 is SLUS-21224P2
            byte[] IDBytes = new byte[12];
            stream.Read(IDBytes, 0, 12);
            Identifier = Encoding.UTF8.GetString(IDBytes).Trim();

            // The region leave it empty for now
            Region = "";
        }

        public new static bool IsCompatible(Stream stream)
        {
            // I could not find information about a UMD ISO, so I did my quick own research
            // From 0x8008 to 0x800F, you can find the phrase "PSP GAME" at the same exact position every time
            // This can be confirmed with the following UMDs: NPJH50701, UCUS98766 and ULUS10490

            // Let's start by moving onto the position of the P
            stream.Position = 0x8008;
            // If the following characters make "PSP GAME", this is a UMD
            if (stream.ReadByte() == 0x50 && stream.ReadByte() == 0x53 && stream.ReadByte() == 0x50 && stream.ReadByte() == 0x20 && // "PSP "
                stream.ReadByte() == 0x47 && stream.ReadByte() == 0x41 && stream.ReadByte() == 0x4D && stream.ReadByte() == 0x45) // "GAME"
            {
                return true;
            }
            // Otherwise, return false
            return false;
        }
    }
}
