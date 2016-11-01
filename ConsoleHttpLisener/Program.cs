using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ConsoleHttpLisener
{
    class Program
    {
        static void Main(string[] args)
        {

            //  SimpleHttpServer test = new SimpleHttpServer();
            // test.Httpserver();
            fileHttpserver test = new fileHttpserver();
            test.startServer();
        }
    }
}
