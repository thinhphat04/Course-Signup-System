using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SchoolHolidayScheduleRepository : ISchoolHolidayScheduleService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public SchoolHolidayScheduleRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<SchoolHolidayScheduleDTO> CreateSchoolHolidaySchedule(SchoolHolidayScheduleDTO schoolHolidayScheduleDTO)
        {
            var sc  = _mapper.Map<SchoolHolidaySchedule>(schoolHolidayScheduleDTO);
            _courseSystemDB.SchoolHolidaySchedules.Add(sc);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<SchoolHolidayScheduleDTO>(sc);
        }

        public async Task<ServiceResponse> DeleteSchoolHolidaySchedule(int Id)
        {
            var sc = await _courseSystemDB.SchoolHolidaySchedules.FindAsync(Id);
            if(sc == null)
            {
                return new ServiceResponse(false, "SchoolHolidaySchedule is null");
            }
            _courseSystemDB.SchoolHolidaySchedules.Remove(sc);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<List<SchoolHolidayScheduleDTO>> GetAllSchoolHolidaySchedules()
        {
            var sc = await _courseSystemDB.SchoolHolidaySchedules.ToListAsync();
            return _mapper.Map<List<SchoolHolidayScheduleDTO>>(sc);
        }

        public async Task<SchoolHolidayScheduleDTO> GetSchoolHolidaySchedule(int Id)
        {
            var sc = await _courseSystemDB.SchoolHolidaySchedules.FindAsync(Id);
            if (sc == null)
            {
                throw new ArgumentException( "SchoolHolidaySchedule is null");
            }
            return _mapper.Map<SchoolHolidayScheduleDTO>(sc);
        }

        public async Task<ServiceResponse> UpdateSchoolHolidaySchedule(SchoolHolidayScheduleDTO schoolHolidayScheduleDTO)
        {
            var sc = await _courseSystemDB.SchoolHolidaySchedules.FindAsync(schoolHolidayScheduleDTO.Id);
            if (sc == null)
            {
                return new ServiceResponse(false, "SchoolHolidaySchedule is null");
            }
            sc.NameHoliday = schoolHolidayScheduleDTO.NameHoliday;
            sc.Reason = schoolHolidayScheduleDTO.Reason;
            sc.StartDate = schoolHolidayScheduleDTO.StartDate;
            sc.EndDate = schoolHolidayScheduleDTO.EndDate;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }
    }
}
