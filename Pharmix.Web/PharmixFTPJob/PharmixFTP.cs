using PharmixFTPJob.Model;
using PharmixFTPJob.WebApiClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Globalization;

namespace PharmixFTPJob
{
    partial class PharmixFTP : ServiceBase
    {
        Timer timer = new Timer();

        PharmixFTPWebApiClient pharmixFTPWebApiClient = new PharmixFTPWebApiClient();
       
        Dmd_FTPFileDownloadDetailsCustomModel dmdFTPFileDownloadDetailsCustomModel = new Dmd_FTPFileDownloadDetailsCustomModel();

        public PharmixFTP()
        {
            InitializeComponent();
        }

        public void onPharmixFTPService()
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
            
            //System.Diagnostics.Debugger.Launch();
            //add this line to text file during start of service
            TraceService("FTP start service");
            TraceService("FTP Job is started at " + DateTime.Now);

            TraceService("FTP Job is ended at " + DateTime.Now);
            //PullDataFromFTP();
            //handle Elapsed event
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            //This statement is used to set interval to 1 minute (= 60,000 milliseconds)
            int jobinterval = Convert.ToInt32(ConfigurationManager.AppSettings["JobFTPInterval"]);
            timer.Interval = (jobinterval * 60000);

            //enabling the timer
            timer.Enabled = true;

        }
        //This method is used to stop the service
        protected override void OnStop()
        {
            timer.Enabled = false;
            TraceService("FTP stopping service");
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            string currentSysTime = DateTime.Now.ToString("HH", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string FTPSyncTime = Convert.ToString(ConfigurationManager.AppSettings["FTPSyncTime"]);
            TraceService("FTP Job is started at " + DateTime.Now);

            if (string.Equals(currentSysTime, FTPSyncTime))
            {
                TraceService("PullDataFromFTP started at" + currentSysTime);
                PullDataFromFTP();
                TraceService("PullDataFromFTP ended at" + currentSysTime);
            }
            TraceService("FTP Current Time is " + currentSysTime);
            TraceService("FTP Sync Time is " + FTPSyncTime);
            TraceService("FTP Job is ended " + DateTime.Now);
        }
        private void TraceService(string content)
        {
            //set up a filestream
            FileStream fs = new FileStream(ConfigurationManager.AppSettings["LogFilePath"].ToString(), FileMode.OpenOrCreate, FileAccess.Write);
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
                if (dmdFTPFileDownloadDetailsCustomModel == null)
                {
                    TraceService("**** dmdFTPFileDownloadDetailsCustomModel is null ****  " + DateTime.Now.ToString());

                }
                //Loop and add details of each File to the DataTable.
                foreach (string changeSetName in rootFolder)
                {
                    TraceService("**** changeSetName  ****  " + changeSetName);
                    string[] dirDetails = changeSetName.Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                    var dirDate = dirDetails[0].Replace('-', '/');
                    var dirTime = dirDetails[1].Substring(0, 5) + ":00 " + dirDetails[1].Substring(5, 2);
                    DateTime dirDateTime = DateTime.Parse(dirDate + " " + dirTime);
                    DateTime? lastSyncDateTime = dmdFTPFileDownloadDetailsCustomModel.DirectoryCreatedOn;

                    TraceService("**** dirDateTime  ****  " + dirDateTime);
                    TraceService("**** lastSyncDateTime  ****  " + lastSyncDateTime);

                    if (dirDateTime > lastSyncDateTime)
                    {
                        TraceService("**** FTP **** Current Directory DateTime " + dirDateTime.ToString() + " **** Last Sync Datetime started " + lastSyncDateTime.ToString());
                        string ftpFileLocation = string.Empty;
                        //Create FTP Request.
                        ftpFileLocation = ftp + dirDetails[3] + ConfigurationManager.AppSettings["ftpFolder"].ToString();
                        FtpWebRequest requestforFile = (FtpWebRequest)WebRequest.Create(ftpFileLocation);
                        requestforFile.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                        requestforFile.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpuserName"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());
                        requestforFile.UsePassive = true;
                        requestforFile.UseBinary = true;
                        requestforFile.EnableSsl = false;
                        
                        FtpWebResponse responseForFile = (FtpWebResponse)requestforFile.GetResponse();

                        using (StreamReader reader = new StreamReader(responseForFile.GetResponseStream()))
                        {
                               changesetFolder = new List<string>();
                               //Read the Response as String and split using New Line character.
                               changesetFolder = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            reader.Dispose();
                        }
                        responseForFile.Close();

                        foreach (var nhsbsaDMDChnagetSetFile in changesetFolder)
                        {
                            string[] getFile = nhsbsaDMDChnagetSetFile.Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                            string fileExtenstion = getFile[3].Substring(getFile[3].Length - 4, 4);
                            if (fileExtenstion == ".zip")
                            {
                                FtpWebRequest requestForDownloadFile =
                                (FtpWebRequest)WebRequest.Create(ftpFileLocation + getFile[3]);
                                requestForDownloadFile.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ftpuserName"].ToString(), ConfigurationManager.AppSettings["ftpPassword"].ToString());
                                requestForDownloadFile.Method = WebRequestMethods.Ftp.DownloadFile;
                                TraceService("**** FTP **** File Name is " + getFile[3] + " **** Downloading started " + DateTime.Now);

                                Stream ftpStream = requestForDownloadFile.GetResponse().GetResponseStream();


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
                
                }

                if (lstdmdFTPFileDownloadDetailsCustomModel != null && lstdmdFTPFileDownloadDetailsCustomModel.Count > 0)
                    ToSaveFTPFileDownloadDetails(lstdmdFTPFileDownloadDetailsCustomModel);
                //IsCompleted = true;

                TraceService("**** FTP call is ****  ended " + DateTime.Now.ToString());
            }
            catch (WebException ex)
            {
                TraceService("**** Exception in PullDataFromFTP ****   " + ex.Message + " " + DateTime.Now.ToString());
            }



        }


        public Dmd_FTPFileDownloadDetailsCustomModel ToGetFTPFileDownloadDetails()
        {
            try
            {
                HttpClient client = pharmixFTPWebApiClient.InitializeClient();
                HttpResponseMessage response = client.GetAsync("api/PharmixApi/ToGetFTPFileDownloadDetails").Result;
                dmdFTPFileDownloadDetailsCustomModel = new Dmd_FTPFileDownloadDetailsCustomModel();
                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var result = response.Content.ReadAsStringAsync().Result;
                    dmdFTPFileDownloadDetailsCustomModel = JsonConvert.DeserializeObject<Dmd_FTPFileDownloadDetailsCustomModel>(result);
                }
                TraceService(response.IsSuccessStatusCode.ToString());
            }
            catch (Exception ex)
            {
                TraceService("asdasdadasd  " + ex.Message.ToString());
            }
            
            return dmdFTPFileDownloadDetailsCustomModel;

        }


        public void ToSaveFTPFileDownloadDetails(List<Dmd_FTPFileDownloadDetailsCustomModel> lstdmdFTPFileDownloadDetailsCustomModel)
        {
            TraceService("Call Api ToSaveFTPFileDownloadDetails call started  " + DateTime.Now);
            string objectToJsonString = JsonConvert.SerializeObject(lstdmdFTPFileDownloadDetailsCustomModel);
            var content = new StringContent(JsonConvert.SerializeObject(objectToJsonString), Encoding.UTF8, "application/json");
            HttpClient client = pharmixFTPWebApiClient.InitializeClient();
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
    }
}
