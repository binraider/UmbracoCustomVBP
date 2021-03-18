using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoCustomVBP {
    internal class WebLogger {

        private string loggingfilepath = "";
        private string fileroot = "";
        private bool dolog = true;
        public WebLogger(string logfolder, string fileroot, bool dolog) {
            this.loggingfilepath = logfolder;
            this.fileroot = fileroot;
            this.dolog = dolog;
        }

        public void Log(string message) {
            if (dolog) {
                try {
                    string filename = loggingfilepath + @"\" + DateTime.Now.ToString("yyyy-MM-dd-HH") + "-" + fileroot + ".txt";

                    using (Stream s = new FileStream(filename, FileMode.Append)) {
                        using (TextWriter tw = new StreamWriter(s)) {
                            tw.WriteLine(message);
                            tw.Flush();
                        }
                        s.Close();
                    }
                } catch (Exception ex) {

                }
            }
        }
    }
}
