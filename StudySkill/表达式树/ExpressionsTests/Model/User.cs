using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpressionsTests
{
    public class User : IValidateObject
    {
        [Required]
        public string Phone { get; set; }
    }
}
