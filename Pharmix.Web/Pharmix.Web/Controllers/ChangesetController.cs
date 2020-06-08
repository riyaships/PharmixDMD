using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PharmixWebApi.CustomModel;
using Ionic.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

using OfficeOpenXml;
using Pharmix.Data.Entities.Context;
using PharmixJob.WebApiClient;
using Pharmix.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Pharmix.Web.Entities;
using System.Data.SqlClient;

namespace Pharmix.Web.Controllers
{
    [Authorize]
    public class ChangesetController : BaseController
    {
        PharmixWebApiClient pharmixWebApiClient = new PharmixWebApiClient();

        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        string fileName = string.Empty;
        string _apiBaseURI = string.Empty;
        private UserManager<ApplicationUser> _userManager;
        private readonly IBusinessService businessService;
        string _SQLQueryPath = string.Empty;
        string _connection = string.Empty;
        string _FreshLoad = string.Empty;

        public ChangesetController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, UserManager<ApplicationUser> userManager, IBusinessService _businessService) : base(userManager, _businessService)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _apiBaseURI = _configuration["ServiceApiURL"].ToString();
            _userManager = userManager;
            businessService = _businessService;
            _SQLQueryPath = configuration["SQLQueryPath"].ToString();
            _FreshLoad = _configuration["FreshLoad"].ToString();
            _connection = _configuration["ConnectionStrings:PharmixEntityContext"].ToString();
        }

        [HttpPost]
        public ActionResult GetToChangesetDetails(int fromchangesetId)
        {
            SelectList changetsetTo = null;
            try
            {
                HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
                TraceService("_apiBaseURI " + _apiBaseURI + DateTime.Now);
                HttpResponseMessage response = client.GetAsync("api/PharmixApi/GetToChangesetDetails?fromchangesetId=" + fromchangesetId).Result;
                CustomReportModel customReportModel = new CustomReportModel();


                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var result = response.Content.ReadAsStringAsync().Result;
                    customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);
                }
                changetsetTo = new SelectList(customReportModel.DmdChangeSetDetails, "DmdChangeSetDetailID", "Description", 0);


            }
            catch (Exception ex)
            {
                TraceService("excetion  " + ex.Message + DateTime.Now); ;

            }

            return Json(changetsetTo);

        }

        [HttpPost]
        public ActionResult GetRevertToPreviousChangeset()
        {
            SelectList changetsetTo = null;
            try
            {
                HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
                TraceService("_apiBaseURI " + _apiBaseURI + DateTime.Now);
                HttpResponseMessage response = client.GetAsync("api/PharmixApi/GetRevertToPreviousChangeset?CurrentUserName=" + CurrentUserName).Result;
                CustomReportModel customReportModel = new CustomReportModel();


                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var result = response.Content.ReadAsStringAsync().Result;
                    customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);
                }

                changetsetTo = new SelectList(customReportModel.DmdChangeSetDetails, "DmdChangeSetDetailID", "Description", 0);



            }
            catch (Exception ex)
            {
                TraceService("excetion  " + ex.Message + DateTime.Now); ;

            }

            return Json(changetsetTo);
        }
        private void TraceService(string content)
        {

            //set up a filestream
            if (!System.IO.Directory.Exists(Environment.CurrentDirectory + "/Logs/"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Logs/");
            }

            FileStream fs = new FileStream(Environment.CurrentDirectory + "/Logs/"+DateTime.Now.ToString("MMyyyy")+ "_Logs.txt", FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();


        }
       
        public IActionResult Index(string universalSearch = "", string universalPageId = "")
        {
            string rootFolder = _hostingEnvironment.WebRootPath + "/Download/";

            string pageID = HttpContext.Request.Query["PageId"].ToString();
            string universalSearchID = HttpContext.Request.Query["universalSearch"].ToString();
            ViewBag.TabId = HttpContext.Request.Query["TabId"].ToString();
            pageID = !string.IsNullOrEmpty(pageID) ? pageID : universalPageId;
            if (!string.IsNullOrEmpty(universalSearch) && universalSearch.Substring(0, 1).Equals("#"))
            {
                universalSearch = universalSearch.Replace('#', '$');
            }
            if (!string.IsNullOrEmpty(universalSearch))
            {
                ViewBag.TabId = "AllItems";
            }

            HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
            HttpResponseMessage response = client.GetAsync("api/PharmixApi/GetChangeWiseReport?pageSize=" + "&skip=" + "&searchValue=" + universalSearch + "&sortColumn=" + "&tabID=" + ViewBag.TabId + "&reportType=" + pageID + "&getTotal=" + true + "&CurrentUserName=" + CurrentUserName).Result;
            CustomReportModel customReportModel = new CustomReportModel();
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var result = response.Content.ReadAsStringAsync().Result;
                customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);
                if (customReportModel.UniversalSearchResultDetails.Count > 0 && !string.IsNullOrEmpty(universalSearch) && !universalSearch.Substring(0, 1).Equals("$"))
                {
                    ViewBag.PageId = "UniversalSearchResults";
                    ViewBag.UniversalSearch = customReportModel.UniversalSearchResultDetails;
                    ViewBag.UniversalSearchID = customReportModel.UniversalSearchResultDetails.FirstOrDefault().UniversalSearch;
                    customReportModel.SearchValue = customReportModel.UniversalSearchResultDetails.FirstOrDefault().UniversalSearch;
                }
                else { ViewBag.PageId = customReportModel.PageId; }
                if (!string.IsNullOrEmpty(universalSearch) && universalSearch.Substring(0, 1).Equals("$"))
                {
                    universalSearch = universalSearch.Replace('$', '#');
                    customReportModel.SearchValue = universalSearch.Replace("#", "$");
                }
                else { customReportModel.SearchValue = universalSearch; }

                ViewBag.SearchValue = universalSearch;
                if (customReportModel.UniversalSearchResultDetails != null && customReportModel.UniversalSearchResultDetails.Count > 0)
                {
                    ViewBag.SearchValue = universalSearch;
                }
                if (ViewBag.PageId != "AdminDetails")
                {
                    if (customReportModel.DmdChangeSetDetails != null && customReportModel.DmdChangeSetDetails.Count > 1 )
                    {
                        customReportModel.DmdChangeSetDetails.RemoveAt(1);
                        customReportModel.DmdChangeSetDetails.Insert(1, new PharmixWebApi.Model.Dmd_ChangeSetDetails { Description = "Initial", DmdChangeSetDetailID = 1 });
                    }
                    if (customReportModel.MyChangesetId > 0 && ViewBag.PageId == "MyBusinessChangesetDetails")
                    {
                        customReportModel.DmdChangeSetDetails = customReportModel.DmdChangeSetDetails.Where(x => x.DmdChangeSetDetailID >= customReportModel.MyChangesetId).ToList();
                    }
                }
                ViewBag.ChangesetName = customReportModel.MyChangeset;
                TempData["ChangesetName"] = customReportModel.MyChangeset;
                TempData.Keep("ChangesetName");
            }
            if (ViewBag.PageId == null)
            { ViewBag.PageId = pageID; }
            if (string.IsNullOrEmpty(ViewBag.PageId) && string.IsNullOrEmpty(pageID))
            {
                ViewBag.PageId = "Index";
            }
            if (ViewBag.PageId == "UniversalSearch")
            {
                ViewBag.PageId = "UniversalSearchResults";
            }
            return View(ViewBag.PageId, customReportModel);
        }

        public IActionResult LoadData(string tabID, string TotalCount, string PageId, int changetSetFrom, int changetSetTo, string SaveData, string universalSearch)
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20
            var length = Request.Form["length"].FirstOrDefault();
            // Sort Column Name
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            if (!string.IsNullOrEmpty(universalSearch) && universalSearch != "/")
            {
                if (universalSearch.Substring(0, 1).Equals("$"))
                {
                    searchValue = universalSearch.Replace('$', '#');
                }
                else
                { searchValue = universalSearch; }
            }

            //Paging Size (10,20,50,100)
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int? recordsTotal = 0;
            dynamic results = null;
            bool isGetTotal = false;
            if (Convert.ToInt32(start) == 0)
            {
                isGetTotal = true;
            }
            if (!string.IsNullOrEmpty(searchValue) && searchValue.Substring(0, 1).Equals("#"))
            {
                searchValue = searchValue.Replace('#', '$');
            }
            //if (!string.IsNullOrEmpty(universalSearch) && !searchValue.Substring(0, 1).Equals("#"))
            //{
            //    PageId = "AllItems";
            //}
            HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
            HttpResponseMessage response = client.GetAsync("api/PharmixApi/GetChangeWiseReport?pageSize=" + pageSize + "&skip=" + skip + "&searchValue=" + searchValue + "&sortColumn=" + sortColumn + "&tabID=" + tabID + "&reportType=" + PageId + "&getTotal=" + isGetTotal + "&changetSetFrom=" + changetSetFrom + "&changetSetTo=" + changetSetTo + "&CurrentUserName=" + CurrentUserName + "&SaveData=" + SaveData + "&sortColumnDirection=" + sortColumnDirection).Result;
            CustomReportModel customReportModel = new CustomReportModel();


            //Checking the response is successful or not which is sent using HttpClient  
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var result = response.Content.ReadAsStringAsync().Result;
                customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);
                if (isGetTotal)
                {

                    recordsTotal = customReportModel.totalCount;
                }
                else
                {
                    recordsTotal = customReportModel.totalCount ?? Convert.ToInt32(TotalCount);
                    //   recordsTotal = Convert.ToInt32(TotalCount);
                }

                if (tabID == "ToBeInserted") { TempData["ToBeInserted"] = recordsTotal; }
                if (tabID == "ToBeUpdated") { @ViewBag.ToBeUpdated = recordsTotal; }
                if (tabID == "ToBeDeleted") { @ViewBag.ToBeDeleted = recordsTotal; }

                if (PageId == "AMPDetails")
                {
                    results = customReportModel.DmdAmpHistory;
                }
                if (PageId == "AMPPDetails")
                {
                    results = customReportModel.DmdAmppHistory;
                }
                if (PageId == "VMPPDetails")
                {
                    results = customReportModel.DmdVmppHistory;
                }
                if (PageId == "VMPDetails")
                {
                    results = customReportModel.DmdVmpHistory;
                }
                if (PageId == "VTMDetails")
                {
                    results = customReportModel.DmdVtmHistory;
                }
                if (PageId == "RouteDetails")
                {
                    results = customReportModel.DmdROUTEHistory;
                }
                if (PageId == "SupplierDetails")
                {
                    results = customReportModel.DmdSUPPLIERHistory;
                }
                if (PageId == "FormDetails")
                {
                    results = customReportModel.DmdFormHistory;
                }
                if (PageId == "GtinDetails")
                {
                    results = customReportModel.DmdGtinHistory;
                }
                if (PageId == "DMDCollatedData")
                {
                    results = customReportModel.DmdReferenceCombinedDataset;
                }
                if (PageId == "AdminDetails")
                {
                    results = customReportModel.DmdChangeSetDetails;
                }
                if (PageId == "MyBusinessChangesetDetails")
                {
                    results = customReportModel.DmdBusinessChangeSetDetails;
                }
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = results });

            }
            return View();
        }

        public IActionResult ExportDataToCSV(string tabID, string TotalCount, string PageId, int changetSetFrom, int changetSetTo, bool allItem, string BusinessUser, bool IsDownloadsqlQuery=true, string changetSetFromId = "", string changetSetToId = "")
        {

            if (!allItem && tabID != "MyBuniessChangeset")
                tabID = "ToBeInserted";
            string rootFolder = _hostingEnvironment.WebRootPath + "/Download/";
            string fileExtension = ".xlsx";
            fileName = string.Concat(PageId, fileExtension);
            if (tabID != "MyBuniessChangeset" && tabID != "AllItems")
            {
                HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);


                TraceService("Api Call Started   " + System.DateTime.Now.ToString());
                TraceService(client.Timeout.TotalMinutes.ToString());
                HttpResponseMessage response = client.GetAsync("api/PharmixApi/ExportData?reportType=" + PageId + "&fileName=" + fileName + "&rootFolder=" + rootFolder + "&changetSetFrom=" + changetSetFrom + "&changetSetTo=" + changetSetTo + "&tabID=" + tabID + "&changetSetFromId=" + changetSetFromId + "&changetSetToId=" + changetSetToId + "&IsDownloadsqlQuery=" + IsDownloadsqlQuery + "&BusinessEmail=" + BusinessUser).Result;
                CustomReportModel customReportModel = new CustomReportModel();
                TraceService("Api Call Ended  " + System.DateTime.Now.ToString());
                if (response.IsSuccessStatusCode)
                {
                    //string query = "exec GetSQLQuery  '',1,3,'C:/Delete/','pharmix@pharmix.co.uk','MyBusinessChangesetDetails20190305170329804.sql'";
                    //ExecuteQuery(query, _connection);
                    TraceService("Api call is done");

                    //TraceService("_connection  " + _connection);
                    //TraceService("query  " + query);
                    var result = response.Content.ReadAsStringAsync().Result;
                    customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);
                    if (tabID == "MyBuniessChangeset")
                    {
                        string zipName = String.Format("MyChangeset_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                        var filesCol = customReportModel.AllFilePathForMychangesetDetails.ToList();
                        // filesCol.Add("C:/Delete/MyBusinessChangesetDetails20190305170329804.sql");
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                            {
                                foreach (var item in filesCol)
                                {

                                    FileInfo file = new FileInfo(item);

                                    ziparchive.CreateEntryFromFile(file.FullName, file.Name);
                                    file.Delete();
                                    TraceService("Files List " + file.FullName + file.Name);

                                }

                            }
                            TraceService("Date 2   " + System.DateTime.Now.ToString());
                            TraceService("zipName  " + zipName);
                            return File(memoryStream.ToArray(), "application/zip", zipName);

                        }
                    }
                    else
                    {
                        FileInfo file = new FileInfo(customReportModel.AllFilePathForMychangesetDetails.FirstOrDefault());
                        TempData["filename"] = file.Name;
                        TempData.Keep("filename");
                    }

                }
                if (!string.IsNullOrEmpty(PageId) && (IsDownloadsqlQuery))
                {
                    rootFolder = _SQLQueryPath;
                }
            }
            else {
                HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
                TraceService("Api Call Started   " + System.DateTime.Now.ToString());
                TraceService(client.Timeout.TotalMinutes.ToString());
                HttpResponseMessage response = client.GetAsync("api/PharmixApi/ToSaveExportDataToCSVDetails?reportType=" + PageId + "&fileName=" + fileName + "&rootFolder=" + rootFolder + "&changetSetFrom=" + changetSetFrom + "&changetSetTo=" + changetSetTo + "&tabID=" + tabID + "&changetSetFromId=" + changetSetFromId + "&changetSetToId=" + changetSetToId + "&IsDownloadsqlQuery=" + IsDownloadsqlQuery + "&BusinessEmail=" + BusinessUser).Result;
                

                return new JsonResult("Message");
            }
            return downloadFile(rootFolder);
        }

        public int ExecuteQuery(string _query, string connectionString)
        {
            SqlConnection _connection = null;
            try
            {
                TraceService("ExecuteQuery " + "ExecuteQuery");
                var test = new SqlConnection().ConnectionTimeout;
                TraceService("ConnectionTimeout " + test);
                _connection = new SqlConnection(connectionString);
               
                _connection.Open();
                SqlCommand _command = new SqlCommand(_query, _connection);
                TraceService(_query);
                TraceService(connectionString);
                _command.CommandTimeout = 0;


                int _count = _command.ExecuteNonQuery();
                _connection.Close();
                TraceService("ExecuteQuery " + "_connection close");
                return _count;
            }
            catch (Exception ex)
            {

                TraceService("Error :" + ex.InnerException.ToString());
                throw null;
            }
            finally
            {
                _connection.Close();
            }

        }

        public FileResult downloadFile(string filePath)
        {
            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(TempData["filename"].ToString());
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, TempData["filename"].ToString());
        }

        public ActionResult DownloadAttachment(string pageId,bool IsDownloadsqlQuery)
        {
            string rootFolder = _hostingEnvironment.WebRootPath + "\\Download";
            string fileExtension = ".xlsx";
            fileName = string.Concat(pageId, fileExtension);
            if (!string.IsNullOrEmpty(pageId) && (IsDownloadsqlQuery))
            {
                rootFolder = _SQLQueryPath;
            }
            return downloadFile(rootFolder);

        }
        public ActionResult DownloadFromMail(string fileName)
        {
            string rootFolder = _FreshLoad;
            TempData["filename"] = fileName;
            TempData.Keep("filename");
            return downloadFile(rootFolder);

        }

     
      public ActionResult GetUniversalSearchResults(string UniversalSearch, string UniversalPageId)
        {
            //string universalSearch = "#68088000";
            return RedirectToAction("Index", new { universalPageId = UniversalPageId, universalSearch = UniversalSearch });
            //string rootFolder = _hostingEnvironment.WebRootPath + "/Download/";

            //string pageID = HttpContext.Request.Query["PageId"].ToString();
            //string universalSearchID = HttpContext.Request.Query["universalSearch"].ToString();
            //ViewBag.TabId = HttpContext.Request.Query["TabId"].ToString();
            //pageID = !string.IsNullOrEmpty(pageID) ? pageID : universalPageId;
            //if (!string.IsNullOrEmpty(universalSearch) && universalSearch.Substring(0, 1).Equals("#"))
            //{
            //    universalSearch = universalSearch.Replace('#', '$');
            //}
            //if (!string.IsNullOrEmpty(universalSearch))
            //{
            //    ViewBag.TabId = "AllItems";
            //}

            //HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
            //HttpResponseMessage response = client.GetAsync("api/PharmixApi/GetChangeWiseReport?pageSize=" + "&skip=" + "&searchValue=" + universalSearch + "&sortColumn=" + "&tabID=" + ViewBag.TabId + "&reportType=" + pageID + "&getTotal=" + true + "&CurrentUserName=" + CurrentUserName).Result;
            //CustomReportModel customReportModel = new CustomReportModel();
            //if (response.IsSuccessStatusCode)
            //{
            //    //Storing the response details recieved from web api   
            //    var result = response.Content.ReadAsStringAsync().Result;
            //    customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);
            //    if (customReportModel.UniversalSearchResultDetails.Count > 0 && !string.IsNullOrEmpty(universalSearch) && !universalSearch.Substring(0, 1).Equals("$"))
            //    {
            //        ViewBag.PageId = "UniversalSearchResults";
            //        ViewBag.UniversalSearch = customReportModel.UniversalSearchResultDetails;
            //        ViewBag.UniversalSearchID = customReportModel.UniversalSearchResultDetails.FirstOrDefault().UniversalSearch;
            //        customReportModel.SearchValue = customReportModel.UniversalSearchResultDetails.FirstOrDefault().UniversalSearch;
            //    }
            //    else { ViewBag.PageId = customReportModel.PageId; }
            //    if (!string.IsNullOrEmpty(universalSearch) && universalSearch.Substring(0, 1).Equals("$"))
            //    {
            //        universalSearch = universalSearch.Replace('$', '#');
            //        customReportModel.SearchValue = universalSearch.Replace("#", "$");
            //    }
            //    else { customReportModel.SearchValue = universalSearch; }

            //    ViewBag.SearchValue = universalSearch;
            //    if (customReportModel.UniversalSearchResultDetails != null && customReportModel.UniversalSearchResultDetails.Count > 0)
            //    {
            //        ViewBag.SearchValue = universalSearch;
            //    }
            //    if (ViewBag.PageId != "AdminDetails")
            //    {
            //        if (customReportModel.DmdChangeSetDetails != null && customReportModel.DmdChangeSetDetails.Count > 1)
            //        {
            //            customReportModel.DmdChangeSetDetails.RemoveAt(1);
            //            customReportModel.DmdChangeSetDetails.Insert(1, new PharmixWebApi.Model.Dmd_ChangeSetDetails { Description = "Initial", DmdChangeSetDetailID = 1 });
            //        }
            //        if (customReportModel.MyChangesetId > 0 && ViewBag.PageId == "MyBusinessChangesetDetails")
            //        {
            //            customReportModel.DmdChangeSetDetails = customReportModel.DmdChangeSetDetails.Where(x => x.DmdChangeSetDetailID >= customReportModel.MyChangesetId).ToList();
            //        }
            //    }
            //    ViewBag.ChangesetName = customReportModel.MyChangeset;
            //    TempData["ChangesetName"] = customReportModel.MyChangeset;
            //    TempData.Keep("ChangesetName");
            //}
            //if (ViewBag.PageId == null)
            //{ ViewBag.PageId = pageID; }
            //if (string.IsNullOrEmpty(ViewBag.PageId) && string.IsNullOrEmpty(pageID))
            //{
            //    ViewBag.PageId = "Index";
            //}
            //if (ViewBag.PageId == "UniversalSearch")
            //{
            //    ViewBag.PageId = "UniversalSearchResults";
            //}
            //return View(ViewBag.PageId, customReportModel);

        }

        public IActionResult LoadDataUniversalSearchResult(string universalSearch)
        {
            string universalSearchID = HttpContext.Request.Query["universalSearch"].ToString();
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20
            var length = Request.Form["length"].FirstOrDefault();
            // Sort Column Name
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int? recordsTotal = 0;
            dynamic results = null;
            if (!string.IsNullOrEmpty(universalSearch) && universalSearch.Substring(0, 1).Equals("#"))
            {
                universalSearch = universalSearch.Replace('#', '$');
            }
            HttpClient client = pharmixWebApiClient.InitializeClient(_apiBaseURI);
            HttpResponseMessage response = client.GetAsync("api/PharmixApi/GetChangeWiseReport?pageSize=" + "&skip=" + "&searchValue=" + universalSearch + "&sortColumn=" + "&tabID=" + ViewBag.TabId + "&reportType=" + "UniversalSearch" + "&getTotal=" + true + "&CurrentUserName=" + CurrentUserName).Result;
            CustomReportModel customReportModel = new CustomReportModel();
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var result = response.Content.ReadAsStringAsync().Result;
                customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);
                results = customReportModel.UniversalSearchResultDetails.ToList();

            }
            if (!string.IsNullOrEmpty(universalSearch) && !universalSearch.Substring(0, 1).Equals("#"))
            {
                recordsTotal = customReportModel.UniversalSearchResultDetails.Count;
            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = results });
        }

        [HttpPost]
        public IActionResult TestData()
        {
            return new JsonResult("Message");
        }
    }
}