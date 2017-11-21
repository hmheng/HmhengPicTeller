using System;
using System.Collections.Generic;
using System.Text;

namespace HmhengPicTeller.Core.Models
{

    //refer to https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/home for returned JSON structure
    public class Caption
    {
        public string text { get; set; }
        public double confidence { get; set; }
    }

    public class Description
    {
        public List<string> tags { get; set; }
        public List<Caption> captions { get; set; }
    }

    public class Metadata
    {
        public int width { get; set; }
        public int height { get; set; }
        public string format { get; set; }
    }

    public class DescriptionResult
    {
        public Description description { get; set; }
        public string requestId { get; set; }
        public Metadata metadata { get; set; }
    }
}
