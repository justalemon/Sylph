using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsThisGame.Formats
{
    /// <summary>
    /// Handheld by Nintendo.
    /// </summary>
    public class NintendoDS : Format
    {
        public NintendoDS(Stream stream) : base(stream)
        {
            // Let's grab the title!
            // Go to the start of the stream
            stream.Position = 0;
            // Grab the first 12 bytes
            byte[] TitleBytes = new byte[12];
            stream.Read(TitleBytes, 0, 12);
            // And save them
            Title = Encoding.UTF8.GetString(TitleBytes).Trim();

            // Let's finish with the console for the game
            // Return to the start of the file
            stream.Position = 0x012;
            // Check what console corresponds to the ID
            switch (stream.ReadByte())
            {
                // For zero, is NDS only
                case 0:
                    Console = "Nintendo DS";
                    break;
                // For two, is NDS + DSi enhanced
                case 2:
                    Console = "Nintendo DS (DSi-enhanced)";
                    break;
                // For three, is a DSi exclusive
                case 3:
                    Console = "Nintendo DSi";
                    break;
            }

            // Let's continue with the Cartdige Identifier
            // Go to the respective position
            stream.Position = 0xC;
            // Grab the 4 bytes
            byte[] IDBytes = new byte[4];
            stream.Read(IDBytes, 0, 4);
            // If the 4 bytes are zeroes, this is homebrew so return an empty string
            if (IDBytes == new byte[4] { 0, 0, 0, 0 })
            {
                Identifier = "";
            }
            // Otherwise, save the identifier with NTR (DS) or TWL (DSI) appended
            else
            {
                string BaseID = Console == "Nintendo DSi" ? "TWL-" : "NTR-";
                Identifier = BaseID + Encoding.UTF8.GetString(IDBytes).Trim();
            }
        }

        public new static bool IsCompatible(Stream stream)
        {
            // On 0x015C, there should be 0x56 and 0xCF as a checksum for the Nintendo Boot Logo
            byte[] Checksum = new byte[2];
            stream.Position = 0x015C;
            stream.Read(Checksum, 0, 2);
            // If there is, tell the user that is valid
            if (Checksum[0] == 0x56 && Checksum[1] == 0xCF)
            {
                return true;
            }
            // Otherwise, say nope
            return false;
        }
    }
}
