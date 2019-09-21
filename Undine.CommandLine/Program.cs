using CommandLine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            // If the file that got passed does not exists
            if (!File.Exists(Parameters.File))
            {
                return 3;
            }

            // Open the file specified by the user
            using (Stream stream = File.Open(Parameters.File, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // Get the format for this rom
                Format format = Detection.Detect(reader);
                // If format is null, return with a code 4
                if (format == null)
                {
                    return 4;
                }

                // Now, time for printing the rom information
                // If the user doesn't want the complete info
                if (!Parameters.Complete)
                {
                    // Print just some of them
                    Console.WriteLine($"Title: {format.Title}");
                    Console.WriteLine($"Identifier: {format.Identifier}");
                    Console.WriteLine($"Developer: {format.Developer}");
                    Console.WriteLine($"Console: {format.Console}");
                    Console.WriteLine($"Region: {format.Region}");
                }
            }

            // If we got here, everything is ok so return with a code zero
            return 0;
        }
    }
}
