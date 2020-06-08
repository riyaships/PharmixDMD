using Microsoft.Extensions.Options;
using PayPal.Api;
using Pharmix.Web.Services.Mappers;
using System.Collections.Generic;
using System.Net;

namespace Pharmix.Web.Services
{
    public class PaypalServices : IPaypalServices
    {
        private readonly ConfigAuthOptions _options;

        public PaypalServices(IOptions<ConfigAuthOptions> options)
        {
            _options = options.Value;
        }

        public Payment CreatePayment(int package, decimal amount, string description, string returnUrl, string cancelUrl, string intent)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            Dictionary<string, string> sdkConfig = new Dictionary<string, string>
            {
                { "mode", "sandbox" }
            };
            APIContext apiContext = new APIContext(new OAuthTokenCredential(_options.PayPalClientId, _options.PayPalClientSecret).GetAccessToken())
            {
                Config = sdkConfig
            };

            Amount amnt = new Amount
            {
                currency = "GBP",
                total = amount.ToString()
            };

            List<Transaction> transactionList = new List<Transaction>();
            Transaction tran = new Transaction
            {
                custom = package.ToString(),
                description = description,
                amount = amnt
            };
            transactionList.Add(tran);

            Payer payr = new Payer
            {
                payment_method = "paypal"
            };

            RedirectUrls redirUrls = new RedirectUrls
            {
                cancel_url = cancelUrl,
                return_url = returnUrl
            };

            Payment pymnt = new Payment
            {
                intent = intent,
                payer = payr,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return pymnt.Create(apiContext);
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(_options.PayPalClientId, _options.PayPalClientSecret).GetAccessToken());
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var executedPayment = new Payment() { id = paymentId }.Execute(apiContext, paymentExecution);
            return executedPayment;
        }
    }
}