using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GradeTypeRepository : IGradeTypeService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public GradeTypeRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<GradeTypeDTO> CreateGradeType(GradeTypeDTO GradeTypeDTO)
        {
           var gradeType = _mapper.Map<GradeType>(GradeTypeDTO);
            _courseSystemDB.GradeTypes.Add(gradeType);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<GradeTypeDTO>(gradeType);
        }

        public async Task<ServiceResponse> DeleteGradeType(int GradeTypeId)
        {
            var gradeType =  await _courseSystemDB.GradeTypes.FindAsync(GradeTypeId);
            if (gradeType == null)
            {
                return new ServiceResponse(false, "gradetype is null");
            }
            _courseSystemDB.GradeTypes.Remove(gradeType);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<GradeTypeDTO>> GetGradeType(int page, int pagesize)
        {
            var query = _courseSystemDB.GradeTypes.AsQueryable();
            var count = await query.CountAsync();
            var gradeType = await query.Skip((page-1)*pagesize).Take(pagesize).ToListAsync();
            var gradeTypeDTO = _mapper.Map<List<GradeTypeDTO>>(gradeType);
            return new PageResult<GradeTypeDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                TotalRecoreds = count,
                Data = gradeTypeDTO
            };
        }

        public async Task<GradeTypeDTO> GetGradeTypeById(int GradeTypeId)
        {
            var gradeType = await _courseSystemDB.GradeTypes.FindAsync(GradeTypeId);
            if (gradeType == null)
            {
               throw new Exception("gradetype is null");
            };
            return _mapper.Map<GradeTypeDTO>(gradeType);
        }

        public async Task<ServiceResponse> UpdateGradeType(int Id, GradeTypeDTO GradeTypeDTO)
        {
            var gradeType = await _courseSystemDB.GradeTypes.FindAsync(Id);
            if (gradeType == null)
            {
                return new ServiceResponse(false, "gradetype is null");
            }
            var gt = _mapper.Map<GradeType>(GradeTypeDTO);
            gradeType.GradeTypeName = gt.GradeTypeName;
            gradeType.Coefficient = gt.Coefficient;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }
    }
}
