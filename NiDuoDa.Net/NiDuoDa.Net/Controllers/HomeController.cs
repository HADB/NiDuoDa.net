using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.ProjectOxford.Face;

namespace NiDuoDa.Net.Controllers
{
    public class HomeController : Controller
    {
        private static FaceServiceClient _instance;

        public static FaceServiceClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FaceServiceClient(WebConfigurationManager.AppSettings["SubscriptionKey"]);
                }
                return _instance;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Analyze(string faceUrl, string faceName, bool isTest)
        {
            var stream = Request.InputStream;
            if (!string.IsNullOrEmpty(faceUrl))
            {
                var req = (HttpWebRequest)WebRequest.Create(faceUrl);

                req.ServicePoint.Expect100Continue = false;
                req.Method = "GET";
                req.KeepAlive = true;

                req.ContentType = "image/png";
                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

                try
                {
                    stream = rsp.GetResponseStream();
                }
                catch { }
            }

            if (stream != null)
            {
                try
                {
                    var faces = await Instance.DetectAsync(stream, false, true, true, false);
                    var result = new { AnalyticsEvent = "", Faces = faces };
                    return new JsonResult() { Data = result };
                }
                catch { }
            }

            return new JsonResult() { Data = null };
        }
    }
}
