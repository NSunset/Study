using System;

namespace lib.Data.Initial
{
    public partial class Order : GeneralEntity
    {
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFulfilled { get; set; }
        public int CustomerId { get; set; }

        //public Customer Customer { get; set; }
        //public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
