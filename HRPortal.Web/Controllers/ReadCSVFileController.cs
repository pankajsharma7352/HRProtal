using CsvHelper;
using HRPortal.Models.ModelsDTO;
using HRPortal.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HRPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadCSVFileController : ControllerBase
    {
   
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ReadCSVFileController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult GetFinancialData(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var financialDataList = new List<EmployeeModel>();
            SummaryModel storeTotalSummary = null;

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
            {
                // Read the first row (header row)
                csv.Read();
                csv.ReadHeader();

                EmployeeModel lastRecord = null;

                while (csv.Read())
                {
                    var pAndLDate = csv.GetField("P&L DATE");
                    var plBalance = csv.GetField("P & L BAL.");

                    if (pAndLDate == "STORE TOTAL:")
                    {
                        // Capture the summary data
                        storeTotalSummary = new SummaryModel
                        {
                            PAndLDate = pAndLDate,
                            PLBalance = plBalance
                        };
                        continue;
                    }

                    var record = new EmployeeModel
                    {
                        PAndLDate = pAndLDate,
                        AccountNo = csv.GetField("ACCOUNT NO."),
                        PLBalance = plBalance,
                        Name = csv.GetField("NAME"),
                        DueDate = csv.GetField("DUE DATE"),
                        Terms = csv.GetField("TERMS"),
                        PaidIn = csv.GetField("PAIDIN"),
                        LastPd = csv.GetField("LAST PD."),
                        PDue = csv.GetField("P DUE"),
                        Col = csv.GetField("COL"),
                        SeqNo = csv.GetField("SEQ.NO"),
                        PLCode = csv.GetField("PLCode")
                    };

                    if (lastRecord != null && string.IsNullOrWhiteSpace(record.AccountNo) && string.IsNullOrWhiteSpace(record.PLBalance))
                    {
                        // Merge with the last record
                        lastRecord.Name += " " + record.Name;
                    }
                    else
                    {
                        // Add the new record
                        financialDataList.Add(record);
                        lastRecord = record;
                    }
                }
            }

            return Ok(new { FinancialData = financialDataList, StoreTotalSummary = storeTotalSummary });
        }



    }
}
