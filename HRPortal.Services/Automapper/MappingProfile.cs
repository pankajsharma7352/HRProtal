using AutoMapper;
using HRPortal.Models.Models;
using HRPortal.Models.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Services.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDetailsDTO, Employee>();
            CreateMap<EmployeeDetailsDTO, Address>();
        }
    }
}
