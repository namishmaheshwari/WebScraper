using System.Collections.Generic;

namespace WebScrapper.models
{
    internal class Stream
    {
        public string Name
        {
            get;
            set;
        }
        public List<Major> Majors{
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("Stream Name: {0}\nStreams Majors: \n{1}", this.Name, string.Join(" \n ", this.Majors.ToString()));

        }
    }
}