using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOExperiment.Analyser.Model
{
    public class AnalysisResult
    {
        public bool Success { get; set; }
        public Result Result { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Result
    {
        public List<Item> Words { get; set; }
        public List<MetaTagInfo> MetaTags { get; set; }
        public List<Item> ExternalLinks { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Total { get; set; }
    }
}
