using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

using Microsoft.AspNetCore.Mvc;



namespace HRPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadPDFController : ControllerBase
    {
         private readonly ILogger<ReadPDFController> _logger;

        public ReadPDFController(ILogger<ReadPDFController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile pdfFile)
        {
            try
            {
                using (var reader = new PdfReader(pdfFile.OpenReadStream()))
                {
                    var pdfDocument = new PdfDocument(reader);
                    var employees = new List<EmployeeModel>();
                    var storeTotals = new List<StoreTotal>();

                    for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
                    {
                        var pageText = await ExtractPageText(pdfDocument, page);

                        var lines = pageText.Split('\n');
                        var customerAccountLines = lines.Skip(2).Take(lines.Length - 4);

                        foreach (var line in customerAccountLines)
                        {
                            var columns = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            var employee = new EmployeeModel
                            {
                                PAndLDate = columns[0],
                                AccountNo = columns[1],
                                PLBalance = columns[2],
                                Name = columns[3],
                                DueDate = columns[4],
                                Terms = columns[5],
                                PaidIn = columns[6],
                                LastPd = columns[7],
                                PDue = columns[8],
                                Col = columns[9],
                                SeqNo = columns[10],
                                PLCode = columns[11]
                            };
                            employees.Add(employee);
                        }

                        var storeTotalLine = lines.Last();
                        var storeTotalColumns = storeTotalLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        var storeTotal = new StoreTotal
                        {
                            Total = decimal.Parse(storeTotalColumns[1])
                        };
                        storeTotals.Add(storeTotal);
                    }

                    var jsonData = new
                    {
                        EmployeeModel = employees,
                        StoreTotals = storeTotals
                    };

                    return Ok(jsonData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading PDF file.");
                return StatusCode(500, "Error reading PDF file.");
            }
        }

        private Task<string> ExtractPageText(PdfDocument pdfDocument, int page)
        {
            var pdfPage = pdfDocument.GetPage(page);
            string text = PdfTextExtractor.GetTextFromPage(pdfPage);
            return Task.FromResult(text);
        }

    }
}
