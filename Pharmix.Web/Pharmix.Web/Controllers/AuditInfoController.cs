using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Pharmix.Web.Entities;
using Pharmix.Web.Models;
using Pharmix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pharmix.Web.Controllers
{
    public class AuditInfoController : BaseController
    {
        public readonly IAuditInfoService _auditInfoService;
        private readonly IBusinessService businessService;
        public AuditInfoController(UserManager<ApplicationUser> userManager, IAuditInfoService auditInfoService, IBusinessService _businessService)
            :base(userManager, _businessService)
        {
            _auditInfoService = auditInfoService;
            businessService = _businessService;
        }


        public ViewResult VersionCompare(string entityName, string keyId)
        {
            SelectList versionListItem = new SelectList(Enumerable.Empty<SelectList>());
            if (!string.IsNullOrEmpty(entityName) && (!string.IsNullOrEmpty(keyId)))
            {
                var versions= _auditInfoService.GetAuditVersionsByName(entityName, keyId);

                versionListItem = new SelectList(versions, "Key", "Value");
            }

            ViewBag.Versions = versionListItem;
            return View("_VersionCompare");
        }

        public ViewResult GetVersionHtml(int id)
        {
            string version = string.Empty;
            JObject obj = new JObject();
            var auditInfo = _auditInfoService.GetAuditInfoById(id);
            if (auditInfo != null)
            {
                version = auditInfo.Version.ToString();
                obj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject<object>(auditInfo.Info);
            }

            ViewBag.Version = auditInfo.Version.ToString();

            return View("_Version",  _auditInfoService.GetAuditVersionInfoById(id));
        }

        public ViewResult GetVersionHtmlByKeyId(string entityName, string keyId)
        {
            JObject obj = new JObject();
            if (!string.IsNullOrEmpty(entityName) && !string.IsNullOrEmpty(keyId))
            {
                switch (entityName)
                {
                    case "IntegerationOrder":
                        //IntegrationOrder IntegrationOrder=
                        break;
                }
            }

            return View();

        }

        public static T CreateInstance<T>(string name) where T : class
        {
            string assemblyPath = Environment.CurrentDirectory + "\\Pharmix.Web.dll";

            Assembly assembly;

            assembly = Assembly.LoadFrom(assemblyPath);
            Type type = assembly.GetType(name);
            return Activator.CreateInstance(type) as T;
        }

    }
}
