namespace PatientManagmentSystem.Domain.Entities
{
    public class PatientRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
