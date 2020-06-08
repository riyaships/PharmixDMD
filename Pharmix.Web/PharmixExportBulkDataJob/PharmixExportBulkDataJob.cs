using Newtonsoft.Json;
using PharmixCommon;
using PharmixJob.Model;
using PharmixJob.WebApiClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO.Compression;
using System.Net;
using System.Data.SqlClient;
using System.Reflection;

namespace PharmixJob
{
    partial class PharmixExportBulkDataJob : ServiceBase
    {
        Timer timer = new Timer();
        Common common = new Common();
        List<UploadedFiles> lstuploadedFiles = new List<UploadedFiles>();
        PharmixWebApiClient pharmixWebApiClient = new PharmixWebApiClient();
        decimal ChangesetFileSize = default(decimal);
        public bool IsCompleted = false;
        Dmd_FTPFileDownloadDetailsCustomModel dmdFTPFileDownloadDetailsCustomModel = new Dmd_FTPFileDownloadDetailsCustomModel();
        public PharmixExportBulkDataJob()
        {
            InitializeComponent();
        }

        public void onPharmixJobService()
        {
            try
            {
                OnStart(null);
            }
            catch (Exception ex)
            {
                TraceService("Exception- " + ex.Message + "at " + DateTime.Now);
            }

        }
        //This method is used to raise event during start of service
        protected override void OnStart(string[] args)
        {
            // System.Diagnostics.Debugger.Launch();
            //add this line to text file during start of service
           
            TraceService("PharmixExportBulkDataJob Job is started at " + DateTime.Now);
            //UnzipSourceFile();
            ToGetSaveExportDataToCSVDetails();
            TraceService("PharmixExportBulkDataJob Job is ended at " + DateTime.Now);

            //handle Elapsed event
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            //This statement is used to set interval to 1 minute (= 60,000 milliseconds)
            int jobinterval = Convert.ToInt32(ConfigurationManager.AppSettings["JobInterval"]);
            timer.Interval = (jobinterval * 60000);

            //enabling the timer
            timer.Enabled = true;

        }
        //This method is used to stop the service
        protected override void OnStop()
        {
            timer.Enabled = false;
            TraceService("stopping service");
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //PullDataFromFTP();

            IsCompleted = false;
          
            ToGetSaveExportDataToCSVDetails();
           

        }
        private void TraceService(string content)
        {

            //set up a filestream
            FileStream fs = new FileStream(ConfigurationManager.AppSettings["LogFilePath"].ToString(), FileMode.OpenOrCreate, FileAccess.Write);
            //FileStream fs = new FileStream("F:My App/Upwork/Files/Log.txt", FileMode.OpenOrCreate, FileAccess.Write);
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

        public List<ExportDataToCSVDetailsModel> ToGetSaveExportDataToCSVDetails()
        {
           
            HttpClient client = pharmixWebApiClient.InitializeClient();
            HttpResponseMessage response = client.GetAsync("api/PharmixApi/ToGetSaveExportDataToCSVDetails").Result;
            List<ExportDataToCSVDetailsModel> ListExportDataToCSVDetailsModel = new List<ExportDataToCSVDetailsModel>();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var result = response.Content.ReadAsStringAsync().Result;
                ListExportDataToCSVDetailsModel = JsonConvert.DeserializeObject<List<ExportDataToCSVDetailsModel>>(result);
              
                foreach (var item in ListExportDataToCSVDetailsModel)
                {
                    HttpResponseMessage responseUpdate = client.GetAsync("api/PharmixApi/ToUpdateExportDataToCSVDetails?ExportDataToCSVDetailId=" + Convert.ToInt32(item.ExportDataToCSVDetailId)).Result;
                    ExportDataToCSV(item.TabID, null, null, Convert.ToInt32(item.ChangetSetFrom), Convert.ToInt32(item.ChangetSetTo), false, item.BusinessUser, Convert.ToBoolean(item.IsDownloadsqlQuery), item.ChangetSetFromId, item.ChangetSetToId);
                    
                }
            }
           
            return ListExportDataToCSVDetailsModel;

        }

