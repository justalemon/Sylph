using CommandLine;
using System;
using System.Collections;
using System.Collections.Generic;
using Undine.Formats;

namespace Undine.CommandLine
{
    public class Program
    {
        /// <summary>
        /// The launch parameters sent via the command line.
        /// </summary>
        private static Options Parameters { get; set; }

        public static int Main(string[] args)
        {
            // Try to parse the launch parameters
            Parser.Default.ParseArguments<Options>(args).WithParsed(opts => Parameters = opts);

            // If there is no parameters to use, exit with a code 2
            if (Parameters == null)
            {
                return 2;
            }

            // If we got here, everything is ok so return with a code zero
            return 0;
        }
    }
}
