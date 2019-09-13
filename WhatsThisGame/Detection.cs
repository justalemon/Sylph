using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsThisGame.Formats;

namespace WhatsThisGame
{
    /// <summary>
    /// The type of rom that has been detected.
    /// </summary>
    public enum RomType
    {
        NotDetected = -1,
        NintendoDS = 0,
    }

    /// <summary>
    /// Class that handles and detects the type of file being loaded.
    /// </summary>
    public static class Detection
    {
        public static Format Detect(Stream stream)
        {
            // Let's start with Nintendo DS
            if (NintendoDS.IsCompatible(stream))
            {
                return new NintendoDS(stream);
            }
            // If we failed, say that the file is invalid
            return null;
        }
    }
}
