using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ExpressionsTests
{
    public class ValidateResult
    {
        public bool IsOk { get; set; }

        public List<string> ErrorMessage { get; set; }

        public void Deconstruct(out bool isOk, out string errorMessage)
        {
            isOk = IsOk;
            errorMessage = ErrorMessage?.FirstOrDefault();
        }

        public void Deconstruct(out bool isOk, out string errorMessage, out List<string> allErrorMessage)
        {
            isOk = IsOk;
            errorMessage = ErrorMessage?.FirstOrDefault();
            allErrorMessage = ErrorMessage;
        }

        public static ValidateResult Ok()
        {
            return new ValidateResult
            {
                IsOk = true
            };
        }

        public static ValidateResult Error(string errorMessage)
        {
            return new ValidateResult
            {
                IsOk = false,
                ErrorMessage = new List<string> { errorMessage }
            };
        }

        public static ValidateResult Error(List<string> errorMessage)
        {
            return new ValidateResult
            {
                IsOk = false,
                ErrorMessage = errorMessage
            };
        }

        public string GetFirstErrorMessage()
        {
            return ErrorMessage.FirstOrDefault();
        }
    }
}
