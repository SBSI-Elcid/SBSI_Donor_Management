namespace BBIS.Domain.Models
{
    public class Patient
    {
        public Guid PatientId { get; set; }
        public string BloodType { get; set; }
        public string Rh { get; set; }
        public string PatientIdNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<TestOrder> TestOrders { get; set; }
    }
}
