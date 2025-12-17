namespace HospitalApi.DTOs.Doctor
{
    public class DoctorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int Years_Of_Experience { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
