using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PayPal.Api;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Extensions;
using Pharmix.Web.Models;
using Pharmix.Web.Services;
using Pharmix.Web.Services.Mappers;

namespace Pharmix.Web.Controllers
{

    public class HomeController : Controller
    {
        private static IHttpContextAccessor m_httpContextAccessor;

        public static HttpContext Current => m_httpContextAccessor.HttpContext;

        public static string AppBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}";

        private readonly string URL;
        private readonly string URL_CreatePayment;
        private readonly string URL_ExecutePayment;
        private readonly string URL_Success;
        private readonly string URL_Cancel;

        private readonly IPaypalServices _PaypalServices;

        private readonly IPackagePlanService packagePlanService;
        private readonly IBusiness_SubscriptionService subscriptionService;
        private readonly IBusinessService businessService;
        private readonly IOptions<ConfigAuthOptions> _options;

        private UserManager<ApplicationUser> _userManager;

        #region Constructor
        public HomeController(IHttpContextAccessor contextAccessor,
                                 UserManager<ApplicationUser> userManager,
                                 IPaypalServices paypalServices,
                                 IPackagePlanService _packagePlanService,
                                 IBusiness_SubscriptionService _subscriptionService,
                                 IBusinessService _businessService, IOptions<ConfigAuthOptions> options)
        {
            _userManager = userManager;

            _PaypalServices = paypalServices;
            packagePlanService = _packagePlanService;
            subscriptionService = _subscriptionService;
            businessService = _businessService;
            _options = options;
            m_httpContextAccessor = contextAccessor;

            URL = AppBaseUrl + "/Home/";
            URL_CreatePayment = URL + "CreatePayment";
            URL_ExecutePayment = URL + "ExecutePayment";
            URL_Success = URL + "Success";
            URL_Cancel = URL + "Cancel";
        }
        #endregion 

        public IActionResult Index()
        {
            return View();
        }

