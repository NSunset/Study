using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.ViewModel
{
    public class InputConsentViewModel
    {
        public string Button { get; set; }

        public IEnumerable<string> ScopesConsented { get; set; }

        public bool AllowRememberConsent { get; set; }

        public string ReturnUrl { get; set; }



    }
}
