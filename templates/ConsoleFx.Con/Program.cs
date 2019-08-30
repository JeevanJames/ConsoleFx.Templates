using System;
using System.Collections.Generic;

using ConsoleFx.CmdLine.Parser;
using ConsoleFx.CmdLine.Program;
using ConsoleFx.CmdLine.Program.HelpBuilders;

namespace ConsoleFx.Con
{
    [Program("my-app")]
    public sealed class Program : ConsoleProgram
    {
        private static int Main()
        {
            var program = new Program();

            program.ScanEntryAssemblyForCommands();

            program.HelpBuilder = new DefaultColorHelpBuilder("help", "h");
#if DEBUG
            string promptArgs = Environment.GetEnvironmentVariable("ConsoleFx.PromptArgs");
            if (string.Equals(promptArgs, "true", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter arguments:");
                Console.Write(program.Name + " ");
                string args = Console.ReadLine();
                IEnumerable<string> tokens = Parser.Tokenize(args);
                return program.Run(tokens);
            }
            else
                return program.RunWithCommandLineArgs();
#else
            return program.RunWithCommandLineArgs();
#endif
        }
    }
}
