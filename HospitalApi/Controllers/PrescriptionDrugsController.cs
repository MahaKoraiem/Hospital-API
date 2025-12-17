using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Data;
using HospitalApi.Models;
using HospitalApi.DTOs.PrescriptionDrug;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionDrugsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionDrugsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PrescriptionDrugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDrugReadDto>>> GetPrescription_Drugs()
        {
            var list = await _context.Prescription_Drugs.ToListAsync();
            var dtoList = list.Select(pd => new PrescriptionDrugReadDto
            {
                Id = pd.Id,
                PrescriptionId = pd.PrescriptionId,
                DrugId = pd.DrugId,
                Quantity_Per_Drug = pd.Quantity_Per_Drug
            });
            return Ok(dtoList);
        }

        // GET: api/PrescriptionDrugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDrugReadDto>> GetPrescriptionDrug(int id)
        {
            var prescriptionDrug = await _context.Prescription_Drugs.FindAsync(id);

            if (prescriptionDrug == null)
            {
                return NotFound();
            }
            var dto = new PrescriptionDrugReadDto
            {
                Id = prescriptionDrug.Id,
                PrescriptionId = prescriptionDrug.PrescriptionId,
                DrugId = prescriptionDrug.DrugId,
                Quantity_Per_Drug = prescriptionDrug.Quantity_Per_Drug
            };

            return Ok(dto);
        }

        // PUT: api/PrescriptionDrugs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescriptionDrug(int id, PrescriptionDrugUpdateDto dto)
        {
            var prescriptionDrug = await _context.Prescription_Drugs.FindAsync(id);
            var drug = await _context.Drugs.FindAsync(dto.DrugId);
         
            if (prescriptionDrug == null || drug == null)
            {
                return BadRequest("does not exist");
            }

            prescriptionDrug.PrescriptionId = dto.PrescriptionId;
            prescriptionDrug.DrugId = dto.DrugId;
            prescriptionDrug.Quantity_Per_Drug = dto.Quantity_Per_Drug;

            _context.Entry(prescriptionDrug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionDrugExists(id))
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

        // POST: api/PrescriptionDrugs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrescriptionDrugReadDto>> PostPrescriptionDrug(PrescriptionDrugCreateDto dto)
        {
            var drug =  await _context.Drugs.FindAsync(dto.DrugId);
            if (drug == null || dto.PrescriptionId == 0)
            {
                return BadRequest("Drug does not exist");
            }
            var prescriptionDrug = new PrescriptionDrug
            {
                PrescriptionId = dto.PrescriptionId,
                DrugId = dto.DrugId,
                Quantity_Per_Drug = dto.Quantity_Per_Drug
            };
            _context.Prescription_Drugs.Add(prescriptionDrug);
            await _context.SaveChangesAsync();

            var prescriptionDrugDto = new PrescriptionDrugReadDto
            {
                Id = prescriptionDrug.Id,
                PrescriptionId = prescriptionDrug.PrescriptionId,
                DrugId = prescriptionDrug.DrugId,
                Quantity_Per_Drug = prescriptionDrug.Quantity_Per_Drug
            };
            return CreatedAtAction(nameof(GetPrescriptionDrug), new { id = prescriptionDrug.Id }, prescriptionDrugDto);
        }

        // DELETE: api/PrescriptionDrugs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescriptionDrug(int id)
        {
            var prescriptionDrug = await _context.Prescription_Drugs.FindAsync(id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            _context.Prescription_Drugs.Remove(prescriptionDrug);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrescriptionDrugExists(int id)
        {
            return _context.Prescription_Drugs.Any(e => e.Id == id);
        }
    }
}
