using HospitalApi.Data;
using HospitalApi.DTOs.Doctor;
using HospitalApi.DTOs.Drug;
using HospitalApi.Models;
using HospitalApi.services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.services.Implementations
{
    public class DrugService : IDrugService
    {
        private readonly AppDbContext _context;
        public DrugService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult<DrugReadDto>> CreateAsync(DrugCreateDto dto)
        {
            var company = await _context.Pharmaceutical_Companies.FindAsync(dto.Company_Id);
            if (company == null)
            {
                return ServiceResult<DrugReadDto>.Fail("Company not found");
            }
            var drug = new Drug
            {
                Trade_Name = dto.Trade_Name,
                Strength = dto.Strength,
                Company_Id = dto.Company_Id
            };

            _context.Drugs.Add(drug);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(drug.Id);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null)
            {
                return ServiceResult<bool>.Fail("Drug not found");
            }

            _context.Drugs.Remove(drug);
            await _context.SaveChangesAsync();
            return ServiceResult<bool>.Ok(true);
        }

        public async Task<ServiceResult<IEnumerable<DrugReadDto>>> GetAllAsync()
        {
            var drugs = await _context.Drugs.Include(d => d.PharmaceuticalCompany).ToListAsync();
            var drugDtos = drugs.Select(drug => new DrugReadDto
            {
                Id = drug.Id,
                Trade_Name = drug.Trade_Name,
                Strength = drug.Strength,
                Company_Id = drug.Company_Id,
                Company = new DTOs.Pharmaceutical_Comp.ReadDto
                {
                    Id = drug.PharmaceuticalCompany.Id,
                    Name = drug.PharmaceuticalCompany.Name,
                    Address = drug.PharmaceuticalCompany.Address,
                    Phone = drug.PharmaceuticalCompany.Phone
                }
            });

            return ServiceResult<IEnumerable<DrugReadDto>>.Ok(drugDtos);
        }

        public async Task<ServiceResult<DrugReadDto>> GetByIdAsync(int id)
        {
            var drug = await _context.Drugs.Include(d => d.PharmaceuticalCompany).FirstOrDefaultAsync(d => d.Id == id);

            if (drug == null)
            {
                return ServiceResult<DrugReadDto>.Fail("Drug not found");
            }

            var drugDto = new DrugReadDto
            {
                Id = drug.Id,
                Trade_Name = drug.Trade_Name,
                Strength = drug.Strength,
                Company_Id = drug.Company_Id,
                Company = new DTOs.Pharmaceutical_Comp.ReadDto
                {
                    Id = drug.PharmaceuticalCompany.Id,
                    Name = drug.PharmaceuticalCompany.Name,
                    Address = drug.PharmaceuticalCompany.Address,
                    Phone = drug.PharmaceuticalCompany.Phone
                }
            };

            return ServiceResult<DrugReadDto>.Ok(drugDto);
        }

        public async Task<ServiceResult<DrugReadDto>> UpdateAsync(int id, DrugUpdateDto dto)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null)
            {
                return ServiceResult<DrugReadDto>.Fail("Drug not found");
            }

            var company = await _context.Pharmaceutical_Companies.FindAsync(dto.Company_Id);
            if (company == null)
            {
                return ServiceResult<DrugReadDto>.Fail("Company not found");
            }
            drug.Trade_Name = dto.Trade_Name;
            drug.Strength = dto.Strength;
            drug.Company_Id = dto.Company_Id;

            

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrugExists(id))
                {
                    return ServiceResult<DrugReadDto>.Fail("Drug does not exist");
                }
                else
                {
                    throw;
                }
            }

            return ServiceResult<DrugReadDto>.Ok(new DrugReadDto
            {
                Id = drug.Id,
                Trade_Name = drug.Trade_Name,
                Strength = drug.Strength,
                Company_Id = drug.Company_Id
            });
        }


        private bool DrugExists(int id)
        {
            return _context.Drugs.Any(e => e.Id == id);
        }
    }
}
