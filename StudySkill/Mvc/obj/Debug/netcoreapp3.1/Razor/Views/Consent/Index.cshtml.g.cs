#pragma checksum "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a5d71bec7315ebaafce8ac57a211f7979e0446cf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Consent_Index), @"mvc.1.0.view", @"/Views/Consent/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\_ViewImports.cshtml"
using Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\_ViewImports.cshtml"
using Mvc.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
using Mvc.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a5d71bec7315ebaafce8ac57a211f7979e0446cf", @"/Views/Consent/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d14c947c23ce1723f7843b90231a2951c10e8cb5", @"/Views/_ViewImports.cshtml")]
    public class Views_Consent_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ConsentViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "checkbox", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<p> Consent Page</p>\r\n\r\n<div class=\"row page-header\">\r\n    <div class=\"col-sm-10\">\r\n");
#nullable restore
#line 8 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
         if (!string.IsNullOrWhiteSpace(Model.ClientLogoUrl))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                <img");
            BeginWriteAttribute("src", " src=\"", 247, "\"", 273, 1);
#nullable restore
#line 11 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
WriteAttributeValue("", 253, Model.ClientLogoUrl, 253, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n\r\n            </div>\r\n");
#nullable restore
#line 14 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h1>\r\n            ");
#nullable restore
#line 16 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
       Write(Model.ClientName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <small>\r\n                希望使用你的账户\r\n            </small>\r\n        </h1>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row\">\r\n\r\n    <div class=\"col-sm-8\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a5d71bec7315ebaafce8ac57a211f7979e0446cf6584", async() => {
                WriteLiteral("\r\n\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a5d71bec7315ebaafce8ac57a211f7979e0446cf6858", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 29 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.ReturnUrl);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n\r\n            <div class=\"alert alert-danger\" >\r\n                <strong>Error</strong>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a5d71bec7315ebaafce8ac57a211f7979e0446cf8667", async() => {
                    WriteLiteral("\r\n\r\n                ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 34 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n\r\n");
#nullable restore
#line 40 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
             if (Model.IdentityScopes.Any())
            {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                <div class=""panel"">
                    <div class=""panel-heading"">
                        <span class=""glyphicon glyphicon-tasks"">

                        </span>
                        用户信息
                    </div>

                    <ul class=""list-group"">
");
#nullable restore
#line 51 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                         foreach (var scope in Model.IdentityScopes)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                       Write(Html.Partial("_ScopeListItem", scope));

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                                                                  
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                    </ul>\r\n                </div>\r\n");
#nullable restore
#line 57 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
#nullable restore
#line 59 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
             if (Model.ApiScopes.Any())
            {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                <div class=""panel"">

                    <div class=""panel-heading"">
                        <span class=""glyphicon glyphicon-tasks"">

                        </span>
                        API 范围
                    </div>

                    <ul class=""list-group"">
");
#nullable restore
#line 71 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                         foreach (var scope in Model.ApiScopes)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                       Write(Html.Partial("_ScopeListItem", scope));

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                                                                  
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                    </ul>\r\n\r\n                </div>\r\n");
#nullable restore
#line 78 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
#nullable restore
#line 80 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
             if (Model.ResourceScopes.Any())
            {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                <div class=""panel"">

                    <div class=""panel-heading"">
                        <span class=""glyphicon glyphicon-tasks"">

                        </span>
                        API 应用权限
                    </div>

                    <ul class=""list-group"">
");
#nullable restore
#line 92 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                         foreach (var scope in Model.ResourceScopes)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                       Write(Html.Partial("_ScopeListItem", scope));

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                                                                  
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                    </ul>\r\n\r\n                </div>\r\n");
#nullable restore
#line 99 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n            <div>\r\n                <label>\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a5d71bec7315ebaafce8ac57a211f7979e0446cf15255", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 104 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.AllowRememberConsent);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                    <strong>记住我的选择</strong>
                </label>
            </div>

            <div>
                <button value=""yes"" class=""btn btn-primary"" name=""button""  autofocus> 
                    同意
                </button>

                <button value=""no"" name=""button"">
                    取消
                </button>

");
#nullable restore
#line 118 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                 if (!string.IsNullOrWhiteSpace(Model.ClientUrl))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <a");
                BeginWriteAttribute("href", " href=\"", 3267, "\"", 3290, 1);
#nullable restore
#line 120 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
WriteAttributeValue("", 3274, Model.ClientUrl, 3274, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"pull-right btn btn-default\">\r\n                        <span class=\"glyphicon glyphicon-info-sign\">\r\n\r\n                        </span>\r\n                        <strong>\r\n                            ");
#nullable restore
#line 125 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                       Write(Model.ClientUrl);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </strong>\r\n                    </a>\r\n");
#nullable restore
#line 128 "E:\work\GitRepository\Study\StudySkill\Mvc\Views\Consent\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n\r\n\r\n            </div>\r\n\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n\r\n\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ConsentViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591