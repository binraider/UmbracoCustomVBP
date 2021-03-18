using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoCustomVBP {
    public static class StaticHelpers {

        public static string GetBinaryAppType(string filename) {
            string outy = "";
            string ext = "";
            if (filename.IndexOf('.') > -1) {
                string[] arr = filename.Split('.');
                ext = arr[arr.Length - 1];
            } else {
                ext = filename;
            }

            switch (ext.Trim().ToLower()) {

                case "csv":
                    outy = "text/csv";
                    break;
                case "xls":
                    outy = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    outy = "application/vnd.openxmlformats";
                    break;

                case "xlsm":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "xltx":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "xltm":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "xlsb":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "xlam":
                    outy = "application/vnd.openxmlformats";
                    break;

                case "doc":
                    outy = "application/msword";
                    break;
                case "docx":
                    //outy = "application/vnd.openxmlformats";
                    outy = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case "rtf":
                    outy = "application/rtf";
                    break;

                case "docm":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "dotx":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "dotm":
                    outy = "application/vnd.openxmlformats";
                    break;

                case "ppt":
                    outy = "application/vnd.ms-powerpoint";
                    break;
                case "pptx":
                    outy = "application/vnd.openxmlformats";
                    break;

                case "pptm":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "potx":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "potm":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "ppam":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "ppsx":
                    outy = "application/vnd.openxmlformats";
                    break;
                case "ppsm":
                    outy = "application/vnd.openxmlformats";
                    break;



                case "pdf":
                    outy = "application/pdf";
                    break;
                case "txt":
                    outy = "text/plain";
                    break;
                case "bmp":
                    outy = "image/bmp";
                    break;
                case "eps":
                    outy = "application/postscript";
                    break;
                case "exe":
                    outy = "application/octet-stream";
                    break;
                case "gif":
                    outy = "image/gif";
                    break;
                case "ico":
                    outy = "image/x-icon";
                    break;
                case "jpg":
                    outy = "image/jpeg";
                    break;
                case "jpeg":
                    outy = "image/jpeg";
                    break;
                case "mpeg":
                    outy = "video/mpeg";
                    break;
                case "mp4":
                    outy = "video/mpeg";
                    break;
                case "png":
                    outy = "image/png";
                    break;
                case "mp3":
                    outy = "audio/mpeg";
                    break;

                case "oga":
                    outy = "audio/ogg";
                    break;
                case "ogv":
                    outy = "video/ogg";
                    break;
                case "ogx":
                    outy = "application/ogg";
                    break;

                case "otf":
                    outy = "font/otf";
                    break;
                case "ttf":
                    outy = "font/ttf";
                    break;
                case "woff":
                    outy = "font/woff";
                    break;
                case "woff2":
                    outy = "font/woff2";
                    break;
                case "eot":
                    outy = "application/vnd.ms-fontobject";
                    break;

                case "ps":
                    outy = "application/postscript";
                    break;
                case "swf":
                    outy = "application/x-shockwave-flash";
                    break;
                case "zip":
                    outy = "application/zip";
                    break;

                case "css":
                    outy = "text/css";
                    break;
                case "js":
                    outy = "text/javascript";
                    break;
                case "html":
                    outy = "text/html";
                    break;
                case "htm":
                    outy = "text/html";
                    break;
                case "json":
                    outy = "application/json";
                    break;
                case "xml":
                    outy = "application/xml";
                    break;
                case "svg":
                    outy = "image/svg+xml";
                    break;
                    //default:
                    //    outy = "application/octet-stream";
                    //    break;

            }
            return outy;
        }

    }
}
