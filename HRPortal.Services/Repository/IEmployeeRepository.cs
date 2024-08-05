using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Services.Repository
{
    public interface IEmployeeRepository
    {
        public void RegisterEmployee(EmployeeDetailsDTO employeeDetails);
        public Employee EmployeeLogin(EmployeeLogin login);
        public List<ShowEmployeeDetails> GetllAllDetails(int pageNumber, int pageSize, string filter, string sortBy);

        public ShowEmployeeDetails EditEmployee(EditEmployeeDetails employeeDetails);

        public string DeleteEmployee(int id);

        public ShowEmployeeDetails FindById(int id);
      


    }
}
