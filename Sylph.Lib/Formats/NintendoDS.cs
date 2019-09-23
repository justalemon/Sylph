using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sylph.Extensions;

namespace Sylph.Formats
{
    /// <summary>
    /// Nintendo DS and DSi: A pair of handhelds released by Nintendo.
    /// </summary>
    public class NintendoDS : Format
    {
        /// <summary>
        /// The basic game title used for internal operations.
        /// </summary>
        [ExtendedInformation]
        public string RAWTitle { get; }
        /// <summary>
        /// The 4 character cart code.
        /// </summary>
        [ExtendedInformation]
        public string GameCode { get; }
        /// <summary>
        /// The 2 character developer/publisher identifier.
        /// </summary>
        /// <seealso cref="Developers"/>
        [ExtendedInformation]
        public string MakerCode { get; }
        /// <summary>
        /// The destination console for this cart.
        /// </summary>
        /// <seealso cref="Consoles"/>
        [ExtendedInformation]
        public byte UnitCode { get; }
        /// <summary>
        /// Encryption Seed Select (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public byte EncryptionSeed { get; }
        /// <summary>
        /// The ID for the chip capacity.
        /// </summary>
        [ExtendedInformation]
        public byte DeviceCapacity { get; }
        /// <summary>
        /// Reserved area #1 (usually zero filled).
        /// </summary>
        [ExtendedInformation]
        public byte[] Reserved01 { get; }
        /// <summary>
        /// Reserved area #2 (usually zero, except on DSi)
        /// </summary>
        [ExtendedInformation]
        public byte[] Reserved02 { get; }
        /// <summary>
        /// The internal region code (0x00=Normal, 0x80=China, 0x40=Korea).
        /// This is used to restrict the use of iQue Roms (China exclusive) on normal DS/DSi and vice versa.
        /// </summary>
        [ExtendedInformation]
        public byte InternalRegion { get; }
        /// <summary>
        /// The version of this rom.
        /// </summary>
        [ExtendedInformation]
        public byte Version { get; }
        /// <summary>
        /// If the Rom can bypass the "Touch...to continue." during the game boot.
        /// </summary>
        [ExtendedInformation]
        public bool AutoStart { get; }
        /// <summary>
        /// The Rom Offset of the ARM9 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM9RomOffset { get; }
        /// <summary>
        /// The entry point for the ARM9 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM9EntryAddress { get; }
        /// <summary>
        /// The RAM address of the ARM9 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM9RamAddress { get; }
        /// <summary>
        /// The size of the ARM9 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM9Size { get; }
        /// <summary>
        /// The Rom Offset of the ARM7 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM7RomOffset { get; }
        /// <summary>
        /// The entry point for the ARM7 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM7EntryAddress { get; }
        /// <summary>
        /// The RAM address of the ARM7 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM7RamAddress { get; }
        /// <summary>
        /// The size of the ARM7 code.
        /// </summary>
        [ExtendedInformation]
        public uint ARM7Size { get; }
        /// <summary>
        /// The offset of the File Name Table.
        /// </summary>
        [ExtendedInformation]
        public uint FNTOffset { get; }
        /// <summary>
        /// The size of the File Name Table.
        /// </summary>
        [ExtendedInformation]
        public uint FNTSize { get; }
        /// <summary>
        /// The offset of the File Allocation Table.
        /// </summary>
        [ExtendedInformation]
        public uint FATOffset { get; }
        /// <summary>
        /// The size of the File Allocation Table.
        /// </summary>
        [ExtendedInformation]
        public uint FATSize { get; }
        /// <summary>
        /// File ARM9 overlay_offset (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint ARM9OverlayOffset { get; }
        /// <summary>
        /// File ARM9 overlay_size (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint ARM9OverlaySize { get; }
        /// <summary>
        /// File ARM7 overlay_offset (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint ARM7OverlayOffset { get; }
        /// <summary>
        /// File ARM7 overlay_size (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint ARM7OverlaySize { get; }
        /// <summary>
        /// Port 0x40001A4 setting for normal commands (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint PortNormalCommands { get; }
        /// <summary>
        /// Port 0x40001A4 setting for KEY1 commands (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint PortKEY1Commands { get; }
        /// <summary>
        /// Location of the Icon and Localized Titles on the ROM.
        /// </summary>
        [ExtendedInformation]
        public uint IconTitleOffset { get; }
        /// <summary>
        /// The CRC-16 between 0x20 and 0x7FFF.
        /// </summary>
        [ExtendedInformation]
        public ushort SecureAreaCRC16 { get; }
        /// <summary>
        /// Secure Area Delay (in 131kHz units) (051Eh=10ms or 0D7Eh=26ms) (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public ushort SecureAreaDelay { get; }
        /// <summary>
        /// ARM9 Auto Load List Hook RAM Address (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint ARM9AutoLoad { get; }
        /// <summary>
        /// ARM7 Auto Load List Hook RAM Address (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public uint ARM7AutoLoad { get; }
        /// <summary>
        /// Secure Area Disable (by encrypted "NmMdOnly") (usually zero) (GBATEK).
        /// </summary>
        [ExtendedInformation]
        public ulong SecureAreaDisable { get; }
        /// <summary>
        /// The total size of the area being used on the ROM.
        /// </summary>
        [ExtendedInformation]
        public uint ROMSize { get; }
        /// <summary>
        /// The size of the ROM header (0x4000).
        /// </summary>
        [ExtendedInformation]
        public uint HeaderSize { get; }
        /// <summary>
        /// Reserved area #3 (usually zero filled, except 0x88 to 0x93 on DSi).
        /// </summary>
        [ExtendedInformation]
        public byte[] Reserved03 { get; }
        /// <summary>
        /// Reserved area #4 (usually zero filled, if set to "DoNotZeroFillMem" disables fastboot).
        /// </summary>
        [ExtendedInformation]
        public byte[] Reserved04 { get; }
        [ExtendedInformation]
        public byte[] Reserved05 { get; }
        [ExtendedInformation]
        public ushort NintendoLogoCRC16 { get; }
        [ExtendedInformation]
        public ushort HeaderCRC16 { get; }
        [ExtendedInformation]
        public uint DebugROMOffset { get; }
        [ExtendedInformation]
        public uint DebugSize { get; }
        [ExtendedInformation]
        public uint DebugRAMAddress { get; }
        [ExtendedInformation]
        public byte[] Reserved06 { get; }
        [ExtendedInformation]
        public byte[] Reserved07 { get; }

