using SEOExperiment.Analyser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEOExperiment.Analyser.Interface
{
    public interface IFilterAnalyser
    {
        bool IsRetrieveMetaTagsInfo { get; }
        bool IsFilterStopWords { get; }
        bool IsCountNumberofWords { get; }
        bool IsGetExternalLinks { get; }
    }

    public interface ISearchAnalyser : IFilterAnalyser
    {
        string SearchText { get; }
        
        Task<List<Item>> GetWordsResult();
        Task<List<MetaTagInfo>> GetMetaTagsResult();
        Task<List<Item>> GetExternalLinksResult();
    }
}
