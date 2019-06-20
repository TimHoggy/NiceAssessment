using System.Collections.Generic;
using System.IO;

namespace CodeChallenge.FileProcessor
{
    public class FileProcessor : IFileProcessor
    {
        SdpMessageConverter _converter = null;
        public FileProcessor()
        {
            _converter = new SdpMessageConverter();

        }

        public IEnumerable<SdpMessage> ProcessFile(string path)
        {
            List<SdpMessage> output = new List<SdpMessage>();

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
//                Sre....
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
                        output.Add(_converter.Convert(fileEntry));
                        fileEntry = new List<string>();
                }
                else
                    fileEntry.Add(line);

            }
            if (fileEntry != null)
                output.Add(_converter.Convert(fileEntry));

        }
    }

    //public static class SdpStreamProcessorParallel
    //{
    //    public static IEnumerable<SdpMessage> ProcessStream(StreamReader sr)
    //    {
    //        List<SdpMessage> output = new List<SdpMessage>();

    //        string line;
    //        while ((line = sr.ReadLine()) != null)
    //        {
    //            if (line == "")
    //            {
    //                output.Add(new SdpMessage(fileEntry));
    //                fileEntry = new List<string>();
    //            }
    //            else
    //                fileEntry.Add(line);

    //        }
    //        if (fileEntry != null)
    //            output.Add(new SdpMessage(fileEntry));

    //        return output;
    //    }
    //}

}
