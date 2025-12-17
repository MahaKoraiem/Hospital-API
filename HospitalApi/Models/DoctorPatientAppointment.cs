namespace HospitalApi.Models
{
    public class DoctorPatientAppointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientUrNumber { get; set; }
        public Patient Patient { get; set; }
        public DateTime Appointment_Date { get; set; }
        public string? Notes { get; set; }
    }
}