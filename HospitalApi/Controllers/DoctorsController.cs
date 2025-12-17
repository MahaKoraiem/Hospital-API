using HospitalApi.Data;
using HospitalApi.DTOs.Doctor;
using HospitalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            var doctorDtos = doctors.Select(doctor => new DoctorReadDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Years_Of_Experience = doctor.Years_Of_Experience,
                Email = doctor.Email,
                Phone = doctor.Phone
            });

            return Ok(doctorDtos);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorReadDto>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var DoctorReadDto = new DoctorReadDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Years_Of_Experience = doctor.Years_Of_Experience,
                Email = doctor.Email,
                Phone = doctor.Phone
            };

            return Ok(DoctorReadDto);
        }

        // PUT: api/Doctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, UpdateDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Name = dto.Name;
            doctor.Specialty = dto.Specialty;
            doctor.Years_Of_Experience = dto.Years_Of_Experience;
            doctor.Email = dto.Email;
            doctor.Phone = dto.Phone;


            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoctorReadDto>> PostDoctor(DoctorCreateDto dto)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                Specialty = dto.Specialty,
                Years_Of_Experience = dto.Years_Of_Experience,
                Email = dto.Email,
                Phone = dto.Phone
            };
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            var DoctorReadDto = new DoctorReadDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Years_Of_Experience = doctor.Years_Of_Experience,
                Email = doctor.Email,
                Phone = doctor.Phone
            };

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, DoctorReadDto);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
