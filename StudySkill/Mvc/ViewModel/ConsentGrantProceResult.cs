using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.ViewModel
{
    public class ConsentGrantProceResult
    {
        public string RedirectUri { get; set; }

        public bool IsRedirect => RedirectUri != null;


        public ConsentViewModel ConsentViewModel { get; set; }

        public string ValidateError { get; set; }
    }
}
