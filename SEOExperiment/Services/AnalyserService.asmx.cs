using Newtonsoft.Json;
using SEOExperiment.Analyser;
using SEOExperiment.Analyser.Model;
using SEOExperiment.Services.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace SEOExperiment.Services
{
    /// <summary>
    /// Summary description for AnalyserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AnalyserService : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string SearchAnalyser(ActionRequest request)
        {
            if (request == null) return null;
            if (string.IsNullOrEmpty(request.FindText)) return null;

            var result = AnalysisProcessor.GetResult(request.FindText, request.IsMetaTagsInfo, request.IsFilterStopWords, request.IsCountNumberofWords, request.IsGetExternalLink).Result;
            return JsonConvert.SerializeObject(result);
        }
    }
}
