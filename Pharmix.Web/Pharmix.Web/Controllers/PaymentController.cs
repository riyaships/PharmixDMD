using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;
using Pharmix.Web.Services;
using System;

namespace Pharmix.Web.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
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

        public PaymentController(IHttpContextAccessor contextAccessor,
                                 UserManager<ApplicationUser> userManager,
                                 IPaypalServices paypalServices,
                                 IPackagePlanService packagePlanService,
                                 IBusiness_SubscriptionService subscriptionService,
                                 IBusinessService _businessService) : base(userManager, _businessService)
        {
            _PaypalServices = paypalServices;
            this.packagePlanService = packagePlanService;
            this.subscriptionService = subscriptionService;
            businessService = _businessService;
            m_httpContextAccessor = contextAccessor;

            URL = AppBaseUrl + "/Payment/";
            URL_CreatePayment = URL + "CreatePayment";
            URL_ExecutePayment = URL + "ExecutePayment";
            URL_Success = URL + "Success";
            URL_Cancel = URL + "Cancel";
        }

        public IActionResult Index(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index", "BuySubscription");
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
            var model = packagePlanService.GetPackagePlanById(id);
            if (model != null)
            {
                Payment payment = _PaypalServices.CreatePayment(id, model.Price, model.PackageName, URL_ExecutePayment, URL_Cancel, "sale");
                return new JsonResult(payment);
            }
            else return RedirectToAction("Index", "BuySubscription");
        }

        public IActionResult ExecutePayment(string paymentId, string token, string PayerID)
        {
            Payment payment = _PaypalServices.ExecutePayment(paymentId, PayerID);
            if (payment.state == "approved")
            {
                int PackagePlanId = Convert.ToInt32(payment.transactions[0].custom);
                var plan = packagePlanService.GetPackagePlanById(PackagePlanId);

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

                Business_Subscription model = new Business_Subscription
                {
                    IdentityUserId = CurrentUserName,
                    PackagePlanId = PackagePlanId,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    PaidAmount = Convert.ToDecimal(payment.transactions[0].amount.total),
                    Currency = payment.transactions[0].amount.currency,
                    PaymentReceived = true,
                    PaymentVia = payment.payer.payment_method,
                    PaymentReceipt = payment.id,
                    PaymentReceivedDate = Convert.ToDateTime(payment.create_time),
                    PaymentNotes = payment.cart + " - " + payment.transactions[0].related_resources[0].sale.payment_mode
                };

                var response = subscriptionService.MapViewModelToBusiness_Subscription(model, CurrentUserName, true);
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