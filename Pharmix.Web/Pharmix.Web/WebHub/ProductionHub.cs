using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Core;
using Pharmix.Data.Enums;
using Pharmix.Web.Services;

namespace Pharmix.Web.WebHub
{
    public class ProductionHub: Hub
    {
        private IProductionService _productionService;
        public ProductionHub(IProductionService productionService)
        {
            _productionService = productionService;
        }
        public async Task SendRequest(string message)
        {
            await Clients.All.SendAsync("SupervisorNotification", message);
        }

        public async Task SendApprovalNotification(int requestId)
        {
            var request = _productionService.FindById(requestId);
            if (request != null)
            {
                var status = Enum.GetName(typeof(RequestStatusEnum),request.LatestRequestStatus);
                await Clients.User(request.RequestedBy).SendAsync("UserNotification", string.Format("One of your request has been {0} by supervisor.", status));
            }
        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Identity.Name;
        }
    }
}
