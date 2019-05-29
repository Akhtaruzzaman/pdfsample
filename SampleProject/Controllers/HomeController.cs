using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PdfGenerate()
        {
            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            var pdfPrintOptions = new PdfPrintOptions()
            {
                MarginTop = 0,
                MarginBottom = 0,
                MarginLeft = 0,
                MarginRight = 0,
                CssMediaType = PdfPrintOptions.PdfCssMediaType.Print,
                CustomCssUrl = new Uri("https://localhost:44320/css/mystyle.css"),
                Header = new HtmlHeaderFooter()
                {
                    Height = 25,
                    LoadStylesAndCSSFromMainHtmlDocument = true,
                    HtmlFragment = $@"<table style='width:100%'>
                                    <tr>
                                        <td style='width:5%'></td>
                                        <td style='width:90%'>
                                        <table cellspacing='0' rules='all' style='font-size:11px;width:100%; border-collapse: collapse; border:1px solid #000; width: 100%; font-family:Calibri;'>
                                        <tr>
                                           <td style='width:30%'>
                                               Item Code: ########<br />
                                               Item Name: ########<br />
                                               Brand: ########<br />
                                               Update: {@DateTime.Now.ToString("dd-MM-yyyy")}
                                           </td>
                                           <td style='width:25%'>
                                               Size: ########<br />
                                               Color: ########<br />
                                               Project: ########<br />
                                               UPS: ########
                                           </td>
                                           <td style='width:20%'>
                                               Hole: ########<br />
                                               Perf: ########<br />
                                               Fld Line: ########
                                           </td>
                                           <td style='width:25%'>
                                               ########: ########<br />
                                               ########.: <br />
                                               ########: ########<br />
                                               ########: ########
                                           </td>
                                        </tr>
                                        </table>
                                        </td>
                                        <td style='width:5%'></td>
                                    </tr>
                                    </table>",
                    DrawDividerLine = false
                },
                Footer = new HtmlHeaderFooter()
                {
                    Height = 25,
                    HtmlFragment = $@"<table style='width:90%;'>
			                            <tr>
				                            <td style='text-align: right;'><span style='font-size: 10px'>Barcode :</span>
					                            <div style='float: right;'>
					                            <table cellspacing='0' rules='all' style='width:100%; border-collapse: collapse; border:1px solid #000; width: 100%; font-family:Calibri;'>
							                            <tr style='height: 20px'>
								                            <td width='50px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
								                            <td width='20px'></td>
							                            </tr>
						                            </table>
					                            </div>
				                            </td>
			                            </tr>
			                            <tr>
				                            <td style='text-align: right; '><span style='padding-right: 20px;font-size: 8px'>|YOKOHAMA LABELS & PRINTING (BD) CO. LTD. Plot # 105-106, Adamjee EPZ, Narayangonj, Bangladesh.|</span> <span style='font-size: 10px'>Lorem ipsum dolor :</span>
					                            <div style='float: right;'>
					                            <table cellspacing='0' rules='all' style='width:100%; border-collapse: collapse; border:1px solid #000; width: 100%; font-family:Calibri;'>
							                            <tr style='height: 20px'>
								                            <td width='40px'></td>
								                            <td width='40px'></td>
								                            <td width='40px'></td>
							                            </tr>
						                            </table>
					                            </div>
				                            </td>
			                            </tr>
		                            </table>",
                    DrawDividerLine = false
                },
                PaperSize = PdfPrintOptions.PdfPaperSize.A4,
                PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait,
                PrintHtmlBackgrounds = true,
                EnableJavaScript = true,
                RenderDelay = 300,
                Zoom = 100,
                DPI = 1200
            };
            Renderer.PrintOptions = pdfPrintOptions;

            IronPdf.License.LicenseKey = "my license";
            var PDF = HtmlToPdf.StaticRenderUrlAsPdf($"https://localhost:44320/Home/Report", pdfPrintOptions);

            //var pdfpath = Path.Combine(Path.GetTempPath(), $"sample.Pdf");
            //PDF.SaveAs(pdfpath);

            return File(PDF.BinaryData, "application/pdf;");
        }
        public IActionResult Report()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
