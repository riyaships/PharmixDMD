using PayPal.Api;

namespace Pharmix.Web.Services
{
    public interface IPaypalServices
    {
        Payment CreatePayment(int package, decimal amount, string description, string returnUrl, string cancelUrl, string intent);

        Payment ExecutePayment(string paymentId, string payerId);
    }
}
