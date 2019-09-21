using CommandLine;

namespace Undine.CommandLine
{
    public class Options
    {
        [Option('f', "file", Required = true, HelpText = "The file to get the information from.")]
        public string File { get; set; }
        [Option('c', "complete", Required = false, HelpText = "If the a verbose version of the rom info should be printed instead.")]
        public bool Complete { get; set; }
    }
}
