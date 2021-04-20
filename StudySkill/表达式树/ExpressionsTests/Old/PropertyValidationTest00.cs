using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionsTests
{
    public class PropertyValidationTest00
    {
        private const int _count = 10_000;

        [Test]
        public void Run()
        {
            for (int i = 0; i < _count; i++)
            {
                //test 1
                {
                    var input = new CreateClaptrapInput();
                    var (isOk, errorMessage) = Validate(input);
                    NUnit.Framework.Assert.IsFalse(isOk);
                    Assert.IsNotNull(errorMessage);
                }

                //test 2
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "1"
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsFalse(isOk);
                    Assert.IsNotNull(errorMessage);
                }

                //test 3
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "学海无涯不进则退"
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsTrue(isOk);
                    Assert.IsNull(errorMessage);
                }
            }
        }

        public static ValidateResult Validate(CreateClaptrapInput input)
        {
            return ValidateCode(input, 3);
        }

        public static ValidateResult ValidateCode(CreateClaptrapInput input, int minLength)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                return ValidateResult.Error($"{nameof(input.Name)}不能为空");
            }
            if (input.Name.Length < minLength)
            {
                return ValidateResult.Error($"{nameof(input.Name)}长度最小是{minLength}");
            }
            return ValidateResult.Ok();
        }

        public class CreateClaptrapInput
        {
            public string Name { get; set; }
        }
    }
}