        private bool ValidateGoogleSecret()
        {
            var gSecret = _options.Value.GoogleCaptchaSecret;
            return ReCaptchaValidator.ValidateCaptcha(Request, gSecret);
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel vm)
        {
            var isValid = ValidateGoogleSecret();
            if (ModelState.IsValid && isValid)
            {
                try
                {
                    StringBuilder str = new StringBuilder();

                    str.Append("<p>The following is a message from Contact Us page;</p><br>");
                    str.Append("<table cellspacing='0' cellpadding='0' border='0' style='width:100%;'>");
                    str.Append("<tbody>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Subject: </td><td>" + vm.Subject.Trim() + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Name: </td><td>" + vm.Name.Trim() + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Email: </td><td>" + vm.Email.Trim() + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Phone: </td><td>" + vm.Phone.Trim() + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Message: </td><td>" + vm.Message.Trim() + "</td></tr>");
                    str.Append("</tbody>");
                    str.Append("</table>");
                    if (new Email().SendEmail("support@pharmix.co.uk", "Pharmix Web Contact", str.ToString()))
                    {
                        string customerEmail = "<p>Thanks for contacting us. Our customer support team will respond to your query within 2 working days.</p>";
                        if (vm.SendCopy)
                        {
                            customerEmail += str.ToString();
                        }
                        new Email().SendEmail(vm.Email, "Pharmix Contact Email", customerEmail);

                        ModelState.Clear();
                        ViewBag.Message = "Thank you for Contacting us ";
                    }
                    else
                    {
                        ModelState.Clear();
                        ViewBag.Message = $" Please contact us at support@pharmix.co.uk for any feedbacks.";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Please contact us at support@pharmix.co.uk for any feedbacks. {ex.Message}";
                }
            }
            return View();
        }

        public IActionResult SubscribeNow()
        {
            return View(packagePlanService.GetAllPackagePlans());
        }

        public IActionResult SignUp(int id = 0)
        {
            if (id > 0)
            {
                var result = packagePlanService.GetPackagePlanById(id);
                if (result != null)
                {
                    ViewBag.PackageID = id;
                    var model = businessService.CreateViewModel(0);
                    return View(model);
                }
                else return RedirectToAction("", "Home");
            }
            else return RedirectToAction("", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(BusinessViewModel model)
        {
            int paymentID = 0;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.ContactEmail, Email = model.ContactEmail };
                IdentityResult result = null;
                if (string.IsNullOrEmpty(model.IdentityUserId) || model.Id.Equals("0"))
                {
                    result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        user = _userManager.FindByEmailAsync(model.ContactEmail).Result;
                        if (user != null)
                        {
                            model.IdentityUserId = user.Email;
                            model.Id = businessService.MapViewModelToSite(model, user.Email, true);

                            var plan = packagePlanService.GetPackagePlanById(model.PackageID);

                            DateTime StartDate = DateTime.Now;
                            DateTime EndDate = DateTime.Now;
                            if (plan.Duration == "Monthly")
                            {
                                EndDate = DateTime.Now.AddMonths(1);
                            }
                            else if (plan.Duration == "Annual")
                            {
                                EndDate = DateTime.Now.AddYears(1);
                            }

                            Business_Subscription models = new Business_Subscription
                            {
                                IdentityUserId = user.Email,
                                PackagePlanId = model.PackageID,
                                StartDate = StartDate,
                                EndDate = EndDate,
                                PaymentReceived = false,
                            };

                            paymentID = subscriptionService.MapViewModelToBusiness_Subscription(models, user.Id, true);
                        }

                        ViewBag.IsSuccess = result.Succeeded;
                        return RedirectToAction("Payment", "Home", new { id = paymentID });
                    }
                    else return View("SignUp", model);
                }
                else return View("SignUp", model);
            }
            else return View("SignUp", model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Payment(int id = 0)
        {
            if (id == 0) return RedirectToAction("", "Home");
            else
            {
                ViewBag.URL_CreatePayment = URL_CreatePayment + "?id=" + id;
                ViewBag.URL_ExecutePayment = URL_ExecutePayment;
                ViewBag.URL_Success = URL_Success;
                return View();
            }
        }

        public IActionResult CreatePayment(int id)
        {
            var model = subscriptionService.GetBusiness_SubscriptionById(id);
            if (model != null)
            {
                var package = packagePlanService.GetPackagePlanById(model.PackagePlanId);

                Payment payment = _PaypalServices.CreatePayment(id, package.Price, package.PackageName, URL_ExecutePayment, URL_Cancel, "sale");
                return new JsonResult(payment);
            }
            else return RedirectToAction("", "Home");
        }

        public IActionResult ExecutePayment(string paymentId, string token, string PayerID)
        {
            Payment payment = _PaypalServices.ExecutePayment(paymentId, PayerID);
            if (payment.state == "approved")
            {
                int SubId = Convert.ToInt32(payment.transactions[0].custom);

                var model = subscriptionService.GetBusiness_SubscriptionById(SubId);

                model.PaidAmount = Convert.ToDecimal(payment.transactions[0].amount.total);
                model.Currency = payment.transactions[0].amount.currency;
                model.PaymentReceived = true;
                model.PaymentVia = payment.payer.payment_method;
                model.PaymentReceipt = payment.id;
                model.PaymentReceivedDate = Convert.ToDateTime(payment.create_time);
                model.PaymentNotes = payment.cart + " - " + payment.transactions[0].related_resources[0].sale.payment_mode;

                var response = subscriptionService.MapViewModelToBusiness_Subscription(model, model.IdentityUserId, true);

                try
                {
                    StringBuilder str = new StringBuilder();

                    str.Append("<p>Your Payment is processed successfully.</p><br>");
                    str.Append("<table cellspacing='0' cellpadding='0' border='0' style='width:100%;'>");
                    str.Append("<tbody>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Email: </td><td>" + model.IdentityUserId + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Paid Amount: </td><td>" + model.PaidAmount.ToString("#,##0.00") + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Currency: </td><td>" + model.Currency + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>PaymentVia: </td><td>" + model.PaymentVia + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Payment Receipt: </td><td>" + model.PaymentReceipt + "</td></tr>");
                    str.Append("<tr><td style='width: 80px;font-weight:bold'>Payment Received Date: </td><td>" + Convert.ToDateTime(payment.create_time) + "</td></tr>");
                    str.Append("</tbody>");
                    str.Append("</table>");
                    new Email().SendEmail("support@pharmix.co.uk", "Pharmix Subscription Email", str.ToString());
                    new Email().SendEmail(model.IdentityUserId, "Pharmix Subscription Email", str.ToString());
                }
                catch { }
            }
            return Ok();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
