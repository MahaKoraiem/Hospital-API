using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Data;
using HospitalApi.Models;
using HospitalApi.DTOs.Patient;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            var patientDtos = patients.Select(patient => new PatientReadDto
            {
                Ur_Number = patient.Ur_Number,
                Name = patient.Name,
                Medicare_Card_Number = patient.Medicare_Card_Number,
                Age = patient.Age,
                Address = patient.Address,
                Phone = patient.Phone,
                Email = patient.Email
            });

            return Ok(patientDtos);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientReadDto>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientDto = new PatientReadDto
            {
                Ur_Number = patient.Ur_Number,
                Name = patient.Name,
                Medicare_Card_Number = patient.Medicare_Card_Number,
                Age = patient.Age,
                Address = patient.Address,
                Phone = patient.Phone,
                Email = patient.Email
            };

            return Ok(patientDto);
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PatientUpdateDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return BadRequest("patient does not exist");
            }

            patient.Name = dto.Name;
            patient.Age = dto.Age;
            patient.Address = dto.Address;
            patient.Phone = dto.Phone;
            patient.Email = dto.Email;

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(PatientCreateDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                Medicare_Card_Number = dto.Medicare_Card_Number,
                Age = dto.Age,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            var patientDto = new PatientReadDto
            {
                Ur_Number = patient.Ur_Number,
                Name = patient.Name,
                Medicare_Card_Number = patient.Medicare_Card_Number,
                Age = patient.Age,
                Address = patient.Address,
                Phone = patient.Phone,
                Email = patient.Email
            };

            return CreatedAtAction(nameof(GetPatient), new { id = patient.Ur_Number }, patientDto);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Ur_Number == id);
        }
    }
}
