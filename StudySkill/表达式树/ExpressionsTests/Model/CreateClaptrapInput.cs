using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpressionsTests
{
    public class CreateClaptrapInput
    {
        [Required, MinLength(3), MaxLength(10)]
        public string Name { get; set; }

        [Required, MinLength(3), MaxLength(8)]
        public string NickName { get; set; }

        [System.ComponentModel.DataAnnotations.RangeAttribute(0, 200)]
        public int Age { get; set; } = 0;

        [Required]
        public int[] Ids { get; set; } = new int[] { 1 };

        public List<int> Ids1 { get; set; } = new List<int> { 3 };

        public IEnumerable<int> Item { get; set; } = new List<int>() { 2 };

        [Required]
        public int? Number { get; set; } = 10;

        [Enum]
        public Sex InputSex { get; set; } = Sex.Man;

        [GreaterThan(Target = nameof(Age))]
        public int NumberOfMeals { get; set; } = 500;

        public string Pwd { get; set; }

        [EqualTo(Target = nameof(Pwd))]
        public string OldPwd { get; set; }
    }
}
