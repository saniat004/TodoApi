using AutoMapper;

namespace TodoApiPractise.Profiles
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile()
        {
            CreateMap<Entities.ToDoList, DTOs.ToDoListDto>();
            CreateMap<DTOs.ToListForCreationDto, Entities.ToDoList>();
            CreateMap<DTOs.ToDoListForUpdateDto, Entities.ToDoList>();
            CreateMap<Entities.ToDoList, DTOs.ToDoListForUpdateDto>();

        }
    }
}
