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

            // Finally, proceed to check the region
            // The following regions do not exist: B and G
            char Character = Identifier[Identifier.Length - 1];
            switch (Character)
            {
                case 'A':
                    Region = "Asia";
                    Identifier += "-ASI";
                    break;
                case 'C':
                    Region = "China";
                    Identifier += "-CHN";
                    break;
                case 'D':
                    if (Identifier == "NTR-BQZD" || Identifier == "NTR-BMYD")
                    {
                        Region = "Germany";
                        Identifier += "-GER";
                    }
                    else
                    {
                        Region = "Europe";
                        Identifier += "-NOE";
                    }
                    break;
                case 'E':
                    Region = "United States of America";
                    Identifier += "-USA";
                    break;
                case 'F':
                    Region = "France";
                    Identifier += "-FRA";
                    break;
                case 'H':
                    Region = "Holland/Netherlands";
                    Identifier += "-HOL";
                    break;
                case 'I':
                    Region = "Italy";
                    Identifier += "-ITA";
                    break;
                case 'J':
                    Region = "Japan";
                    Identifier += "-JPN";
                    break;
                case 'K':
                    Region = "South Korea";
                    Identifier += "-KOR";
                    break;
                case 'L':
                    Region = "United States of America";
                    Identifier += "-USA";
                    break;
                case 'M':
                    Region = "Sweeden";
                    Identifier += "-SWE";
                    break;
                case 'N':
                    Region = "Norway";
                    Identifier += "-NOR";
                    break;
                case 'O':
                    Region = "International";
                    Identifier += "-INT";
                    break;
                case 'P':
                    Region = "Europe";
                    Identifier += "-EUR";
                    break;
                case 'Q':
                    Region = "Denmark";
                    Identifier += "-DEN";
                    break;
                case 'R':
                    Region = "Russia";
                    Identifier += "-RUS";
                    break;
                case 'S':
                    Region = "Spain/Latin America";
                    Identifier += "-SPA";
                    break;
                case 'U':
                    Region = "Australia";
                    Identifier += "-AUS";
                    break;
                case 'V':
                    if (Identifier.StartsWith("NTR-"))
                    {
                        Region = "United Kingdom";
                        Identifier += "-UKV";
                    }
                    else
                    {
                        Region = "Europe/Schengen Area";
                        Identifier += "-EUR/EUU";
                    }
                    break;
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    Region = "Europe";
                    Identifier += "-EUU";
                    break;
                default:
                    Region = "Unknown";
                    break;
            }
        }

        public new static bool IsCompatible(Stream stream)
        {
            // On 0x015C, there should be 0x56 and 0xCF as a checksum for the Nintendo Boot Logo
            stream.Position = 0x015C;
            // If there is, tell the user that is valid
            if (stream.ReadByte() == 0x56 && stream.ReadByte() == 0xCF)
            {
                return true;
            }
            // Otherwise, say nope
            return false;
        }
    }
}