        public void ExportDataToCSV(string tabID, string TotalCount, string PageId, int changetSetFrom, int changetSetTo, bool allItem, string BusinessUser, bool IsDownloadsqlQuery = true, string changetSetFromId = "", string changetSetToId = "")
        {
           
            if (!allItem && tabID != "MyBuniessChangeset")
                tabID = "ToBeInserted";

            string fileExtension = ".xlsx";
            string fileName = string.Concat(PageId, fileExtension);

            HttpClient client = pharmixWebApiClient.InitializeClient();

            
            TraceService(client.Timeout.TotalMinutes.ToString());
            HttpResponseMessage response = client.GetAsync("api/PharmixApi/ExportData?reportType=" + PageId + "&fileName=" + fileName + "&rootFolder=" + ConfigurationManager.AppSettings["ZipFilePath"].ToString() + "/Files" + "&changetSetFrom=" + changetSetFrom + "&changetSetTo=" + changetSetTo + "&tabID=" + tabID + "&changetSetFromId=" + changetSetFromId + "&changetSetToId=" + changetSetToId + "&IsDownloadsqlQuery=" + IsDownloadsqlQuery + "&BusinessEmail=" + BusinessUser).Result;
            CustomReportModel customReportModel = new CustomReportModel();
           
            if (response.IsSuccessStatusCode)
            {

                TraceService("Api call is done");

                //TraceService("_connection  " + _connection);
                //TraceService("query  " + query);
                var result = response.Content.ReadAsStringAsync().Result;
                customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);

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

                    using (var fileStream = new FileStream(ConfigurationManager.AppSettings["ZipFilePath"].ToString() + zipName, FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                    EmailBody(BusinessUser, changetSetFromId, changetSetToId, zipName);
                }

            }

            TraceService("ExportDataToCSV Job is ended at " + DateTime.Now);
        }

        public void UnzipSourceFile()
        {
            try
            {

                TraceService("UnzipSourceFile is started " + DateTime.Now);
                string SourceFilePath = ConfigurationManager.AppSettings["SourceFilePath"].ToString();
                string UploadFilesPath = ConfigurationManager.AppSettings["UploadFiles"].ToString();
                string ProcessFilesPath = ConfigurationManager.AppSettings["ProcessFilesPath"].ToString();
                string currentSysTime = DateTime.Now.ToString("HH", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string EmailSyncTime = Convert.ToString(ConfigurationManager.AppSettings["EmailSyncTime"]);
                string EmailSyncDay = Convert.ToString(ConfigurationManager.AppSettings["EmailSyncDay"]);

                TraceService(SourceFilePath);
                TraceService(UploadFilesPath);
                TraceService(ProcessFilesPath);

                string[] getAllSourceFile = Directory.GetFiles(SourceFilePath, "*.*", SearchOption.AllDirectories).ToArray();
                foreach (var itemSource in getAllSourceFile)
                {
                    FileInfo uploadfilesize = new FileInfo(itemSource);
                    ChangesetFileSize = default(decimal);
                    ChangesetFileSize = Decimal.Divide(uploadfilesize.Length, 1048576);
                    string changesetName = string.Empty;
                    string[] SourceFilePathSplit = itemSource.Split('_');
                    changesetName = ("nhsbsa_" + SourceFilePathSplit[1] + "_" + SourceFilePathSplit[2]);
                    ZipFile.ExtractToDirectory(itemSource, System.IO.Path.Combine(UploadFilesPath, changesetName));
                    TraceService("File# is " + itemSource + " Extracted To Directory at" + DateTime.Now);
                    System.IO.File.Delete(itemSource);
                    AddExtractFilesToDb(System.IO.Path.Combine(UploadFilesPath, changesetName), SourceFilePathSplit[3]);
                    if (!System.IO.Directory.Exists(System.IO.Path.Combine(ProcessFilesPath, changesetName)))
                    {
                        System.IO.Directory.Move(System.IO.Path.Combine(UploadFilesPath, changesetName),
                                        System.IO.Path.Combine(ProcessFilesPath, changesetName));
                    }
                    else
                    {

                        TraceService("File not found " + System.IO.Path.Combine(ProcessFilesPath, changesetName) + DateTime.Now);
                    }


                }
                if (getAllSourceFile.Length == 0)
                {

                    TraceService("************** No Changeset File is founded to process  " + DateTime.Now + " **************");
                }
                if (string.Equals(currentSysTime, EmailSyncTime) && string.Equals(System.DateTime.Now.DayOfWeek.ToString(), EmailSyncDay))
                {
                    SendChangesetEmail();
                }
                IsCompleted = true;
                TraceService("UnzipSourceFile is ended " + DateTime.Now);
            }
            catch (Exception ex)
            {

                TraceService("Exception UnzipSourceFile " + ex.Message + DateTime.Now);
            }



        }
        public void AddExtractFilesToDb(string filePath, string ChangesetName)
        {
            try
            {
                TraceService("AddExtractFilesToDb is started " + DateTime.Now);
                lstuploadedFiles = new List<UploadedFiles>();
                string[] getAllGtnFileName = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".zip")).ToArray();
                foreach (var itemfileName in getAllGtnFileName)
                {
                    ZipFile.ExtractToDirectory(itemfileName, System.IO.Path.Combine(filePath));
                }
                string[] getAllFileName = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories)
                 .Where(s => s.EndsWith(".xml")).ToArray();
                string[] fileNameSplit = null;
                ChangesetName = GetChangeSetName(filePath, ChangesetName);

