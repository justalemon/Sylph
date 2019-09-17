using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Undine.Formats;

namespace Undine
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
