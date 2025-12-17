using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApi.Data;
using HospitalApi.Models;
using HospitalApi.DTOs.Prescription;
namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionReadDto>>> GetPrescriptions()
        {
            var prescription = await _context.Prescriptions.ToListAsync();
            var prescriptionDtos = prescription.Select(prescription => new PrescriptionReadDto
            {
                Id = prescription.Id,
                Patient_Ur_Number = prescription.Patient_Ur_Number,
                Doctor_Id = prescription.Doctor_Id,
                Total_Quantity_Per_Prescription = prescription.Total_Quantity_Per_Prescription,
                Date = prescription.Date
            });

            return Ok(prescriptionDtos);
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionReadDto>> GetPrescription(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(p=> p.Doctor)
                .Include(p=>p.Patient)
                .Include(p=> p.PrescriptionDrugs)
                    .ThenInclude(pd=> pd.Drug)
                .FirstOrDefaultAsync(p=>p.Id == id);

            if (prescription == null)
            {
                return NotFound();
            }

            var prescriptionDto = new PrescriptionReadDto
            {
                Id = prescription.Id,
                Patient_Ur_Number = prescription.Patient_Ur_Number,
                Doctor_Id = prescription.Doctor_Id,
                Total_Quantity_Per_Prescription = prescription.Total_Quantity_Per_Prescription,
                Date = prescription.Date,
                Doctor = new DTOs.Doctor.DoctorReadDto
                {
                    Id = prescription.Doctor.Id,
                    Name = prescription.Doctor.Name,
                    Specialty = prescription.Doctor.Specialty,
                    Phone = prescription.Doctor.Phone
                },
                Patient = new DTOs.Patient.PatientReadDto
                {
                    Ur_Number = prescription.Patient.Ur_Number,
                    Name = prescription.Patient.Name,
                    Age = prescription.Patient.Age,
                    Address = prescription.Patient.Address,
                    Phone = prescription.Patient.Phone
                },
                Prescriped_Drugs = prescription.PrescriptionDrugs?.Select(pd => new DTOs.PrescriptionDrug.PrescriptionDrugReadDto
                {
                    Id = pd.Id,
                    PrescriptionId = pd.PrescriptionId,
                    DrugId = pd.DrugId,
                    Quantity_Per_Drug = pd.Quantity_Per_Drug,
                    Drug = new DTOs.Drug.DrugReadDto
                    {
                        Id = pd.Drug.Id,
                        Company_Id = pd.Drug.Company_Id,
                        Strength = pd.Drug.Strength,
                        Trade_Name = pd.Drug.Trade_Name

                    }
                }).ToList()
            };

            return Ok(prescriptionDto);
        }

        // PUT: api/Prescriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescription(int id, PrescriptionUpdateDto dto)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            var patient = await _context.Patients.FindAsync(dto.Patient_Ur_Number);
            var doctor = await _context.Doctors.FindAsync(dto.Doctor_Id);
            if (prescription == null || patient == null || doctor == null)
            {
                return BadRequest("dose not exist ");
            }

            prescription.Total_Quantity_Per_Prescription = dto.Total_Quantity_Per_Prescription;
            prescription.Date = dto.Date;
            prescription.Patient_Ur_Number = dto.Patient_Ur_Number;
            prescription.Doctor_Id = dto.Doctor_Id;

            _context.Entry(prescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id))
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

        // POST: api/Prescriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrescriptionReadDto>> PostPrescription(PrescriptionCreateDto dto)
        {
            var patient = await _context.Patients.FindAsync(dto.Patient_Ur_Number);
            var doctor = await _context.Doctors.FindAsync(dto.Doctor_Id);
            if (patient == null || doctor == null)
            {
                return BadRequest("Invalid");
            }
            var newPrescription = new Prescription
            {
                Total_Quantity_Per_Prescription = dto.Total_Quantity_Per_Prescription,
                Date = dto.Date,
                Patient_Ur_Number = dto.Patient_Ur_Number,
                Doctor_Id = dto.Doctor_Id
            };
            _context.Prescriptions.Add(newPrescription);
            await _context.SaveChangesAsync();

            var prescriptionDto = new PrescriptionReadDto
            {
                Id = newPrescription.Id,
                Patient_Ur_Number = newPrescription.Patient_Ur_Number,
                Doctor_Id = newPrescription.Doctor_Id,
                Total_Quantity_Per_Prescription = newPrescription.Total_Quantity_Per_Prescription,
                Date = newPrescription.Date
            };

            return CreatedAtAction(nameof(GetPrescription), new { id = newPrescription.Id }, prescriptionDto);
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.Id == id);
        }
    }
}
