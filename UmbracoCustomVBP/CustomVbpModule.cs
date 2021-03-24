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

        public void Dispose() {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void Init(HttpApplication application) {
            application.PostAuthorizeRequest += Application_PostAuthorizeRequest;
        }

        private void Application_PostAuthorizeRequest(object sender, EventArgs e) {

            HttpContext context = ((HttpApplication)sender).Context;
            string rawurl = context.Request.RawUrl.Split('?')[0].ToLower();
            bool pathfound = false;
            string barepath = "";

            bool IsNormalFile = StaticHelpers.IsNormalFileType(rawurl, true);
            List<string> paths = StaticHelpers.GetStartPaths();

            if (paths.Count > 0) {
                if (IsNormalFile) { // I.e. it is a js, css, whatever
                    foreach (var x in paths) {
                        if (rawurl.StartsWith("/" + x + "/")) {
                            pathfound = true;
                            break;
                        }
                    }
                } else {
                    foreach (var x in paths) {
                        if (rawurl.StartsWith("/" + x)) {
                            pathfound = true;
                            if (rawurl.Equals("/" + x, StringComparison.InvariantCultureIgnoreCase)) {
                                barepath = "/" + x;
                            }
                            break;
                        }
                    }
                }

                if (pathfound) {
                    if (barepath.Length > 0) {
                        if (!barepath.EndsWith("/")) {
                            string redirecturl = context.Request.RawUrl.Split('?')[0].ToLower() + "/";
                            context.Response.StatusCode = 302;
                            context.Response.Redirect(redirecturl);
                        }
                    }
                    VbpFileHandler vbphandler = new VbpFileHandler();
                    context.RemapHandler(vbphandler);
                }
            }
        }
    }

}
