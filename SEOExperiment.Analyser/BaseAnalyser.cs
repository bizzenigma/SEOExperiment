using SEOExperiment.Analyser.Interface;
using SEOExperiment.Analyser.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEOExperiment.Analyser
{
    public abstract class BaseAnalyser : IFilterAnalyser
    {
        public string SearchText { get; }
        public bool IsRetrieveMetaTagsInfo { get; }
        public bool IsFilterStopWords { get; }
        public bool IsCountNumberofWords { get; }
        public bool IsGetExternalLinks { get; }

        public BaseAnalyser(string searchText)
        {
            SearchText = searchText;
            IsRetrieveMetaTagsInfo = true;
            IsFilterStopWords = true;
            IsCountNumberofWords = true;
            IsGetExternalLinks = true;
        }

        public BaseAnalyser(string searchText, bool isRetrieveMetaTags = true, bool isFilterStopWord = true, bool isCountNumberofWords = true, bool isGetExternalLinks = true)
        {
            SearchText = searchText;
            IsRetrieveMetaTagsInfo = isRetrieveMetaTags;
            IsFilterStopWords = isFilterStopWord;
            IsCountNumberofWords = isCountNumberofWords;
            IsGetExternalLinks = isGetExternalLinks;
        }
    }
}
