using CommandLine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Undine.Formats;

namespace Undine.CommandLine
{
    public class Program
    {
        /// <summary>
        /// The launch parameters sent via the command line.
        /// </summary>
        private static Options Parameters { get; set; }
        /// <summary>
        /// The basic properties of a rom.
        /// </summary>
        private static readonly List<string> BasicProperties = new List<string>
        {
            "Title",
            "Identifier",
            "Developer",
            "Console",
            "Region",
            "LocalizedTitles",
        };

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
                Format format = Format.Detect(reader);
                // If we were unable to detect the format, return with a code 4
                if (format == null)
                {
                    return 4;
                }

                // Print the basic information
                Console.WriteLine($"Title: {format.Title}");
                Console.WriteLine($"Identifier: {format.Identifier}");
                Console.WriteLine($"Developer: {format.Developer}");
                Console.WriteLine($"Console: {format.Console}");
                Console.WriteLine($"Region: {format.Region}");
                Console.WriteLine($"Localized Titles: {format.LocalizedTitles.Count}");
                foreach (KeyValuePair<string, string> keyPair in format.LocalizedTitles)
                {
                    Console.WriteLine($"    {keyPair.Key}: {keyPair.Value}");
                }

                // And if the user wants the complete info
                if (Parameters.Complete)
                {
                    // Add an empty line as a separator
                    Console.WriteLine();

                    // Get all of the properties
                    foreach (PropertyInfo prop in format.GetType().GetProperties())
                    {
                        // If the property has not been already printed
                        if (!BasicProperties.Contains(prop.Name))
                        {
                            // Do it
                            Console.WriteLine($"{prop.Name}: {prop.GetValue(format, null)}");
                        }
                    }
                }
            }

            // If we got here, everything is ok so return with a code zero
            return 0;
        }
    }
}
