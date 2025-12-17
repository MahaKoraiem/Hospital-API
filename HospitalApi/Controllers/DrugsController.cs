using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Data;
using HospitalApi.Models;
using HospitalApi.DTOs.Drug;
using HospitalApi.services.Implementations;
using HospitalApi.services.Interfaces;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : BaseApiController
    {
        private readonly IDrugService _service;

        public DrugsController(IDrugService service)
        {
            _service = service;
        }

        // GET: api/Drugs
        [HttpGet]
        public async Task<IActionResult> GetDrugs()
        {
            var result = await _service.GetAllAsync();
            return FromServiceResult(result);
            #region
            //var drugs = await _context.Drugs.Include(d => d.PharmaceuticalCompany).ToListAsync();
            //var drugDtos = drugs.Select(drug => new DrugReadDto
            //{
            //    Id = drug.Id,
            //    Trade_Name = drug.Trade_Name,
            //    Strength = drug.Strength,
            //    Company_Id = drug.Company_Id,
            //    Company = new DTOs.Pharmaceutical_Comp.ReadDto
            //    {
            //        Id = drug.PharmaceuticalCompany.Id,
            //        Name = drug.PharmaceuticalCompany.Name,
            //        Address = drug.PharmaceuticalCompany.Address,
            //        Phone = drug.PharmaceuticalCompany.Phone
            //    }
            //});

            //return Ok(drugDtos);
            #endregion
        }

        // GET: api/Drugs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrug(int id)
        {
            var result =  await _service.GetByIdAsync(id);
            //if (result == null)
            //{
            //    return BadRequest(result.ErrorMessage);
            //}
            if (!result.Success)
                return NotFoundResponse(result.ErrorMessage);

            #region
            //var drug = await _context.Drugs.Include(d=>d.PharmaceuticalCompany).FirstOrDefaultAsync(d=>d.Id==id);

            //if (drug == null)
            //{
            //    return NotFound();
            //}

            //var drugDto = new DrugReadDto
            //{
            //    Id = drug.Id,
            //    Trade_Name = drug.Trade_Name,
            //    Strength = drug.Strength,
            //    Company_Id = drug.Company_Id,
            //    Company = new DTOs.Pharmaceutical_Comp.ReadDto
            //    {
            //        Id = drug.PharmaceuticalCompany.Id,
            //        Name = drug.PharmaceuticalCompany.Name,
            //        Address = drug.PharmaceuticalCompany.Address,
            //        Phone = drug.PharmaceuticalCompany.Phone
            //    }
            //};

            //return Ok(drugDto);
            #endregion
            return FromServiceResult(result);
        }

        // PUT: api/Drugs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrug(int id, DrugUpdateDto updateDto)
        {
            var result = await _service.UpdateAsync(id, updateDto);
            return FromServiceResult(result);
            #region
            //var drug = await _context.Drugs.FindAsync(id);
            //if (drug == null)
            //{
            //    return BadRequest("drug does not exist");
            //}

            //var company = await _context.Pharmaceutical_Companies.FindAsync(updateDto.Company_Id);
            //if (company == null)
            //{
            //    return BadRequest("Invalid Company_Id: Pharmaceutical Company does not exist.");
            //}
            //drug.Trade_Name = updateDto.Trade_Name;
            //drug.Strength = updateDto.Strength;
            //drug.Company_Id = updateDto.Company_Id;

            //_context.Entry(drug).State = EntityState.Modified;

            //try
            //{

            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DrugExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            #endregion
            //if (!result.Success)
            //{
            //    return BadRequest(result.ErrorMessage);
            //}
            //return NoContent();

        }

        // POST: api/Drugs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDrug(DrugCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            //if (result == null)
            //{
            //    return BadRequest(result.ErrorMessage);
            //}
            return FromServiceResult(result);
            #region
            //var company = await _context.Pharmaceutical_Companies.FindAsync(dto.Company_Id);
            //if (company == null)
            //{
            //    return BadRequest("Invalid Company_Id: Pharmaceutical Company does not exist.");
            //}
            //var drug = new Drug
            //{
            //    Trade_Name = dto.Trade_Name,
            //    Strength = dto.Strength,
            //    Company_Id = dto.Company_Id
            //};

            //_context.Drugs.Add(drug);
            //await _context.SaveChangesAsync();

            //var drugDto = new DrugReadDto
            //{
            //    Id = drug.Id,
            //    Trade_Name = drug.Trade_Name,
            //    Strength = drug.Strength,
            //    Company_Id = drug.Company_Id
            //};
            #endregion
            //return CreatedAtAction(nameof(GetDrug), new { id = result.Data.Id }, result);
        }

        // DELETE: api/Drugs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrug(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success)
                return NotFoundResponse(result.ErrorMessage);

            return FromServiceResult(result);
            #region
            //var drug = await _context.Drugs.FindAsync(id);
            //if (drug == null)
            //{
            //    return NotFound();
            //}

            //_context.Drugs.Remove(drug);
            //await _context.SaveChangesAsync();
            #endregion
            //if (!result.Success)
            //{
            //    return BadRequest(result.ErrorMessage);
            //}
            //return NoContent();
        }

        //private bool DrugExists(int id)
        //{
        //    return _context.Drugs.Any(e => e.Id == id);
        //}
    }
}
