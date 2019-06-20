using System;
using System.Collections.Generic;
using System.IO;

namespace CodeChallenge.FileProcessor
{
    internal interface IFileProcessor
    {
        IEnumerable<SdpMessage> ProcessFile(string path);
        void ProcessStream(List<SdpMessage> output, StreamReader sr);

    }
         

}
