using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Undine.Formats
{
    /// <summary>
    /// Interface that defines the base of all rom formats.
    /// </summary>
    public abstract class Format
    {
        /// <summary>
        /// The Title of the game.
        /// </summary>
        public string Title { get; protected set; } = "Unavailable";
        /// <summary>
        /// Unique identifier by the console manufacturer.
        /// </summary>
        public string Identifier { get; protected set; } = "Unavailable";
        /// <summary>
        /// The name of the developer of the game.
        /// </summary>
        public string Developer { get; protected set; } = "Unavailable";
        /// <summary>
        /// Console where this game runs.
        /// </summary>
        public string Console { get; protected set; } = "Unavailable";
        /// <summary>
        /// The readable region where this game belongs to.
        /// </summary>
        public string Region { get; protected set; } = "Unavailable";
        /// <summary>
        /// The Localized Titles that are part of the media format.
        /// </summary>
        public Dictionary<string, string> LocalizedTitles { get; } = new Dictionary<string, string>();

        public Format(BinaryReader reader) { }

        /// <summary>
        /// Detects the type of rom based on the file contents.
        /// </summary>
        /// <param name="reader">The file open as a BinaryReader.</param>
        /// <returns>The correct format for that file, null if the file is invalid or not supported.</returns>
        public static Format Detect(BinaryReader reader)
        {
            // Let's start with Nintendo DS
            if (NintendoDS.IsCompatible(reader))
            {
                return new NintendoDS(reader);
            }
            // And then go to PSP
            else if (PlayStationPortable.IsCompatible(reader))
            {
                return new PlayStationPortable(reader);
            }
            // If we failed, say that the file is invalid
            return null;
        }
    }
}
