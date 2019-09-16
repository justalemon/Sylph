using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsThisGame.Formats
{
    /// <summary>
    /// Nintendo DS and DSi: A pair of handhelds released by Nintendo.
    /// </summary>
    public class NintendoDS : Format
    {
        /// <summary>
        /// The possible console values of a cart.
        /// </summary>
        private static readonly Dictionary<byte, string> Consoles = new Dictionary<byte, string>
        {
            { 0, "Nintendo DS" },
            { 2, "Nintendo DS (DSi-enhanced)" },
            { 3, "Nintendo DSi" },
        };
        public NintendoDS(Stream stream) : base(stream)
        {
            // For safety reasons, go to the start of the stream
            stream.Position = 0;

            // Open a BinaryReader
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // Get the characters of the title
                Title = new string(reader.ReadChars(12)).Trim();
                // Then, get the console for the game
                reader.BaseStream.Position = 0x012;
                byte ConsoleID = reader.ReadByte();
                Console = Consoles.ContainsKey(ConsoleID) ? Consoles[ConsoleID] : $"Unknown (ID: {ConsoleID})";

                // Now is time for the cartdige identifier (also known as game code)
                // Set the position to the 4 characters code
                reader.BaseStream.Position = 0xC;
                // Read the characters and trim the whitespaces
                string Code = new string(reader.ReadChars(4)).Trim();
                // To create the real code, we need to get the destination console for the game
                string BaseID = Console == "Nintendo DSi" ? "TWL-" : "NTR-";
                Identifier = string.IsNullOrWhiteSpace(Code) ? string.Empty : BaseID + Code;

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