        [ExtendedInformation]
        public ushort IconTitleVersion { get; }
        [ExtendedInformation]
        public ushort EntriesCRC16 { get; }
        [ExtendedInformation]
        public ushort EntriesCRC16v2 { get; }
        [ExtendedInformation]
        public ushort EntriesCRC16v3 { get; }
        [ExtendedInformation]
        public ushort EntriesCRC16v259 { get; }
        [ExtendedInformation]
        public byte[] ReservedHeader01 { get; }
        [ExtendedInformation]
        public byte[] IconBitmap { get; }
        [ExtendedInformation]
        public byte[] IconPalette { get; }

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
        /// All of the regions that are used on Nintendo DS carts.
        /// </summary>
        private static readonly Dictionary<char, RegionSet> DSRegions = new Dictionary<char, RegionSet>
        {
            { 'A', new RegionSet("Asia", "ASI") },

            { 'C', new RegionSet("China", "CHN") },
            { 'D', new RegionSet("Germany", "GER") },
            { 'E', new RegionSet("United States of America", "USA") },
            { 'F', new RegionSet("France", "FRA") },

            { 'H', new RegionSet("Holland/Netherlands", "HOL") },
            { 'I', new RegionSet("Italy", "ITA") },
            { 'J', new RegionSet("Japan", "JPN") },
            { 'K', new RegionSet("South Korea", "KOR") },
            { 'L', new RegionSet("United States of America", "USA") },
            { 'M', new RegionSet("Sweeden", "SWE") },
            { 'N', new RegionSet("Norway", "NOR") },
            { 'O', new RegionSet("International", "INT") },
            { 'P', new RegionSet("Europe", "EUR") },
            { 'Q', new RegionSet("Denmark", "DEN") },
            { 'R', new RegionSet("Russia", "RUS") },
            { 'S', new RegionSet("Spain/Latin America", "SPA") },
            { 'T', new RegionSet("USA/Australia", "???") },  // TODO: Find the correct code
            { 'U', new RegionSet("Australia", "AUS") },
            { 'V', new RegionSet("United Kingdom/Australia", "UKV") },
            { 'W', new RegionSet("Europe", "EUU") },
            { 'X', new RegionSet("Europe", "EUU") },
            { 'Y', new RegionSet("Europe", "EUU") },
            { 'Z', new RegionSet("Europe", "EUU") },
        };
        /// <summary>
        /// Regions that are used exclusively for DSi carts.
        /// </summary>
        private static readonly Dictionary<char, RegionSet> DSiRegions = new Dictionary<char, RegionSet>
        {
            { 'V', new RegionSet("Europe/Schengen Area", "EUR/EUU") },
        };
        /// <summary>
        /// The list of Nintendo DS/DSi developers/publishers.
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

