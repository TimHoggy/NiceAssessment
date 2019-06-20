using CodeChallenge.FileProcessor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeChallenge.Test
{
    [TestFixture]
    public class FileProcessorTests
    {
        string testLine1 = @"v=0
o=- 0 0 IN IP4 43.173.154.12
s=session
c=IN IP4 3.211.46.198
b=CT:2210
m=audio 61376 RTP/AVP 9 0 8
a=crypto:2 AES_CM_128_HMAC_SHA1_80 inline:sgM6l8+kAoc35BShuTl1+TWYybx22LjLdh9x5/SG|2^31|1:1
a=rtpmap:9 G722/8000
a=rtpmap:0 PCMU/8000
a=rtpmap:8 PCMA/8000
a=encryption:required

v=0
o=- 0 0 IN IP4 43.173.154.12
s=session
b=CT:7147
m=audio 32466 RTP/AVP 9
c=IN IP4 179.245.9.79
a=rtpmap:9 G722/8000
a=encryption:optional

v=0
o=- 0 0 IN IP4 43.173.154.12
s=session
b=CT:671
m=audio 18977 RTP/AVP 9 0 8
c=IN IP4 143.141.57.175
a=rtpmap:9 G722/8000
a=rtpmap:0 PCMU/8000
a=rtpmap:8 PCMA/8000
a=encryption:optional

v=0
o=- 0 0 IN IP4 43.173.154.12
s=session
b=CT:9687
m=audio 1791 RTP/AVP 9
c=IN IP4 238.12.45.50
a=rtpmap:9 G722/8000
a=encryption:optional";


        string testLine2 = @"v=0
o=- 0 0 IN IP4 43.173.154.12
s=session
c = IN IP4 3.211.46.198
b=CT:2210
m=audio 61376 RTP/AVP 9 0 8
a=crypto:2 AES_CM_128_HMAC_SHA1_80 inline:sgM6l8+kAoc35BShuTl1+TWYybx22LjLdh9x5/SG|2^31|1:1
a=rtpmap:9 G722/8000
a=rtpmap:0 PCMU/8000
a=rtpmap:8 PCMA/8000
a=encryption:required";

        [SetUp]
        public void SetUp()
        { }


        [TearDown]
        public void Cleanup()
        { }

        [Test]
        public void SdpMessageConstrutorTest()
        {
            List<string> singleMessageEntries = new List<string>();
            singleMessageEntries = testLine2.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();

            SdpMessage message = new SdpMessage(singleMessageEntries);

            Assert.AreEqual(message.VEntry, singleMessageEntries.First(x => x[0] == 'v').Substring(2));

            Assert.AreEqual(message.IpAddress1, singleMessageEntries.First(x => x[0] == 'o').Substring(2));
            Assert.AreEqual(message.SessionType, singleMessageEntries.First(x => x[0] == 's').Substring(2));
            Assert.AreEqual(message.IpAddress2, singleMessageEntries.First(x => x[0] == 'c').Substring(2));
            Assert.AreEqual(message.Port, singleMessageEntries.First(x => x[0] == 'b').Substring(2));
            Assert.AreEqual(message.AudioId, singleMessageEntries.First(x => x[0] == 'm').Substring(2));
            Assert.AreEqual(message.Codecs.Count, singleMessageEntries.Where(x => x[0] == 'a').Count());
        }

        [Test]
        public void FileProcessorTest()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(testLine1);
            MemoryStream stream = new MemoryStream(byteArray);

            IFileProcessor fp = new FileProcessor.FileProcessor();
            List<SdpMessage> output = new List<SdpMessage>();

            using (StreamReader sr = new StreamReader(stream))
            {
                fp.ProcessStream(output, sr);
            }

            Assert.AreEqual(output.Count, 4);

        }
    } 
}
