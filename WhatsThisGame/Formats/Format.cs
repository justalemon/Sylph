using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsThisGame.Formats
{
    /// <summary>
    /// Interface that defines the base of all rom formats.
    /// </summary>
    public abstract class Format
    {
        /// <summary>
        /// The Title of the game.
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// Unique identifier by the console manufacturer.
        /// </summary>
        public string Identifier { get; protected set; }
        /// <summary>
        /// The name of the developer of the game.
        /// </summary>
        public string Developer { get; protected set; }
        /// <summary>
        /// Console where this game runs.
        /// </summary>
        public string Console { get; protected set; }
        /// <summary>
        /// The readable region where this game belongs to.
        /// </summary>
        public string Region { get; protected set; }

        public Format(BinaryReader reader) { }

        /// <summary>
        /// Checks if the stream is compatible with the current format.
        /// </summary>
        /// <param name="stream">The open file to read.</param>
        /// <returns>true if this format can handle the file, false otherwise.</returns>
        public static bool IsCompatible(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
