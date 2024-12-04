using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface ICourseGroupService
    {
        Task<CourseGroupDto> CreateCourseGroup(CourseGroupDto classDTO);
        Task<PageResult<CourseGroupDto>> GetAllCourseGroup(int page, int pagesize);
        Task<ServiceResponse> DeleteCourseGroup(string CourseGroupId);
        Task<CourseGroupDto> GetCourseGroupById(string ClassId);
        Task<ServiceResponse> UpdateCourseGroup(CourseGroupDto CourseGroupDTO);
    }
}
