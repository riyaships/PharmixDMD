using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Pharmix.Web.Services.Mappers
{
    public class ConfigAuthOptions
    {
        public string GoogleCaptchaSecret { get; set; }
        public string PayPalClientId { get; set; }

        public string PayPalClientSecret { get; set; }

        public string PayPalPayeeEmail { get; set; }

        public string PayPalPayeeMerchant_ID { get; set; }
    }

}
