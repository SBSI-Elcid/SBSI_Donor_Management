namespace BBIS.Application.DTOs.DonorTestOrder
{
    public class DonorTestOrderListDto : DonorTestOrderDto
    {
        public string RegistrationNumber { get; set; }
        public string FullName { get; set; }
        public bool IsReactive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
