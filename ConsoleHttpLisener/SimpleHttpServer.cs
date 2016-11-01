using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ConsoleHttpLisener
{
    class SimpleHttpServer
    {
        public void Httpserver()
        {
            System.Net.HttpListener lisener = null;
            try
            {
                lisener = new HttpListener();
                lisener.Prefixes.Add("http://localhost:8080/simpleserver/");
                lisener.Start();
                while (true)
                {
                    Console.WriteLine("Waiting..");
                    HttpListenerContext context = lisener.GetContext();
                    string msg = "hello world";
                    context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    using (Stream stream = context.Response.OutputStream)
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(msg);
                        }
                    }
                    Console.WriteLine("Msg sent");

                }
            }
            catch (WebException e)
            {

                Console.WriteLine(e.Status);

            }
        }
    }
}
