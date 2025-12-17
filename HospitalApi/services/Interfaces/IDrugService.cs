using HospitalApi.DTOs.Drug;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.services.Interfaces
{
    public interface IDrugService
    {
        Task<ServiceResult<IEnumerable<DrugReadDto>>> GetAllAsync();
        Task<ServiceResult<DrugReadDto>> GetByIdAsync(int id);
        Task<ServiceResult<DrugReadDto>> CreateAsync(DrugCreateDto dto);
        Task<ServiceResult<DrugReadDto>> UpdateAsync(int id, DrugUpdateDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
