using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Data;
using HospitalApi.Models;
using HospitalApi.DTOs.Pharmaceutical_Comp;
using HospitalApi.DTOs.Drug;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmaceuticalCompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PharmaceuticalCompaniesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PharmaceuticalCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadDto>>> GetPharmaceutical_Companies()
        {
             var companies = await _context.Pharmaceutical_Companies.ToListAsync();
            var companyDtos = companies.Select(company => new ReadDto
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Phone = company.Phone
            });

            return Ok(companyDtos);

        }

        // GET: api/PharmaceuticalCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadDto>> GetPharmaceuticalCompany(int id)
        {
            var pharmaceuticalCompany = await _context.Pharmaceutical_Companies.Include(ph=>ph.Drugs).FirstOrDefaultAsync(ph=> ph.Id==id);

            if (pharmaceuticalCompany == null)
            {
                return NotFound();
            }

            var pharmaceuticalCompanyDto = new ReadDto
            {
                Id = pharmaceuticalCompany.Id,
                Name = pharmaceuticalCompany.Name,
                Address = pharmaceuticalCompany.Address,
                Phone = pharmaceuticalCompany.Phone,
                Drugs = pharmaceuticalCompany.Drugs?.Select(drug => new DrugReadDto
                {
                    Id = drug.Id,
                    Trade_Name = drug.Trade_Name,
                    Strength = drug.Strength,
                    Company_Id = drug.Company_Id
                }).ToList()
            };

            return Ok(pharmaceuticalCompanyDto);
        }

        // PUT: api/PharmaceuticalCompanies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPharmaceuticalCompany(int id, CreateUpdateDto dto)
        {
            var pharmaceuticalCompany = await _context.Pharmaceutical_Companies.FindAsync(id);
            if (pharmaceuticalCompany == null)
            {
                return NotFound();
            }

            pharmaceuticalCompany.Name = dto.Name;
            pharmaceuticalCompany.Address = dto.Address;
            pharmaceuticalCompany.Phone = dto.Phone;

            _context.Entry(pharmaceuticalCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmaceuticalCompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PharmaceuticalCompanies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReadDto>> PostPharmaceuticalCompany(CreateUpdateDto dto)
        {
            var pharmaceuticalCompany = new PharmaceuticalCompany
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone
            };
            _context.Pharmaceutical_Companies.Add(pharmaceuticalCompany);
            await _context.SaveChangesAsync();

            var pharmaceuticalCompanyDto = new ReadDto
            {
                Id = pharmaceuticalCompany.Id,
                Name = pharmaceuticalCompany.Name,
                Address = pharmaceuticalCompany.Address,
                Phone = pharmaceuticalCompany.Phone
            };

            return CreatedAtAction(nameof(GetPharmaceuticalCompany), new { id = pharmaceuticalCompany.Id }, pharmaceuticalCompanyDto);
        }

        // DELETE: api/PharmaceuticalCompanies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmaceuticalCompany(int id)
        {
            var pharmaceuticalCompany = await _context.Pharmaceutical_Companies.FindAsync(id);
            if (pharmaceuticalCompany == null)
            {
                return NotFound();
            }

            _context.Pharmaceutical_Companies.Remove(pharmaceuticalCompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PharmaceuticalCompanyExists(int id)
        {
            return _context.Pharmaceutical_Companies.Any(e => e.Id == id);
        }
    }
}
