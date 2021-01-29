

namespace lib.Data.Default
{
    public class User : GeneralEntity
    {
        //[Required]
        //[Column(TypeName = "varchar(20)")]
        public string UserName { get; set; }

        //[Required]
        //[Column(TypeName = "varchar(20)")]
        public string Pwd { get; set; }
    }
}
