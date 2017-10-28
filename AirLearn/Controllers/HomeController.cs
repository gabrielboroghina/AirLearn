using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Net;
using System.Collections.Specialized;

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {
        string c = null;
        public ActionResult Index()
        {
            return View();
        }
        public string afis_sursa(string fisier)
        {
            string buffer = "";
            char c;
            string folder = Server.MapPath("/Content/documente/");
            System.IO.StreamReader fin = new System.IO.StreamReader(folder + fisier);
            while (fin.Peek() >= 0)
            {
                c = (char)fin.Read();
                if (c == '<') buffer += "&#60;";
                else if (c == '>') buffer += "&#62;";
                else buffer += c;
            }
            fin.Close();
            return buffer;
        }

        public ActionResult bio()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult grafuri()
        {
            return View();
        }

        public ActionResult grileinfo()
        {
            return View();
        }

        public ActionResult cpp(bool sw, string fisier)
        {
            string folder = Server.MapPath("/Content/documente/");
            string s = null;
            if (sw)
            {
                executare();
                if (c.Length < 3)
                {
                    c = "Successful compiling!!!";
                    System.Threading.Thread.Sleep(2000);
                }
                ViewBag.comp = c;
                System.IO.StreamReader fin = new System.IO.StreamReader(folder + "data.in");
                while (fin.Peek() >= 0) s += fin.ReadLine() + "\n";
                fin.Close();
                ViewBag.i = s;
                s = null;
                fin = new System.IO.StreamReader(folder + "data.out");
                while (fin.Peek() >= 0) s += fin.ReadLine() + "<br/>";
                fin.Close();
                ViewBag.o = s;
            }
            ViewBag.fisier = fisier;
            return View();
        }
        public void executare()
        {
            string folder = Server.MapPath("/Content");
            string compilare, exec;
            compilare = folder + "/MinGW/bin/mingw32-g++.exe -Wall -fexceptions -g -c " + folder + "/documente/main.cpp" + " -o " + folder + "/documente/main.o" + "\n";
            exec = folder + "/MinGW/bin/mingw32-g++.exe -o " + folder + "/documente/main.exe " + folder + "/documente/main.o" + "\n";
            // Create the ProcessInfo object
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardError = true;
            psi.WorkingDirectory = folder + "/documente";

            // Start the process
            Process proc = Process.Start(psi);

            // Attach the in for writing
            System.IO.StreamWriter sIn = proc.StandardInput;
            sIn.WriteLine(@compilare);
            sIn.WriteLine(@exec);
            sIn.WriteLine(@"cd " + folder + "/documente" + "\n");
            sIn.WriteLine(@"main.exe");
            sIn.WriteLine("exit");
            c = proc.StandardError.ReadToEnd();
            proc.Close();
            sIn.Close();
            if (c != "")
            {
                System.IO.StreamWriter fin = new System.IO.StreamWriter(folder + "data.out");
                fin.Write("");
                fin.Close();
            }
        }
    }
}