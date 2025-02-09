namespace PatientManagmentSystem.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
