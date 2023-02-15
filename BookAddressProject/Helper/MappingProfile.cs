using AddressBook.ViewModels;
using AddressBookProject.Models;
using AutoMapper;
using BookAddressProject.ViewModels;

namespace BookAddressProject.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Job, JobViewModel>();
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<AddresBook, AddressBookViewModel>()
                .ForMember(d => d.Job, o => o.MapFrom(s => s.job!.JobTitle))
                .ForMember(d => d.Department, o => o.MapFrom(s => s.department!.DepName));

        }
    }
}
