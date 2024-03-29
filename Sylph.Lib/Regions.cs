﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylph
{
    /// <summary>
    /// A class for saving the information of a Region.
    /// </summary>
    public class RegionSet
    {
        /// <summary>
        /// The readable name of the Region.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The identifier for the region shown at the end of the Cartridge ID.
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Sylph.RegionSet class.
        /// </summary>
        public RegionSet(string name, string identifier)
        {
            Name = name;
            Identifier = identifier;
        }
    }
}
