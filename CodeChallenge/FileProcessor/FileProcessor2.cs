using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.FileProcessor
{
    /// <summary>
    /// Just read the file into an List of strings
    /// </summary>
    class FileProcessor2 : IFileProcessor
    {

        public IEnumerable<SdpMessage> ProcessFile(string path)
        {
            List<SdpMessage> output = new List<SdpMessage>();

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                //todo: add parallel tasks here
                ProcessStream(output, sr);
            }
            return output;
        }

        public void ProcessStream(List<SdpMessage> output, StreamReader sr)
        {
            List<string> fileEntry = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    output.Add(new SdpMessage(fileEntry));
                    fileEntry = new List<string>();
                }
                else
                    fileEntry.Add(line);

            }
            if (fileEntry != null)
                output.Add(new SdpMessage(fileEntry));

        }
    }
}
