using jsreport.Binary;
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

            Console.WriteLine("Rendering localy stored template jsreport/data/templates/Certificate into certificate.pdf");
            var certificateReport = rs.RenderByNameAsync("Certificate", CertificateData).Result;
            certificateReport.Content.CopyTo(File.OpenWrite("certificate.pdf"));

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

        static object CertificateData = new
        {
            certificate = new
            {
                userID = "kkurbat@cpat.com",
                name = "Katya Kurbat",
                employeeID = "",
                score = 0.0,
                dateUserStarted = 1673287618319,
                course = "0109 storyline test",
                courseCompletion = 1,
                courseCompletionInstant = 1673879840791,
                topic = "Chapter 1",
                topicCompletion = 1,
                topicCompletionInstant = 1673879840791,
                topicTotalTime = 1917259,
                lesson = "0109 storyline test",
                lessonScore = 0.0,
                lessonScoreWeight = 0.0,
                lessonCredit = 0,
                lessonCompletion = 1,
                lessonCompletionInstant = 1673879840791,
                lessonStartInstant = 1673287618319,
                courseTotalTime = 1917259,
                leafTotalTime = 1917259,
                logoURL = "",
                lmsDisplayName = "  CPaT Test LMS QA",
                certificateSignature = "//content.click4cbt.com/themes/client/3/certificate/Assinatura_ART_1648668770330.png",
                certificateSignatureTyped = "James Earl Jones",
                certificateSignatureTitle = "Daria Harris",
                certificateText = "",
                versionNumber = -1.0,
                versionName = "0109 storyline test",
                versionInstant = 1673287322382,
                curriculumStartInstant = 1672876800000,
                curriculumDuration = 2764799999
            },
            certificateJSON = new
            {
                id = 3,
                showLogo = false,
                logoURL = "",
                titleInfoSection = new
                {
                    showLmsName = true,
                    lmsName = "cpat 2 lms",
                    showHeader = false,
                    header = "Informational Header"
                },
                showCertificateTitle = true,
                showAssignmentTitle = true,
                showCompletionStatement = true,
                useAssignmentEndDate = false,
                summaryInfoSection = new
                {
                    showSummaryInfo = true,
                    showEmployeeID = true,
                    showPosition = true,
                    showScore = true,
                    showAssignmentTotalTime = true,
                    showBase = true,
                    showFleet = true,
                    showDateStarted = true
                },
                assignmentListSection = new
                {
                    showAssignmentList = true,
                    showAssignmentListTime = false
                },
                certificateTextSection = new
                {
                    showCertificateText = true,
                    certificateText = "This is the certificate text. A certification is a credential that you earn to show that you have specific skills or knowledge.A certification is a credential that you earn to show that you have specific skills or knowledge.A certification is a credential that you earn to show that you have specific skills or knowledge."
                },
                signatureSection = new
                {
                    signatureKey = 0,
                    showSignature = true,
                    showPrintedName = true,
                    showJobTitle = true,
                    showDate = true,
                    printedName = "James Earl Jones",
                    jobTitle = "Vice President of Training",
                    signatureURL = "//accesslmsqa.blob.core.windows.net/themes/client/3/certificate/3/signature/0/4146565830_37b6c79333_3k_1665781375156.jpeg"
                },
                additionalSignatures = "",
                templateURL = "//accesslmsqa.blob.core.windows.net/themes/certificate/3/certificate.html",
                cssURL = "//accesslmsqa.blob.core.windows.net/themes/certificate/custom_default/certificate.css"
            }
        };
    }
}
