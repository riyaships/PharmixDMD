using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmixWebApi.Context;
using PharmixWebApi.CustomModel;
using PharmixWebApi.Model;
using PharmixWebApi.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Dynamic;
using System.IO;
using OfficeOpenXml;
using Microsoft.Extensions.Configuration;
using System.IO.Compression;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace DmdWebApi.Controllers
{
     
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PharmixApiController : Controller
    {
        CustomReportModel customReportModel = new CustomReportModel();
        private readonly ApplicationContext _context;
        List<UploadedFiles> lstuploadedFiles = new List<UploadedFiles>();
        List<string> actionType;
        private IDmdAmpHistoryRepository<Dmd_Amp_History, int> _iDmdAmpHistoryRepository;
        private IDmdChangeSetDetailsRepository<Dmd_ChangeSetDetails, int> _iDmdChangeSetDetailsRepository;
        private IUploadedFilesRepository<UploadedFiles, int> _iUploadedFilesRepository;
        private IDmdBusinessChangeSetDetailsRepository<Dmd_BusinessChangeSetDetails, string> _iDmdBusinessChangeSetDetailsRepository;
        private IDmd_FTPFileDownloadDetailsRepository<Dmd_FTPFileDownloadDetails, int> _iDmd_FTPFileDownloadDetailsRepository;
        private IExportDataToCSVDetailsRepository<ExportDataToCSVDetails, int> _iExportDataToCSVDetailsRepository;
        List<string> getAllFilePathForMychangesetDetails = new List<string>();
        List<string> getAllReportyTypes = new List<string>();
        string _rootFolder = string.Empty;
        string _XMLFilePath = string.Empty;
        string _LogPath = string.Empty;
        string _TemplatePath = string.Empty;
        private IConfiguration _configuration;
        string _ConnectionString = string.Empty;
        string _SQLQueryPath = string.Empty;
        string TimeOut = string.Empty;
        string _FreshLoad = string.Empty;


        public PharmixApiController(ApplicationContext context,
            IDmdAmpHistoryRepository<Dmd_Amp_History, int> iDmdAmpHistoryRepository,
            IDmdChangeSetDetailsRepository<Dmd_ChangeSetDetails, int> iDmdChangeSetDetailsRepository,
            IUploadedFilesRepository<UploadedFiles, int> iUploadedFilesRepository,
            IDmdBusinessChangeSetDetailsRepository<Dmd_BusinessChangeSetDetails, string> iDmdBusinessChangeSetDetailsRepository,
            IDmd_FTPFileDownloadDetailsRepository<Dmd_FTPFileDownloadDetails, int> iDmd_FTPFileDownloadDetailsRepository,
            IConfiguration configuration,
            IExportDataToCSVDetailsRepository<ExportDataToCSVDetails, int> iExportDataToCSVDetailsRepository)
        {
            _context = context;
            _iDmdAmpHistoryRepository = iDmdAmpHistoryRepository;
            _iDmdChangeSetDetailsRepository = iDmdChangeSetDetailsRepository;
            _iUploadedFilesRepository = iUploadedFilesRepository;
            _iDmdBusinessChangeSetDetailsRepository = iDmdBusinessChangeSetDetailsRepository;
            _iDmd_FTPFileDownloadDetailsRepository = iDmd_FTPFileDownloadDetailsRepository;
            actionType = new List<string>() { "ToBeInserted", "ToBeUpdated", "ToBeDeleted" };
            _configuration = configuration;
            _rootFolder = _configuration["RootFolder"].ToString();
            _XMLFilePath = _configuration["XMLFilePath"].ToString();
            _LogPath = _configuration["LogPath"].ToString();
            _TemplatePath = _configuration["TemplatePath"].ToString();
            _ConnectionString = _configuration["ConnectionStrings:PharmixApplicationDB"].ToString();
            _SQLQueryPath = _configuration["SQLQueryPath"].ToString();
            _FreshLoad = _configuration["FreshLoad"].ToString();
            //TimeOut= _configuration["TimeOut"].ToString();
            _iExportDataToCSVDetailsRepository = iExportDataToCSVDetailsRepository;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            // TraceService("done######################################### " );
            return "value";
        }

        [HttpGet]
        [Route("ToGetLatestChangeSetDetails")]
        public Dmd_BusinessChangeSetDetails ToGetLatestChangeSetDetails(string userName)
        {
            Dmd_BusinessChangeSetDetails result = new Dmd_BusinessChangeSetDetails();
            result = (from dmdBusinessChangeSetDetails in _context.Dmd_BusinessChangeSetDetails
                      select dmdBusinessChangeSetDetails).Where(x => x.BusinessEmail == userName).OrderByDescending(x => x.DmdBusinessChangeSetDetailID).FirstOrDefault();


            return result;

        }
        [HttpGet]
        [Route("ToGetFTPFileDownloadDetails")]
        public Dmd_FTPFileDownloadDetails ToGetFTPFileDownloadDetails(string userName)
        {
            TraceService("ToGetFTPFileDownloadDetails start");
            Dmd_FTPFileDownloadDetails result = new Dmd_FTPFileDownloadDetails();
            result = (from dmFTPFileDownloadDetails in _context.Dmd_FTPFileDownloadDetails
                      select dmFTPFileDownloadDetails).OrderByDescending(x => x.FTPFileDownloadID).FirstOrDefault();

            TraceService("ToGetFTPFileDownloadDetails end");
            return result;

        }

        [HttpGet]
        [Route("GetChangeWiseReport")]
        public CustomReportModel GetChangeWiseReport(int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string CurrentUserName, string SaveData, string sortColumnDirection)
        {
            CustomReportModel customReportModel = new CustomReportModel();
            customReportModel.UniversalSearchResultDetails = new List<UniversalSearchResults>();
            bool isUniversalSearch = false;
            bool IdSearch = false;
            //TraceService("tabID " + tabID + DateTime.Now);
            if (tabID != "AllItems")
            {
                actionType = new List<string>();
                actionType.Add(tabID);

            }
            if (reportType != "AllItems" && reportType != "UniversalSearch")
            {
                if (tabID == "/")
                {
                    actionType = new List<string>() { "ToBeInserted", "ToBeUpdated", "ToBeDeleted" };
                }
                getAllReportyTypes = new List<string>() { reportType };
            }
            if (reportType == "UniversalSearch")
            {
                if (!string.IsNullOrEmpty(searchValue) && searchValue != "/")
                {
                    if (searchValue.Substring(0, 1).Equals("$"))
                    {
                        actionType = new List<string>() { "ToBeInserted", "ToBeUpdated", "ToBeDeleted" };
                        getAllReportyTypes = new List<string>() { "AMPDetails", "AMPPDetails", "VMPPDetails", "VMPDetails", "VTMDetails", "RouteDetails", "SupplierDetails",
                "FormDetails", "GtinDetails" };
                        pageSize = 50;
                        isUniversalSearch = true;
                    }
                    else
                    {

                        customReportModel.UniversalSearchResultDetails = _context.UniversalSearchResults.FromSql("exec SP_UniversalSearch " + searchValue).ToList<UniversalSearchResults>();

                        //if (dsResult.Tables.Count > 0)
                        //{
                        //    foreach (DataRow item in dsResult.Tables[0].Rows)
                        //    {
                        //        UniversalSearchResults universalSearchResults = new UniversalSearchResults();
                        //        universalSearchResults.TempName = item[1].ToString();
                        //        universalSearchResults.ReportName = item[2].ToString();
                        //        universalSearchResults.ReportID = item[3].ToString();
                        //        universalSearchResults.ReportDesc = item[4].ToString();
                        //        universalSearchResults.UniversalSearch = searchValue;
                        //        customReportModel.UniversalSearchResultDetails.Add(universalSearchResults);
                        //    }
                        //}
                    }
                }
            }

            Dmd_BusinessChangeSetDetails result = (from dmdBusinessChangeSetDetails in _context.Dmd_BusinessChangeSetDetails
                                                   select dmdBusinessChangeSetDetails).Where(x => x.BusinessEmail == CurrentUserName).OrderByDescending(x => x.DmdBusinessChangeSetDetailID).FirstOrDefault();

            if (result != null)
            {
                customReportModel.MyChangesetId = result.ToDateChangesetId;
                customReportModel.MyChangeset = result.ToDateChangeset;
            }
            else
            {
                customReportModel.MyChangesetId = 0;
            }
            foreach (var itemreportType in getAllReportyTypes)
            {
                // UniversalSearchResults universalSearchResults = new UniversalSearchResults();

                switch (itemreportType)
                {
                    case "AMPDetails":
                        customReportModel = AmpDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdAmpHistory != null && customReportModel.DmdAmpHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "AMPDetails";
                                //universalSearchResults.TempName = "View AMPs";
                                //universalSearchResults.ReportName = "AMPDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdAmpHistory.FirstOrDefault().APIDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdAmpHistory.FirstOrDefault().NMChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "AMPPDetails":
                        customReportModel = AmppDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdAmppHistory != null && customReportModel.DmdAmppHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "AMPPDetails";
                                //universalSearchResults.TempName = "View AMPPs";
                                //universalSearchResults.ReportName = "AMPPDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdAmppHistory.FirstOrDefault().APPIDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdAmppHistory.FirstOrDefault().NMChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "VMPPDetails":
                        customReportModel = VMPPDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdVmppHistory != null && customReportModel.DmdVmppHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "VMPPDetails";
                                //universalSearchResults.TempName = "View VMPPs";
                                //universalSearchResults.ReportName = "VMPPDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdVmppHistory.FirstOrDefault().VPPIDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdVmppHistory.FirstOrDefault().NMChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }

                        break;
                    case "VMPDetails":
                        customReportModel = VMPDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdVmpHistory != null && customReportModel.DmdVmpHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "VMPDetails";
                                //universalSearchResults.TempName = "View VMPs";
                                //universalSearchResults.ReportName = "VMPDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdVmpHistory.FirstOrDefault().VPIDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdVmpHistory.FirstOrDefault().NMChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "VTMDetails":
                        customReportModel = VTMDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdVtmHistory != null && customReportModel.DmdVtmHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "VTMDetails";
                                //universalSearchResults.TempName = "View VTMs";
                                //universalSearchResults.ReportName = "VTMDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdVtmHistory.FirstOrDefault().VTMIDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdVtmHistory.FirstOrDefault().NMChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "RouteDetails":
                        customReportModel = RouteDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdROUTEHistory != null && customReportModel.DmdROUTEHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "RouteDetails";
                                //universalSearchResults.TempName = "View Routes";
                                //universalSearchResults.ReportName = "RouteDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdROUTEHistory.FirstOrDefault().CDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdROUTEHistory.FirstOrDefault().DESCChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }

                        break;
                    case "SupplierDetails":
                        customReportModel = SupplierDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdSUPPLIERHistory != null && customReportModel.DmdSUPPLIERHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "SupplierDetails";
                                //universalSearchResults.TempName = "View Suppliers";
                                //universalSearchResults.ReportName = "SupplierDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdSUPPLIERHistory.FirstOrDefault().CDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdSUPPLIERHistory.FirstOrDefault().DESCChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "FormDetails":
                        customReportModel = FormDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdFormHistory != null && customReportModel.DmdFormHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "FormDetails";
                                //universalSearchResults.TempName = "View Forms";
                                //universalSearchResults.ReportName = "FormDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdFormHistory.FirstOrDefault().CDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdFormHistory.FirstOrDefault().DESCChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "GtinDetails":
                        customReportModel = GtinDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdGtinHistory != null && customReportModel.DmdGtinHistory.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "GtinDetails";
                                //universalSearchResults.TempName = "View Gtins";
                                //universalSearchResults.ReportName = "GtinDetails";
                                //universalSearchResults.ReportID = customReportModel.DmdGtinHistory.FirstOrDefault().AMPPIDChangeset.ToString();
                                //universalSearchResults.ReportDesc = customReportModel.DmdGtinHistory.FirstOrDefault().GTINDATAGTINChangeset.ToString();
                                //universalSearchResults.UniversalSearch = searchValue;
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "DMDCollatedData":
                        customReportModel = DMDCollatedData(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, sortColumnDirection);
                        if (isUniversalSearch)
                        {
                            if (customReportModel.DmdReferenceCombinedDataset != null && customReportModel.DmdReferenceCombinedDataset.Count > 0 && !IdSearch)
                            {
                                customReportModel.PageId = "DMDCollatedData";
                                if (searchValue.Substring(0, 1).Equals("$"))
                                {
                                    IdSearch = true;
                                }
                            }
                        }
                        break;
                    case "AdminDetails":
                        customReportModel = AdminDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo);
                        break;
                    case "MyBusinessChangesetDetails":
                        customReportModel = MyBusinessChangesetDetails(customReportModel, pageSize, skip, searchValue, sortColumn, tabID, reportType, getTotal, changetSetFrom, changetSetTo, CurrentUserName, SaveData);
                        break;
                }
                //if (universalSearchResults.ReportID != null)
                //{
                //    //customReportModel.UniversalSearchResultDetails.Add(universalSearchResults);
                //}
            }

            if (reportType != "AdminDetails")
            {
                customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            }

            if (customReportModel.DmdChangeSetDetails != null && reportType != "AdminDetails")
                customReportModel.DmdChangeSetDetails.Insert(0, new Dmd_ChangeSetDetails { Description = "--Select--", DmdChangeSetDetailID = 0 });
            return customReportModel;
        }

        private CustomReportModel AmpDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "NMChangeset";
            var propertyInfo = typeof(Dmd_Amp_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
              (from dmdAmpHistory in _context.Dmd_Amp_History.Where(amp => actionType.Contains(amp.ActionType))
                                  .OrderBy(x => propertyInfo.GetValue(x, null))
               select dmdAmpHistory)
                : (from dmdAmpHistory in _context.Dmd_Amp_History.Where(amp => actionType.Contains(amp.ActionType))
                                  .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdAmpHistory);

            if (!string.IsNullOrEmpty(searchValue) && searchValue != "/")
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.ABBREVNMLive.Contains(searchValue) || data.NMLive.Contains(searchValue)
                                    || data.NMChangeset.Contains(searchValue) || data.ABBREVNMChangeset.Contains(searchValue)
                                     || data.DESCLive.Contains(searchValue) || data.DESCChangeset.Contains(searchValue));
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.APIDLive) == searchValue || Convert.ToString(data.APIDChangeset) == searchValue);
                }


            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList().Count;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdAmpHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdAmpHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                    && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();

                }
                else
                {
                    customReportModel.DmdAmpHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel AmppDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            // Getting all Customer data
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "NMChangeset";
            var propertyInfo = typeof(Dmd_Ampp_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
              (from dmdAmpHistory in _context.Dmd_Ampp_History.Where(amp => actionType.Contains(amp.ActionType))
                                .OrderBy(x => propertyInfo.GetValue(x, null))
               select dmdAmpHistory)
                : (from dmdAmpHistory in _context.Dmd_Ampp_History.Where(amp => actionType.Contains(amp.ActionType))
                                 .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdAmpHistory);

            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.ABBREVNMLive.Contains(searchValue) || data.NMLive.Contains(searchValue)
                                    || data.NMChangeset.Contains(searchValue) || data.ABBREVNMChangeset.Contains(searchValue));
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.APPIDLive) == searchValue || Convert.ToString(data.APPIDChangeset) == searchValue);
                }


            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList().Count;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdAmppHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdAmppHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                    && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdAmppHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }

            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel VMPPDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            // Getting all Customer data
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "NMChangeset";
            var propertyInfo = typeof(Dmd_Vmpp_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
              (from dmdVmppHistory in _context.Dmd_Vmpp_History.Where(amp => actionType.Contains(amp.ActionType))
                                 .OrderBy(x => propertyInfo.GetValue(x, null))
               select dmdVmppHistory)
                : (from dmdVmppHistory in _context.Dmd_Vmpp_History.Where(amp => actionType.Contains(amp.ActionType))
                                 .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdVmppHistory);

            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.ABBREVNMLive.Contains(searchValue) || data.NMLive.Contains(searchValue)
                                    || data.NMChangeset.Contains(searchValue) || data.ABBREVNMChangeset.Contains(searchValue));
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.VPPIDLive) == searchValue || Convert.ToString(data.VPIDChangeset) == searchValue);
                }


            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList().Count;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdVmppHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdVmppHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                   && x.DmdChangeSetDetailID <= changetSetTo && actionType.Contains(x.ActionType)).Skip(skip).Take(pageSize).ToList();

                }
                else
                {
                    customReportModel.DmdVmppHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel VMPDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            // Getting all Customer data
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "NMChangeset";
            var propertyInfo = typeof(Dmd_Vmp_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
               (from dmdVmpHistory in _context.Dmd_Vmp_History.Where(amp => actionType.Contains(amp.ActionType))
                                 .OrderBy(x => propertyInfo.GetValue(x, null))
                select dmdVmpHistory)
                : (from dmdVmpHistory in _context.Dmd_Vmp_History.Where(amp => actionType.Contains(amp.ActionType))
                                 .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdVmpHistory);


            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.ABBREVNMLive.Contains(searchValue) || data.NMLive.Contains(searchValue)
                                    || data.NMChangeset.Contains(searchValue) || data.ABBREVNMChangeset.Contains(searchValue));
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.VPIDLive) == searchValue || Convert.ToString(data.VPIDChangeset) == searchValue);
                }


            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList().Count;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdVmpHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdVmpHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                  && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdVmpHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }

            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel VTMDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {

            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "NMChangeset";
            var propertyInfo = typeof(Dmd_Vtm_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
               (from dmdVtmHistory in _context.Dmd_Vtm_History.Where(amp => actionType.Contains(amp.ActionType))
                                .OrderBy(x => propertyInfo.GetValue(x, null))
                select dmdVtmHistory)
                : (from dmdVtmHistory in _context.Dmd_Vtm_History.Where(amp => actionType.Contains(amp.ActionType))
                                .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdVtmHistory);


            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.ABBREVNMLive.Contains(searchValue) || data.NMLive.Contains(searchValue)
                                    || data.NMChangeset.Contains(searchValue) || data.ABBREVNMChangeset.Contains(searchValue));
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.VTMIDLive) == searchValue || Convert.ToString(data.VTMIDChangeset) == searchValue);
                }


            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdVtmHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdVtmHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                  && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdVtmHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel RouteDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "DESCChangeset";
            var propertyInfo = typeof(Dmd_ROUTE_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
               (from dmdRouteHistory in _context.Dmd_ROUTE_History.Where(amp => actionType.Contains(amp.ActionType))
                                .OrderBy(x => propertyInfo.GetValue(x, null))
                select dmdRouteHistory)
                : (from dmdRouteHistory in _context.Dmd_ROUTE_History.Where(amp => actionType.Contains(amp.ActionType))
                                .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdRouteHistory);


            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.CDLive.Contains(searchValue) || data.DESCLive.Contains(searchValue)
                                    || data.CDChangeset.Contains(searchValue) || data.DESCChangeset.Contains(searchValue));
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.CDLive) == searchValue || Convert.ToString(data.CDChangeset) == searchValue);
                }
            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdROUTEHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdROUTEHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                  && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdROUTEHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel SupplierDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            // Getting all Customer data            
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "DESCChangeset";
            var propertyInfo = typeof(Dmd_SUPPLIER_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
               (from dmdSupplierHistory in _context.Dmd_SUPPLIER_History.Where(amp => actionType.Contains(amp.ActionType))
                                   .OrderBy(x => propertyInfo.GetValue(x, null))
                select dmdSupplierHistory)
                : (from dmdSupplierHistory in _context.Dmd_SUPPLIER_History.Where(amp => actionType.Contains(amp.ActionType))
                                   .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdSupplierHistory);

            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.CDLive.Contains(searchValue) || data.DESCLive.Contains(searchValue)
                                    || data.CDChangeset.Contains(searchValue) || data.DESCChangeset.Contains(searchValue));
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.CDLive) == searchValue || Convert.ToString(data.CDChangeset) == searchValue);
                }
            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdSUPPLIERHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdSUPPLIERHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                  && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdSUPPLIERHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel FormDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            // Getting all Customer data
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "DESCChangeset";
            var propertyInfo = typeof(Dmd_Form_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
                (from dmdformHistory in _context.Dmd_Form_History.Where(amp => actionType.Contains(amp.ActionType))
                .OrderBy(x => propertyInfo.GetValue(x, null))
                 select dmdformHistory)
                : (from dmdformHistory in _context.Dmd_Form_History.Where(amp => actionType.Contains(amp.ActionType))
                .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdformHistory);




            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.CDLive.Contains(searchValue) ||
                    data.CDDTLive.Contains(searchValue) || data.CDPREVLive.Contains(searchValue) || data.DESCLive.Contains(searchValue) ||
                    data.CDChangeset.Contains(searchValue) || data.CDDTChangeset.Contains(searchValue) || data.CDPREVChangeset.Contains(searchValue) || data.DESCChangeset.Contains(searchValue)
                    );
                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.CDLive) == searchValue || Convert.ToString(data.CDChangeset) == searchValue);
                }
            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdFormHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdFormHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                  && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdFormHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel GtinDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            // Getting all Customer data
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "AMPPIDChangeset";
            var propertyInfo = typeof(Dmd_Gtin_History).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
                (from dmdGinHistory in _context.Dmd_Gtin_History.Where(amp => actionType.Contains(amp.ActionType))
                 .OrderBy(x => propertyInfo.GetValue(x, null))
                 select dmdGinHistory)
                : (from dmdGinHistory in _context.Dmd_Gtin_History.Where(amp => actionType.Contains(amp.ActionType))
                   .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdGinHistory);


            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.GTINDATAGTINLive.Contains(searchValue) || data.GTINDATASTARTDTLive.Contains(searchValue) ||
                    data.GTINDATAGTINChangeset.Contains(searchValue) || data.GTINDATASTARTDTChangeset.Contains(searchValue));

                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.GTINDATAGTINLive) == searchValue
                    || Convert.ToString(data.GTINDATAGTINChangeset) == searchValue);
                }
            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdGtinHistory = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdGtinHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
                  && x.DmdChangeSetDetailID <= changetSetTo).Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdGtinHistory = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                }
                customReportModel.totalCount = customerData.Where(x => x.DmdChangeSetDetailID > changetSetFrom
               && x.DmdChangeSetDetailID <= changetSetTo).ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }

        private CustomReportModel DMDCollatedData(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string sortColumnDirection)
        {
            // Getting all Customer data
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "ProductPackName";
            var propertyInfo = typeof(DmdReferenceCombinedDataset).GetProperty(sortColumn);
            var customerData = (string.IsNullOrEmpty(sortColumnDirection) || sortColumnDirection == "asc") ?
               (from dmdReferenceCombinedDataset in _context.DmdReferenceCombinedDataset
                                 .OrderBy(x => propertyInfo.GetValue(x, null))
                select dmdReferenceCombinedDataset)
                : (from dmdReferenceCombinedDataset in _context.DmdReferenceCombinedDataset
                                 .OrderByDescending(x => propertyInfo.GetValue(x, null))
                   select dmdReferenceCombinedDataset);


            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!searchValue.Substring(0, 1).Equals("$"))
                {
                    customerData = customerData.Where(data => data.ProductName.Contains(searchValue)
                    || data.ProductPackName.Contains(searchValue)
                    || data.VmpName.Contains(searchValue)
                    || data.VtmName.Contains(searchValue)
                    || data.VmppName.Contains(searchValue));

                }
                else
                {
                    searchValue = searchValue.Replace("$", "");
                    customerData = customerData.Where(data => Convert.ToString(data.VtmId).Contains(searchValue)
                    || Convert.ToString(data.VmpId).Contains(searchValue)
                    || Convert.ToString(data.VmppId).Contains(searchValue)
                    || Convert.ToString(data.ProductId).Contains(searchValue)
                    || Convert.ToString(data.ProductPackId).Contains(searchValue));
                }
            }
            if (getTotal && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0) && changetSetFrom == 0 && changetSetTo == 0)
            {
                customReportModel.DmdReferenceCombinedDataset = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0) && changetSetFrom > 0 && changetSetTo > 0)
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdReferenceCombinedDataset = customerData.Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdReferenceCombinedDataset = customerData.ToList();
                }
                customReportModel.totalCount = customerData.ToList().Count;
            }
            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().ToList();
            if (reportType == "UniversalSearch")
            { customReportModel.totalCount = customerData.ToList().Count; }
            return customReportModel;
        }


        private CustomReportModel AdminDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo)
        {
            // Getting all Customer data
            var customerData = (from dmdChangeSetDetails in _context.Dmd_ChangeSetDetails
                                select dmdChangeSetDetails);

            if (!string.IsNullOrEmpty(searchValue))
            {

                customerData = customerData.Where(data => data.Description.Contains(searchValue) ||
                data.WeekNo.Contains(searchValue) || data.YearNo.Contains(searchValue));

            }
            if (getTotal)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0))
            {
                customReportModel.DmdChangeSetDetails = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0))
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdChangeSetDetails = customerData.Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdChangeSetDetails = customerData.ToList();
                }
                customReportModel.totalCount = customerData.ToList().Count;
            }

            return customReportModel;
        }


        private CustomReportModel MyBusinessChangesetDetails(CustomReportModel customReportModel, int pageSize, int skip, string searchValue, string sortColumn, string tabID, string reportType, bool getTotal, int changetSetFrom, int changetSetTo, string CurrentUserName, string SaveData)
        {
            int latestchangetSetTo = default(int);

            if ((changetSetFrom > 0) && (!string.IsNullOrEmpty(SaveData) && SaveData == "Yes"))
                SaveMyBusinessChangesetDetails(changetSetFrom, CurrentUserName, customReportModel.MyChangesetId);

            // Getting all Customer data
            var customerData = (from dmdBusinessChangeSetDetails in _context.Dmd_BusinessChangeSetDetails
                                select dmdBusinessChangeSetDetails).Where(x => x.BusinessEmail == CurrentUserName);

            if (!string.IsNullOrEmpty(searchValue))
            {

                customerData = customerData.Where(data => data.FromDateChangeset.Contains(searchValue) ||
                data.ToDateChangeset.Contains(searchValue));

            }
            if (getTotal)
            {
                customReportModel.totalCount = customerData.ToList() != null ? customerData.ToList().Count : 0;
                //customReportModel.totalCount = 500;
            }
            if ((pageSize > 0))
            {
                customReportModel.DmdBusinessChangeSetDetails = customerData.Skip(skip).Take(pageSize).ToList();
                //customReportModel.totalCount = customReportModel.DmdVtmHistory.Count;
            }
            if ((pageSize > 0))
            {
                if (pageSize > 10)
                {
                    customReportModel.DmdBusinessChangeSetDetails = customerData.Skip(skip).Take(pageSize).ToList();
                }
                else
                {
                    customReportModel.DmdBusinessChangeSetDetails = customerData.ToList();
                }
                customReportModel.totalCount = customerData.ToList().Count;
            }
            if (customReportModel.DmdBusinessChangeSetDetails != null && customReportModel.DmdBusinessChangeSetDetails.Count > 0)
            {
                customReportModel.DmdBusinessChangeSetDetails = customReportModel.DmdBusinessChangeSetDetails.OrderByDescending(x => x.DmdBusinessChangeSetDetailID).ToList();
                latestchangetSetTo = customReportModel.DmdBusinessChangeSetDetails.OrderByDescending(x => x.DmdBusinessChangeSetDetailID).FirstOrDefault().ToDateChangesetId;
            }
            else
            {

                customReportModel.DmdBusinessChangeSetDetails = customReportModel.DmdBusinessChangeSetDetails = new List<Dmd_BusinessChangeSetDetails>();
            }

            customReportModel.DmdChangeSetDetails = GetAllDmChangeSetDetails().Where(x => x.DmdChangeSetDetailID > latestchangetSetTo).ToList();

            return customReportModel;
        }

        [HttpGet]
        [Route("ExportData")]
        public CustomReportModel ExportData(string reportType, string fileName, string rootFolder, int changetSetFrom, int changetSetTo, string tabID, string changetSetFromId, string changetSetToId, bool IsDownloadsqlQuery, string BusinessEmail)
        {
            TraceService("---ExportData Method Started------");



            CustomReportModel customReportModel = new CustomReportModel();
            getAllFilePathForMychangesetDetails = new List<string>();
            if (tabID == "MyBuniessChangeset" || tabID == "FreshLoad")
            {
                fileName = tabID == "MyBuniessChangeset" ? "MyBusinessChangesetDetails.xlsx" : "InitialChangeset.xlsx";
                getAllReportyTypes = new List<string>() { "AMPDetails", "AMPPDetails", "VMPPDetails", "VMPDetails", "VTMDetails", "RouteDetails", "SupplierDetails",
                "FormDetails", "GtinDetails" };
                if (changetSetFromId != "Initial")
                {
                    changetSetFrom = Convert.ToInt32(_iDmdChangeSetDetailsRepository.GetAll().ToList().Where(x => x.Description == changetSetFromId).FirstOrDefault().DmdChangeSetDetailID);
                }
                if (changetSetFromId == "Initial")
                {
                    changetSetFrom = Convert.ToInt32(_iDmdChangeSetDetailsRepository.GetAll().ToList().FirstOrDefault().DmdChangeSetDetailID);
                }
                if (!string.IsNullOrEmpty(changetSetToId))
                    changetSetTo = Convert.ToInt32(_iDmdChangeSetDetailsRepository.GetAll().ToList().Where(x => x.Description == changetSetToId).FirstOrDefault().DmdChangeSetDetailID);

            }
            else
            {
                getAllReportyTypes = new List<string>() { reportType };
            }
            if (!IsDownloadsqlQuery || reportType == string.Empty || tabID == "MyBuniessChangeset" || tabID == "FreshLoad")
            {
                foreach (var itemreportType in getAllReportyTypes)
                {
                    switch (itemreportType)
                    {
                        case "AMPDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdAmpHistory = (from dmdVtmHistory in _context.Dmd_Amp_History
                                                                   select dmdVtmHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdAmpHistory = (from dmdVtmHistory in _context.Dmd_Amp_History
                                                                   select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdAmpHistory = (from dmdVtmHistory in _context.Dmd_Amp_History
                                                                   select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                         && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }
                            ExportAMPDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "AMPPDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdAmppHistory = (from dmdVtmHistory in _context.Dmd_Ampp_History
                                                                    select dmdVtmHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdAmppHistory = (from dmdVtmHistory in _context.Dmd_Ampp_History
                                                                    select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdAmppHistory = (from dmdVtmHistory in _context.Dmd_Ampp_History
                                                                    select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                         && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }
                            ExportAMPPDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "VMPPDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdVmppHistory = (from dmdVtmHistory in _context.Dmd_Vmpp_History
                                                                    select dmdVtmHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdVmppHistory = (from dmdVtmHistory in _context.Dmd_Vmpp_History
                                                                    select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdVmppHistory = (from dmdVtmHistory in _context.Dmd_Vmpp_History
                                                                    select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                         && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }

                            ExportVMPPDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "VMPDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdVmpHistory = (from dmdVtmHistory in _context.Dmd_Vmp_History
                                                                   select dmdVtmHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdVmpHistory = (from dmdVtmHistory in _context.Dmd_Vmp_History
                                                                   select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdVmpHistory = (from dmdVtmHistory in _context.Dmd_Vmp_History
                                                                   select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                         && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }

                            ExportVMPDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "VTMDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdVtmHistory = (from dmdVtmHistory in _context.Dmd_Vtm_History
                                                                   select dmdVtmHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdVtmHistory = (from dmdVtmHistory in _context.Dmd_Vtm_History
                                                                   select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdVtmHistory = (from dmdVtmHistory in _context.Dmd_Vtm_History
                                                                   select dmdVtmHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                         && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }

                            ExportVTMDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "RouteDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdROUTEHistory = (from dmdRouteHistory in _context.Dmd_ROUTE_History
                                                                     select dmdRouteHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdROUTEHistory = (from dmdRouteHistory in _context.Dmd_ROUTE_History
                                                                     select dmdRouteHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdROUTEHistory = (from dmdRouteHistory in _context.Dmd_ROUTE_History
                                                                     select dmdRouteHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                           && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }
                            ExportRouteDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "SupplierDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdSUPPLIERHistory = (from dmdSupplierHistory in _context.Dmd_SUPPLIER_History
                                                                        select dmdSupplierHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdSUPPLIERHistory = (from dmdSupplierHistory in _context.Dmd_SUPPLIER_History
                                                                        select dmdSupplierHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdSUPPLIERHistory = (from dmdSupplierHistory in _context.Dmd_SUPPLIER_History
                                                                        select dmdSupplierHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                              && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }

                            ExportSupplierDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "FormDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdFormHistory = (from dmdFormrHistory in _context.Dmd_Form_History
                                                                    select dmdFormrHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdFormHistory = (from dmdFormrHistory in _context.Dmd_Form_History
                                                                    select dmdFormrHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdFormHistory = (from dmdFormrHistory in _context.Dmd_Form_History
                                                                    select dmdFormrHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                          && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }

                            ExportFormDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;

                        case "GtinDetails":
                            if (tabID == "AllItems")
                            {
                                customReportModel.DmdGtinHistory = (from dmdGtinrHistory in _context.Dmd_Gtin_History
                                                                    select dmdGtinrHistory).ToList();
                            }
                            else if (tabID == "FreshLoad")
                            {
                                customReportModel.DmdGtinHistory = (from dmdGtinrHistory in _context.Dmd_Gtin_History
                                                                    select dmdGtinrHistory).Where(x => x.DmdChangeSetDetailID == changetSetFrom).ToList();
                            }
                            else
                            {
                                customReportModel.DmdGtinHistory = (from dmdGtinrHistory in _context.Dmd_Gtin_History
                                                                    select dmdGtinrHistory).Where(x => x.DmdChangeSetDetailID > changetSetFrom
                                                                          && x.DmdChangeSetDetailID <= changetSetTo).ToList();
                            }

                            ExportGtinDetails(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "DMDCollatedData":
                            //if (tabID == "AllItems")
                            //{
                            customReportModel.DmdReferenceCombinedDataset = (from dmdReferenceCombinedDataset in _context.DmdReferenceCombinedDataset
                                                                             select dmdReferenceCombinedDataset).ToList();
                            //}


                            ExportDMDCollatedData(customReportModel, fileName, rootFolder, reportType, tabID);
                            break;
                        case "AdminDetails":

                            customReportModel.DmdChangeSetDetails = (from dmdChangeSetDetails in _context.Dmd_ChangeSetDetails
                                                                     select dmdChangeSetDetails).ToList();

                            break;
                    }

                    customReportModel.AllFilePathForMychangesetDetails = getAllFilePathForMychangesetDetails;
                }
            }
            if (IsDownloadsqlQuery)
            {
                fileName = fileName == "MyBusinessChangesetDetails.xlsx" ? "MyBusinessChangesetDetails.sql" : reportType + ".sql";
                fileName = MyExtensions.AppendTimeStamp(fileName);
                FileInfo file = new FileInfo(Path.Combine(_SQLQueryPath, fileName));
                if (file.Exists)
                {
                    file.Delete();
                }
                // getAllFilePathForMychangesetDetails.Add(file.FullName);

                string query = string.Empty;
                if (string.IsNullOrEmpty(reportType))
                    reportType = "";
                query = "exec GetSQLQuery ";
                query = query + " '" + reportType + "',";
                query = query + changetSetFrom + ",";
                query = query + changetSetTo + ",";
                query = query + "'" + _SQLQueryPath + "'" + ",";
                query = query + "'" + BusinessEmail + "'" + ",";
                query = query + "'" + fileName + "'";
                PharmixCommon.Common common = new PharmixCommon.Common();

                
                ExecuteQuery(query, _ConnectionString);
                customReportModel.AllFilePathForMychangesetDetails = customReportModel.AllFilePathForMychangesetDetails ?? new List<string>();
                customReportModel.AllFilePathForMychangesetDetails.Add(file.FullName);
               
            }

            return customReportModel;


        }

        [HttpPost]
        [Route("SaveMyBusinessChangesetDetails")]
        public void SaveMyBusinessChangesetDetails(int changetSetFrom, string CurrentUserName, int MyChangesetId)
        {
            Dmd_BusinessChangeSetDetails dmdBusinessChangeSetDetails = new Dmd_BusinessChangeSetDetails();
            dmdBusinessChangeSetDetails.ToDateChangesetId = changetSetFrom;
            dmdBusinessChangeSetDetails.CreatedOn = System.DateTime.Now;
            dmdBusinessChangeSetDetails.BusinessEmail = CurrentUserName;
            var getMyBusinessDetails = _iDmdBusinessChangeSetDetailsRepository.Get(CurrentUserName);
            int toDateChangeset = default(int);
            if (getMyBusinessDetails != null)
            {
                toDateChangeset = changetSetFrom > MyChangesetId ? (getMyBusinessDetails.ToDateChangesetId + 1) : changetSetFrom;
                dmdBusinessChangeSetDetails.IsActive = changetSetFrom > MyChangesetId ? true : false;
            }
            else
            {
                toDateChangeset = 1;
                dmdBusinessChangeSetDetails.IsActive = true;
            }
            dmdBusinessChangeSetDetails.FromDateChangesetId = toDateChangeset;
            dmdBusinessChangeSetDetails.FromDateChangeset = toDateChangeset == 1 ? "Initial" : _iDmdChangeSetDetailsRepository.Get(toDateChangeset).Description;
            dmdBusinessChangeSetDetails.ToDateChangeset = _iDmdChangeSetDetailsRepository.Get(changetSetFrom).Description;
            _iDmdBusinessChangeSetDetailsRepository.Add(dmdBusinessChangeSetDetails);
        }

        [HttpPost]
        [Route("AddUploadedFilesWebAi")]
        public void AddUploadedFilesWebAi([FromBody]string JsonStringToJSon)
        {
            try
            {
                //TraceService("exception " + JsonStringToJSon);
                customReportModel.UploadedFiles = JsonConvert.DeserializeObject<List<UploadedFiles>>(JsonStringToJSon);
                foreach (var item in customReportModel.UploadedFiles)
                {
                    int userDetailID = _iUploadedFilesRepository.Add(item);
                }
            }
            catch (Exception ex)
            {

                TraceService("exception " + ex.Message);
            }


        }

        [HttpPost]
        [Route("ToSaveFTPFileDownloadDetails")]
        public void ToSaveFTPFileDownloadDetails([FromBody]string JsonStringToJSon)
        {
            try
            {
                //TraceService("exception " + JsonStringToJSon);
                customReportModel.DmdFTPFileDownloadDetails = JsonConvert.DeserializeObject<List<Dmd_FTPFileDownloadDetails>>(JsonStringToJSon);
                foreach (var item in customReportModel.DmdFTPFileDownloadDetails)
                {
                    int id = _iDmd_FTPFileDownloadDetailsRepository.Add(item);
                }
            }
            catch (Exception ex)
            {

                TraceService("exception " + ex.Message);
            }


        }

        private void TraceService(string content)
        {

            //set up a filestream
            FileStream fs = new FileStream(_LogPath, FileMode.OpenOrCreate, FileAccess.Write);

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

        public IEnumerable<Dmd_Amp_History> GetAllDmdAmpHistory()
        {
            return _context.Dmd_Amp_History;
        }
        public IEnumerable<Dmd_ChangeSetDetails> GetAllDmChangeSetDetails()
        {
            return _context.Dmd_ChangeSetDetails;
        }
        [HttpGet]
        [Route("GetToChangesetDetails")]
        public CustomReportModel GetToChangesetDetails(int fromchangesetId)
        {
            CustomReportModel customReportModel = new CustomReportModel();
            customReportModel.DmdChangeSetDetails = _context.Dmd_ChangeSetDetails.Where(changeSet => changeSet.DmdChangeSetDetailID > fromchangesetId).ToList();
            return customReportModel;

        }
        [HttpGet]
        [Route("GetRevertToPreviousChangeset")]
        public CustomReportModel GetRevertToPreviousChangeset(string CurrentUserName)
        {

            CustomReportModel customReportModel = new CustomReportModel();
            customReportModel.DmdChangeSetDetails = _context.Dmd_ChangeSetDetails.OrderBy(x => x.DmdChangeSetDetailID).ToList();

            customReportModel.DmdChangeSetDetails.Insert(0, new Dmd_ChangeSetDetails { Description = "--Select--", DmdChangeSetDetailID = 0 });
            customReportModel.DmdChangeSetDetails.RemoveAt(1);
            customReportModel.DmdChangeSetDetails.Insert(1, new PharmixWebApi.Model.Dmd_ChangeSetDetails { Description = "Initial", DmdChangeSetDetailID = 1 });
            customReportModel.DmdChangeSetDetails = customReportModel.DmdChangeSetDetails.OrderBy(x => x.DmdChangeSetDetailID).ToList();
            return customReportModel;

        }

        #region Exports

        #region ExportAMPDetails
        private void ExportAMPDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "AMPDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_Amp_History> DmdAmpHistory = new List<Dmd_Amp_History>();
                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdAmpHistory.Where(amp => amp.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "APID";
                    wsToBeInserted.Cells[1, 2].Value = "INVALID";
                    wsToBeInserted.Cells[1, 3].Value = "VPID";
                    wsToBeInserted.Cells[1, 4].Value = "NM";
                    wsToBeInserted.Cells[1, 5].Value = "ABBREVNM";
                    wsToBeInserted.Cells[1, 6].Value = "DESC";
                    wsToBeInserted.Cells[1, 7].Value = "NMDT";
                    wsToBeInserted.Cells[1, 8].Value = "NM_PREV";
                    wsToBeInserted.Cells[1, 9].Value = "SUPPCD";
                    wsToBeInserted.Cells[1, 10].Value = "LIC_AUTHCD";
                    wsToBeInserted.Cells[1, 11].Value = "LIC_AUTH_PREVCD";
                    wsToBeInserted.Cells[1, 12].Value = "LIC_AUTHCHANGECD";
                    wsToBeInserted.Cells[1, 13].Value = "LIC_AUTHCHANGEDT";
                    wsToBeInserted.Cells[1, 14].Value = "COMBPRODCD";
                    wsToBeInserted.Cells[1, 15].Value = "FLAVOURCD";
                    wsToBeInserted.Cells[1, 16].Value = "EMA";
                    wsToBeInserted.Cells[1, 17].Value = "PARALLEL_IMPORT";
                    wsToBeInserted.Cells[1, 18].Value = "AVAIL_RESTRICTCD";
                    wsToBeInserted.Cells[1, 19].Value = "AMPS_Id";

                    var fullSql = string.Empty;


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmpHistory = customReportModel.DmdAmpHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdAmpHistory[currentcol].APIDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdAmpHistory[currentcol].INVALIDChangeset;
                        wsToBeInserted.Cells[row, 3].Value = "" + DmdAmpHistory[currentcol].VPIDChangeset;
                        wsToBeInserted.Cells[row, 4].Value = "" + DmdAmpHistory[currentcol].NMChangeset;
                        wsToBeInserted.Cells[row, 5].Value = "" + DmdAmpHistory[currentcol].ABBREVNMChangeset;
                        wsToBeInserted.Cells[row, 6].Value = "" + DmdAmpHistory[currentcol].DESCChangeset;
                        wsToBeInserted.Cells[row, 7].Value = "" + DmdAmpHistory[currentcol].NMDTChangeset;
                        wsToBeInserted.Cells[row, 8].Value = "" + DmdAmpHistory[currentcol].NM_PREVChangeset;
                        wsToBeInserted.Cells[row, 9].Value = "" + DmdAmpHistory[currentcol].SUPPCDChangeset;
                        wsToBeInserted.Cells[row, 10].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCDChangeset;
                        wsToBeInserted.Cells[row, 11].Value = "" + DmdAmpHistory[currentcol].LIC_AUTH_PREVCDChangeset;
                        wsToBeInserted.Cells[row, 12].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGECDChangeset;
                        wsToBeInserted.Cells[row, 13].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGEDTChangeset;
                        wsToBeInserted.Cells[row, 14].Value = "" + DmdAmpHistory[currentcol].COMBPRODCDChangeset;
                        wsToBeInserted.Cells[row, 15].Value = "" + DmdAmpHistory[currentcol].FLAVOURCDChangeset;
                        wsToBeInserted.Cells[row, 16].Value = "" + DmdAmpHistory[currentcol].EMAChangeset;
                        wsToBeInserted.Cells[row, 17].Value = "" + DmdAmpHistory[currentcol].PARALLEL_IMPORTChangeset;
                        wsToBeInserted.Cells[row, 18].Value = "" + DmdAmpHistory[currentcol].AVAIL_RESTRICTCDChangeset;
                        wsToBeInserted.Cells[row, 19].Value = "" + DmdAmpHistory[currentcol].AMPS_IdChangeset;

                        var rowSql =
                            $"INSERT INTO [dbo].[DMD_AMP](APID, INVALID, VPID, NM, ABBREVNM,  DESC, NMDT, NM_PREV, SUPPCD, LIC_AUTHCD, LIC_AUTH_PREVCD, LIC_AUTHCHANGECD, LIC_AUTHCHANGEDT, COMBPRODCD,FLAVOURCD, FLAVOURCD, EMA, PARALLEL_IMPORT, AVAIL_RESTRICTCD, AMPS_Id) " +
                            $"VALUES({wsToBeInserted.Cells[row, 1].Value}, {wsToBeInserted.Cells[row, 2].Value}, {wsToBeInserted.Cells[row, 3].Value}, {wsToBeInserted.Cells[row, 4].Value}, {wsToBeInserted.Cells[row, 5].Value},{wsToBeInserted.Cells[row, 5].Value},{wsToBeInserted.Cells[row, 6].Value},{wsToBeInserted.Cells[row, 7].Value},{wsToBeInserted.Cells[row, 8].Value},{wsToBeInserted.Cells[row, 9].Value},{wsToBeInserted.Cells[row, 10].Value},{wsToBeInserted.Cells[row, 11].Value},{wsToBeInserted.Cells[row, 12].Value},{wsToBeInserted.Cells[row, 13].Value},{wsToBeInserted.Cells[row, 14].Value},{wsToBeInserted.Cells[row, 15].Value},{wsToBeInserted.Cells[row, 16].Value},{wsToBeInserted.Cells[row, 17].Value},{wsToBeInserted.Cells[row, 18].Value},{wsToBeInserted.Cells[row, 19].Value})";
                        fullSql += rowSql;

                        currentcol++;
                    }
                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdAmpHistory.Where(Amp => Amp.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "APID Current";
                    wsToBeUpdated.Cells[1, 2].Value = "INVALID Current";
                    wsToBeUpdated.Cells[1, 3].Value = "VPID Current";
                    wsToBeUpdated.Cells[1, 4].Value = "NM Current";
                    wsToBeUpdated.Cells[1, 5].Value = "ABBREVNM Current";
                    wsToBeUpdated.Cells[1, 6].Value = "DESC Current";
                    wsToBeUpdated.Cells[1, 7].Value = "NMDT Current";
                    wsToBeUpdated.Cells[1, 8].Value = "NM_PREV Current";
                    wsToBeUpdated.Cells[1, 9].Value = "SUPPCD Current";
                    wsToBeUpdated.Cells[1, 10].Value = "LIC_AUTHCD Current";
                    wsToBeUpdated.Cells[1, 11].Value = "LIC_AUTH_PREVCD Current";
                    wsToBeUpdated.Cells[1, 12].Value = "LIC_AUTHCHANGECD Current";
                    wsToBeUpdated.Cells[1, 13].Value = "LIC_AUTHCHANGEDT Current";
                    wsToBeUpdated.Cells[1, 14].Value = "COMBPRODCD Current";
                    wsToBeUpdated.Cells[1, 15].Value = "FLAVOURCD Current";
                    wsToBeUpdated.Cells[1, 16].Value = "EMA Current";
                    wsToBeUpdated.Cells[1, 17].Value = "PARALLEL_IMPORT Current";
                    wsToBeUpdated.Cells[1, 18].Value = "AVAIL_RESTRICTCD Current";
                    wsToBeUpdated.Cells[1, 19].Value = "AMPS_Id Current";
                    wsToBeUpdated.Cells[1, 20].Value = "APID Previous";
                    wsToBeUpdated.Cells[1, 21].Value = "INVALID Previous";
                    wsToBeUpdated.Cells[1, 22].Value = "VPID Previous";
                    wsToBeUpdated.Cells[1, 23].Value = "NM Previous";
                    wsToBeUpdated.Cells[1, 24].Value = "ABBREVNM Previous";
                    wsToBeUpdated.Cells[1, 25].Value = "DESC Previous";
                    wsToBeUpdated.Cells[1, 26].Value = "NMDT Previous";
                    wsToBeUpdated.Cells[1, 27].Value = "NM_PREV Previous";
                    wsToBeUpdated.Cells[1, 28].Value = "SUPPCD Previous";
                    wsToBeUpdated.Cells[1, 29].Value = "LIC_AUTHCD Previous";
                    wsToBeUpdated.Cells[1, 30].Value = "LIC_AUTH_PREVCD Previous";
                    wsToBeUpdated.Cells[1, 31].Value = "LIC_AUTHCHANGECD Previous";
                    wsToBeUpdated.Cells[1, 32].Value = "LIC_AUTHCHANGEDT Previous";
                    wsToBeUpdated.Cells[1, 33].Value = "COMBPRODCD Previous";
                    wsToBeUpdated.Cells[1, 34].Value = "FLAVOURCD Previous";
                    wsToBeUpdated.Cells[1, 35].Value = "EMA Previous";
                    wsToBeUpdated.Cells[1, 36].Value = "PARALLEL_IMPORT Previous";
                    wsToBeUpdated.Cells[1, 37].Value = "AVAIL_RESTRICTCD Previous";
                    wsToBeUpdated.Cells[1, 38].Value = "AMPS_Id Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmpHistory = customReportModel.DmdAmpHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();
                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdAmpHistory[currentcol].APIDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdAmpHistory[currentcol].INVALIDChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdAmpHistory[currentcol].VPIDChangeset;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdAmpHistory[currentcol].NMChangeset;
                        wsToBeUpdated.Cells[row, 5].Value = "" + DmdAmpHistory[currentcol].ABBREVNMChangeset;
                        wsToBeUpdated.Cells[row, 6].Value = "" + DmdAmpHistory[currentcol].DESCChangeset;
                        wsToBeUpdated.Cells[row, 7].Value = "" + DmdAmpHistory[currentcol].NMDTChangeset;
                        wsToBeUpdated.Cells[row, 8].Value = "" + DmdAmpHistory[currentcol].NM_PREVChangeset;
                        wsToBeUpdated.Cells[row, 9].Value = "" + DmdAmpHistory[currentcol].SUPPCDChangeset;
                        wsToBeUpdated.Cells[row, 10].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCDChangeset;
                        wsToBeUpdated.Cells[row, 11].Value = "" + DmdAmpHistory[currentcol].LIC_AUTH_PREVCDChangeset;
                        wsToBeUpdated.Cells[row, 12].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGECDChangeset;
                        wsToBeUpdated.Cells[row, 13].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGEDTChangeset;
                        wsToBeUpdated.Cells[row, 14].Value = "" + DmdAmpHistory[currentcol].COMBPRODCDChangeset;
                        wsToBeUpdated.Cells[row, 15].Value = "" + DmdAmpHistory[currentcol].FLAVOURCDChangeset;
                        wsToBeUpdated.Cells[row, 16].Value = "" + DmdAmpHistory[currentcol].EMAChangeset;
                        wsToBeUpdated.Cells[row, 17].Value = "" + DmdAmpHistory[currentcol].PARALLEL_IMPORTChangeset;
                        wsToBeUpdated.Cells[row, 18].Value = "" + DmdAmpHistory[currentcol].AVAIL_RESTRICTCDChangeset;
                        wsToBeUpdated.Cells[row, 19].Value = "" + DmdAmpHistory[currentcol].AMPS_IdChangeset;
                        wsToBeUpdated.Cells[row, 20].Value = "" + DmdAmpHistory[currentcol].APIDLive;
                        wsToBeUpdated.Cells[row, 21].Value = "" + DmdAmpHistory[currentcol].INVALIDLive;
                        wsToBeUpdated.Cells[row, 22].Value = "" + DmdAmpHistory[currentcol].VPIDLive;
                        wsToBeUpdated.Cells[row, 23].Value = "" + DmdAmpHistory[currentcol].NMLive;
                        wsToBeUpdated.Cells[row, 24].Value = "" + DmdAmpHistory[currentcol].ABBREVNMLive;
                        wsToBeUpdated.Cells[row, 25].Value = "" + DmdAmpHistory[currentcol].DESCLive;
                        wsToBeUpdated.Cells[row, 26].Value = "" + DmdAmpHistory[currentcol].NMDTLive;
                        wsToBeUpdated.Cells[row, 27].Value = "" + DmdAmpHistory[currentcol].NM_PREVLive;
                        wsToBeUpdated.Cells[row, 28].Value = "" + DmdAmpHistory[currentcol].SUPPCDLive;
                        wsToBeUpdated.Cells[row, 29].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCDLive;
                        wsToBeUpdated.Cells[row, 30].Value = "" + DmdAmpHistory[currentcol].LIC_AUTH_PREVCDLive;
                        wsToBeUpdated.Cells[row, 31].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGECDLive;
                        wsToBeUpdated.Cells[row, 32].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGEDTLive;
                        wsToBeUpdated.Cells[row, 33].Value = "" + DmdAmpHistory[currentcol].COMBPRODCDLive;
                        wsToBeUpdated.Cells[row, 34].Value = "" + DmdAmpHistory[currentcol].FLAVOURCDLive;
                        wsToBeUpdated.Cells[row, 35].Value = "" + DmdAmpHistory[currentcol].EMALive;
                        wsToBeUpdated.Cells[row, 36].Value = "" + DmdAmpHistory[currentcol].PARALLEL_IMPORTLive;
                        wsToBeUpdated.Cells[row, 37].Value = "" + DmdAmpHistory[currentcol].AVAIL_RESTRICTCDLive;
                        wsToBeUpdated.Cells[row, 38].Value = "" + DmdAmpHistory[currentcol].AMPS_IdLive;

                        currentcol++;
                    }


                    #endregion

                    #region ToBeDeleted
                    currentcol = default(int);
                    totalRows = customReportModel.DmdAmpHistory.Where(Amp => Amp.ActionType == "ToBeDeleted").ToList().Count;
                    wsToBeDeleted.Cells[1, 1].Value = "APID";
                    wsToBeDeleted.Cells[1, 2].Value = "INVALID";
                    wsToBeDeleted.Cells[1, 3].Value = "VPID";
                    wsToBeDeleted.Cells[1, 4].Value = "NM";
                    wsToBeDeleted.Cells[1, 5].Value = "ABBREVNM";
                    wsToBeDeleted.Cells[1, 6].Value = "DESC";
                    wsToBeDeleted.Cells[1, 7].Value = "NMDT";
                    wsToBeDeleted.Cells[1, 8].Value = "NM_PREV";
                    wsToBeDeleted.Cells[1, 9].Value = "SUPPCD";
                    wsToBeDeleted.Cells[1, 10].Value = "LIC_AUTHCD";
                    wsToBeDeleted.Cells[1, 11].Value = "LIC_AUTH_PREVCD";
                    wsToBeDeleted.Cells[1, 12].Value = "LIC_AUTHCHANGECD";
                    wsToBeDeleted.Cells[1, 13].Value = "LIC_AUTHCHANGEDT";
                    wsToBeDeleted.Cells[1, 14].Value = "COMBPRODCD";
                    wsToBeDeleted.Cells[1, 15].Value = "FLAVOURCD";
                    wsToBeDeleted.Cells[1, 16].Value = "EMA";
                    wsToBeDeleted.Cells[1, 17].Value = "PARALLEL_IMPORT";
                    wsToBeDeleted.Cells[1, 18].Value = "AVAIL_RESTRICTCD";
                    wsToBeDeleted.Cells[1, 19].Value = "AMPS_Id";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmpHistory = customReportModel.DmdAmpHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeDeleted.Cells[row, 1].Value = "" + DmdAmpHistory[currentcol].APIDLive;
                        wsToBeDeleted.Cells[row, 2].Value = "" + DmdAmpHistory[currentcol].INVALIDLive;
                        wsToBeDeleted.Cells[row, 3].Value = "" + DmdAmpHistory[currentcol].VPIDLive;
                        wsToBeDeleted.Cells[row, 4].Value = "" + DmdAmpHistory[currentcol].NMLive;
                        wsToBeDeleted.Cells[row, 5].Value = "" + DmdAmpHistory[currentcol].ABBREVNMLive;
                        wsToBeDeleted.Cells[row, 6].Value = "" + DmdAmpHistory[currentcol].DESCLive;
                        wsToBeDeleted.Cells[row, 7].Value = "" + DmdAmpHistory[currentcol].NMDTLive;
                        wsToBeDeleted.Cells[row, 8].Value = "" + DmdAmpHistory[currentcol].NM_PREVLive;
                        wsToBeDeleted.Cells[row, 9].Value = "" + DmdAmpHistory[currentcol].SUPPCDLive;
                        wsToBeDeleted.Cells[row, 10].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCDLive;
                        wsToBeDeleted.Cells[row, 11].Value = "" + DmdAmpHistory[currentcol].LIC_AUTH_PREVCDLive;
                        wsToBeDeleted.Cells[row, 12].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGECDLive;
                        wsToBeDeleted.Cells[row, 13].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGEDTLive;
                        wsToBeDeleted.Cells[row, 14].Value = "" + DmdAmpHistory[currentcol].COMBPRODCDLive;
                        wsToBeDeleted.Cells[row, 15].Value = "" + DmdAmpHistory[currentcol].FLAVOURCDLive;
                        wsToBeDeleted.Cells[row, 16].Value = "" + DmdAmpHistory[currentcol].EMALive;
                        wsToBeDeleted.Cells[row, 17].Value = "" + DmdAmpHistory[currentcol].PARALLEL_IMPORTLive;
                        wsToBeDeleted.Cells[row, 18].Value = "" + DmdAmpHistory[currentcol].AVAIL_RESTRICTCDLive;
                        wsToBeDeleted.Cells[row, 19].Value = "" + DmdAmpHistory[currentcol].AMPS_IdLive;


                        currentcol++;
                    }
                    #endregion
                }
                else
                {
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                    #region AllItems
                    currentcol = default(int);
                    totalRows = customReportModel.DmdAmpHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "APID Current";
                    wsAllItems.Cells[1, 2].Value = "INVALID Current";
                    wsAllItems.Cells[1, 3].Value = "VPID Current";
                    wsAllItems.Cells[1, 4].Value = "NM Current";
                    wsAllItems.Cells[1, 5].Value = "ABBREVNM Current";
                    wsAllItems.Cells[1, 6].Value = "DESC Current";
                    wsAllItems.Cells[1, 7].Value = "NMDT Current";
                    wsAllItems.Cells[1, 8].Value = "NM_PREV Current";
                    wsAllItems.Cells[1, 9].Value = "SUPPCD Current";
                    wsAllItems.Cells[1, 10].Value = "LIC_AUTHCD Current";
                    wsAllItems.Cells[1, 11].Value = "LIC_AUTH_PREVCD Current";
                    wsAllItems.Cells[1, 12].Value = "LIC_AUTHCHANGECD Current";
                    wsAllItems.Cells[1, 13].Value = "LIC_AUTHCHANGEDT Current";
                    wsAllItems.Cells[1, 14].Value = "COMBPRODCD Current";
                    wsAllItems.Cells[1, 15].Value = "FLAVOURCD Current";
                    wsAllItems.Cells[1, 16].Value = "EMA Current";
                    wsAllItems.Cells[1, 17].Value = "PARALLEL_IMPORT Current";
                    wsAllItems.Cells[1, 18].Value = "AVAIL_RESTRICTCD Current";
                    wsAllItems.Cells[1, 19].Value = "AMPS_Id Current";
                    wsAllItems.Cells[1, 20].Value = "APID Previous";
                    wsAllItems.Cells[1, 21].Value = "INVALID Previous";
                    wsAllItems.Cells[1, 22].Value = "VPID Previous";
                    wsAllItems.Cells[1, 23].Value = "NM Previous";
                    wsAllItems.Cells[1, 24].Value = "ABBREVNM Previous";
                    wsAllItems.Cells[1, 25].Value = "DESC Previous";
                    wsAllItems.Cells[1, 26].Value = "NMDT Previous";
                    wsAllItems.Cells[1, 27].Value = "NM_PREV Previous";
                    wsAllItems.Cells[1, 28].Value = "SUPPCD Previous";
                    wsAllItems.Cells[1, 29].Value = "LIC_AUTHCD Previous";
                    wsAllItems.Cells[1, 30].Value = "LIC_AUTH_PREVCD Previous";
                    wsAllItems.Cells[1, 31].Value = "LIC_AUTHCHANGECD Previous";
                    wsAllItems.Cells[1, 32].Value = "LIC_AUTHCHANGEDT Previous";
                    wsAllItems.Cells[1, 33].Value = "COMBPRODCD Previous";
                    wsAllItems.Cells[1, 34].Value = "FLAVOURCD Previous";
                    wsAllItems.Cells[1, 35].Value = "EMA Previous";
                    wsAllItems.Cells[1, 36].Value = "PARALLEL_IMPORT Previous";
                    wsAllItems.Cells[1, 37].Value = "AVAIL_RESTRICTCD Previous";
                    wsAllItems.Cells[1, 38].Value = "AMPS_Id Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmpHistory = customReportModel.DmdAmpHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdAmpHistory[currentcol].APIDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdAmpHistory[currentcol].INVALIDChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdAmpHistory[currentcol].VPIDChangeset;
                        wsAllItems.Cells[row, 4].Value = "" + DmdAmpHistory[currentcol].NMChangeset;
                        wsAllItems.Cells[row, 5].Value = "" + DmdAmpHistory[currentcol].ABBREVNMChangeset;
                        wsAllItems.Cells[row, 6].Value = "" + DmdAmpHistory[currentcol].DESCChangeset;
                        wsAllItems.Cells[row, 7].Value = "" + DmdAmpHistory[currentcol].NMDTChangeset;
                        wsAllItems.Cells[row, 8].Value = "" + DmdAmpHistory[currentcol].NM_PREVChangeset;
                        wsAllItems.Cells[row, 9].Value = "" + DmdAmpHistory[currentcol].SUPPCDChangeset;
                        wsAllItems.Cells[row, 10].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCDChangeset;
                        wsAllItems.Cells[row, 11].Value = "" + DmdAmpHistory[currentcol].LIC_AUTH_PREVCDChangeset;
                        wsAllItems.Cells[row, 12].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGECDChangeset;
                        wsAllItems.Cells[row, 13].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGEDTChangeset;
                        wsAllItems.Cells[row, 14].Value = "" + DmdAmpHistory[currentcol].COMBPRODCDChangeset;
                        wsAllItems.Cells[row, 15].Value = "" + DmdAmpHistory[currentcol].FLAVOURCDChangeset;
                        wsAllItems.Cells[row, 16].Value = "" + DmdAmpHistory[currentcol].EMAChangeset;
                        wsAllItems.Cells[row, 17].Value = "" + DmdAmpHistory[currentcol].PARALLEL_IMPORTChangeset;
                        wsAllItems.Cells[row, 18].Value = "" + DmdAmpHistory[currentcol].AVAIL_RESTRICTCDChangeset;
                        wsAllItems.Cells[row, 19].Value = "" + DmdAmpHistory[currentcol].AMPS_IdChangeset;
                        wsAllItems.Cells[row, 20].Value = "" + DmdAmpHistory[currentcol].APIDLive;
                        wsAllItems.Cells[row, 21].Value = "" + DmdAmpHistory[currentcol].INVALIDLive;
                        wsAllItems.Cells[row, 22].Value = "" + DmdAmpHistory[currentcol].VPIDLive;
                        wsAllItems.Cells[row, 23].Value = "" + DmdAmpHistory[currentcol].NMLive;
                        wsAllItems.Cells[row, 24].Value = "" + DmdAmpHistory[currentcol].ABBREVNMLive;
                        wsAllItems.Cells[row, 25].Value = "" + DmdAmpHistory[currentcol].DESCLive;
                        wsAllItems.Cells[row, 26].Value = "" + DmdAmpHistory[currentcol].NMDTLive;
                        wsAllItems.Cells[row, 27].Value = "" + DmdAmpHistory[currentcol].NM_PREVLive;
                        wsAllItems.Cells[row, 28].Value = "" + DmdAmpHistory[currentcol].SUPPCDLive;
                        wsAllItems.Cells[row, 29].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCDLive;
                        wsAllItems.Cells[row, 30].Value = "" + DmdAmpHistory[currentcol].LIC_AUTH_PREVCDLive;
                        wsAllItems.Cells[row, 31].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGECDLive;
                        wsAllItems.Cells[row, 32].Value = "" + DmdAmpHistory[currentcol].LIC_AUTHCHANGEDTLive;
                        wsAllItems.Cells[row, 33].Value = "" + DmdAmpHistory[currentcol].COMBPRODCDLive;
                        wsAllItems.Cells[row, 34].Value = "" + DmdAmpHistory[currentcol].FLAVOURCDLive;
                        wsAllItems.Cells[row, 35].Value = "" + DmdAmpHistory[currentcol].EMALive;
                        wsAllItems.Cells[row, 36].Value = "" + DmdAmpHistory[currentcol].PARALLEL_IMPORTLive;
                        wsAllItems.Cells[row, 37].Value = "" + DmdAmpHistory[currentcol].AVAIL_RESTRICTCDLive;
                        wsAllItems.Cells[row, 38].Value = "" + DmdAmpHistory[currentcol].AMPS_IdLive;

                        currentcol++;
                    }


                    #endregion
                }

                package.Save();

            }
        }
        #endregion

        #region ExportAMPPDetails
        private void ExportAMPPDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "AMPPDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_Ampp_History> DmdAmppHistory = new List<Dmd_Ampp_History>();

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdAmppHistory.Where(vmpp => vmpp.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "APPID";
                    wsToBeInserted.Cells[1, 2].Value = "INVALID";
                    wsToBeInserted.Cells[1, 3].Value = "NM";
                    wsToBeInserted.Cells[1, 4].Value = "ABBREVNM";
                    wsToBeInserted.Cells[1, 5].Value = "VPPID";
                    wsToBeInserted.Cells[1, 6].Value = "APID";
                    wsToBeInserted.Cells[1, 7].Value = "COMBPACKCD";
                    wsToBeInserted.Cells[1, 8].Value = "LEGAL_CATCD";
                    wsToBeInserted.Cells[1, 9].Value = "SUBP";
                    wsToBeInserted.Cells[1, 10].Value = "DISCCD";
                    wsToBeInserted.Cells[1, 11].Value = "DISCDT";
                    wsToBeInserted.Cells[1, 12].Value = "AMPPS_Id";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmppHistory = customReportModel.DmdAmppHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdAmppHistory[currentcol].APPIDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdAmppHistory[currentcol].INVALIDChangeset;
                        wsToBeInserted.Cells[row, 3].Value = "" + DmdAmppHistory[currentcol].NMChangeset;
                        wsToBeInserted.Cells[row, 4].Value = "" + DmdAmppHistory[currentcol].ABBREVNMChangeset;
                        wsToBeInserted.Cells[row, 5].Value = "" + DmdAmppHistory[currentcol].VPPIDChangeset;
                        wsToBeInserted.Cells[row, 6].Value = "" + DmdAmppHistory[currentcol].APIDChangeset;
                        wsToBeInserted.Cells[row, 7].Value = "" + DmdAmppHistory[currentcol].COMBPACKCDChangeset;
                        wsToBeInserted.Cells[row, 8].Value = "" + DmdAmppHistory[currentcol].LEGAL_CATCDChangeset;
                        wsToBeInserted.Cells[row, 9].Value = "" + DmdAmppHistory[currentcol].SUBPChangeset;
                        wsToBeInserted.Cells[row, 10].Value = "" + DmdAmppHistory[currentcol].DISCCDChangeset;
                        wsToBeInserted.Cells[row, 11].Value = "" + DmdAmppHistory[currentcol].DISCDTChangeset;
                        wsToBeInserted.Cells[row, 12].Value = "" + DmdAmppHistory[currentcol].AMPPS_IdChangeset;

                        currentcol++;
                    }

                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdAmppHistory.Where(vmpp => vmpp.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "APPID Current";
                    wsToBeUpdated.Cells[1, 2].Value = "INVALID Current";
                    wsToBeUpdated.Cells[1, 3].Value = "NM Current";
                    wsToBeUpdated.Cells[1, 4].Value = "ABBREVNM Current";
                    wsToBeUpdated.Cells[1, 5].Value = "VPPID Current";
                    wsToBeUpdated.Cells[1, 6].Value = "APID Current";
                    wsToBeUpdated.Cells[1, 7].Value = "COMBPACKCD Current";
                    wsToBeUpdated.Cells[1, 8].Value = "LEGAL_CATCD Current";
                    wsToBeUpdated.Cells[1, 9].Value = "SUBP Current";
                    wsToBeUpdated.Cells[1, 10].Value = "DISCCD Current";
                    wsToBeUpdated.Cells[1, 11].Value = "DISCDT Current";
                    wsToBeUpdated.Cells[1, 12].Value = "AMPPS_Id Current";
                    wsToBeUpdated.Cells[1, 13].Value = "APPID Previous";
                    wsToBeUpdated.Cells[1, 14].Value = "INVALID Previous";
                    wsToBeUpdated.Cells[1, 15].Value = "NM Previous";
                    wsToBeUpdated.Cells[1, 16].Value = "ABBREVNM Previous";
                    wsToBeUpdated.Cells[1, 17].Value = "VPPID Previous";
                    wsToBeUpdated.Cells[1, 18].Value = "APID Previous";
                    wsToBeUpdated.Cells[1, 19].Value = "COMBPACKCD Previous";
                    wsToBeUpdated.Cells[1, 20].Value = "LEGAL_CATCD Previous";
                    wsToBeUpdated.Cells[1, 21].Value = "SUBP Previous";
                    wsToBeUpdated.Cells[1, 22].Value = "DISCCD Previous";
                    wsToBeUpdated.Cells[1, 23].Value = "DISCDT Previous";
                    wsToBeUpdated.Cells[1, 24].Value = "AMPPS_Id Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmppHistory = customReportModel.DmdAmppHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdAmppHistory[currentcol].APPIDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdAmppHistory[currentcol].INVALIDChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdAmppHistory[currentcol].NMChangeset;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdAmppHistory[currentcol].ABBREVNMChangeset;
                        wsToBeUpdated.Cells[row, 5].Value = "" + DmdAmppHistory[currentcol].VPPIDChangeset;
                        wsToBeUpdated.Cells[row, 6].Value = "" + DmdAmppHistory[currentcol].APIDChangeset;
                        wsToBeUpdated.Cells[row, 7].Value = "" + DmdAmppHistory[currentcol].COMBPACKCDChangeset;
                        wsToBeUpdated.Cells[row, 8].Value = "" + DmdAmppHistory[currentcol].LEGAL_CATCDChangeset;
                        wsToBeUpdated.Cells[row, 9].Value = "" + DmdAmppHistory[currentcol].SUBPChangeset;
                        wsToBeUpdated.Cells[row, 10].Value = "" + DmdAmppHistory[currentcol].DISCCDChangeset;
                        wsToBeUpdated.Cells[row, 11].Value = "" + DmdAmppHistory[currentcol].DISCDTChangeset;
                        wsToBeUpdated.Cells[row, 12].Value = "" + DmdAmppHistory[currentcol].AMPPS_IdChangeset;
                        wsToBeUpdated.Cells[row, 13].Value = "" + DmdAmppHistory[currentcol].APPIDLive;
                        wsToBeUpdated.Cells[row, 14].Value = "" + DmdAmppHistory[currentcol].INVALIDLive;
                        wsToBeUpdated.Cells[row, 15].Value = "" + DmdAmppHistory[currentcol].NMLive;
                        wsToBeUpdated.Cells[row, 16].Value = "" + DmdAmppHistory[currentcol].ABBREVNMLive;
                        wsToBeUpdated.Cells[row, 17].Value = "" + DmdAmppHistory[currentcol].VPPIDLive;
                        wsToBeUpdated.Cells[row, 18].Value = "" + DmdAmppHistory[currentcol].APIDLive;
                        wsToBeUpdated.Cells[row, 19].Value = "" + DmdAmppHistory[currentcol].COMBPACKCDLive;
                        wsToBeUpdated.Cells[row, 20].Value = "" + DmdAmppHistory[currentcol].LEGAL_CATCDLive;
                        wsToBeUpdated.Cells[row, 21].Value = "" + DmdAmppHistory[currentcol].SUBPLive;
                        wsToBeUpdated.Cells[row, 22].Value = "" + DmdAmppHistory[currentcol].DISCCDLive;
                        wsToBeUpdated.Cells[row, 23].Value = "" + DmdAmppHistory[currentcol].DISCDTLive;
                        wsToBeUpdated.Cells[row, 24].Value = "" + DmdAmppHistory[currentcol].AMPPS_IdLive;



                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted
                    currentcol = default(int);
                    totalRows = customReportModel.DmdAmppHistory.Where(vmpp => vmpp.ActionType == "ToBeDeleted").ToList().Count;

                    wsToBeDeleted.Cells[1, 1].Value = "APPID";
                    wsToBeDeleted.Cells[1, 2].Value = "INVALID";
                    wsToBeDeleted.Cells[1, 3].Value = "NM";
                    wsToBeDeleted.Cells[1, 4].Value = "ABBREVNM";
                    wsToBeDeleted.Cells[1, 5].Value = "VPPID";
                    wsToBeDeleted.Cells[1, 6].Value = "APID";
                    wsToBeDeleted.Cells[1, 7].Value = "COMBPACKCD";
                    wsToBeDeleted.Cells[1, 8].Value = "LEGAL_CATCD";
                    wsToBeDeleted.Cells[1, 9].Value = "SUBP";
                    wsToBeDeleted.Cells[1, 10].Value = "DISCCD";
                    wsToBeDeleted.Cells[1, 11].Value = "DISCDT";
                    wsToBeDeleted.Cells[1, 12].Value = "AMPPS_Id";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmppHistory = customReportModel.DmdAmppHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeDeleted.Cells[row, 1].Value = "" + DmdAmppHistory[currentcol].APPIDLive;
                        wsToBeDeleted.Cells[row, 2].Value = "" + DmdAmppHistory[currentcol].INVALIDLive;
                        wsToBeDeleted.Cells[row, 3].Value = "" + DmdAmppHistory[currentcol].NMLive;
                        wsToBeDeleted.Cells[row, 4].Value = "" + DmdAmppHistory[currentcol].ABBREVNMLive;
                        wsToBeDeleted.Cells[row, 5].Value = "" + DmdAmppHistory[currentcol].VPPIDLive;
                        wsToBeDeleted.Cells[row, 6].Value = "" + DmdAmppHistory[currentcol].APIDLive;
                        wsToBeDeleted.Cells[row, 7].Value = "" + DmdAmppHistory[currentcol].COMBPACKCDLive;
                        wsToBeDeleted.Cells[row, 8].Value = "" + DmdAmppHistory[currentcol].LEGAL_CATCDLive;
                        wsToBeDeleted.Cells[row, 9].Value = "" + DmdAmppHistory[currentcol].SUBPLive;
                        wsToBeDeleted.Cells[row, 10].Value = "" + DmdAmppHistory[currentcol].DISCCDLive;
                        wsToBeDeleted.Cells[row, 11].Value = "" + DmdAmppHistory[currentcol].DISCDTLive;
                        wsToBeDeleted.Cells[row, 12].Value = "" + DmdAmppHistory[currentcol].AMPPS_IdLive;

                        currentcol++;
                    }

                    #endregion
                }
                else
                {
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                    #region wsAllItems
                    currentcol = default(int);
                    totalRows = customReportModel.DmdAmppHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "APPID Current";
                    wsAllItems.Cells[1, 2].Value = "INVALID Current";
                    wsAllItems.Cells[1, 3].Value = "NM Current";
                    wsAllItems.Cells[1, 4].Value = "ABBREVNM Current";
                    wsAllItems.Cells[1, 5].Value = "VPPID Current";
                    wsAllItems.Cells[1, 6].Value = "APID Current";
                    wsAllItems.Cells[1, 7].Value = "COMBPACKCD Current";
                    wsAllItems.Cells[1, 8].Value = "LEGAL_CATCD Current";
                    wsAllItems.Cells[1, 9].Value = "SUBP Current";
                    wsAllItems.Cells[1, 10].Value = "DISCCD Current";
                    wsAllItems.Cells[1, 11].Value = "DISCDT Current";
                    wsAllItems.Cells[1, 12].Value = "AMPPS_Id Current";
                    wsAllItems.Cells[1, 13].Value = "APPID Previous";
                    wsAllItems.Cells[1, 14].Value = "INVALID Previous";
                    wsAllItems.Cells[1, 15].Value = "NM Previous";
                    wsAllItems.Cells[1, 16].Value = "ABBREVNM Previous";
                    wsAllItems.Cells[1, 17].Value = "VPPID Previous";
                    wsAllItems.Cells[1, 18].Value = "APID Previous";
                    wsAllItems.Cells[1, 19].Value = "COMBPACKCD Previous";
                    wsAllItems.Cells[1, 20].Value = "LEGAL_CATCD Previous";
                    wsAllItems.Cells[1, 21].Value = "SUBP Previous";
                    wsAllItems.Cells[1, 22].Value = "DISCCD Previous";
                    wsAllItems.Cells[1, 23].Value = "DISCDT Previous";
                    wsAllItems.Cells[1, 24].Value = "AMPPS_Id Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdAmppHistory = customReportModel.DmdAmppHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdAmppHistory[currentcol].APPIDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdAmppHistory[currentcol].INVALIDChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdAmppHistory[currentcol].NMChangeset;
                        wsAllItems.Cells[row, 4].Value = "" + DmdAmppHistory[currentcol].ABBREVNMChangeset;
                        wsAllItems.Cells[row, 5].Value = "" + DmdAmppHistory[currentcol].VPPIDChangeset;
                        wsAllItems.Cells[row, 6].Value = "" + DmdAmppHistory[currentcol].APIDChangeset;
                        wsAllItems.Cells[row, 7].Value = "" + DmdAmppHistory[currentcol].COMBPACKCDChangeset;
                        wsAllItems.Cells[row, 8].Value = "" + DmdAmppHistory[currentcol].LEGAL_CATCDChangeset;
                        wsAllItems.Cells[row, 9].Value = "" + DmdAmppHistory[currentcol].SUBPChangeset;
                        wsAllItems.Cells[row, 10].Value = "" + DmdAmppHistory[currentcol].DISCCDChangeset;
                        wsAllItems.Cells[row, 11].Value = "" + DmdAmppHistory[currentcol].DISCDTChangeset;
                        wsAllItems.Cells[row, 12].Value = "" + DmdAmppHistory[currentcol].AMPPS_IdChangeset;
                        wsAllItems.Cells[row, 13].Value = "" + DmdAmppHistory[currentcol].APPIDLive;
                        wsAllItems.Cells[row, 14].Value = "" + DmdAmppHistory[currentcol].INVALIDLive;
                        wsAllItems.Cells[row, 15].Value = "" + DmdAmppHistory[currentcol].NMLive;
                        wsAllItems.Cells[row, 16].Value = "" + DmdAmppHistory[currentcol].ABBREVNMLive;
                        wsAllItems.Cells[row, 17].Value = "" + DmdAmppHistory[currentcol].VPPIDLive;
                        wsAllItems.Cells[row, 18].Value = "" + DmdAmppHistory[currentcol].APIDLive;
                        wsAllItems.Cells[row, 19].Value = "" + DmdAmppHistory[currentcol].COMBPACKCDLive;
                        wsAllItems.Cells[row, 20].Value = "" + DmdAmppHistory[currentcol].LEGAL_CATCDLive;
                        wsAllItems.Cells[row, 21].Value = "" + DmdAmppHistory[currentcol].SUBPLive;
                        wsAllItems.Cells[row, 22].Value = "" + DmdAmppHistory[currentcol].DISCCDLive;
                        wsAllItems.Cells[row, 23].Value = "" + DmdAmppHistory[currentcol].DISCDTLive;
                        wsAllItems.Cells[row, 24].Value = "" + DmdAmppHistory[currentcol].AMPPS_IdLive;

                        currentcol++;
                    }

                    #endregion
                }


                package.Save();

            }
        }
        #endregion

        #region ExportVMPDetails
        private void ExportVMPDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "VMPDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_Vmp_History> DmdVmpHistory = new List<Dmd_Vmp_History>();

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdVmpHistory.Where(vmp => vmp.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "VPID";
                    wsToBeInserted.Cells[1, 2].Value = "VPIDDT";
                    wsToBeInserted.Cells[1, 3].Value = "VPIDPREV";
                    wsToBeInserted.Cells[1, 4].Value = "VTMID";
                    wsToBeInserted.Cells[1, 5].Value = "INVALID";
                    wsToBeInserted.Cells[1, 6].Value = "NM";
                    wsToBeInserted.Cells[1, 7].Value = "ABBREVNM";
                    wsToBeInserted.Cells[1, 8].Value = "BASISCD";
                    wsToBeInserted.Cells[1, 9].Value = "NMDT";
                    wsToBeInserted.Cells[1, 10].Value = "NMPREV";
                    wsToBeInserted.Cells[1, 11].Value = "BASIS_PREVCD";
                    wsToBeInserted.Cells[1, 12].Value = "NMCHANGECD";
                    wsToBeInserted.Cells[1, 13].Value = "COMBPRODCD";
                    wsToBeInserted.Cells[1, 14].Value = "PRES_STATCD";
                    wsToBeInserted.Cells[1, 15].Value = "SUG_F";
                    wsToBeInserted.Cells[1, 16].Value = "GLU_F";
                    wsToBeInserted.Cells[1, 17].Value = "PRES_F";
                    wsToBeInserted.Cells[1, 18].Value = "CFC_F";
                    wsToBeInserted.Cells[1, 19].Value = "NON_AVAILCD";
                    wsToBeInserted.Cells[1, 20].Value = "NON_AVAILDT";
                    wsToBeInserted.Cells[1, 21].Value = "DF_INDCD";
                    wsToBeInserted.Cells[1, 22].Value = "UDFS";
                    wsToBeInserted.Cells[1, 23].Value = "UDFS_UOMCD";
                    wsToBeInserted.Cells[1, 24].Value = "UNIT_DOSE_UOMCD";
                    wsToBeInserted.Cells[1, 25].Value = "VMPS_Id";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmpHistory = customReportModel.DmdVmpHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdVmpHistory[currentcol].VPIDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdVmpHistory[currentcol].VPIDDTChangeset;
                        wsToBeInserted.Cells[row, 3].Value = "" + DmdVmpHistory[currentcol].VPIDPREVChangeset;
                        wsToBeInserted.Cells[row, 4].Value = "" + DmdVmpHistory[currentcol].VTMIDChangeset;
                        wsToBeInserted.Cells[row, 5].Value = "" + DmdVmpHistory[currentcol].INVALIDChangeset;
                        wsToBeInserted.Cells[row, 6].Value = "" + DmdVmpHistory[currentcol].NMChangeset;
                        wsToBeInserted.Cells[row, 7].Value = "" + DmdVmpHistory[currentcol].ABBREVNMChangeset;
                        wsToBeInserted.Cells[row, 8].Value = "" + DmdVmpHistory[currentcol].BASISCDChangeset;
                        wsToBeInserted.Cells[row, 9].Value = "" + DmdVmpHistory[currentcol].NMDTChangeset;
                        wsToBeInserted.Cells[row, 10].Value = "" + DmdVmpHistory[currentcol].NMPREVChangeset;
                        wsToBeInserted.Cells[row, 11].Value = "" + DmdVmpHistory[currentcol].BASISChangeset_PREVCD;
                        wsToBeInserted.Cells[row, 12].Value = "" + DmdVmpHistory[currentcol].NMCHANGECDChangeset;
                        wsToBeInserted.Cells[row, 13].Value = "" + DmdVmpHistory[currentcol].COMBPRODCDChangeset;
                        wsToBeInserted.Cells[row, 14].Value = "" + DmdVmpHistory[currentcol].PRES_STATCDChangeset;
                        wsToBeInserted.Cells[row, 15].Value = "" + DmdVmpHistory[currentcol].SUG_FChangeset;
                        wsToBeInserted.Cells[row, 16].Value = "" + DmdVmpHistory[currentcol].GLU_FChangeset;
                        wsToBeInserted.Cells[row, 17].Value = "" + DmdVmpHistory[currentcol].PRES_FChangeset;
                        wsToBeInserted.Cells[row, 18].Value = "" + DmdVmpHistory[currentcol].CFC_FChangeset;
                        wsToBeInserted.Cells[row, 19].Value = "" + DmdVmpHistory[currentcol].NON_AVAILCDChangeset;
                        wsToBeInserted.Cells[row, 20].Value = "" + DmdVmpHistory[currentcol].NON_AVAILDTChangeset;
                        wsToBeInserted.Cells[row, 21].Value = "" + DmdVmpHistory[currentcol].DF_INDCDChangeset;
                        wsToBeInserted.Cells[row, 22].Value = "" + DmdVmpHistory[currentcol].UDFSChangeset;
                        wsToBeInserted.Cells[row, 23].Value = "" + DmdVmpHistory[currentcol].UDFS_UOMCDChangeset;
                        wsToBeInserted.Cells[row, 24].Value = "" + DmdVmpHistory[currentcol].UNIT_DOSE_UOMCDChangeset;
                        wsToBeInserted.Cells[row, 25].Value = "" + DmdVmpHistory[currentcol].VMPS_IdChangeset;

                        currentcol++;
                    }

                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVmpHistory.Where(vmpp => vmpp.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "VPID Current";
                    wsToBeUpdated.Cells[1, 2].Value = "VPIDDT Current";
                    wsToBeUpdated.Cells[1, 3].Value = "VPIDPREV Current";
                    wsToBeUpdated.Cells[1, 4].Value = "VTMID Current";
                    wsToBeUpdated.Cells[1, 5].Value = "INVALID Current";
                    wsToBeUpdated.Cells[1, 6].Value = "NM Current";
                    wsToBeUpdated.Cells[1, 7].Value = "ABBREVNM Current";
                    wsToBeUpdated.Cells[1, 8].Value = "BASISCD Current";
                    wsToBeUpdated.Cells[1, 9].Value = "NMDT Current";
                    wsToBeUpdated.Cells[1, 10].Value = "NMPREV Current";
                    wsToBeUpdated.Cells[1, 11].Value = "BASIS_PREVCD Current";
                    wsToBeUpdated.Cells[1, 12].Value = "NMCHANGECD Current";
                    wsToBeUpdated.Cells[1, 13].Value = "COMBPRODCD Current";
                    wsToBeUpdated.Cells[1, 14].Value = "PRES_STATCD Current";
                    wsToBeUpdated.Cells[1, 15].Value = "SUG_F Current";
                    wsToBeUpdated.Cells[1, 16].Value = "GLU_F Current";
                    wsToBeUpdated.Cells[1, 17].Value = "PRES_F Current";
                    wsToBeUpdated.Cells[1, 18].Value = "CFC_F Current";
                    wsToBeUpdated.Cells[1, 19].Value = "NON_AVAILCD Current";
                    wsToBeUpdated.Cells[1, 20].Value = "NON_AVAILDT Current";
                    wsToBeUpdated.Cells[1, 21].Value = "DF_INDCD Current";
                    wsToBeUpdated.Cells[1, 22].Value = "UDFS Current";
                    wsToBeUpdated.Cells[1, 23].Value = "UDFS_UOMCD Current";
                    wsToBeUpdated.Cells[1, 24].Value = "UNIT_DOSE_UOMCD Current";
                    wsToBeUpdated.Cells[1, 25].Value = "VMPS_Id Current";
                    wsToBeUpdated.Cells[1, 26].Value = "VPID Previous";
                    wsToBeUpdated.Cells[1, 27].Value = "VPIDDT Previous";
                    wsToBeUpdated.Cells[1, 28].Value = "VPIDPREV Previous";
                    wsToBeUpdated.Cells[1, 29].Value = "VTMID Previous";
                    wsToBeUpdated.Cells[1, 30].Value = "INVALID Previous";
                    wsToBeUpdated.Cells[1, 31].Value = "NM Previous";
                    wsToBeUpdated.Cells[1, 32].Value = "ABBREVNM Previous";
                    wsToBeUpdated.Cells[1, 33].Value = "BASISCD Previous";
                    wsToBeUpdated.Cells[1, 34].Value = "NMDT Previous";
                    wsToBeUpdated.Cells[1, 35].Value = "NMPREV Previous";
                    wsToBeUpdated.Cells[1, 36].Value = "BASIS Previous_PREVCD";
                    wsToBeUpdated.Cells[1, 37].Value = "NMCHANGECD Previous";
                    wsToBeUpdated.Cells[1, 38].Value = "COMBPRODCD Previous";
                    wsToBeUpdated.Cells[1, 39].Value = "PRES_STATCD Previous";
                    wsToBeUpdated.Cells[1, 40].Value = "SUG_F Previous";
                    wsToBeUpdated.Cells[1, 41].Value = "GLU_F Previous";
                    wsToBeUpdated.Cells[1, 42].Value = "PRES_F Previous";
                    wsToBeUpdated.Cells[1, 43].Value = "CFC_F Previous";
                    wsToBeUpdated.Cells[1, 44].Value = "NON_AVAILCD Previous";
                    wsToBeUpdated.Cells[1, 45].Value = "NON_AVAILDT Previous";
                    wsToBeUpdated.Cells[1, 46].Value = "DF_INDCD Previous";
                    wsToBeUpdated.Cells[1, 47].Value = "UDFS Previous";
                    wsToBeUpdated.Cells[1, 48].Value = "UDFS_UOMCD Previous";
                    wsToBeUpdated.Cells[1, 49].Value = "UNIT_DOSE_UOMCD Previous";
                    wsToBeUpdated.Cells[1, 50].Value = "VMPS_Id Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmpHistory = customReportModel.DmdVmpHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdVmpHistory[currentcol].VPIDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdVmpHistory[currentcol].VPIDDTChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdVmpHistory[currentcol].VPIDPREVChangeset;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdVmpHistory[currentcol].VTMIDChangeset;
                        wsToBeUpdated.Cells[row, 5].Value = "" + DmdVmpHistory[currentcol].INVALIDChangeset;
                        wsToBeUpdated.Cells[row, 6].Value = "" + DmdVmpHistory[currentcol].NMChangeset;
                        wsToBeUpdated.Cells[row, 7].Value = "" + DmdVmpHistory[currentcol].ABBREVNMChangeset;
                        wsToBeUpdated.Cells[row, 8].Value = "" + DmdVmpHistory[currentcol].BASISCDChangeset;
                        wsToBeUpdated.Cells[row, 9].Value = "" + DmdVmpHistory[currentcol].NMDTChangeset;
                        wsToBeUpdated.Cells[row, 10].Value = "" + DmdVmpHistory[currentcol].NMPREVChangeset;
                        wsToBeUpdated.Cells[row, 11].Value = "" + DmdVmpHistory[currentcol].BASISChangeset_PREVCD;
                        wsToBeUpdated.Cells[row, 12].Value = "" + DmdVmpHistory[currentcol].NMCHANGECDChangeset;
                        wsToBeUpdated.Cells[row, 13].Value = "" + DmdVmpHistory[currentcol].COMBPRODCDChangeset;
                        wsToBeUpdated.Cells[row, 14].Value = "" + DmdVmpHistory[currentcol].PRES_STATCDChangeset;
                        wsToBeUpdated.Cells[row, 15].Value = "" + DmdVmpHistory[currentcol].SUG_FChangeset;
                        wsToBeUpdated.Cells[row, 16].Value = "" + DmdVmpHistory[currentcol].GLU_FChangeset;
                        wsToBeUpdated.Cells[row, 17].Value = "" + DmdVmpHistory[currentcol].PRES_FChangeset;
                        wsToBeUpdated.Cells[row, 18].Value = "" + DmdVmpHistory[currentcol].CFC_FChangeset;
                        wsToBeUpdated.Cells[row, 19].Value = "" + DmdVmpHistory[currentcol].NON_AVAILCDChangeset;
                        wsToBeUpdated.Cells[row, 20].Value = "" + DmdVmpHistory[currentcol].NON_AVAILDTChangeset;
                        wsToBeUpdated.Cells[row, 21].Value = "" + DmdVmpHistory[currentcol].DF_INDCDChangeset;
                        wsToBeUpdated.Cells[row, 22].Value = "" + DmdVmpHistory[currentcol].UDFSChangeset;
                        wsToBeUpdated.Cells[row, 23].Value = "" + DmdVmpHistory[currentcol].UDFS_UOMCDChangeset;
                        wsToBeUpdated.Cells[row, 24].Value = "" + DmdVmpHistory[currentcol].UNIT_DOSE_UOMCDChangeset;
                        wsToBeUpdated.Cells[row, 25].Value = "" + DmdVmpHistory[currentcol].VMPS_IdChangeset;
                        wsToBeUpdated.Cells[row, 26].Value = "" + DmdVmpHistory[currentcol].VPIDLive;
                        wsToBeUpdated.Cells[row, 27].Value = "" + DmdVmpHistory[currentcol].VPIDDTLive;
                        wsToBeUpdated.Cells[row, 28].Value = "" + DmdVmpHistory[currentcol].VPIDPREVLive;
                        wsToBeUpdated.Cells[row, 29].Value = "" + DmdVmpHistory[currentcol].VTMIDLive;
                        wsToBeUpdated.Cells[row, 30].Value = "" + DmdVmpHistory[currentcol].INVALIDLive;
                        wsToBeUpdated.Cells[row, 31].Value = "" + DmdVmpHistory[currentcol].NMLive;
                        wsToBeUpdated.Cells[row, 32].Value = "" + DmdVmpHistory[currentcol].ABBREVNMLive;
                        wsToBeUpdated.Cells[row, 33].Value = "" + DmdVmpHistory[currentcol].BASISCDLive;
                        wsToBeUpdated.Cells[row, 34].Value = "" + DmdVmpHistory[currentcol].NMDTLive;
                        wsToBeUpdated.Cells[row, 35].Value = "" + DmdVmpHistory[currentcol].NMPREVLive;
                        wsToBeUpdated.Cells[row, 36].Value = "" + DmdVmpHistory[currentcol].BASIS_PREVCDLive;
                        wsToBeUpdated.Cells[row, 37].Value = "" + DmdVmpHistory[currentcol].NMCHANGECDLive;
                        wsToBeUpdated.Cells[row, 38].Value = "" + DmdVmpHistory[currentcol].COMBPRODCDLive;
                        wsToBeUpdated.Cells[row, 39].Value = "" + DmdVmpHistory[currentcol].PRES_STATCDLive;
                        wsToBeUpdated.Cells[row, 40].Value = "" + DmdVmpHistory[currentcol].SUG_FLive;
                        wsToBeUpdated.Cells[row, 41].Value = "" + DmdVmpHistory[currentcol].GLU_FLive;
                        wsToBeUpdated.Cells[row, 42].Value = "" + DmdVmpHistory[currentcol].PRES_FLive;
                        wsToBeUpdated.Cells[row, 43].Value = "" + DmdVmpHistory[currentcol].CFC_FLive;
                        wsToBeUpdated.Cells[row, 44].Value = "" + DmdVmpHistory[currentcol].NON_AVAILCDLive;
                        wsToBeUpdated.Cells[row, 45].Value = "" + DmdVmpHistory[currentcol].NON_AVAILDTLive;
                        wsToBeUpdated.Cells[row, 46].Value = "" + DmdVmpHistory[currentcol].DF_INDCDLive;
                        wsToBeUpdated.Cells[row, 47].Value = "" + DmdVmpHistory[currentcol].UDFSLive;
                        wsToBeUpdated.Cells[row, 48].Value = "" + DmdVmpHistory[currentcol].UDFS_UOMCDLive;
                        wsToBeUpdated.Cells[row, 49].Value = "" + DmdVmpHistory[currentcol].UNIT_DOSE_UOMCDLive;
                        wsToBeUpdated.Cells[row, 50].Value = "" + DmdVmpHistory[currentcol].VMPS_IdLive;


                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVmpHistory.Where(vmpp => vmpp.ActionType == "ToBeDeleted").ToList().Count;

                    wsToBeDeleted.Cells[1, 1].Value = "VPID";
                    wsToBeDeleted.Cells[1, 2].Value = "VPIDDT";
                    wsToBeDeleted.Cells[1, 3].Value = "VPIDPREV";
                    wsToBeDeleted.Cells[1, 4].Value = "VTMID";
                    wsToBeDeleted.Cells[1, 5].Value = "INVALID";
                    wsToBeDeleted.Cells[1, 6].Value = "NM";
                    wsToBeDeleted.Cells[1, 7].Value = "ABBREVNM";
                    wsToBeDeleted.Cells[1, 8].Value = "BASISCD";
                    wsToBeDeleted.Cells[1, 9].Value = "NMDT";
                    wsToBeDeleted.Cells[1, 10].Value = "NMPREV";
                    wsToBeDeleted.Cells[1, 11].Value = "BASIS_PREVCD";
                    wsToBeDeleted.Cells[1, 12].Value = "NMCHANGECD";
                    wsToBeDeleted.Cells[1, 13].Value = "COMBPRODCD";
                    wsToBeDeleted.Cells[1, 14].Value = "PRES_STATCD";
                    wsToBeDeleted.Cells[1, 15].Value = "SUG_F";
                    wsToBeDeleted.Cells[1, 16].Value = "GLU_F";
                    wsToBeDeleted.Cells[1, 17].Value = "PRES_F";
                    wsToBeDeleted.Cells[1, 18].Value = "CFC_F";
                    wsToBeDeleted.Cells[1, 19].Value = "NON_AVAILCD";
                    wsToBeDeleted.Cells[1, 20].Value = "NON_AVAILDT";
                    wsToBeDeleted.Cells[1, 21].Value = "DF_INDCD";
                    wsToBeDeleted.Cells[1, 22].Value = "UDFS";
                    wsToBeDeleted.Cells[1, 23].Value = "UDFS_UOMCD";
                    wsToBeDeleted.Cells[1, 24].Value = "UNIT_DOSE_UOMCD";
                    wsToBeDeleted.Cells[1, 25].Value = "VMPS_Id";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmpHistory = customReportModel.DmdVmpHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeDeleted.Cells[row, 1].Value = "" + DmdVmpHistory[currentcol].VPIDLive;
                        wsToBeDeleted.Cells[row, 2].Value = "" + DmdVmpHistory[currentcol].VPIDDTLive;
                        wsToBeDeleted.Cells[row, 3].Value = "" + DmdVmpHistory[currentcol].VPIDPREVLive;
                        wsToBeDeleted.Cells[row, 4].Value = "" + DmdVmpHistory[currentcol].VTMIDLive;
                        wsToBeDeleted.Cells[row, 5].Value = "" + DmdVmpHistory[currentcol].INVALIDLive;
                        wsToBeDeleted.Cells[row, 6].Value = "" + DmdVmpHistory[currentcol].NMLive;
                        wsToBeDeleted.Cells[row, 7].Value = "" + DmdVmpHistory[currentcol].ABBREVNMLive;
                        wsToBeDeleted.Cells[row, 8].Value = "" + DmdVmpHistory[currentcol].BASISCDLive;
                        wsToBeDeleted.Cells[row, 9].Value = "" + DmdVmpHistory[currentcol].NMDTLive;
                        wsToBeDeleted.Cells[row, 10].Value = "" + DmdVmpHistory[currentcol].NMPREVLive;
                        wsToBeDeleted.Cells[row, 11].Value = "" + DmdVmpHistory[currentcol].BASIS_PREVCDLive;
                        wsToBeDeleted.Cells[row, 12].Value = "" + DmdVmpHistory[currentcol].NMCHANGECDLive;
                        wsToBeDeleted.Cells[row, 13].Value = "" + DmdVmpHistory[currentcol].COMBPRODCDLive;
                        wsToBeDeleted.Cells[row, 14].Value = "" + DmdVmpHistory[currentcol].PRES_STATCDLive;
                        wsToBeDeleted.Cells[row, 15].Value = "" + DmdVmpHistory[currentcol].SUG_FLive;
                        wsToBeDeleted.Cells[row, 16].Value = "" + DmdVmpHistory[currentcol].GLU_FLive;
                        wsToBeDeleted.Cells[row, 17].Value = "" + DmdVmpHistory[currentcol].PRES_FLive;
                        wsToBeDeleted.Cells[row, 18].Value = "" + DmdVmpHistory[currentcol].CFC_FLive;
                        wsToBeDeleted.Cells[row, 19].Value = "" + DmdVmpHistory[currentcol].NON_AVAILCDLive;
                        wsToBeDeleted.Cells[row, 20].Value = "" + DmdVmpHistory[currentcol].NON_AVAILDTLive;
                        wsToBeDeleted.Cells[row, 21].Value = "" + DmdVmpHistory[currentcol].DF_INDCDLive;
                        wsToBeDeleted.Cells[row, 22].Value = "" + DmdVmpHistory[currentcol].UDFSLive;
                        wsToBeDeleted.Cells[row, 23].Value = "" + DmdVmpHistory[currentcol].UDFS_UOMCDLive;
                        wsToBeDeleted.Cells[row, 24].Value = "" + DmdVmpHistory[currentcol].UNIT_DOSE_UOMCDLive;
                        wsToBeDeleted.Cells[row, 25].Value = "" + DmdVmpHistory[currentcol].VMPS_IdLive;

                        currentcol++;
                    }

                    #endregion
                }
                else
                {
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");

                    #region AllItems
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVmpHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "VPID Current";
                    wsAllItems.Cells[1, 2].Value = "VPIDDT Current";
                    wsAllItems.Cells[1, 3].Value = "VPIDPREV Current";
                    wsAllItems.Cells[1, 4].Value = "VTMID Current";
                    wsAllItems.Cells[1, 5].Value = "INVALID Current";
                    wsAllItems.Cells[1, 6].Value = "NM Current";
                    wsAllItems.Cells[1, 7].Value = "ABBREVNM Current";
                    wsAllItems.Cells[1, 8].Value = "BASISCD Current";
                    wsAllItems.Cells[1, 9].Value = "NMDT Current";
                    wsAllItems.Cells[1, 10].Value = "NMPREV Current";
                    wsAllItems.Cells[1, 11].Value = "BASIS_PREVCD Current";
                    wsAllItems.Cells[1, 12].Value = "NMCHANGECD Current";
                    wsAllItems.Cells[1, 13].Value = "COMBPRODCD Current";
                    wsAllItems.Cells[1, 14].Value = "PRES_STATCD Current";
                    wsAllItems.Cells[1, 15].Value = "SUG_F Current";
                    wsAllItems.Cells[1, 16].Value = "GLU_F Current";
                    wsAllItems.Cells[1, 17].Value = "PRES_F Current";
                    wsAllItems.Cells[1, 18].Value = "CFC_F Current";
                    wsAllItems.Cells[1, 19].Value = "NON_AVAILCD Current";
                    wsAllItems.Cells[1, 20].Value = "NON_AVAILDT Current";
                    wsAllItems.Cells[1, 21].Value = "DF_INDCD Current";
                    wsAllItems.Cells[1, 22].Value = "UDFS Current";
                    wsAllItems.Cells[1, 23].Value = "UDFS_UOMCD Current";
                    wsAllItems.Cells[1, 24].Value = "UNIT_DOSE_UOMCD Current";
                    wsAllItems.Cells[1, 25].Value = "VMPS_Id Current";
                    wsAllItems.Cells[1, 26].Value = "VPID Previous";
                    wsAllItems.Cells[1, 27].Value = "VPIDDT Previous";
                    wsAllItems.Cells[1, 28].Value = "VPIDPREV Previous";
                    wsAllItems.Cells[1, 29].Value = "VTMID Previous";
                    wsAllItems.Cells[1, 30].Value = "INVALID Previous";
                    wsAllItems.Cells[1, 31].Value = "NM Previous";
                    wsAllItems.Cells[1, 32].Value = "ABBREVNM Previous";
                    wsAllItems.Cells[1, 33].Value = "BASISCD Previous";
                    wsAllItems.Cells[1, 34].Value = "NMDT Previous";
                    wsAllItems.Cells[1, 35].Value = "NMPREV Previous";
                    wsAllItems.Cells[1, 36].Value = "BASIS Previous_PREVCD";
                    wsAllItems.Cells[1, 37].Value = "NMCHANGECD Previous";
                    wsAllItems.Cells[1, 38].Value = "COMBPRODCD Previous";
                    wsAllItems.Cells[1, 39].Value = "PRES_STATCD Previous";
                    wsAllItems.Cells[1, 40].Value = "SUG_F Previous";
                    wsAllItems.Cells[1, 41].Value = "GLU_F Previous";
                    wsAllItems.Cells[1, 42].Value = "PRES_F Previous";
                    wsAllItems.Cells[1, 43].Value = "CFC_F Previous";
                    wsAllItems.Cells[1, 44].Value = "NON_AVAILCD Previous";
                    wsAllItems.Cells[1, 45].Value = "NON_AVAILDT Previous";
                    wsAllItems.Cells[1, 46].Value = "DF_INDCD Previous";
                    wsAllItems.Cells[1, 47].Value = "UDFS Previous";
                    wsAllItems.Cells[1, 48].Value = "UDFS_UOMCD Previous";
                    wsAllItems.Cells[1, 49].Value = "UNIT_DOSE_UOMCD Previous";
                    wsAllItems.Cells[1, 50].Value = "VMPS_Id Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmpHistory = customReportModel.DmdVmpHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdVmpHistory[currentcol].VPIDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdVmpHistory[currentcol].VPIDDTChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdVmpHistory[currentcol].VPIDPREVChangeset;
                        wsAllItems.Cells[row, 4].Value = "" + DmdVmpHistory[currentcol].VTMIDChangeset;
                        wsAllItems.Cells[row, 5].Value = "" + DmdVmpHistory[currentcol].INVALIDChangeset;
                        wsAllItems.Cells[row, 6].Value = "" + DmdVmpHistory[currentcol].NMChangeset;
                        wsAllItems.Cells[row, 7].Value = "" + DmdVmpHistory[currentcol].ABBREVNMChangeset;
                        wsAllItems.Cells[row, 8].Value = "" + DmdVmpHistory[currentcol].BASISCDChangeset;
                        wsAllItems.Cells[row, 9].Value = "" + DmdVmpHistory[currentcol].NMDTChangeset;
                        wsAllItems.Cells[row, 10].Value = "" + DmdVmpHistory[currentcol].NMPREVChangeset;
                        wsAllItems.Cells[row, 11].Value = "" + DmdVmpHistory[currentcol].BASISChangeset_PREVCD;
                        wsAllItems.Cells[row, 12].Value = "" + DmdVmpHistory[currentcol].NMCHANGECDChangeset;
                        wsAllItems.Cells[row, 13].Value = "" + DmdVmpHistory[currentcol].COMBPRODCDChangeset;
                        wsAllItems.Cells[row, 14].Value = "" + DmdVmpHistory[currentcol].PRES_STATCDChangeset;
                        wsAllItems.Cells[row, 15].Value = "" + DmdVmpHistory[currentcol].SUG_FChangeset;
                        wsAllItems.Cells[row, 16].Value = "" + DmdVmpHistory[currentcol].GLU_FChangeset;
                        wsAllItems.Cells[row, 17].Value = "" + DmdVmpHistory[currentcol].PRES_FChangeset;
                        wsAllItems.Cells[row, 18].Value = "" + DmdVmpHistory[currentcol].CFC_FChangeset;
                        wsAllItems.Cells[row, 19].Value = "" + DmdVmpHistory[currentcol].NON_AVAILCDChangeset;
                        wsAllItems.Cells[row, 20].Value = "" + DmdVmpHistory[currentcol].NON_AVAILDTChangeset;
                        wsAllItems.Cells[row, 21].Value = "" + DmdVmpHistory[currentcol].DF_INDCDChangeset;
                        wsAllItems.Cells[row, 22].Value = "" + DmdVmpHistory[currentcol].UDFSChangeset;
                        wsAllItems.Cells[row, 23].Value = "" + DmdVmpHistory[currentcol].UDFS_UOMCDChangeset;
                        wsAllItems.Cells[row, 24].Value = "" + DmdVmpHistory[currentcol].UNIT_DOSE_UOMCDChangeset;
                        wsAllItems.Cells[row, 25].Value = "" + DmdVmpHistory[currentcol].VMPS_IdChangeset;
                        wsAllItems.Cells[row, 26].Value = "" + DmdVmpHistory[currentcol].VPIDLive;
                        wsAllItems.Cells[row, 27].Value = "" + DmdVmpHistory[currentcol].VPIDDTLive;
                        wsAllItems.Cells[row, 28].Value = "" + DmdVmpHistory[currentcol].VPIDPREVLive;
                        wsAllItems.Cells[row, 29].Value = "" + DmdVmpHistory[currentcol].VTMIDLive;
                        wsAllItems.Cells[row, 30].Value = "" + DmdVmpHistory[currentcol].INVALIDLive;
                        wsAllItems.Cells[row, 31].Value = "" + DmdVmpHistory[currentcol].NMLive;
                        wsAllItems.Cells[row, 32].Value = "" + DmdVmpHistory[currentcol].ABBREVNMLive;
                        wsAllItems.Cells[row, 33].Value = "" + DmdVmpHistory[currentcol].BASISCDLive;
                        wsAllItems.Cells[row, 34].Value = "" + DmdVmpHistory[currentcol].NMDTLive;
                        wsAllItems.Cells[row, 35].Value = "" + DmdVmpHistory[currentcol].NMPREVLive;
                        wsAllItems.Cells[row, 36].Value = "" + DmdVmpHistory[currentcol].BASIS_PREVCDLive;
                        wsAllItems.Cells[row, 37].Value = "" + DmdVmpHistory[currentcol].NMCHANGECDLive;
                        wsAllItems.Cells[row, 38].Value = "" + DmdVmpHistory[currentcol].COMBPRODCDLive;
                        wsAllItems.Cells[row, 39].Value = "" + DmdVmpHistory[currentcol].PRES_STATCDLive;
                        wsAllItems.Cells[row, 40].Value = "" + DmdVmpHistory[currentcol].SUG_FLive;
                        wsAllItems.Cells[row, 41].Value = "" + DmdVmpHistory[currentcol].GLU_FLive;
                        wsAllItems.Cells[row, 42].Value = "" + DmdVmpHistory[currentcol].PRES_FLive;
                        wsAllItems.Cells[row, 43].Value = "" + DmdVmpHistory[currentcol].CFC_FLive;
                        wsAllItems.Cells[row, 44].Value = "" + DmdVmpHistory[currentcol].NON_AVAILCDLive;
                        wsAllItems.Cells[row, 45].Value = "" + DmdVmpHistory[currentcol].NON_AVAILDTLive;
                        wsAllItems.Cells[row, 46].Value = "" + DmdVmpHistory[currentcol].DF_INDCDLive;
                        wsAllItems.Cells[row, 47].Value = "" + DmdVmpHistory[currentcol].UDFSLive;
                        wsAllItems.Cells[row, 48].Value = "" + DmdVmpHistory[currentcol].UDFS_UOMCDLive;
                        wsAllItems.Cells[row, 49].Value = "" + DmdVmpHistory[currentcol].UNIT_DOSE_UOMCDLive;
                        wsAllItems.Cells[row, 50].Value = "" + DmdVmpHistory[currentcol].VMPS_IdLive;

                        currentcol++;
                    }

                    #endregion
                }

                package.Save();

            }
        }
        #endregion

        #region ExportVMPPDetails
        private void ExportVMPPDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "VMPPDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_Vmpp_History> DmdVmppHistory = new List<Dmd_Vmpp_History>();


                var finalSql = string.Empty;

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdVmppHistory.Where(vmp => vmp.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "VPPID";
                    wsToBeInserted.Cells[1, 2].Value = "INVALID";
                    wsToBeInserted.Cells[1, 3].Value = "NM";
                    wsToBeInserted.Cells[1, 4].Value = "ABBREVNM";
                    wsToBeInserted.Cells[1, 5].Value = "VPID";
                    wsToBeInserted.Cells[1, 6].Value = "QTYVAL";
                    wsToBeInserted.Cells[1, 7].Value = "QTY_UOMCD";
                    wsToBeInserted.Cells[1, 8].Value = "COMBPACKCD";
                    wsToBeInserted.Cells[1, 9].Value = "VMPPS_Id";




                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmppHistory = customReportModel.DmdVmppHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdVmppHistory[currentcol].VPPIDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdVmppHistory[currentcol].INVALIDChangeset;
                        wsToBeInserted.Cells[row, 3].Value = "" + DmdVmppHistory[currentcol].NMChangeset;
                        wsToBeInserted.Cells[row, 4].Value = "" + DmdVmppHistory[currentcol].ABBREVNMChangeset;
                        wsToBeInserted.Cells[row, 5].Value = "" + DmdVmppHistory[currentcol].VPIDChangeset;
                        wsToBeInserted.Cells[row, 6].Value = "" + DmdVmppHistory[currentcol].QTYVALChangeset;
                        wsToBeInserted.Cells[row, 7].Value = "" + DmdVmppHistory[currentcol].QTY_UOMCDChangeset;
                        wsToBeInserted.Cells[row, 8].Value = "" + DmdVmppHistory[currentcol].COMBPACKCDChangeset;
                        wsToBeInserted.Cells[row, 9].Value = "" + DmdVmppHistory[currentcol].VMPPS_IdChangeset;

                        var rowInsertSql = $"\nINSERT INTO [dbo].[Dmd_Vmpp](VPPID, INVALID, NM, ABBREVNM, VPID, QTYVAL, QTY_UOMCD, COMBPACKCD, VMPPS_Id)  VALUES" +
                                     $"(" +
                                     $"{DmdVmppHistory[currentcol].VPPIDChangeset}, {DmdVmppHistory[currentcol].INVALIDChangeset}, {DmdVmppHistory[currentcol].NMChangeset}, {DmdVmppHistory[currentcol].ABBREVNMChangeset}, {DmdVmppHistory[currentcol].VPIDChangeset}, { DmdVmppHistory[currentcol].QTYVALChangeset}, {DmdVmppHistory[currentcol].QTY_UOMCDChangeset}, {DmdVmppHistory[currentcol].COMBPACKCDChangeset}, {DmdVmppHistory[currentcol].VMPPS_IdChangeset}" +
                                     $")";
                        finalSql += rowInsertSql;

                        currentcol++;
                    }

                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVmppHistory.Where(vmpp => vmpp.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "VPPID Current";
                    wsToBeUpdated.Cells[1, 2].Value = "INVALID Current";
                    wsToBeUpdated.Cells[1, 3].Value = "NM Current";
                    wsToBeUpdated.Cells[1, 4].Value = "ABBREVNM Current";
                    wsToBeUpdated.Cells[1, 5].Value = "VPID Current";
                    wsToBeUpdated.Cells[1, 6].Value = "QTYVAL Current";
                    wsToBeUpdated.Cells[1, 7].Value = "QTY_UOMCD Current";
                    wsToBeUpdated.Cells[1, 8].Value = "COMBPACKCD Current";
                    wsToBeUpdated.Cells[1, 9].Value = "VMPPS_Id Current";
                    wsToBeUpdated.Cells[1, 10].Value = "VPPID Previous";
                    wsToBeUpdated.Cells[1, 11].Value = "INVALID Previous";
                    wsToBeUpdated.Cells[1, 12].Value = "NM Previous";
                    wsToBeUpdated.Cells[1, 13].Value = "ABBREVNM Previous";
                    wsToBeUpdated.Cells[1, 14].Value = "VPID Previous";
                    wsToBeUpdated.Cells[1, 15].Value = "QTYVAL Previous";
                    wsToBeUpdated.Cells[1, 16].Value = "QTY_UOMCD Previous";
                    wsToBeUpdated.Cells[1, 17].Value = "COMBPACKCD Previous";
                    wsToBeUpdated.Cells[1, 18].Value = "VMPPS_Id Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmppHistory = customReportModel.DmdVmppHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdVmppHistory[currentcol].VPPIDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdVmppHistory[currentcol].INVALIDChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdVmppHistory[currentcol].NMChangeset;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdVmppHistory[currentcol].ABBREVNMChangeset;
                        wsToBeUpdated.Cells[row, 5].Value = "" + DmdVmppHistory[currentcol].VPIDChangeset;
                        wsToBeUpdated.Cells[row, 6].Value = "" + DmdVmppHistory[currentcol].QTYVALChangeset;
                        wsToBeUpdated.Cells[row, 7].Value = "" + DmdVmppHistory[currentcol].QTY_UOMCDChangeset;
                        wsToBeUpdated.Cells[row, 8].Value = "" + DmdVmppHistory[currentcol].COMBPACKCDChangeset;
                        wsToBeUpdated.Cells[row, 9].Value = "" + DmdVmppHistory[currentcol].VMPPS_IdChangeset;
                        wsToBeUpdated.Cells[row, 10].Value = "" + DmdVmppHistory[currentcol].VPPIDLive;
                        wsToBeUpdated.Cells[row, 11].Value = "" + DmdVmppHistory[currentcol].INVALIDLive;
                        wsToBeUpdated.Cells[row, 12].Value = "" + DmdVmppHistory[currentcol].NMLive;
                        wsToBeUpdated.Cells[row, 13].Value = "" + DmdVmppHistory[currentcol].ABBREVNMLive;
                        wsToBeUpdated.Cells[row, 14].Value = "" + DmdVmppHistory[currentcol].VPIDLive;
                        wsToBeUpdated.Cells[row, 15].Value = "" + DmdVmppHistory[currentcol].QTYVALLive;
                        wsToBeUpdated.Cells[row, 16].Value = "" + DmdVmppHistory[currentcol].QTY_UOMCDLive;
                        wsToBeUpdated.Cells[row, 17].Value = "" + DmdVmppHistory[currentcol].COMBPACKCDLive;
                        wsToBeUpdated.Cells[row, 18].Value = "" + DmdVmppHistory[currentcol].VMPPS_IdLive;

                        var rowUpdateSql =
                            $"\nUPDATE [dbo].[Dmd_Vmpp] SET INVALID={wsToBeUpdated.Cells[row, 2].Value}, NM={wsToBeUpdated.Cells[row, 3].Value}, ABBREVNM={wsToBeUpdated.Cells[row, 4].Value}, VPID={wsToBeUpdated.Cells[row, 5].Value}, QTYVAL={wsToBeUpdated.Cells[row, 6].Value}, QTY_UOMCD={wsToBeUpdated.Cells[row, 7].Value}, COMBPACKCD={wsToBeUpdated.Cells[row, 8].Value}, VMPPS_Id={wsToBeUpdated.Cells[row, 9].Value} WHERE VPPID={wsToBeUpdated.Cells[row, 1].Value}";
                        finalSql += rowUpdateSql;
                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVmppHistory.Where(vmpp => vmpp.ActionType == "ToBeDeleted").ToList().Count;

                    wsToBeDeleted.Cells[1, 1].Value = "VPPID";
                    wsToBeDeleted.Cells[1, 2].Value = "INVALID";
                    wsToBeDeleted.Cells[1, 3].Value = "NM";
                    wsToBeDeleted.Cells[1, 4].Value = "ABBREVNM";
                    wsToBeDeleted.Cells[1, 5].Value = "VPID";
                    wsToBeDeleted.Cells[1, 6].Value = "QTYVAL";
                    wsToBeDeleted.Cells[1, 7].Value = "QTY_UOMCD";
                    wsToBeDeleted.Cells[1, 8].Value = "COMBPACKCD";
                    wsToBeDeleted.Cells[1, 9].Value = "VMPPS_Id";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmppHistory = customReportModel.DmdVmppHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeDeleted.Cells[row, 1].Value = "" + DmdVmppHistory[currentcol].VPPIDLive;
                        wsToBeDeleted.Cells[row, 2].Value = "" + DmdVmppHistory[currentcol].INVALIDLive;
                        wsToBeDeleted.Cells[row, 3].Value = "" + DmdVmppHistory[currentcol].NMLive;
                        wsToBeDeleted.Cells[row, 4].Value = "" + DmdVmppHistory[currentcol].ABBREVNMLive;
                        wsToBeDeleted.Cells[row, 5].Value = "" + DmdVmppHistory[currentcol].VPIDLive;
                        wsToBeDeleted.Cells[row, 6].Value = "" + DmdVmppHistory[currentcol].QTYVALLive;
                        wsToBeDeleted.Cells[row, 7].Value = "" + DmdVmppHistory[currentcol].QTY_UOMCDLive;
                        wsToBeDeleted.Cells[row, 8].Value = "" + DmdVmppHistory[currentcol].COMBPACKCDLive;
                        wsToBeDeleted.Cells[row, 9].Value = "" + DmdVmppHistory[currentcol].VMPPS_IdLive;

                        currentcol++;
                    }

                    #endregion
                }
                else
                {

                    #region AllItems
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVmppHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "VPPID Current";
                    wsAllItems.Cells[1, 2].Value = "INVALID Current";
                    wsAllItems.Cells[1, 3].Value = "NM Current";
                    wsAllItems.Cells[1, 4].Value = "ABBREVNM Current";
                    wsAllItems.Cells[1, 5].Value = "VPID Current";
                    wsAllItems.Cells[1, 6].Value = "QTYVAL Current";
                    wsAllItems.Cells[1, 7].Value = "QTY_UOMCD Current";
                    wsAllItems.Cells[1, 8].Value = "COMBPACKCD Current";
                    wsAllItems.Cells[1, 9].Value = "VMPPS_Id Current";
                    wsAllItems.Cells[1, 10].Value = "VPPID Previous";
                    wsAllItems.Cells[1, 11].Value = "INVALID Previous";
                    wsAllItems.Cells[1, 12].Value = "NM Previous";
                    wsAllItems.Cells[1, 13].Value = "ABBREVNM Previous";
                    wsAllItems.Cells[1, 14].Value = "VPID Previous";
                    wsAllItems.Cells[1, 15].Value = "QTYVAL Previous";
                    wsAllItems.Cells[1, 16].Value = "QTY_UOMCD Previous";
                    wsAllItems.Cells[1, 17].Value = "COMBPACKCD Previous";
                    wsAllItems.Cells[1, 18].Value = "VMPPS_Id Previous";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVmppHistory = customReportModel.DmdVmppHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdVmppHistory[currentcol].VPPIDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdVmppHistory[currentcol].INVALIDChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdVmppHistory[currentcol].NMChangeset;
                        wsAllItems.Cells[row, 4].Value = "" + DmdVmppHistory[currentcol].ABBREVNMChangeset;
                        wsAllItems.Cells[row, 5].Value = "" + DmdVmppHistory[currentcol].VPIDChangeset;
                        wsAllItems.Cells[row, 6].Value = "" + DmdVmppHistory[currentcol].QTYVALChangeset;
                        wsAllItems.Cells[row, 7].Value = "" + DmdVmppHistory[currentcol].QTY_UOMCDChangeset;
                        wsAllItems.Cells[row, 8].Value = "" + DmdVmppHistory[currentcol].COMBPACKCDChangeset;
                        wsAllItems.Cells[row, 9].Value = "" + DmdVmppHistory[currentcol].VMPPS_IdChangeset;
                        wsAllItems.Cells[row, 10].Value = "" + DmdVmppHistory[currentcol].VPPIDLive;
                        wsAllItems.Cells[row, 11].Value = "" + DmdVmppHistory[currentcol].INVALIDLive;
                        wsAllItems.Cells[row, 12].Value = "" + DmdVmppHistory[currentcol].NMLive;
                        wsAllItems.Cells[row, 13].Value = "" + DmdVmppHistory[currentcol].ABBREVNMLive;
                        wsAllItems.Cells[row, 14].Value = "" + DmdVmppHistory[currentcol].VPIDLive;
                        wsAllItems.Cells[row, 15].Value = "" + DmdVmppHistory[currentcol].QTYVALLive;
                        wsAllItems.Cells[row, 16].Value = "" + DmdVmppHistory[currentcol].QTY_UOMCDLive;
                        wsAllItems.Cells[row, 17].Value = "" + DmdVmppHistory[currentcol].COMBPACKCDLive;
                        wsAllItems.Cells[row, 18].Value = "" + DmdVmppHistory[currentcol].VMPPS_IdLive;

                        currentcol++;
                    }

                    #endregion
                }


                package.Save();

            }
        }
        #endregion

        #region ExportVTMDetails
        private void ExportVTMDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);

            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "VTMDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);


            using (ExcelPackage package = new ExcelPackage(file))
            {


                List<Dmd_Vtm_History> DmdVtmHistory = new List<Dmd_Vtm_History>();

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdVtmHistory.Where(vtm => vtm.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "VTMID";
                    wsToBeInserted.Cells[1, 2].Value = "INVALID";
                    wsToBeInserted.Cells[1, 3].Value = "NM";
                    wsToBeInserted.Cells[1, 4].Value = "ABBREVNM";
                    wsToBeInserted.Cells[1, 5].Value = "VTMIDPREV";
                    wsToBeInserted.Cells[1, 6].Value = "VTMIDDT";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVtmHistory = customReportModel.DmdVtmHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdVtmHistory[currentcol].VTMIDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdVtmHistory[currentcol].INVALIDChangeset;
                        wsToBeInserted.Cells[row, 3].Value = "" + DmdVtmHistory[currentcol].NMChangeset;
                        wsToBeInserted.Cells[row, 4].Value = "" + DmdVtmHistory[currentcol].ABBREVNMChangeset;
                        wsToBeInserted.Cells[row, 5].Value = "" + DmdVtmHistory[currentcol].VTMIDPREVChangeset;
                        wsToBeInserted.Cells[row, 6].Value = "" + DmdVtmHistory[currentcol].VTMIDDTChangeset;

                        currentcol++;
                    }
                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVtmHistory.Where(vtm => vtm.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "VTMID Current";
                    wsToBeUpdated.Cells[1, 2].Value = "INVALID Current";
                    wsToBeUpdated.Cells[1, 3].Value = "NM Current";
                    wsToBeUpdated.Cells[1, 4].Value = "ABBREVNM Current";
                    wsToBeUpdated.Cells[1, 5].Value = "VTMIDPREV Current";
                    wsToBeUpdated.Cells[1, 6].Value = "VTMIDDT Current";
                    wsToBeUpdated.Cells[1, 7].Value = "VTMID Previous";
                    wsToBeUpdated.Cells[1, 8].Value = "INVALID Previous";
                    wsToBeUpdated.Cells[1, 9].Value = "NM Previous";
                    wsToBeUpdated.Cells[1, 10].Value = "ABBREVNM Previous";
                    wsToBeUpdated.Cells[1, 11].Value = "VTMIDPREV Previous";
                    wsToBeUpdated.Cells[1, 12].Value = "VTMIDDT Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVtmHistory = customReportModel.DmdVtmHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdVtmHistory[currentcol].VTMIDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdVtmHistory[currentcol].INVALIDChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdVtmHistory[currentcol].NMChangeset;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdVtmHistory[currentcol].ABBREVNMChangeset;
                        wsToBeUpdated.Cells[row, 5].Value = "" + DmdVtmHistory[currentcol].VTMIDPREVChangeset;
                        wsToBeUpdated.Cells[row, 6].Value = "" + DmdVtmHistory[currentcol].VTMIDDTChangeset;
                        wsToBeUpdated.Cells[row, 7].Value = "" + DmdVtmHistory[currentcol].VTMIDLive;
                        wsToBeUpdated.Cells[row, 8].Value = "" + DmdVtmHistory[currentcol].INVALIDLive;
                        wsToBeUpdated.Cells[row, 9].Value = "" + DmdVtmHistory[currentcol].NMLive;
                        wsToBeUpdated.Cells[row, 10].Value = "" + DmdVtmHistory[currentcol].ABBREVNMLive;
                        wsToBeUpdated.Cells[row, 11].Value = "" + DmdVtmHistory[currentcol].VTMIDPREVLive;
                        wsToBeUpdated.Cells[row, 12].Value = "" + DmdVtmHistory[currentcol].VTMIDDTLive;

                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted

                    totalRows = customReportModel.DmdVtmHistory.Where(vtm => vtm.ActionType == "ToBeDeleted").ToList().Count;
                    currentcol = default(int);
                    wsToBeDeleted.Cells[1, 1].Value = "VTMID";
                    wsToBeDeleted.Cells[1, 2].Value = "INVALID";
                    wsToBeDeleted.Cells[1, 3].Value = "NM";
                    wsToBeDeleted.Cells[1, 4].Value = "ABBREVNM";
                    wsToBeDeleted.Cells[1, 5].Value = "VTMIDPREV";
                    wsToBeDeleted.Cells[1, 6].Value = "VTMIDDT";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVtmHistory = customReportModel.DmdVtmHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeDeleted.Cells[row, 1].Value = "" + DmdVtmHistory[currentcol].VTMIDLive;
                        wsToBeDeleted.Cells[row, 2].Value = "" + DmdVtmHistory[currentcol].INVALIDLive;
                        wsToBeDeleted.Cells[row, 3].Value = "" + DmdVtmHistory[currentcol].NMLive;
                        wsToBeDeleted.Cells[row, 4].Value = "" + DmdVtmHistory[currentcol].ABBREVNMLive;
                        wsToBeDeleted.Cells[row, 5].Value = "" + DmdVtmHistory[currentcol].VTMIDPREVLive;
                        wsToBeDeleted.Cells[row, 6].Value = "" + DmdVtmHistory[currentcol].VTMIDDTLive;

                        currentcol++;
                    }
                    #endregion
                }
                else
                {
                    #region AllItems
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                    currentcol = default(int);
                    totalRows = customReportModel.DmdVtmHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "VTMID Current";
                    wsAllItems.Cells[1, 2].Value = "INVALID Current";
                    wsAllItems.Cells[1, 3].Value = "NM Current";
                    wsAllItems.Cells[1, 4].Value = "ABBREVNM Current";
                    wsAllItems.Cells[1, 5].Value = "VTMIDPREV Current";
                    wsAllItems.Cells[1, 6].Value = "VTMIDDT Current";
                    wsAllItems.Cells[1, 7].Value = "VTMID Previous";
                    wsAllItems.Cells[1, 8].Value = "INVALID Previous";
                    wsAllItems.Cells[1, 9].Value = "NM Previous";
                    wsAllItems.Cells[1, 10].Value = "ABBREVNM Previous";
                    wsAllItems.Cells[1, 11].Value = "VTMIDPREV Previous";
                    wsAllItems.Cells[1, 12].Value = "VTMIDDT Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdVtmHistory = customReportModel.DmdVtmHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdVtmHistory[currentcol].VTMIDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdVtmHistory[currentcol].INVALIDChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdVtmHistory[currentcol].NMChangeset;
                        wsAllItems.Cells[row, 4].Value = "" + DmdVtmHistory[currentcol].ABBREVNMChangeset;
                        wsAllItems.Cells[row, 5].Value = "" + DmdVtmHistory[currentcol].VTMIDPREVChangeset;
                        wsAllItems.Cells[row, 6].Value = "" + DmdVtmHistory[currentcol].VTMIDDTChangeset;
                        wsAllItems.Cells[row, 7].Value = "" + DmdVtmHistory[currentcol].VTMIDLive;
                        wsAllItems.Cells[row, 8].Value = "" + DmdVtmHistory[currentcol].INVALIDLive;
                        wsAllItems.Cells[row, 9].Value = "" + DmdVtmHistory[currentcol].NMLive;
                        wsAllItems.Cells[row, 10].Value = "" + DmdVtmHistory[currentcol].ABBREVNMLive;
                        wsAllItems.Cells[row, 11].Value = "" + DmdVtmHistory[currentcol].VTMIDPREVLive;
                        wsAllItems.Cells[row, 12].Value = "" + DmdVtmHistory[currentcol].VTMIDDTLive;

                        currentcol++;
                    }
                }
                #endregion


                package.Save();

            }
        }
        #endregion

        #region ExportRouteDetails
        private void ExportRouteDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "RouteDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_ROUTE_History> DmdROUTEHistory = new List<Dmd_ROUTE_History>();

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");


                    #region ToBeInserted
                    currentcol = default(int);
                    totalRows = customReportModel.DmdROUTEHistory.Where(vtm => vtm.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "CD";
                    wsToBeInserted.Cells[1, 2].Value = "DESC";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdROUTEHistory = customReportModel.DmdROUTEHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdROUTEHistory[currentcol].CDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdROUTEHistory[currentcol].DESCChangeset;

                        currentcol++;
                    }
                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdROUTEHistory.Where(vtm => vtm.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "CD Current";
                    wsToBeUpdated.Cells[1, 2].Value = "DESC Current";
                    wsToBeUpdated.Cells[1, 3].Value = "CD Previous";
                    wsToBeUpdated.Cells[1, 4].Value = "DESC Previous";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdROUTEHistory = customReportModel.DmdROUTEHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdROUTEHistory[currentcol].CDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdROUTEHistory[currentcol].DESCChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdROUTEHistory[currentcol].CDLive;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdROUTEHistory[currentcol].DESCLive;

                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted

                    totalRows = customReportModel.DmdROUTEHistory.Where(vtm => vtm.ActionType == "ToBeDeleted").ToList().Count;
                    currentcol = default(int);

                    wsToBeDeleted.Cells[1, 1].Value = "CD";
                    wsToBeDeleted.Cells[1, 2].Value = "DESC";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdROUTEHistory = customReportModel.DmdROUTEHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdROUTEHistory[currentcol].CDLive;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdROUTEHistory[currentcol].DESCLive;

                        currentcol++;
                    }
                    #endregion
                }
                else
                {
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                    #region AllItems
                    currentcol = default(int);
                    totalRows = customReportModel.DmdROUTEHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "CD Current";
                    wsAllItems.Cells[1, 2].Value = "DESC Current";
                    wsAllItems.Cells[1, 3].Value = "CD Previous";
                    wsAllItems.Cells[1, 4].Value = "DESC Previous";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdROUTEHistory = customReportModel.DmdROUTEHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdROUTEHistory[currentcol].CDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdROUTEHistory[currentcol].DESCChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdROUTEHistory[currentcol].CDLive;
                        wsAllItems.Cells[row, 4].Value = "" + DmdROUTEHistory[currentcol].DESCLive;

                        currentcol++;
                    }

                    #endregion
                }

                package.Save();

            }
        }
        #endregion

        #region ExportSupplierDetails
        private void ExportSupplierDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "SupplierDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_SUPPLIER_History> DmdSUPPLIERHistory = new List<Dmd_SUPPLIER_History>();

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdSUPPLIERHistory.Where(vtm => vtm.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "CD";
                    wsToBeInserted.Cells[1, 2].Value = "DESC";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdSUPPLIERHistory = customReportModel.DmdSUPPLIERHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdSUPPLIERHistory[currentcol].CDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdSUPPLIERHistory[currentcol].DESCChangeset;

                        currentcol++;
                    }
                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdSUPPLIERHistory.Where(vtm => vtm.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "CD Current";
                    wsToBeUpdated.Cells[1, 2].Value = "DESC Current";
                    wsToBeUpdated.Cells[1, 3].Value = "CD Previous";
                    wsToBeUpdated.Cells[1, 4].Value = "DESC Previous";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdSUPPLIERHistory = customReportModel.DmdSUPPLIERHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdSUPPLIERHistory[currentcol].CDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdSUPPLIERHistory[currentcol].DESCChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdSUPPLIERHistory[currentcol].CDLive;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdSUPPLIERHistory[currentcol].DESCLive;

                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted

                    totalRows = customReportModel.DmdSUPPLIERHistory.Where(vtm => vtm.ActionType == "ToBeDeleted").ToList().Count;
                    currentcol = default(int);

                    wsToBeDeleted.Cells[1, 1].Value = "CD";
                    wsToBeDeleted.Cells[1, 2].Value = "DESC";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdSUPPLIERHistory = customReportModel.DmdSUPPLIERHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdSUPPLIERHistory[currentcol].CDLive;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdSUPPLIERHistory[currentcol].DESCLive;

                        currentcol++;
                    }
                    #endregion
                }
                else
                {
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");

                    #region AllItems
                    currentcol = default(int);
                    totalRows = customReportModel.DmdSUPPLIERHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "CD Current";
                    wsAllItems.Cells[1, 2].Value = "DESC Current";
                    wsAllItems.Cells[1, 3].Value = "CD Previous";
                    wsAllItems.Cells[1, 4].Value = "DESC Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdSUPPLIERHistory = customReportModel.DmdSUPPLIERHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdSUPPLIERHistory[currentcol].CDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdSUPPLIERHistory[currentcol].DESCChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdSUPPLIERHistory[currentcol].CDLive;
                        wsAllItems.Cells[row, 4].Value = "" + DmdSUPPLIERHistory[currentcol].DESCLive;

                        currentcol++;
                    }

                    #endregion
                }

                package.Save();

            }
        }
        #endregion

        #region #ExportFormDetails
        private void ExportFormDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "FormDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_Form_History> DmdFormHistory = new List<Dmd_Form_History>();

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdFormHistory.Where(vtm => vtm.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "CD";
                    wsToBeInserted.Cells[1, 2].Value = "CDDT";
                    wsToBeInserted.Cells[1, 3].Value = "CDPREV";
                    wsToBeInserted.Cells[1, 4].Value = "DESC";



                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdFormHistory = customReportModel.DmdFormHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdFormHistory[currentcol].CDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdFormHistory[currentcol].CDDTChangeset;
                        wsToBeInserted.Cells[row, 3].Value = "" + DmdFormHistory[currentcol].CDPREVChangeset;
                        wsToBeInserted.Cells[row, 4].Value = "" + DmdFormHistory[currentcol].DESCChangeset;


                        currentcol++;
                    }
                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdFormHistory.Where(vtm => vtm.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "CD Current";
                    wsToBeUpdated.Cells[1, 2].Value = "CDDT Current";
                    wsToBeUpdated.Cells[1, 3].Value = "CDPREV Current";
                    wsToBeUpdated.Cells[1, 4].Value = "DESC Current";
                    wsToBeUpdated.Cells[1, 5].Value = "CD Previous";
                    wsToBeUpdated.Cells[1, 6].Value = "CDDT Previous";
                    wsToBeUpdated.Cells[1, 7].Value = "CDPREV Previous";
                    wsToBeUpdated.Cells[1, 8].Value = "DESC Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdFormHistory = customReportModel.DmdFormHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdFormHistory[currentcol].CDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdFormHistory[currentcol].CDDTChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdFormHistory[currentcol].CDPREVChangeset;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdFormHistory[currentcol].DESCChangeset;
                        wsToBeUpdated.Cells[row, 5].Value = "" + DmdFormHistory[currentcol].CDLive;
                        wsToBeUpdated.Cells[row, 6].Value = "" + DmdFormHistory[currentcol].CDDTLive;
                        wsToBeUpdated.Cells[row, 7].Value = "" + DmdFormHistory[currentcol].CDPREVLive;
                        wsToBeUpdated.Cells[row, 8].Value = "" + DmdFormHistory[currentcol].DESCLive;


                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted

                    totalRows = customReportModel.DmdFormHistory.Where(vtm => vtm.ActionType == "ToBeDeleted").ToList().Count;
                    currentcol = default(int);

                    wsToBeDeleted.Cells[1, 1].Value = "CD";
                    wsToBeDeleted.Cells[1, 2].Value = "CDDT";
                    wsToBeDeleted.Cells[1, 3].Value = "CDPREV";
                    wsToBeDeleted.Cells[1, 4].Value = "DESC";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdFormHistory = customReportModel.DmdFormHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeDeleted.Cells[row, 1].Value = "" + DmdFormHistory[currentcol].CDLive;
                        wsToBeDeleted.Cells[row, 2].Value = "" + DmdFormHistory[currentcol].CDDTLive;
                        wsToBeDeleted.Cells[row, 3].Value = "" + DmdFormHistory[currentcol].CDPREVLive;
                        wsToBeDeleted.Cells[row, 4].Value = "" + DmdFormHistory[currentcol].DESCLive;


                        currentcol++;
                    }
                    #endregion
                }
                else
                {
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                    #region AllItems
                    currentcol = default(int);
                    totalRows = customReportModel.DmdFormHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "CD Current";
                    wsAllItems.Cells[1, 2].Value = "CDDT Current";
                    wsAllItems.Cells[1, 3].Value = "CDPREV Current";
                    wsAllItems.Cells[1, 4].Value = "DESC Current";
                    wsAllItems.Cells[1, 5].Value = "CD Previous";
                    wsAllItems.Cells[1, 6].Value = "CDDT Previous";
                    wsAllItems.Cells[1, 7].Value = "CDPREV Previous";
                    wsAllItems.Cells[1, 8].Value = "DESC Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdFormHistory = customReportModel.DmdFormHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdFormHistory[currentcol].CDLive;
                        wsAllItems.Cells[row, 2].Value = "" + DmdFormHistory[currentcol].CDDTLive;
                        wsAllItems.Cells[row, 3].Value = "" + DmdFormHistory[currentcol].CDPREVLive;
                        wsAllItems.Cells[row, 4].Value = "" + DmdFormHistory[currentcol].DESCLive;
                        wsAllItems.Cells[row, 5].Value = "" + DmdFormHistory[currentcol].CDChangeset;
                        wsAllItems.Cells[row, 6].Value = "" + DmdFormHistory[currentcol].CDDTChangeset;
                        wsAllItems.Cells[row, 7].Value = "" + DmdFormHistory[currentcol].CDPREVChangeset;
                        wsAllItems.Cells[row, 8].Value = "" + DmdFormHistory[currentcol].DESCChangeset;


                        currentcol++;
                    }

                    #endregion
                }

                package.Save();

            }
        }
        #endregion

        private void ExportGtinDetails(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "GtinDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<Dmd_Gtin_History> DmdGtinHistory = new List<Dmd_Gtin_History>();

                if (tabID != "AllItems")
                {
                    ExcelWorksheet wsToBeInserted = package.Workbook.Worksheets.Add("ToBeInserted");
                    ExcelWorksheet wsToBeUpdated = package.Workbook.Worksheets.Add("ToBeUpdated");
                    ExcelWorksheet wsToBeDeleted = package.Workbook.Worksheets.Add("ToBeDeleted");

                    #region ToBeInserted

                    totalRows = customReportModel.DmdGtinHistory.Where(vtm => vtm.ActionType == "ToBeInserted").ToList().Count;

                    wsToBeInserted.Cells[1, 1].Value = "AMPPID";
                    wsToBeInserted.Cells[1, 2].Value = "GTINDATAGTIN";
                    wsToBeInserted.Cells[1, 3].Value = "GTINDATASTARTDT";


                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdGtinHistory = customReportModel.DmdGtinHistory.Where(x => x.ActionType == "ToBeInserted").ToList();

                        wsToBeInserted.Cells[row, 1].Value = "" + DmdGtinHistory[currentcol].AMPPIDChangeset;
                        wsToBeInserted.Cells[row, 2].Value = "" + DmdGtinHistory[currentcol].GTINDATAGTINChangeset;
                        wsToBeInserted.Cells[row, 3].Value = "" + DmdGtinHistory[currentcol].GTINDATASTARTDTChangeset;

                        currentcol++;
                    }
                    #endregion

                    #region ToBeUpdated
                    currentcol = default(int);
                    totalRows = customReportModel.DmdGtinHistory.Where(vtm => vtm.ActionType == "ToBeUpdated").ToList().Count;

                    wsToBeUpdated.Cells[1, 1].Value = "AMPPID Current";
                    wsToBeUpdated.Cells[1, 2].Value = "GTINDATAGTIN Current";
                    wsToBeUpdated.Cells[1, 3].Value = "GTINDATASTARTDT Current";
                    wsToBeUpdated.Cells[1, 4].Value = "AMPPID Previous";
                    wsToBeUpdated.Cells[1, 5].Value = "GTINDATAGTIN Previous";
                    wsToBeUpdated.Cells[1, 6].Value = "GTINDATASTARTDT Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdGtinHistory = customReportModel.DmdGtinHistory.Where(x => x.ActionType == "ToBeUpdated").ToList();

                        wsToBeUpdated.Cells[row, 1].Value = "" + DmdGtinHistory[currentcol].AMPPIDChangeset;
                        wsToBeUpdated.Cells[row, 2].Value = "" + DmdGtinHistory[currentcol].GTINDATAGTINChangeset;
                        wsToBeUpdated.Cells[row, 3].Value = "" + DmdGtinHistory[currentcol].GTINDATASTARTDTChangeset;
                        wsToBeUpdated.Cells[row, 4].Value = "" + DmdGtinHistory[currentcol].AMPPIDLive;
                        wsToBeUpdated.Cells[row, 5].Value = "" + DmdGtinHistory[currentcol].GTINDATAGTINLive;
                        wsToBeUpdated.Cells[row, 6].Value = "" + DmdGtinHistory[currentcol].GTINDATASTARTDTLive;

                        currentcol++;
                    }

                    #endregion

                    #region ToBeDeleted

                    totalRows = customReportModel.DmdGtinHistory.Where(vtm => vtm.ActionType == "ToBeDeleted").ToList().Count;
                    currentcol = default(int);

                    wsToBeDeleted.Cells[1, 1].Value = "AMPPID";
                    wsToBeDeleted.Cells[1, 2].Value = "GTINDATAGTIN";
                    wsToBeDeleted.Cells[1, 3].Value = "GTINDATASTARTDT";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdGtinHistory = customReportModel.DmdGtinHistory.Where(x => x.ActionType == "ToBeDeleted").ToList();

                        wsToBeDeleted.Cells[row, 1].Value = "" + DmdGtinHistory[currentcol].AMPPIDLive;
                        wsToBeDeleted.Cells[row, 2].Value = "" + DmdGtinHistory[currentcol].GTINDATAGTINLive;
                        wsToBeDeleted.Cells[row, 3].Value = "" + DmdGtinHistory[currentcol].GTINDATASTARTDTLive;

                        currentcol++;
                    }
                    #endregion
                }
                else
                {
                    ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                    #region AllItems
                    currentcol = default(int);
                    totalRows = customReportModel.DmdGtinHistory.ToList().Count;

                    wsAllItems.Cells[1, 1].Value = "AMPPID Current";
                    wsAllItems.Cells[1, 2].Value = "GTINDATAGTIN Current";
                    wsAllItems.Cells[1, 3].Value = "GTINDATASTARTDT Current";
                    wsAllItems.Cells[1, 4].Value = "AMPPID Previous";
                    wsAllItems.Cells[1, 5].Value = "GTINDATAGTIN Previous";
                    wsAllItems.Cells[1, 6].Value = "GTINDATASTARTDT Previous";

                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        DmdGtinHistory = customReportModel.DmdGtinHistory.ToList();

                        wsAllItems.Cells[row, 1].Value = "" + DmdGtinHistory[currentcol].AMPPIDChangeset;
                        wsAllItems.Cells[row, 2].Value = "" + DmdGtinHistory[currentcol].GTINDATAGTINChangeset;
                        wsAllItems.Cells[row, 3].Value = "" + DmdGtinHistory[currentcol].GTINDATASTARTDTChangeset;
                        wsAllItems.Cells[row, 4].Value = "" + DmdGtinHistory[currentcol].AMPPIDLive;
                        wsAllItems.Cells[row, 5].Value = "" + DmdGtinHistory[currentcol].GTINDATAGTINLive;
                        wsAllItems.Cells[row, 6].Value = "" + DmdGtinHistory[currentcol].GTINDATASTARTDTLive;

                        currentcol++;
                    }

                    #endregion
                }

                package.Save();

            }
        }

        private void ExportDMDCollatedData(CustomReportModel customReportModel, string fileName, string rootFolder, string PageId, string tabID)
        {
            int totalRows = default(int);
            int currentcol = default(int);
            fileName = fileName != "MyBusinessChangesetDetails.xlsx" ? fileName : "GtinDetails.xlsx";
            fileName = MyExtensions.AppendTimeStamp(fileName);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
            }
            getAllFilePathForMychangesetDetails.Add(file.FullName);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<DmdReferenceCombinedDataset> dmdReferenceCombinedDataset = new List<DmdReferenceCombinedDataset>();
                ExcelWorksheet wsAllItems = package.Workbook.Worksheets.Add("AllItems");
                #region AllItems
                currentcol = default(int);
                totalRows = customReportModel.DmdReferenceCombinedDataset.ToList().Count;

                wsAllItems.Cells[1, 1].Value = "ProductPackId";
                wsAllItems.Cells[1, 2].Value = "ProductPackName";
                wsAllItems.Cells[1, 3].Value = "ProductId";
                wsAllItems.Cells[1, 4].Value = "ProductName";
                wsAllItems.Cells[1, 5].Value = "VtmId";
                wsAllItems.Cells[1, 6].Value = "VtmName";
                wsAllItems.Cells[1, 7].Value = "VmpId";
                wsAllItems.Cells[1, 8].Value = "VmpName";
                wsAllItems.Cells[1, 9].Value = "VmppId";
                wsAllItems.Cells[1, 10].Value = "VmppName";

                for (int row = 2; row <= totalRows + 1; row++)
                {
                    dmdReferenceCombinedDataset = customReportModel.DmdReferenceCombinedDataset.ToList();

                    wsAllItems.Cells[row, 1].Value = "" + dmdReferenceCombinedDataset[currentcol].ProductPackId;
                    wsAllItems.Cells[row, 2].Value = "" + dmdReferenceCombinedDataset[currentcol].ProductPackName;
                    wsAllItems.Cells[row, 3].Value = "" + dmdReferenceCombinedDataset[currentcol].ProductId;
                    wsAllItems.Cells[row, 4].Value = "" + dmdReferenceCombinedDataset[currentcol].ProductName;
                    wsAllItems.Cells[row, 5].Value = "" + dmdReferenceCombinedDataset[currentcol].VtmId;
                    wsAllItems.Cells[row, 6].Value = "" + dmdReferenceCombinedDataset[currentcol].VtmName;
                    wsAllItems.Cells[row, 7].Value = "" + dmdReferenceCombinedDataset[currentcol].VmpId;
                    wsAllItems.Cells[row, 8].Value = "" + dmdReferenceCombinedDataset[currentcol].VmpName;
                    wsAllItems.Cells[row, 9].Value = "" + dmdReferenceCombinedDataset[currentcol].VmppId;
                    wsAllItems.Cells[row, 10].Value = "" + dmdReferenceCombinedDataset[currentcol].VmppName;

                    currentcol++;
                }

                #endregion


                package.Save();

            }
        }

        #endregion

        [HttpGet]
        [Route("SaveBusinessDetails")]
        public void SaveBusinessDetails(string CurrentUserName, string BusinessName)
        {
            CustomReportModel customReportModel = new CustomReportModel();
            Dmd_ChangeSetDetails dmdChangeSetDetails = _iDmdChangeSetDetailsRepository.GetAll().FirstOrDefault();
            string filePath = string.Empty;
            var getMyBusinessDetails = _iDmdBusinessChangeSetDetailsRepository.Get(CurrentUserName);
            if (getMyBusinessDetails == null)
            {
                Dmd_BusinessChangeSetDetails dmdBusinessChangeSetDetails = new Dmd_BusinessChangeSetDetails();
                dmdBusinessChangeSetDetails.ToDateChangesetId = Convert.ToInt32(dmdChangeSetDetails.DmdChangeSetDetailID);
                dmdBusinessChangeSetDetails.CreatedOn = System.DateTime.Now;
                dmdBusinessChangeSetDetails.BusinessEmail = CurrentUserName;
                dmdBusinessChangeSetDetails.IsActive = true;
                dmdBusinessChangeSetDetails.FromDateChangesetId = Convert.ToInt32(dmdChangeSetDetails.DmdChangeSetDetailID); ;
                dmdBusinessChangeSetDetails.FromDateChangeset = dmdChangeSetDetails.Description;
                dmdBusinessChangeSetDetails.ToDateChangeset = dmdChangeSetDetails.Description;
                var result = _iDmdBusinessChangeSetDetailsRepository.Add(dmdBusinessChangeSetDetails);

                if (result != 0)
                {
                    filePath = GenerateZipFiles(customReportModel, CurrentUserName);
                    EmailBody(string.Concat(filePath), CurrentUserName, BusinessName);
                }

            }
        }


        public string GenerateZipFiles(CustomReportModel customReportModel, string CurrentUserName)
        {
            string zipName = String.Format("MyChangeset_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            string fullFilePath = _FreshLoad + zipName;
            List<string> getAllFileName = Directory.GetFiles(_FreshLoad, "*.*", SearchOption.AllDirectories)
                 .Where(s => s.EndsWith(".xlsx")).ToList();


            string query = string.Empty;
            string fileName = "MyBusinessChangesetDetails.sql";
            fileName = MyExtensions.AppendTimeStamp(fileName);

            query = "exec GetSQLQueryForFreshLoad ";
            query = query + " '',";
            query = query + "'" + _FreshLoad + "'" + ",";
            query = query + "'" + CurrentUserName + "'" + ",";
            query = query + "'" + fileName + "'";
            PharmixCommon.Common common = new PharmixCommon.Common();
            ExecuteQuery(query, _ConnectionString);
            getAllFileName.Add(_FreshLoad + fileName);


            using (var memoryStream = new MemoryStream())
            {
                using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var filePath in getAllFileName)
                    {
                        FileInfo file = new FileInfo(filePath);
                        ziparchive.CreateEntryFromFile(file.FullName, file.Name);
                    }
                }
                using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
            FileInfo fileDetele = new FileInfo(_FreshLoad + fileName);
            return zipName;
        }

        public void EmailBody(string filePath, string CurrentUserName, string BusinessName)
        {

            TraceService("******************************** EmailBody is started  " + DateTime.Now + " ******************************");
            string messageBody = string.Empty;
            using (StreamReader reader = new StreamReader(_TemplatePath))
            {
                messageBody = reader.ReadToEnd();
            }
            StringBuilder str = new StringBuilder();
            //messageBody = string.Concat(_XMLFilePath, filePath);
            messageBody = messageBody.Replace("{EMAIL_NAME}", BusinessName);
            messageBody = messageBody.Replace("{EMAIL_DOWNLOAD_LINK}", string.Concat(_XMLFilePath, filePath));


            new PharmixCommon.Email().SendEmail(CurrentUserName, "New Registration", messageBody, filePath);

            TraceService("******************************** EmailBody is completed  " + DateTime.Now + " ******************************");
        }

        public void TEst()
        {
            //using (var db = new ApplicationContext())
            //{
            //    var param1 = new SqlParameter();
            //    param1.ParameterName = "@Value1";
            //    param1.SqlDbType = SqlDbType.Int;
            //    param1.SqlValue = val1;

            //    var param2 = new SqlParameter();
            //    param2.ParameterName = "@Value2";
            //    param2.SqlDbType = SqlDbType.NVarChar;

            //    param2.SqlValue = val2; var result = db.Dmd_Ampp_History.SqlQuery("SP_Name @Value1,@Value2", param1, param2).ToList();
            //    var test1 = db.Database.ExecuteSqlCommand("");
            //    SqlQuery<Dmd_Ampp_History>("storedProcedureName",params);
            //}
        }

        public DataSet GetDataSet(string sqlquery, string connectionString)
        {
            DataSet _dsResult = new DataSet();
            try
            {
                TraceService("connectionString " + connectionString);
                SqlConnection _connection = new SqlConnection(connectionString);
                SqlCommand _command = new SqlCommand(sqlquery, _connection);
                _command.CommandTimeout = Convert.ToInt32(TimeOut);
                SqlDataAdapter _Adapter = new SqlDataAdapter(_command);
                _Adapter.Fill(_dsResult);
                TraceService("done " + "done");
                TraceService("TimeOut " + TimeOut);
                TraceService("sqlquery " + sqlquery);
            }
            catch (Exception ex)
            {

                TraceService("Eror : " + ex.Message);
            }

            return _dsResult;

        }

        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        private void TraceService(string content, string path)
        {

            //set up a filestream
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

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

        #region Execute Query 

        public int ExecuteQuery(string _query, string connectionString)
        {
            SqlConnection _connection = null;
            try
            {
                TraceService("ExecuteQuery Started" + System.DateTime.Now.ToString());
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                TraceService(_query);
                TraceService(connectionString);
                SqlCommand _command = new SqlCommand(_query, _connection);
                _command.CommandTimeout = 0;
                int _count = _command.ExecuteNonQuery();
                _connection.Close();

                TraceService("ExecuteQuery Ended" + System.DateTime.Now.ToString());
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

        #endregion

        [HttpGet]
        [Route("ToSaveExportDataToCSVDetails")]
        public void ToSaveExportDataToCSVDetails(string reportType, string fileName, string rootFolder, int changetSetFrom, int changetSetTo, string tabID, string changetSetFromId, string changetSetToId, bool IsDownloadsqlQuery, string BusinessEmail)
        {
            try
            {
                ExportDataToCSVDetails exportDataToCSVDetails = new ExportDataToCSVDetails();
                exportDataToCSVDetails.ReportType = reportType;
                exportDataToCSVDetails.FileName = fileName;
                exportDataToCSVDetails.RootFolder = rootFolder;
                exportDataToCSVDetails.ChangetSetFrom = changetSetFrom;
                exportDataToCSVDetails.ChangetSetTo = changetSetTo;
                exportDataToCSVDetails.TabID = tabID;
                exportDataToCSVDetails.ChangetSetFromId = changetSetFromId;
                exportDataToCSVDetails.ChangetSetToId = changetSetToId;
                exportDataToCSVDetails.IsDownloadsqlQuery = IsDownloadsqlQuery;
                exportDataToCSVDetails.BusinessUser = BusinessEmail;
                exportDataToCSVDetails.CreatedOn = System.DateTime.Now;
                exportDataToCSVDetails.IsActive = true;
                _iExportDataToCSVDetailsRepository.Add(exportDataToCSVDetails);
            }
            catch (Exception ex)
            {

                TraceService("exception " + ex.Message);
            }

        }

        [HttpGet]
        [Route("ToGetSaveExportDataToCSVDetails")]
        public List<ExportDataToCSVDetails> ToGetSaveExportDataToCSVDetails()
        {
            TraceService("ToGetSaveExportDataToCSVDetails start");
            List<ExportDataToCSVDetails> result = new List<ExportDataToCSVDetails>();
            result = (from exportDataToCSVDetails in _context.ExportDataToCSVDetails
                      select exportDataToCSVDetails).Where(x => x.IsActive == true).OrderBy(x=>x.ExportDataToCSVDetailId).ToList();
           
            TraceService("ToGetSaveExportDataToCSVDetails end");
            return result;

        }

        [HttpGet]
        [Route("ToUpdateExportDataToCSVDetails")]
        public ExportDataToCSVDetails ToUpdateExportDataToCSVDetails(int ExportDataToCSVDetailId)
        {
            TraceService("ToUpdateExportDataToCSVDetails start");

            ExportDataToCSVDetails result = new ExportDataToCSVDetails();
            result = (from exportDataToCSVDetails in _context.ExportDataToCSVDetails
                      select exportDataToCSVDetails).Where(x => x.IsActive == true && x.ExportDataToCSVDetailId == ExportDataToCSVDetailId).OrderBy(x => x.ExportDataToCSVDetailId).FirstOrDefault();
            result.IsActive = false;
            _iExportDataToCSVDetailsRepository.Update(result);

            TraceService("ToUpdateExportDataToCSVDetails end");
            return result;
        }

    }
}


public static class MyExtensions
{
    public static string AppendTimeStamp(this string fileName)
    {
        return string.Concat(
            Path.GetFileNameWithoutExtension(fileName),
            DateTime.Now.ToString("yyyyMMddHHmmssfff"),
            Path.GetExtension(fileName)
            );
    }
    public static IQueryable Sort(this IQueryable collection, string sortBy, bool reverse = false)
    {
        return collection.OrderBy(sortBy + (reverse ? " descending" : ""));
    }
}