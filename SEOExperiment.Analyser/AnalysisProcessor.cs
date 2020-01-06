using HtmlAgilityPack;
using SEOExperiment.Analyser.Interface;
using SEOExperiment.Analyser.Model;
using System;
using System.Threading.Tasks;

namespace SEOExperiment.Analyser
{
    public static class AnalysisProcessor
    {
        public static async Task<AnalysisResult> GetResult(string searchText, bool isRetrieveMetaTags, bool isFilterStopWord, bool isCountNumberofWords, bool isGetExternalLinks)
        {
            Uri uriResult;
            bool isURL = Uri.TryCreate(searchText, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (isURL)
            {
                var web = new HtmlWeb();
                var doc = await web.LoadFromWebAsync(searchText).ConfigureAwait(false);
                return await ProcessSearchText(new URLAnalyser(doc, searchText, isRetrieveMetaTags, isFilterStopWord, isCountNumberofWords, isGetExternalLinks)).ConfigureAwait(false);
            }
            else 
            {
                return await ProcessSearchText(new TextAnalyser(searchText, isRetrieveMetaTags, isFilterStopWord, isCountNumberofWords, isGetExternalLinks)).ConfigureAwait(false);
            }
        }

        private static async Task<AnalysisResult> ProcessSearchText(ISearchAnalyser analyser)
        {
            var result = new Result();

            try
            {
                if (analyser.IsCountNumberofWords)
                {
                    result.Words = await analyser.GetWordsResult().ConfigureAwait(false);
                }

                if (analyser.IsRetrieveMetaTagsInfo)
                {
                    result.MetaTags = await analyser.GetMetaTagsResult().ConfigureAwait(false);
                }

                if (analyser.IsGetExternalLinks)
                {
                    result.ExternalLinks = await analyser.GetExternalLinksResult().ConfigureAwait(false);
                }

                return new AnalysisResult()
                {
                    Success = true,
                    Result = result
                };
            }
            catch(Exception ex)
            {
                return new AnalysisResult()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
