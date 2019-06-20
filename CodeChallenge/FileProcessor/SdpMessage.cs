using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.FileProcessor
{
    public interface MessageConverter
    {
        IMesssage Convert(List<string> fileEntries);
    }

    public interface IMesssage
    {
        string VEntry { get; set; }
        string IpAddress1 { get; set; }
        string SessionType { get; set; }
        string Port { get; set; }
        string AudioId { get; set; }
        string IpAddress2 { get; set; }
        List<string> Codecs { get; set; }
    }

    public class SdpMessageConverter
    {
        public SdpMessage Convert(List<string> sdpEntries)
        {
            List<string> aEntries = new List<string>();
            SdpMessage message = new SdpMessage();

            //todo(take out into another class)
            foreach (string s in sdpEntries)
            {
                try
                {
                    switch (s.ToLower()[0])
                    {
                        case ('v'):
                            message.VEntry = s.Substring(2);
                            break;
                        case ('o'):
                            message.IpAddress1 = s.Substring(2);
                            break;
                        case ('s'):
                            message.SessionType = s.Substring(2);
                            break;
                        case ('c'):
                            message.IpAddress2 = s.Substring(2);
                            break;
                        case ('b'):
                            message.Port = s.Substring(2);
                            break;
                        case ('m'):
                            message.AudioId = s.Substring(2);
                            break;
                        case ('a'):
                            aEntries.Add(s.Substring(2));
                            break;
                        default:
                            Console.WriteLine("Unnable to process entry: " + s);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to process entry because of exception: " + e);
                }
            }
            message.Codecs = aEntries;
            return message;
        }
    }

    public class SdpMessage : IMesssage
    {
        public string VEntry { get;  set; }
        public string IpAddress1 { get;  set; }
        public string SessionType { get;  set; }
        public string Port { get;  set; }
        public string AudioId { get;  set; }
        public string IpAddress2 { get;  set; }
        public List<string> Codecs { get;  set; }

        public string TrueIpAddress2 {
            get {
                return IpAddress2.Split(new[] { "IP4 " }, StringSplitOptions.None)[1];
            }
        }
        public string TruePort
        {
            get
            {
                return "";
            }
        }


        //public string ShowCodecs
        //{
        //    get
        //    {
        //        return "";
        //    }
        //}

        public SdpMessage()
        { }

        public SdpMessage(List<string> fileInput)
        {
            List<string> aEntries = new List<string>();

            //todo(take out into another class)
            foreach (string s in fileInput)
            {
                try
                {
                    switch (s.ToLower()[0])
                    {
                        case ('v'):
                            VEntry = s.Substring(2);
                            break;
                        case ('o'):
                            IpAddress1 = s.Substring(2);
                            break;
                        case ('s'):
                            SessionType = s.Substring(2);
                            break;
                        case ('c'):
                            IpAddress2 = s.Substring(2);
                            break;
                        case ('b'):
                            Port = s.Substring(2);
                            break;
                        case ('m'):
                            AudioId = s.Substring(2);
                            break;
                        case ('a'):
                            aEntries.Add(s.Substring(2));
                            break;
                        default:
                            Console.WriteLine("Unnable to process entry: " + s);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to process entry because of exception: "+e);
                }
            }
            Codecs = aEntries;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(VEntry + "\r" + IpAddress1 + "\r" + SessionType + "\r" + IpAddress2 + "\r" + Port + "\r" + AudioId);
            foreach (string x in Codecs)
            {
                builder.AppendLine(x);
            }
            return builder.ToString();
        }
    }
}