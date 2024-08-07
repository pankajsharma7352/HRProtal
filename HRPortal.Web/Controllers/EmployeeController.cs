using CsvHelper;
using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using HRPortal.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HRPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices employeeServices;
        private readonly PdfService _pdfService;


        public EmployeeController(IEmployeeServices employeeServices, PdfService pdfService)
        {
            this.employeeServices = employeeServices;
            _pdfService = pdfService;
        }

        [HttpGet("getAllDetails")]
        public List<ShowEmployeeDetails> getAllDetails([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "", [FromQuery] string sortBy = "")
        {

            return employeeServices.GetllAllDetails(pageNumber, pageSize, search, sortBy);
        }

        [HttpPut("EditEmployee")]
        public ShowEmployeeDetails EditEmployee(EditEmployeeDetails employeeDetails)
        {
            return employeeServices.editEmployee(employeeDetails);
        }

        [HttpDelete("{id}")]
        public string DeleteEmpById(int id)
        {
            return employeeServices.DeleteEmployee(id);
        }


        [HttpGet("SearchById{id}")]
        public ShowEmployeeDetails Search(int id)
        {
            return employeeServices.FindById(id);
        }

        [HttpGet("exportIntoCSV")]
        public IActionResult ExportEmployeeDetailsToCsv()
        {
            var employeeDetails = employeeServices.GetllAllDetails();

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(employeeDetails);
                writer.Flush();
                var result = memoryStream.ToArray();
                return File(result, "text/csv", "EmployeeDetails.csv");
            }
        }

        [HttpGet("exportCSVById{id}")]
        public IActionResult ExportEmployeeDetailsToCsv(int id)
        {
            ShowEmployeeDetails employeeDetail = employeeServices.FindById(id);

            if (employeeDetail == null)
            {
                return NotFound();
            }

            using (var mermoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(mermoryStream))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecord(employeeDetail);
                        
                    }
                    var result = mermoryStream.ToArray();
                    return File(result, "text/csv", "EmployeeDetail.csv");
                }
            }
        }




        [HttpGet("exportIntoPDF")]
        public IActionResult ExportEmployeeDetailsToPDF()
        {
            var employeeDetails = employeeServices.GetllAllDetails();

            var pdf = _pdfService.GeneratePdf(employeeDetails);
            return File(pdf, "application/pdf", "EmployeeDetails.pdf");

        }


        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not selected");
            }

            List<EmployeeDetailsDTO> employees = new List<EmployeeDetailsDTO>();

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    employees = csv.GetRecords<EmployeeDetailsDTO>().ToList();

                    foreach( var employee in employees)
                    {
                        employeeServices.RegisterEmployee(employee);
                    }
                }
            }

            return Ok(employees);
        }

    }
}
