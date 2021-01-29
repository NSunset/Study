

namespace lib.Data.Initial
{
    public partial class Customer : GeneralEntity
    {
        //[Required]
        //[Column(TypeName = "varchar(20)")]
        public string FirstName { get; set; }

        //[Required]
        //[Column(TypeName = "varchar(20)")]
        public string LastName { get; set; }

        //[Column(TypeName = "varchar(20)")]
        public string StreetAddress { get; set; }

        //[Column(TypeName = "varchar(20)")]
        public string City { get; set; }

        //[Column(TypeName = "varchar(20)")]
        public string StateOrProvinceAbbr { get; set; }

        //[Column(TypeName = "varchar(20)")]
        public string Country { get; set; }

        //[Column(TypeName = "varchar(20)")]
        public string PostalCode { get; set; }

        //[Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }

        //[Column(TypeName = "varchar(20)")]
        public string Email { get; set; }

        //public ICollection<Order> Orders { get; set; }
    }
}
