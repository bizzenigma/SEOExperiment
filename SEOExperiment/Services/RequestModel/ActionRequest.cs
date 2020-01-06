using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEOExperiment.Services.RequestModel
{
    public class ActionRequest
    {
        public string FindText { get; set; }
        public bool IsFilterStopWords { get; set; }
        public bool IsCountNumberofWords { get; set; }
        public bool IsMetaTagsInfo { get; set; }
        public bool IsGetExternalLink { get; set; }
    }
}