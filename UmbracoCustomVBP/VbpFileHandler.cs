using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UmbracoCustomVBP {
    public class VbpFileHandler : HttpTaskAsyncHandler {
        //public bool IsReusable {
        //    get { return false; }
        //}

        public override async Task ProcessRequestAsync(HttpContext context) {

            //string rawurl = context.Request.RawUrl.Split('?')[0].ToLower();
            string rawurl = context.Request.RawUrl.Split('?')[0];
            string blobcontainerpath = "";
            string finalpath = "";
            string finalurl = "";
            string defaultdocument = "index.html";
            string default404document = "404.html";
            bool DoLogging = false;


            #region AppSettings
            if (ConfigurationManager.AppSettings["vbp:defaultdocument"] != null) {
                defaultdocument = ConfigurationManager.AppSettings["vbp:defaultdocument"].Trim();
            }
            if (ConfigurationManager.AppSettings["vbp:404document"] != null) {
                default404document = ConfigurationManager.AppSettings["vbp:404document"].Trim();
            }
            if (ConfigurationManager.AppSettings["vbp:blobcontainerpath"] != null) {
                blobcontainerpath = ConfigurationManager.AppSettings["vbp:blobcontainerpath"].Trim();
                if (blobcontainerpath.Length > 0) {
                    if (blobcontainerpath.EndsWith("/")) {
                        blobcontainerpath = blobcontainerpath.Substring(blobcontainerpath.Length - 1);
                    }
                }
            }
            if (ConfigurationManager.AppSettings["vbp:debuglogging"] != null) {
                DoLogging = ConfigurationManager.AppSettings["vbp:debuglogging"].Trim().ToLower().Equals("true");
            }
            #endregion



            if (blobcontainerpath.Length > 0) {

                WebLogger logger = new WebLogger(AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\Logs", "UmbracoCustomVBP.VbpFileHandler", DoLogging);

                if (rawurl.EndsWith("/")) {
                    finalpath = rawurl + defaultdocument;
                } else {
                    finalpath = rawurl;
                }

                finalurl = blobcontainerpath + finalpath;

                logger.Log("finalurl[" + finalurl + "]");

                var wc = new System.Net.WebClient();

                byte[] filebytes = null;

                try {
                    filebytes = await wc.DownloadDataTaskAsync(finalurl);
                } catch (Exception ex) {
                    logger.Log("Error getting filebytes for [" + finalurl + "]:" + ex.ToString());
                }

                if (filebytes != null) {
                    context.Response.ContentType = StaticHelpers.GetBinaryAppType(Path.GetExtension(finalpath));
                    context.Response.BinaryWrite(filebytes);
                    context.Response.End();
                } else {
                    bool IsJsCssWhatever = StaticHelpers.IsNormalFileType(finalurl, false);

                    if (IsJsCssWhatever) {

                    } else {
                        string default404documentLow = default404document.ToLower();
                        bool containsstartpath = false;

                        List<string> startpaths = StaticHelpers.GetStartPaths();
                        for (int i = 0; i < startpaths.Count; i++) {
                            if (default404documentLow.Contains(startpaths[i].ToLower())) {
                                containsstartpath = true;
                                break;
                            }
                        }

                        if (containsstartpath) {
                            // Oh no no no
                        } else {
                            if (default404documentLow.StartsWith("http")) {
                                context.Response.StatusCode = 404;
                                context.Response.Redirect(default404documentLow);
                            } else {
                                finalurl = blobcontainerpath + "/" + default404document;
                                try {
                                    filebytes = await wc.DownloadDataTaskAsync(finalurl);
                                } catch (Exception ex) {
                                    logger.Log("Error getting 404 page for [" + finalurl + "]:" + ex.ToString());
                                }
                                if (filebytes != null) {
                                    context.Response.ContentType = "text/html";
                                    context.Response.StatusCode = 404;
                                    context.Response.BinaryWrite(filebytes);
                                    context.Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
