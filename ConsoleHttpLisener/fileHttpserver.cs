using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ConsoleHttpLisener
{
    class fileHttpserver
    {
        private HttpListener listener = null;
        public void startServer()   // un a button click , and needs to be in a backgroundworker. or a loop break statment.
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/simpleserver/"); 
            listener.Start();
            try
            {   //  
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();                    
                    WriteFile(context, @"C:\books.json");  // test file location , lanch a task ??   
                }
            }
            catch (WebException x)
            {
                Console.WriteLine(x.Status);
            }
            
        }
        public void stopserver()
        {
            listener.Close();
        }

        // not my code need to learn what it does, limitation is probably 600mb for a file..
        void WriteFile(HttpListenerContext ctx, string path)
        {
            var response = ctx.Response;
            using (FileStream fs = File.OpenRead(path))
            {
                string filename = Path.GetFileName(path);
                //response is HttpListenerContext.Response...
                response.ContentLength64 = fs.Length;
                response.SendChunked = false;
                response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                response.AddHeader("Content-disposition", "attachment; filename=" + filename);

                byte[] buffer = new byte[64 * 1024];
                int read;
                using (BinaryWriter bw = new BinaryWriter(response.OutputStream))
                {
                    while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, read);
                        bw.Flush(); //seems to have no effect
                    }

                    bw.Close();
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.StatusDescription = "OK";
                response.OutputStream.Close();
            }
        }

    }
}
