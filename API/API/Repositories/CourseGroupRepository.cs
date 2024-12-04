using API.Entities;
using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CourseGroupRepository : ICourseGroupService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public CourseGroupRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _courseSystemDB = courseSystemDB;
            _mapper = mapper;
        }
        public async Task<CourseGroupDto> CreateCourseGroup(CourseGroupDto classDTO)
        {
            var CourseGroup = _mapper.Map<CourseGroup>(classDTO);
            
            _courseSystemDB.CourseGroup.Add(CourseGroup);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<CourseGroupDto>(CourseGroup);
        }

        public async Task<ServiceResponse> DeleteCourseGroup(string CourseGroupId)
        {
            var CourseGroup = await _courseSystemDB.CourseGroup.FindAsync(CourseGroupId);
            if(CourseGroup == null)
            {
                return new ServiceResponse(false, "class of is null");
            }
            _courseSystemDB.CourseGroup.Remove(CourseGroup);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<CourseGroupDto>> GetAllCourseGroup(int page, int pagesize)
        {
            var query = _courseSystemDB.CourseGroup.AsQueryable();
            var count = await query.CountAsync();

            var CourseGroup = await query.Skip((page-1)*pagesize)
                                        .Take(pagesize).ToListAsync();
            var CourseGroupDTO = _mapper.Map<List<CourseGroupDto>>(CourseGroup);
            return new PageResult<CourseGroupDto>
            {
                Page = page,
                PageSize = pagesize,
                TotalRecoreds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                Data = CourseGroupDTO
            };
        }

        public async Task<CourseGroupDto> GetCourseGroupById(string ClassId)
        {
           var CourseGroup = await _courseSystemDB.CourseGroup.FindAsync(ClassId);
            if(CourseGroup == null)
            {
                throw new ArgumentException("class of is null");
            }
            return _mapper.Map<CourseGroupDto>(CourseGroup);
        }

        public async Task<ServiceResponse> UpdateCourseGroup(CourseGroupDto CourseGroupDTO)
        {
            var CourseGroup = await _courseSystemDB.CourseGroup.FindAsync(CourseGroupDTO.CourseGroupId);
            if (CourseGroup == null)
            {
                return new ServiceResponse(false, "update don't success");
            }
            //var CourseGroup = _mapper.Map<CourseGroup>(CourseGroupDTO);
            CourseGroup.CourseGroupName = CourseGroup.CourseGroupName;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true,"update success");
        }
    }
}
