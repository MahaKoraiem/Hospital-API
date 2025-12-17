using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Data;
using HospitalApi.Models;
using HospitalApi.DTOs.DoctorPatient;
using HospitalApi.DTOs.Doctor;
using HospitalApi.DTOs.Patient;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorPatientAppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorPatientAppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DoctorPatientAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorPatientReadDto>>> GetDoctor_Patient_Appointments()
        {
            var list = await _context.Doctor_Patient_Appointments.ToListAsync();
            var dtoList = list.Select(appointment => new DoctorPatientReadDto
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientUrNumber = appointment.PatientUrNumber,
                Appointment_Date = appointment.Appointment_Date,
                Notes = appointment.Notes
            });
            return Ok(dtoList);
        }

        // GET: api/DoctorPatientAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorPatientReadDto>> GetDoctorPatientAppointment(int id)
        {
            var doctorPatientAppointment = await _context.Doctor_Patient_Appointments.Include(dpa=> dpa.Patient).Include(dpa=> dpa.Doctor).FirstOrDefaultAsync(dpa => dpa.Id == id);

            if (doctorPatientAppointment == null)
            {
                return NotFound();
            }

            var dto = new DoctorPatientReadDto
            {
                Id = doctorPatientAppointment.Id,
                DoctorId = doctorPatientAppointment.DoctorId,
                PatientUrNumber = doctorPatientAppointment.PatientUrNumber,
                Appointment_Date = doctorPatientAppointment.Appointment_Date,
                Notes = doctorPatientAppointment.Notes,
                Doctor = new DoctorReadDto
                {
                    Id = doctorPatientAppointment.Doctor.Id,
                    Name = doctorPatientAppointment.Doctor.Name,
                    Specialty = doctorPatientAppointment.Doctor.Specialty,
                    Phone = doctorPatientAppointment.Doctor.Phone
                },
                Patient = new PatientReadDto
                {
                    Ur_Number = doctorPatientAppointment.Patient.Ur_Number,
                    Medicare_Card_Number = doctorPatientAppointment.Patient.Medicare_Card_Number,
                    Name = doctorPatientAppointment.Patient.Name,
                    Age = doctorPatientAppointment.Patient.Age,
                    Address = doctorPatientAppointment.Patient.Address,
                    Phone = doctorPatientAppointment.Patient.Phone
                }
            };

            return Ok(dto);
        }

        // PUT: api/DoctorPatientAppointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctorPatientAppointment(int id, DoctorPatientUpdateDto dto)
        {
            var doctorPatientAppointment = await _context.Doctor_Patient_Appointments.FindAsync(id);
            var patient = await _context.Patients.FindAsync(dto.PatientUrNumber);
            var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
            if (doctorPatientAppointment == null || patient == null || doctor == null)
            {
                return BadRequest("Invalid");
            }

            doctorPatientAppointment.DoctorId = dto.DoctorId;
            doctorPatientAppointment.PatientUrNumber = dto.PatientUrNumber;
            doctorPatientAppointment.Appointment_Date = dto.Appointment_Date;
            doctorPatientAppointment.Notes = dto.Notes;

            _context.Entry(doctorPatientAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorPatientAppointmentExists(id))
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

        // POST: api/DoctorPatientAppointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoctorPatientReadDto>> PostDoctorPatientAppointment(DoctorPatientCreateDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
            var patient = await _context.Patients.FindAsync(dto.PatientUrNumber);
            if (doctor == null || patient == null)
            {
                return BadRequest("Invalid DoctorId or PatientUrNumber: Doctor or Patient does not exist.");
            }
            var doctorPatientAppointment = new DoctorPatientAppointment
            {
                DoctorId = dto.DoctorId,
                PatientUrNumber = dto.PatientUrNumber,
                Appointment_Date = dto.Appointment_Date,
                Notes = dto.Notes
            };
            _context.Doctor_Patient_Appointments.Add(doctorPatientAppointment);
            await _context.SaveChangesAsync();

            var readDto = new DoctorPatientReadDto
            {
                Id = doctorPatientAppointment.Id,
                DoctorId = doctorPatientAppointment.DoctorId,
                PatientUrNumber = doctorPatientAppointment.PatientUrNumber,
                Appointment_Date = doctorPatientAppointment.Appointment_Date,
                Notes = doctorPatientAppointment.Notes
            };

            return CreatedAtAction(nameof(GetDoctorPatientAppointment), new { id = doctorPatientAppointment.Id }, readDto);
        }

        // DELETE: api/DoctorPatientAppointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorPatientAppointment(int id)
        {
            var doctorPatientAppointment = await _context.Doctor_Patient_Appointments.FindAsync(id);
            if (doctorPatientAppointment == null)
            {
                return NotFound();
            }

            _context.Doctor_Patient_Appointments.Remove(doctorPatientAppointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorPatientAppointmentExists(int id)
        {
            return _context.Doctor_Patient_Appointments.Any(e => e.Id == id);
        }
    }
}
