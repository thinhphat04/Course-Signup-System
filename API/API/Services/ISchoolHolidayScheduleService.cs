using API.DTO;
using API.DTO.Reponse;

namespace API.Services
{
    public interface ISchoolHolidayScheduleService
    {
        Task<SchoolHolidayScheduleDTO> CreateSchoolHolidaySchedule(SchoolHolidayScheduleDTO schoolHolidayScheduleDTO);
        Task<ServiceResponse> UpdateSchoolHolidaySchedule(SchoolHolidayScheduleDTO schoolHolidayScheduleDTO);
        Task<ServiceResponse> DeleteSchoolHolidaySchedule(int Id);
        Task<SchoolHolidayScheduleDTO> GetSchoolHolidaySchedule(int Id);
        Task<List<SchoolHolidayScheduleDTO>> GetAllSchoolHolidaySchedules();
    }
}
