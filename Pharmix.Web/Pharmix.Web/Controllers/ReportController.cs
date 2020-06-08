using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmix.Web.Models;
using Pharmix.Web.Services;

namespace Pharmix.Web.Controllers
{
    public class ReportController : Controller
    {
        private IIntegrationOrderService _integrationOrderService;

        public ReportController(IIntegrationOrderService integrationOrderService)
        {
            _integrationOrderService = integrationOrderService;
        }

        public IActionResult Index()
        {


            return View();
        }

        public JsonResult GetReportData()
        {
            var isolatorOrderReportViewModel = new ReportViewModel();

            var integerationOrders = _integrationOrderService.GetIntegrationOrders();
            var startDate = DateTime.Now.AddMonths(-6);
            startDate = new DateTime(startDate.Year, startDate.Month, 1);

            integerationOrders = integerationOrders.Where(x => x.ScheduledDate >= startDate);
            var months = integerationOrders.Select(x => x.ScheduledDate.HasValue ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.ScheduledDate.Value.Month) : string.Empty).Distinct().ToList();

            isolatorOrderReportViewModel.labels = months;

            isolatorOrderReportViewModel.datasets = (from odr in integerationOrders
                                                     where odr.ScheduledDate != null
                                                     group odr by new { odr.AllocatedIsolatorId} into grpd
                                                     select new ReportDataSetViewModel
                                                     {
                                                         Id = grpd.First().AllocatedIsolatorId,
                                                         label = grpd.First().Name,
                                                     }).ToList();

            for (int i = 0; i < isolatorOrderReportViewModel.datasets.Count; i++)
            {
                foreach (var month in months)
                {
                    var cnt = integerationOrders.Where(x => x.AllocatedIsolatorId== isolatorOrderReportViewModel.datasets[i].Id && x.ScheduledDate != null && CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.ScheduledDate.Value.Month) == month).Count();
                    if (isolatorOrderReportViewModel.datasets[i].data == null)
                        isolatorOrderReportViewModel.datasets[i].data = new List<int>();
                    isolatorOrderReportViewModel.datasets[i].data.Add(cnt);
                }
            }

            return Json(new { OrderReportData = isolatorOrderReportViewModel });
        }
    }
}