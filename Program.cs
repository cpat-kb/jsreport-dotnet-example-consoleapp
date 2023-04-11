﻿using jsreport.Binary;
using jsreport.Local;
using jsreport.Types;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing local jsreport.exe utility");
            var rs = new LocalReporting()
                .RunInDirectory(Path.Combine(Directory.GetCurrentDirectory(), "jsreport"))
                .KillRunningJsReportProcesses()
                .UseBinary(JsReportBinary.GetBinary())
                .Configure(cfg => cfg.DoTrustUserCode().FileSystemStore().BaseUrlAsWorkingDirectory())
                .AsUtility()
                .Create();

            Console.WriteLine("Rendering localy stored template jsreport/data/templates/Invoice into invoice.pdf");
            var invoiceReport = rs.RenderByNameAsync("Invoice", InvoiceData).Result;
            invoiceReport.Content.CopyTo(File.OpenWrite("invoice.pdf"));

            Console.WriteLine("Rendering custom report fully described through the request object into customReport.pdf");
            var customReport = rs.RenderAsync(CustomRenderRequest).Result;
            customReport.Content.CopyTo(File.OpenWrite("customReport.pdf"));
            Console.WriteLine("Done, hit any key...");
            Console.ReadKey();
        }

        private static RenderRequest CustomRenderRequest = new RenderRequest()
        {
            Template = new Template()
            {
                Content = "Helo world from {{message}}",
                Engine = Engine.Handlebars,
                Recipe = Recipe.ChromePdf
            },
            Data = new
            {
                message = "jsreport for .NET!!!"
            }
        };

        static object InvoiceData = new
        {
            number = "123",
            seller = new
            {
                name = "Next Step Webs, Inc.",
                road = "12345 Sunny Road",
                country = "Sunnyville, TX 12345"
            },
            buyer = new
            {
                name = "Acme Corp.",
                road = "16 Johnson Road",
                country = "Paris, France 8060"
            },
            items = new[]
            {
                new { name = "Website design", price = 300 }
            }
        };
    }
}
