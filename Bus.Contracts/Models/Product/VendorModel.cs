namespace Bus.Contracts.Models.Product
{
    public sealed record VendorModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string FullNameAr { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string LogoImageName { get; set; } = null!;
        public string CompanyNameAr { get; set; } = null!;
        public string CompanyNameEn { get; set; } = null!;
        public string Address { get; set; } = null!;
        public double Rate { get; set; }
    }
}
