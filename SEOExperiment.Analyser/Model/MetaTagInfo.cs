using System.Collections.Generic;

namespace SEOExperiment.Analyser.Model
{
    public class MetaTagInfo
    {
        public string Name { get; set; }
        public string Property { get; set; }
        public string ItemProp { get; set; }
        public string HttpEquiv { get; set; }
        public string Content { get; set; }
        public List<Item> URLInfoList { get; set; }
        public List<Item> WordsInfoList { get; set; }
        public int TotalWordCount { get; set; }
    }
}
