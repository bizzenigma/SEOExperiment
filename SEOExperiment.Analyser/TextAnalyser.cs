using SEOExperiment.Analyser.Interface;
using SEOExperiment.Analyser.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEOExperiment.Analyser
{
    public class TextAnalyser : BaseAnalyser, ISearchAnalyser, IFilterAnalyser
    {
        public TextAnalyser(string searchText) : base(searchText)
        {
        }

        public TextAnalyser(string searchText, bool isRetrieveMetaTags = true, bool isFilterStopWord = true, bool isCountNumberofWords = true, bool isGetExternalLinks = true) : base(searchText, isRetrieveMetaTags, isFilterStopWord, isCountNumberofWords, isGetExternalLinks)
        {
        }

        public Task<List<Item>> GetExternalLinksResult()
        {
            var listofURL = SearchText.GetExternalURLs();
            return Task.FromResult(listofURL.GroupListOfString());
        }

        public Task<List<MetaTagInfo>> GetMetaTagsResult()
        {
            return Task.FromResult(new List<MetaTagInfo>());
        }

        public Task<List<Item>> GetWordsResult()
        {
            var listOfWords = new List<string>();

            listOfWords = SearchText.GetAllWords();

            if (IsFilterStopWords)
            {
                listOfWords = listOfWords.FilterStopWords();
            }
            return Task.FromResult(listOfWords.GroupListOfString());
        }
    }
}