        public NintendoDS(BinaryReader reader) : base(reader)
        {
            // For safety reasons, go to the start of the stream
            reader.BaseStream.Position = 0;

            // Now, grab every single bit of data from the cart
            // DS Header Spec: http://problemkaputt.de/gbatek.htm#dscartridgeheader
            RAWTitle = new string(reader.ReadChars(12)).SanitizeTitle();
            GameCode = new string(reader.ReadChars(4));
            MakerCode = new string(reader.ReadChars(2));
            UnitCode = reader.ReadByte();
            EncryptionSeed = reader.ReadByte();
            DeviceCapacity = reader.ReadByte();
            Reserved01 = reader.ReadBytes(7);
            Reserved02 = reader.ReadBytes(1);
            InternalRegion = reader.ReadByte();
            Version = reader.ReadByte();
            AutoStart = reader.ReadBoolean();
            ARM9RomOffset = reader.ReadUInt32();
            ARM9EntryAddress = reader.ReadUInt32();
            ARM9RamAddress = reader.ReadUInt32();
            ARM9Size = reader.ReadUInt32();
            ARM7RomOffset = reader.ReadUInt32();
            ARM7EntryAddress = reader.ReadUInt32();
            ARM7RamAddress = reader.ReadUInt32();
            ARM7Size = reader.ReadUInt32();
            FNTOffset = reader.ReadUInt32();
            FNTSize = reader.ReadUInt32();
            FATOffset = reader.ReadUInt32();
            FATSize = reader.ReadUInt32();
            ARM9OverlayOffset = reader.ReadUInt32();
            ARM9OverlaySize = reader.ReadUInt32();
            ARM7OverlayOffset = reader.ReadUInt32();
            ARM7OverlaySize = reader.ReadUInt32();
            PortNormalCommands = reader.ReadUInt32();
            PortKEY1Commands = reader.ReadUInt32();
            IconTitleOffset = reader.ReadUInt32();
            SecureAreaCRC16 = reader.ReadUInt16();
            SecureAreaDelay = reader.ReadUInt16();
            ARM9AutoLoad = reader.ReadUInt32();
            ARM7AutoLoad = reader.ReadUInt32();
            SecureAreaDisable = reader.ReadUInt64();
            ROMSize = reader.ReadUInt32();
            HeaderSize = reader.ReadUInt32();
            Reserved03 = reader.ReadBytes(0x28);
            Reserved04 = reader.ReadBytes(0x10);
            Reserved05 = reader.ReadBytes(0x9C);
            NintendoLogoCRC16 = reader.ReadUInt16();
            HeaderCRC16 = reader.ReadUInt16();
            DebugROMOffset = reader.ReadUInt32();
            DebugSize = reader.ReadUInt32();
            DebugRAMAddress = reader.ReadUInt32();
            Reserved06 = reader.ReadBytes(4);
            Reserved07 = reader.ReadBytes(0x90);
            // Now, move to the Icon and Title area
            // DS Icon/Title Spec: http://problemkaputt.de/gbatek.htm#dscartridgeicontitle
            reader.BaseStream.Position = IconTitleOffset;
            // And start saving values
            IconTitleVersion = reader.ReadUInt16();
            EntriesCRC16 = reader.ReadUInt16();
            EntriesCRC16v2 = reader.ReadUInt16();
            EntriesCRC16v3 = reader.ReadUInt16();
            EntriesCRC16v259 = reader.ReadUInt16();
            ReservedHeader01 = reader.ReadBytes(0x16);
            IconBitmap = reader.ReadBytes(0x200);
            IconPalette = reader.ReadBytes(0x20);
            LocalizedTitles.Add("Japanese", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());
            LocalizedTitles.Add("English", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());
            LocalizedTitles.Add("French", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());
            LocalizedTitles.Add("German", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());
            LocalizedTitles.Add("Italian", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());
            LocalizedTitles.Add("Spanish", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());
            LocalizedTitles.Add("Chinese", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());
            LocalizedTitles.Add("Korean", Encoding.Unicode.GetString(reader.ReadBytes(0x100)).SanitizeTitle());

            // Now, final step: prepare the readable basic information
            // Save the title as: English (RAW Ver.)
            Title = LocalizedTitles["English"] + $" ({RAWTitle})";
            // For the console is easy: Grab it from the Dictionary or print it
            Console = Consoles.ContainsKey(UnitCode) ? Consoles[UnitCode] : $"Unknown (ID: {UnitCode})";
            // To create the real code, we need to get the destination console for the game and append the identifier
            string BaseID = Console == "Nintendo DSi" ? "TWL-" : "NTR-";
            Identifier = string.IsNullOrWhiteSpace(GameCode) ? string.Empty : BaseID + GameCode + "-";
            // Now, set the developer name from the dictionary
            Developer = Developers.ContainsKey(MakerCode) ? Developers[MakerCode] : $"Unknown (MakerCode: {MakerCode})";
            // And finally, proceed to check the region
            // Please note that the following regions do not exist: B and G
            char Character = Identifier[Identifier.Length - 2];
            // If the region is on the DSi only list and this is a DSi Exclusive
            if (DSiRegions.ContainsKey(Character) && Console == "Nintendo DSi")
            {
                Region = DSiRegions[Character].Name;
                Identifier += DSiRegions[Character].Identifier;
            }
            // Or if the region is on the DS list
            else if (DSRegions.ContainsKey(Character))
            {
                Region = DSRegions[Character].Name;
                Identifier += DSRegions[Character].Identifier;
            }
            // Otherwise, set the region to unknown and print the region code
            else
            {
                Region = $"Unknown (code {Character})";
            }
        }

        public static bool IsCompatible(BinaryReader reader)
        {
            // On 0x015C, there should be 0x56 and 0xCF as a checksum for the Nintendo Boot Logo
            reader.BaseStream.Position = 0x015C;
            // If there is, tell the user that is valid
            if (reader.ReadByte() == 0x56 && reader.ReadByte() == 0xCF)
            {
                return true;
            }
            // Otherwise, say nope
            return false;
        }
    }
}
