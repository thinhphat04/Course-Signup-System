using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TuitionTypeRepository : ITuitionTypeService
    {
        private readonly CourseSystemDB _db;
        private readonly IMapper _mapper;
        public TuitionTypeRepository(CourseSystemDB db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TuitionTypeDTO> CreateTuitionType(TuitionTypeDTO TuitionTypeDTO)
        {
           var tuitiontype = _mapper.Map<TuitionType>(TuitionTypeDTO);
            _db.TuitionTypes.Add(tuitiontype);
            await _db.SaveChangesAsync();
            return _mapper.Map<TuitionTypeDTO>(tuitiontype);
        }

        public async Task<ServiceResponse> DeleteTuitionType(int TuitionTypeId)
        {
            var tuitiontype = await _db.TuitionTypes.FindAsync(TuitionTypeId);
            if (tuitiontype == null)
            {
                return new ServiceResponse(false, "tuitionType is null");
            }
            _db.TuitionTypes.Remove(tuitiontype);
            await _db.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<List<TuitionTypeDTO>> GetTuitionType()
        {
            var tuitionType = await _db.TuitionTypes.ToListAsync();
            return _mapper.Map<List<TuitionTypeDTO>>(tuitionType);
        }

        public async Task<TuitionTypeDTO> GetTuitionTypeById(int TuitionTypeId)
        {
            var tuitionType = await _db.TuitionTypes.FindAsync(TuitionTypeId);
            if (tuitionType == null)
            {
                throw new ArgumentNullException("tuitionType is null");
            }
            return _mapper.Map<TuitionTypeDTO>(tuitionType);
        }

        public async Task<ServiceResponse> UpdateTuitionType(int Id, TuitionTypeDTO TuitionTypeDTO)
        {
            var tuitionType = await _db.TuitionTypes.FindAsync(Id);
            if (tuitionType == null)
            {
                return new ServiceResponse(false,"tuitionType is null");
            }
            tuitionType.TuitionName = TuitionTypeDTO.TuitionName;
            await _db.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }
    }
}
