using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TeacherScheduleRepository : ITeacherScheduleService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public TeacherScheduleRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<TeacherScheduleDTO> CreateTeacherSchedule(TeacherScheduleDTO TeacherScheduleDTO)
        {
           var teachSchedule = _mapper.Map<TeachSchedule>(TeacherScheduleDTO);
           _courseSystemDB.TeachSchedules.Add(teachSchedule);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<TeacherScheduleDTO>(teachSchedule);
        }

        public async Task<ServiceResponse> DeleteTeacherSchedule(int TeacherScheduleId)
        {
            var teachSchedule = await _courseSystemDB.TeachSchedules.FindAsync(TeacherScheduleId);
            if (teachSchedule == null)
            {
                return new ServiceResponse(false, "delete don't success");
            }
            _courseSystemDB.TeachSchedules.Remove(teachSchedule);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<TeacherScheduleDTO>> GetTeacherSchedule(int page, int pagesize)
        {
            var query = _courseSystemDB.TeachSchedules.AsQueryable();
            var count = await query.CountAsync();
            var teachSchedule = await query.Skip((page-1)*pagesize)
                                            .Take(pagesize).ToListAsync();
            var teachScheduleDTO = _mapper.Map<List<TeacherScheduleDTO>>(teachSchedule);
            return new PageResult<TeacherScheduleDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalRecoreds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                Data = teachScheduleDTO
            };
        }

        public async Task<TeacherScheduleDTO> GetTeacherScheduleById(int TeacherScheduleId)
        {
           var teachSchedule = await _courseSystemDB.TeachSchedules.FindAsync(TeacherScheduleId);
            if(teachSchedule == null)
            {
                throw new ArgumentException("teach schedule is null");
            }
            return _mapper.Map<TeacherScheduleDTO>(teachSchedule);
        }

        public async Task<ServiceResponse> UpdateTeacherSchedule(int Id, TeacherScheduleDTO TeacherScheduleDTO)
        {
            var teachSchedule = await _courseSystemDB.TeachSchedules.FindAsync(Id);
            if (teachSchedule == null)
            {
                return new ServiceResponse(false, "teach schedule is null");
            }
            var shedule = _mapper.Map<TeachSchedule>(TeacherScheduleDTO);
            teachSchedule.StudyDay = shedule.StudyDay;
            teachSchedule.StudyTime = shedule.StudyTime;
            teachSchedule. StudyTimeEnd = shedule.StudyTimeEnd;
            teachSchedule.ClassRoom = shedule.ClassRoom;
            teachSchedule.StartTime = shedule.StartTime;
            teachSchedule.EndTime = shedule.EndTime;
            teachSchedule.ClassId = teachSchedule.ClassId;
            teachSchedule.UserId = teachSchedule.UserId;
            teachSchedule.SubjectId = teachSchedule.SubjectId;  
            return new ServiceResponse(true, "update success");
        }
    }
}
