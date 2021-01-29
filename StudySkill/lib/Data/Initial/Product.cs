

namespace lib.Data.Initial
{
    public partial class Product : GeneralEntity
    {

        //[Required]
        //[Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
