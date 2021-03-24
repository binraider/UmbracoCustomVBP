using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Hosting;
using System.Collections;
using System.Web.Caching;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Reflection;
using System.Threading.Tasks;

namespace UmbracoCustomVBP {


    // https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio-net-framework


    public class CustomVbpModule : IHttpModule {
        //private WebLogger logger;


        public void Dispose() {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void Init(HttpApplication application) {

            //logger = new WebLogger(AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\Logs", "VPPModule");

            application.PostAuthorizeRequest += Application_PostAuthorizeRequest;


        }

        private void Application_PostAuthorizeRequest(object sender, EventArgs e) {

            HttpContext context = ((HttpApplication)sender).Context;
            string rawurl = context.Request.RawUrl.Split('?')[0].ToLower();
            bool pathfound = false;
            string startpath = "";
            List<string> paths = StaticHelpers.GetStartPaths();

            if (paths.Count > 0) {

                foreach (var x in paths) {
                    if (rawurl.StartsWith("/" + x + "/")) {
                        pathfound = true;
                        startpath = "/" + x + "/";
                        break;
                    }
                }

                if (pathfound) {
                    VbpFileHandler vbphandler = new VbpFileHandler();
                    context.RemapHandler(vbphandler);
                }
            }
        }
    }

}
