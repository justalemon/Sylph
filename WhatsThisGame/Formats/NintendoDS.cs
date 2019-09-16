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
        /// <summary>
        /// The list of Nintendo DS/DSi developers.
        /// </summary>
        private static readonly Dictionary<string, string> Developers = new Dictionary<string, string>
        {
            { "01", "Nintendo" },
            { "02", "Rocket Games" },
            { "03", "Imagineer Zoom" },
            { "04", "Gray Matter" },
            { "05", "Zamuse" },
            { "06", "Falcom" },
            { "07", "Enix" },
            { "08", "Capcom" },
            { "09", "Hot B" },
            { "0A", "Jaleco" },
            { "13", "Electronic Arts Japan" },
            { "18", "Hudson Entertainment" },
            { "20", "Destination Software" },
            { "36", "Codemasters" },
            { "41", "Ubisoft" },
            { "4A", "Gakken" },
            { "4F", "Eidos" },
            { "4Q", "Disney Interactive Studios" },
            { "4Z", "Crave Entertainment" },
            { "52", "Activision" },
            { "54", "Rockstar Games" },
            { "5D", "Midway" },
            { "5G", "Majesco Entertainment" },
            { "64", "LucasArts Entertainment" },
            { "69", "Electronic Arts" },
            { "6K", "UFO Interactive" },
            { "6V", "JoWooD Entertainment" },
            { "70", "Atari" },
            { "78", "THQ" },
            { "7D", "Vivendi Universal Games" },
            { "7J", "Zoo Digital Publishing" },
            { "7N", "Empire Interactive" },
            { "7U", "Ignition Entertainment" },
            { "7V", "Summitsoft Entertainment" },
            { "8J", "General Entertainment" },
            { "8P", "SEGA" },
            { "99", "Rising Star Games" },
            { "A4", "Konami" },
            { "AF", "Namco" },
            { "B2", "Bandai" },
            { "E9", "Natsume" },
            { "EB", "Atlus" },
            { "FH", "Foreign Media Games" },
            { "FK", "The Game Factory" },
            { "FP", "Mastiff" },
            { "FR", "DTP Young" },
            { "G9", "D3Publisher" },
            { "GD", "SQUARE ENIX" },
            { "GL", "Gameloft" },
            { "GN", "Oxygen Interactive" },
            { "GR", "GSP" },
            { "GT", "505 Games" },
            { "GQ", "Engine Software" },
            { "GY", "The Game Factory" },
            { "H3", "Zen" },
            { "H4", "SNK PLAYMORE" },
            { "H6", "MYCOM" },
            { "HC", "Plato" },
            { "HF", "Level 5" },
            { "HG", "Graffiti Entertainment" },
            { "HM", "HMH INTERACTIVE" },
            { "HV", "BHV Software" },
            { "LR", "Asylum Entertainment" },
            { "KJ", "Gamebridge" },
            { "KM", "Deep Silver" },
            { "MJ", "MumboJumbo" },
            { "MT", "Blast Entertainment" },
            { "NK", "Neko Entertainment" },
            { "NP", "Nobilis Publishing" },
            { "PG", "Phoenix Games" },
            { "PL", "Playlogic" },
            { "SU", "Slitherine Software" },
            { "SV", "SevenOne Intermedia" },
            { "RM", "Rondomedia" },
            { "RT", "RTL Games" },
            { "TK", "Tasuke" },
            { "TR", "Tetris Online" },
            { "TV", "Tivola Publishing" },
            { "VP", "Virgin Play" },
            { "WP", "White Park Bay" },
            { "WR", "Warner Bros" },
            { "XS", "Aksys Games" },
        };

        public NintendoDS(Stream stream) : base(stream)
        {
            // Open a BinaryReader
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // For safety reasons, go to the start of the stream
                reader.BaseStream.Position = 0;

                // Get the characters of the title
                Title = new string(reader.ReadChars(12)).Trim();

                // Then, set the position for the console ID
                reader.BaseStream.Position = 0x012;
                // Read the next byte
                byte ConsoleID = reader.ReadByte();
                // And obtain the name from the respective dict
                Console = Consoles.ContainsKey(ConsoleID) ? Consoles[ConsoleID] : $"Unknown (ID: {ConsoleID})";

                // Now is time for the cartdige identifier (also known as game code)
                // Set the position to the 4 characters code
                reader.BaseStream.Position = 0xC;
                // Read the characters and trim the whitespaces
                string Code = new string(reader.ReadChars(4)).Trim();
                // To create the real code, we need to get the destination console for the game
                string BaseID = Console == "Nintendo DSi" ? "TWL-" : "NTR-";
                Identifier = string.IsNullOrWhiteSpace(Code) ? string.Empty : BaseID + Code;

                // Time for the developer name!
                // Set the position to the 4 characters code
                reader.BaseStream.Position = 0x10;
                // And read the two characters as a string
                string DevCode = new string(reader.ReadChars(2));
                // Now, set the developer name
                Developer = Developers.ContainsKey(DevCode) ? Developers[DevCode] : $"Unknown (Makercode: {DevCode})";

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
