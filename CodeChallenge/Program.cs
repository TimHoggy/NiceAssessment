using CodeChallenge.FileProcessor;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputFile("sdp_input huge.txt");
          //  OutputFile("sdp_input.txt");

        }
        

        private static void OutputFile(string file)
        {
            IFileProcessor fp = new FileProcessor.FileProcessor();
            List<SdpMessage> output = fp.ProcessFile(file).ToList();
            output.ForEach(message => Console.WriteLine(message));
            Console.ReadLine();
        }
    }
}
