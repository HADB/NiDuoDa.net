using System.Web.Optimization;

namespace NiDuoDa.Net
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/NiDuoDa")
                .Include("~/Resources/ni-duo-da.js"));

            bundles.Add(new StyleBundle("~/Styles/NiDuoDa")
                .Include("~/Resources/ni-duo-da.css"));
        }
    }
}
