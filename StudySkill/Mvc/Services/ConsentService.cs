using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Services
{
    public class ConsentService
    {
        private readonly IClientStore _clientStore;

        private readonly IResourceStore _resourceStore;

        private readonly IIdentityServerInteractionService _identityServerInteractionService;


        public ConsentService(
                IClientStore clientStore,
                IResourceStore resourceStore,
                IIdentityServerInteractionService identityServerInteractionService
            )
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;
        }

        #region Private Method

        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request, Client client, Resources resources, InputConsentViewModel input = null)
        {
            var rememberConsent = input?.AllowRememberConsent ?? client.AllowRememberConsent;
            var enableScopes = input?.ScopesConsented ?? Enumerable.Empty<string>();
            bool isEdit = input != null;
            var vm = new ConsentViewModel
            {
                ClientName = client.ClientName,
                ClientId = client.ClientId,
                ClientUrl = client.ClientUri,
                ClientLogoUrl = client.LogoUri,
                AllowRememberConsent = rememberConsent
            };
            vm.IdentityScopes = resources.IdentityResources.Select(identity => CreateScopeViewModel(identity,isEdit, enableScopes.Contains(identity.Name)));
            vm.ResourceScopes = resources.ApiResources.Select(api => CreateScopeViewModel(api,isEdit, enableScopes.Contains(api.Name)));
            vm.ApiScopes = resources.ApiScopes.Select(api => CreateScopeViewModel(api, isEdit, enableScopes.Contains(api.Name)));
            return vm;
        }

        private ScopeViewModel CreateScopeViewModel(Resource resource,bool isEdit, bool enabled)
        {
            var vm = new ScopeViewModel
            {
                Description = resource.Description,
                DisplayName = resource.DisplayName,
                Enabled = isEdit? enabled : resource.Enabled,
                Name = resource.Name,
            };
            if (resource is IdentityResource)
            {
                vm.Required = (resource as IdentityResource).Required;
                vm.Emphasize = (resource as IdentityResource).Emphasize;
            }
            if (resource is ApiScope)
            {
                vm.Required = (resource as ApiScope).Required;
                vm.Emphasize = (resource as ApiScope).Emphasize;
            }
            return vm;
        }

        #endregion


        public async Task<ConsentViewModel> BindConsentViewModelAsync(string returnUrl, InputConsentViewModel input = null)
        {
            AuthorizationRequest request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
                return null;

            Client client = await _clientStore.FindEnabledClientByIdAsync(request.Client.ClientId);

            Resources resource = await _resourceStore.FindEnabledResourcesByScopeAsync(client.AllowedScopes);


            var vm = CreateConsentViewModel(request, client, resource, input);
            vm.ReturnUrl = returnUrl;
            return vm;
        }

        public async Task<ConsentGrantProceResult> ConsentGrantProce(InputConsentViewModel model)
        {
            ConsentResponse consentResponse = null;
            var result = new ConsentGrantProceResult();

            if (model.Button == "no")
            {
                consentResponse = new ConsentResponse { Error = AuthorizationError.AccessDenied };
            }
            else if (model.Button == "yes")
            {
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = model.AllowRememberConsent,
                        ScopesValuesConsented = model.ScopesConsented
                    };
                }
                else
                {
                    result.ValidateError = "请至少选中一个权限";
                }
            }
            if (consentResponse != null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(model.ReturnUrl);

                await _identityServerInteractionService.GrantConsentAsync(request, consentResponse);

                result.RedirectUri = model.ReturnUrl;
            }
            else
            {
                result.ConsentViewModel = await BindConsentViewModelAsync(model.ReturnUrl, model);
            }
            return result;
        }


    }
}
