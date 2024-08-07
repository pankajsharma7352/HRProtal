using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using HRPortal.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Services.Services
{
    public interface IEmployeeServices
    {
        public void RegisterEmployee(EmployeeDetailsDTO employeeDetails);

        public Employee EmployeeLogin(EmployeeLogin login);

        public List<ShowEmployeeDetails> GetllAllDetails(int pageNumber, int pageSize,string filter,string sortBy);

        public ShowEmployeeDetails editEmployee(EditEmployeeDetails editEmployeeDetials);

        public string DeleteEmployee(int id);

        public ShowEmployeeDetails FindById(int id);

        public List<ShowEmployeeDetails> GetllAllDetails();

    }
}
