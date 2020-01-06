using HtmlAgilityPack;
using NUglify;
using SEOExperiment.Analyser.Interface;
using SEOExperiment.Analyser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOExperiment.Analyser
{
    public class URLAnalyser : BaseAnalyser, ISearchAnalyser, IFilterAnalyser
    {
        private readonly HtmlDocument _htmlDocument;
        public URLAnalyser(HtmlDocument htmlDocument, string searchText) : base(searchText)
        {
            _htmlDocument = htmlDocument;
        }

        public URLAnalyser(HtmlDocument htmlDocument, string searchText, bool isRetrieveMetaTags = true, bool isFilterStopWord = true, bool isCountNumberofWords = true, bool isGetExternalLinks = true) : base(searchText, isRetrieveMetaTags, isFilterStopWord, isCountNumberofWords, isGetExternalLinks)
        {
            _htmlDocument = htmlDocument;
        }

        public Task<List<Item>> GetExternalLinksResult()
        {
            var listofURL = new List<String>();
            var nodeSingle = _htmlDocument.DocumentNode.SelectSingleNode("//html");
            listofURL = nodeSingle.OuterHtml.GetExternalURLs();

            return Task.FromResult(listofURL.GroupListOfString());
        }

        public async Task<List<MetaTagInfo>> GetMetaTagsResult()
        {
            var metaTags = _htmlDocument.DocumentNode.SelectNodes("//meta");
            var allMetaTagsInfo = new List<MetaTagInfo>();

            foreach (var tag in metaTags.ToList())
            {
                var metaTagInfo = new MetaTagInfo();

                List<string> listofURL = new List<string>();
                List<string> listofWords = new List<string>();

                string content = tag.Attributes["content"] != null ? tag.Attributes["content"].Value : "";
                string property = tag.Attributes["property"] != null ? tag.Attributes["property"].Value : "";
                string name = tag.Attributes["name"] != null ? tag.Attributes["name"].Value : "";
                string itemProp = tag.Attributes["itemprop"] != null ? tag.Attributes["itemprop"].Value : "";
                string httpEquiv = tag.Attributes["http-equiv"] != null ? tag.Attributes["http-equiv"].Value : "";

                metaTagInfo.Content = content;
                metaTagInfo.Property = property;
                metaTagInfo.Name = name;
                metaTagInfo.ItemProp = itemProp;
                metaTagInfo.HttpEquiv = httpEquiv;

                var hrefList = Regex.Replace(metaTagInfo.Content, Utils.GetURLsRegex, "$1");

                if (hrefList.ToString().ToUpper().Contains("HTTP") || hrefList.ToString().ToUpper().Contains("://"))
                {
                    //check if the text is url
                    listofURL.Add(hrefList);
                }
                else
                {
                    //check if the text is word
                    if (string.IsNullOrEmpty(hrefList) == false)
                    {
                        var words = await Task.Run(() => { return Utils.SplitSentence(hrefList.ToLower()); });
                        listofWords.AddRange(words);
                    }
                }

                if (IsFilterStopWords)
                {
                    listofWords = await Task.Run(()=> { return listofWords.FilterStopWords(); });
                }

                metaTagInfo.TotalWordCount = listofWords != null? listofWords.Count(): 0;
                metaTagInfo.URLInfoList = listofURL?.GroupListOfString();
                metaTagInfo.WordsInfoList = listofWords?.GroupListOfString();

                if (!string.IsNullOrWhiteSpace(metaTagInfo.Content))
                {
                    allMetaTagsInfo.Add(metaTagInfo);
                }
            }

            return allMetaTagsInfo;
        }

        public Task<List<Item>> GetWordsResult()
        {
            var listOfWords = new List<string>();

            var root = _htmlDocument.DocumentNode.SelectSingleNode("//body");
            
            var allText = Uglify.HtmlToText(root.OuterHtml);
            listOfWords = allText.Code.GetAllWords();

            if (IsFilterStopWords)
            {
                listOfWords = listOfWords.FilterStopWords();
            }

            return Task.FromResult(listOfWords.GroupListOfString());
        }
    }
}