                foreach (var itemfileName in getAllFileName)
                {
                    var result = System.IO.Path.GetFileNameWithoutExtension(itemfileName);
                    fileNameSplit = result.Split('_');
                    lstuploadedFiles.Add(new UploadedFiles
                    {
                        TableName = fileNameSplit[0] + "_" + fileNameSplit[1],
                        FilePath = itemfileName,
                        ChangesetID = ChangesetName,
                        CreatedOn = DateTime.Now,
                        WeekNo = ChangesetName.Substring(5, 2),
                        YearNo = ChangesetName.Substring(15, 4),
                        ChangesetFileSize = ChangesetFileSize

                    });
                }
                TraceService("AddUploadedFilesWebAi is started  " + DateTime.Now);
                AddUploadedFilesWebAi(ChangesetName);
                TraceService("AddUploadedFilesWebAi is ended  " + DateTime.Now);
                TraceService("AddExtractFilesToDb is ended " + DateTime.Now);
            }
            catch (Exception ex)
            {

                TraceService("Exception: AddExtractFilesToDb  at " + ex.Message + " " + DateTime.Now);
            }

        }

        public string GetChangeSetName(string filePath, string ChangesetName)
        {
            try
            {
                string[] getWeekFileName = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories)
           .Where(s => s.EndsWith(".zip")).ToArray();
                FileInfo file = new FileInfo(getWeekFileName[0]);
                getWeekFileName[0] = getWeekFileName[0].ToLower();

                if (getWeekFileName[0].Contains("week"))
                {
                    ChangesetName = string.Concat(ChangesetName.Substring(0, 8), file.Name.Substring(4, 2));

                    ChangesetName = "Week " + ChangesetName.Substring(8, 2) + ", " + ChangesetName.Substring(6, 2) + "/" + ChangesetName.Substring(4, 2) + "/" + ChangesetName.Substring(0, 4);
                }
            }
            catch (Exception ex)
            {

                TraceService("Exception: GetChangeSetName  at " + ex.Message + " " + DateTime.Now);
            }

            return ChangesetName;
        }
        public void AddUploadedFilesWebAi(string changeSetId)
        {

            string objectToJsonString = JsonConvert.SerializeObject(lstuploadedFiles);
            var content = new StringContent(JsonConvert.SerializeObject(objectToJsonString), Encoding.UTF8, "application/json");
            HttpClient client = pharmixWebApiClient.InitializeClient();
            TraceService("Call Api Service call end at " + client.BaseAddress.ToString() + DateTime.Now);
            HttpResponseMessage response = client.PostAsync("api/PharmixApi/AddUploadedFilesWebAi", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TraceService("Api call is Success " + DateTime.Now);
                DmdProcessXMLFileToChangeset(changeSetId);
            }
            else
            {
                TraceService("Error at " + response.StatusCode + DateTime.Now);
            }

        }

        private void DmdProcessXMLFileToChangeset(string changeSetId)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
                TraceService("DmdProcessXMLFileToChangeset started at " + DateTime.Now);
                DataSet dsResult = GetDataSet("exec SP_DmdProcessXMLFileToChangeset ", connectionString, ConfigurationManager.AppSettings["LogFilePath"].ToString());
                TraceService("DmdProcessXMLFileToChangeset is ended " + DateTime.Now);
                TraceService("******************************** Changeset ID is  " + changeSetId + " completed  " + DateTime.Now + " ******************************");

            }
            catch (Exception ex)
            {
                TraceService("Exception- " + ex.Message + "at " + DateTime.Now);
            }

        }

        public void PullDataFromFTP()
        {
            //IsCompleted = false;

            //FTP Server URL.
            string ftp = ConfigurationManager.AppSettings["ftp"].ToString();

            //FTP Folder name. Leave blank if you want to list files from root folder.


            try
            {
                TraceService("**** FTP call is ****  started " + DateTime.Now.ToString());
                dmdFTPFileDownloadDetailsCustomModel = ToGetFTPFileDownloadDetails();
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpuserName"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                //Fetch the Response and read it using StreamReader.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                List<string> rootFolder = new List<string>();
                List<string> changesetFolder = new List<string>();
                List<Dmd_FTPFileDownloadDetailsCustomModel> lstdmdFTPFileDownloadDetailsCustomModel = new List<Dmd_FTPFileDownloadDetailsCustomModel>();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    //Read the Response as String and split using New Line character.
                    rootFolder = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                response.Close();

                TraceService("**** FTP Folder /File Access is fine  ****  " + DateTime.Now.ToString());

                //Loop and add details of each File to the DataTable.
                foreach (string changeSetName in rootFolder)
                {

                    string[] dirDetails = changeSetName.Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                    var dirDate = dirDetails[0].Replace('-', '/');
                    var dirTime = dirDetails[1].Substring(0, 5) + ":00 " + dirDetails[1].Substring(5, 2);
                    DateTime dirDateTime = DateTime.Parse(dirDate + " " + dirTime);
                    DateTime? lastSyncDateTime = dmdFTPFileDownloadDetailsCustomModel.DirectoryCreatedOn;



                    if (dirDateTime > lastSyncDateTime)
                    {
                        TraceService("**** FTP **** Current Directory DateTime " + dirDateTime.ToString() + " **** Last Sync Datetime started " + lastSyncDateTime.ToString());

                        //Create FTP Request.
                        ftp = ftp + dirDetails[3] + ConfigurationManager.AppSettings["ftpFolder"].ToString();
                        FtpWebRequest requestforFile = (FtpWebRequest)WebRequest.Create(ftp);
                        requestforFile.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                        requestforFile.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpuserName"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());
                        requestforFile.UsePassive = true;
                        requestforFile.UseBinary = true;
                        requestforFile.EnableSsl = false;

                        FtpWebResponse responseForFile = (FtpWebResponse)requestforFile.GetResponse();

                        using (StreamReader reader = new StreamReader(responseForFile.GetResponseStream()))
                        {
                            //Read the Response as String and split using New Line character.
                            changesetFolder = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        }
                        responseForFile.Close();

                        foreach (var nhsbsaDMDChnagetSetFile in changesetFolder)
                        {
                            string[] getFile = nhsbsaDMDChnagetSetFile.Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                            string fileExtenstion = getFile[3].Substring(getFile[3].Length - 4, 4);
                            if (fileExtenstion == ".zip")
                            {
                                FtpWebRequest requestForDownloadFile =
                                (FtpWebRequest)WebRequest.Create(ftp + getFile[3]);
                                requestForDownloadFile.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpuserName"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());
                                requestForDownloadFile.Method = WebRequestMethods.Ftp.DownloadFile;
                                TraceService("**** FTP **** File Name is " + getFile[3] + " **** Downloading started " + DateTime.Now);

                                using (Stream ftpStream = requestForDownloadFile.GetResponse().GetResponseStream())

                                using (Stream fileStream = File.Create(ConfigurationManager.AppSettings["SourceFilePath"].ToString() + getFile[3]))
                                {
                                    byte[] buffer = new byte[10240];
                                    int read;
                                    while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        fileStream.Write(buffer, 0, read);
                                        Console.WriteLine("Downloaded {0} bytes", fileStream.Position);
                                    }
                                }

                                TraceService("**** FTP **** File Name is " + getFile[3] + " **** Downloading ended " + DateTime.Now);

                                lstdmdFTPFileDownloadDetailsCustomModel.Add(new Dmd_FTPFileDownloadDetailsCustomModel
                                {
                                    FTPFileDownloadID = 0,
                                    ChagetsetName = dirDetails[3],
                                    DirectoryCreatedOn = dirDateTime
                                });
                            }

                        }


                    }
                    else
                    {
                        TraceService("**** FTP No Sync with FTP ***** Current Directory DateTime " + dirDateTime.ToString() + " **** Last Sync Datetime started " + lastSyncDateTime.ToString());
                    }
                    if (lstdmdFTPFileDownloadDetailsCustomModel != null && lstdmdFTPFileDownloadDetailsCustomModel.Count > 0)
                        ToSaveFTPFileDownloadDetails(lstdmdFTPFileDownloadDetailsCustomModel);
                    //IsCompleted = true;

                }

                TraceService("**** FTP call is ****  ended " + DateTime.Now.ToString());
            }
            catch (WebException ex)
            {
                TraceService("**** Exception in PullDataFromFTP ****   " + ex.Message + " " + DateTime.Now.ToString());
            }



        }


        public Dmd_FTPFileDownloadDetailsCustomModel ToGetFTPFileDownloadDetails()
        {

            HttpClient client = pharmixWebApiClient.InitializeClient();
            HttpResponseMessage response = client.GetAsync("api/PharmixApi/ToGetFTPFileDownloadDetails").Result;
            dmdFTPFileDownloadDetailsCustomModel = new Dmd_FTPFileDownloadDetailsCustomModel();
            //Checking the response is successful or not which is sent using HttpClient  
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var result = response.Content.ReadAsStringAsync().Result;
                dmdFTPFileDownloadDetailsCustomModel = JsonConvert.DeserializeObject<Dmd_FTPFileDownloadDetailsCustomModel>(result);
            }
            return dmdFTPFileDownloadDetailsCustomModel;

        }


        public void ToSaveFTPFileDownloadDetails(List<Dmd_FTPFileDownloadDetailsCustomModel> lstdmdFTPFileDownloadDetailsCustomModel)
        {
            TraceService("Call Api ToSaveFTPFileDownloadDetails call started  " + DateTime.Now);
            string objectToJsonString = JsonConvert.SerializeObject(lstdmdFTPFileDownloadDetailsCustomModel);
            var content = new StringContent(JsonConvert.SerializeObject(objectToJsonString), Encoding.UTF8, "application/json");
            HttpClient client = pharmixWebApiClient.InitializeClient();
            TraceService("Call Api ToSaveFTPFileDownloadDetails call end  " + client.BaseAddress.ToString() + DateTime.Now);
            HttpResponseMessage response = client.PostAsync("api/PharmixApi/ToSaveFTPFileDownloadDetails", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TraceService("Api call ToSaveFTPFileDownloadDetails is Success " + DateTime.Now);
            }
            else
            {
                TraceService("Error in ToSaveFTPFileDownloadDetails  " + response.StatusCode + DateTime.Now);
            }

        }

        private void SendChangesetEmail()
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
                TraceService("EmailTrigger started at " + DateTime.Now);
                DataSet dsResult = GetDataSet("exec SP_EmailTrigger ", connectionString, ConfigurationManager.AppSettings["LogFilePath"].ToString());
                foreach (DataRow item in dsResult.Tables.Count > 2 ? dsResult.Tables[2].Rows : dsResult.Tables[0].Rows)
                {
                    var isNotifyWeekly = (bool?)item["NotifyWeekly"];
                    if (isNotifyWeekly == null || isNotifyWeekly == false)
                    {
                        if (DateTime.Now.Day > 7)
                        {
                            continue;
                        }
                    }

                    string filePath = GenerateZipFiles(item);
                    string fullFilePath = ConfigurationManager.AppSettings["applicationURl"].ToString();

                    //EmailBody(item, string.Concat(fullFilePath, filePath));
                    string sqlQuery = "update Dmd_EmailTrigger set status = 2 where DmdEmailTriggerID = " + item[0];
                    int resut = common.ExecuteQuery(sqlQuery, connectionString);
                    sqlQuery = "insert into Dmd_BusinessChangeSetDetails (FromDateChangeset,ToDateChangeset,FromDateChangesetId,ToDateChangesetId,CreatedOn,BusinessEmail,IsActive) values (";
                    sqlQuery += "'" + item[2] + "','" + item[3] + "','" + item[6] + "','" + item[7] + "','" + System.DateTime.Now + "','" + item[1] + "','1'";
                    sqlQuery += ")";
                    resut = common.ExecuteQuery(sqlQuery, connectionString);
                }
                TraceService("******************************** EmailTrigger is completed  " + DateTime.Now + " ******************************");

            }
            catch (Exception ex)
            {
                TraceService("Exception- " + ex.Message + "at " + DateTime.Now);
            }

        }

        public void EmailBody(string email, string changesetFrom, string changesetTo,string fileName)
        {
            try
            {
                TraceService("******************************** EmailBody is started  " + DateTime.Now + " ******************************");
                TraceService(ConfigurationManager.AppSettings["PharmixChangesetTemplate"].ToString());
                string messageBody = string.Empty;
                using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings["PharmixChangesetTemplate"].ToString()))
                {
                    messageBody = reader.ReadToEnd();
                }

                messageBody = messageBody.Replace("{EMAIL_NAME}", email);
                messageBody = messageBody.Replace("{ChangetSetFrom}", changesetFrom);
                messageBody = messageBody.Replace("{ChangetSetToId}", changesetTo);
                messageBody = messageBody.Replace("{EMAIL_DOWNLOAD_LINK}", ConfigurationManager.AppSettings["applicationURl"].ToString() + fileName + "&isFreshLoad=" + false);

                new Email().SendEmail(email, "Download Business Changeset", messageBody);

                TraceService("******************************** EmailBody is completed  " + DateTime.Now + " ******************************");
            }
            catch (Exception ex)
            {
                TraceService("exception in email" + ex.InnerException.ToString());

                throw;
            }
            
        }
        public string GenerateZipFiles(DataRow item)
        {
            TraceService("******************************** GenerateZipFiles is started  " + DateTime.Now + " ******************************");
            HttpClient client = pharmixWebApiClient.InitializeClient();
            client.Timeout = TimeSpan.FromMinutes(30);
            HttpResponseMessage response = client.GetAsync("api/PharmixApi/ExportData?reportType=" + string.Empty + "&fileName=" + string.Empty + "&rootFolder=" + ConfigurationManager.AppSettings["RootFolder"].ToString() + "&changetSetFrom=" + item[2] + "&changetSetTo=" + item[3] + "&tabID=" + "MyBuniessChangeset" + "&changetSetFromId=" + item[2] + "&changetSetToId=" + item[3] + "&IsDownloadsqlQuery=true" + "&BusinessEmail=" + item[1]).Result;
            CustomReportModel customReportModel = new CustomReportModel();
            string zipName = String.Format("MyChangeset_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            string fullFilePath = ConfigurationManager.AppSettings["RootFolder"].ToString() + zipName;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                customReportModel = JsonConvert.DeserializeObject<CustomReportModel>(result);


                List<string> filesCol = customReportModel.AllFilePathForMychangesetDetails.ToList();
                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var filePath in filesCol)
                        {
                            FileInfo file = new FileInfo(filePath);
                            ziparchive.CreateEntryFromFile(file.FullName, file.Name);
                            file.Delete();
                        }
                    }
                    using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                }
            }
            TraceService("******************************** GenerateZipFiles is completed  " + "File Path " + zipName + " " + DateTime.Now + " ******************************");
            return zipName;

        }

        public DataSet GetDataSet(string sqlquery, string connectionString, string path)
        {
            DataSet _dsResult = new DataSet();
            try
            {
                //TraceService("connectionString " , connectionString);
                SqlConnection _connection = new SqlConnection(connectionString);
                SqlCommand _command = new SqlCommand(sqlquery, _connection);
                _command.CommandTimeout = 0;
                SqlDataAdapter _Adapter = new SqlDataAdapter(_command);
                _Adapter.Fill(_dsResult);

            }
            catch (Exception ex)
            {

                TraceService("Eror : " + ex.Message, path);
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
    }
}
