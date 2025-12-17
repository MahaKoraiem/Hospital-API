using HospitalApi.DTOs.Doctor;
using HospitalApi.DTOs.Patient;
namespace HospitalApi.DTOs.DoctorPatient
{
    public class DoctorPatientReadDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        
        public int PatientUrNumber { get; set; }
        
        public DateTime Appointment_Date { get; set; }
        public string? Notes { get; set; }

        public DoctorReadDto? Doctor { get; set; }
        public PatientReadDto? Patient { get; set; }
    }
}
