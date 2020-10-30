using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mvc.ViewModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Mvc.Services;

namespace Mvc.Controllers
{
    public class ConsentController : Controller
    {
        private readonly ConsentService _consentService;
        public ConsentController(ConsentService consentService)
        {
            _consentService = consentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var model = await _consentService.BindConsentViewModelAsync(returnUrl);
            if (model != null)
            {
                return View(model);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel input)
        {
            var result = await _consentService.ConsentGrantProce(input);

            if (!string.IsNullOrEmpty(result.ValidateError))
            {
                ModelState.AddModelError("", result.ValidateError);
            }

            if (result.IsRedirect)
            {
                return Redirect(result.RedirectUri);
            }

            return View(result.ConsentViewModel);
        }

        

    }
}
