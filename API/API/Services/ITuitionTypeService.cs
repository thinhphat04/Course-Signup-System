using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface ITuitionTypeService
    {
        Task<TuitionTypeDTO> CreateTuitionType(TuitionTypeDTO TuitionTypeDTO);
        Task<ServiceResponse> UpdateTuitionType(int Id, TuitionTypeDTO TuitionTypeDTO);
        Task<List<TuitionTypeDTO>> GetTuitionType();
        Task<ServiceResponse> DeleteTuitionType(int TuitionTypeId);
        Task<TuitionTypeDTO> GetTuitionTypeById(int TuitionTypeId);
    }
}
