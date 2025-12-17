namespace HospitalApi.DTOs.DoctorPatient
{
    public class DoctorPatientUpdateDto
    {
        public int DoctorId { get; set; }

        public int PatientUrNumber { get; set; }

        public DateTime Appointment_Date { get; set; }
        public string? Notes { get; set; }
    }
}
