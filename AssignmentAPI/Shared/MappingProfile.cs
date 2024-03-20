using AutoMapper;
using static Mysqlx.Notice.Warning.Types;
using AssignmentAPI.DTO.BuildingDTO;
using AssignmentAPI.DTO.LevelDTO;
using AssignmentAPI.DTO.RoomDTO;
using AssignmentAPI.DTO.VisitorsDTO;
using AssignmentAPI.DTO.UserDTO;
using AssignmentAPI.DTO.GuestAccessDTO;
using AssignmentAPI.DTO.CategoryDTO;
using AssignmentAPI.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBuildingDTO, BuildingModel>().ReverseMap();
        CreateMap<UpdateBuildDTO, BuildingModel>().ReverseMap();
        CreateMap<CreateLevelDTO, LevelModel>().ReverseMap();
        CreateMap<CreateRoomDTO, RoomModel>().ReverseMap();
        CreateMap<CreateVisitorsDTO, VisitorsModel>().ReverseMap();
        CreateMap<UpdateVisitorsDTO, VisitorsModel>().ReverseMap();
        CreateMap<CreateUserDTO, UserModel>().ReverseMap();
        CreateMap<UpdateUserDTO, UserModel>().ReverseMap();
        CreateMap<CreateGuestAccessDTO, GuestAccessModel>().ReverseMap();
        CreateMap<UpdateGuestAccessDTO, GuestAccessModel>().ReverseMap();
        CreateMap<GetVisitorsDTO, VisitorsModel>().ReverseMap();
        CreateMap<UpdateCategoryDTO, CategoryModel>().ReverseMap();
        CreateMap<CreateCategoryDTO, CategoryModel>().ReverseMap();
        CreateMap<CategoryModel, TreeNode>()
           .ForMember(dest => dest.children, opt => opt.MapFrom(src => src.ChildCategories));
    }
}
