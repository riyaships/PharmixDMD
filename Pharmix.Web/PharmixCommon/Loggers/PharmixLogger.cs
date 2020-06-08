using System;
using System.Configuration;
using System.IO;
public class PharmixLogger
{
    public static event EventHandler StatusUpdated;
    public static string LoggingDirectory
    {
        get { return (ConfigurationManager.AppSettings["LogPath"] ?? "C:\\Pharmix\\Apps\\DMD\\Logs"); }
    }

    public static string LoggingFile
    {
        get { return LoggingDirectory + "\\Logs_" + DateTime.Now.ToString("ddMMyyyy") + ".log"; }
    }

    public static void LogDetails(string message, string currentFile = null, bool ignoreTimestamp = false)
    {
        if (currentFile == null)
        {
            currentFile = LoggingFile;
        }
        string logfile = currentFile;
        if (!File.Exists(logfile))
        {
            if (!Directory.Exists(Path.GetDirectoryName(logfile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logfile));
            }
            var stream = File.Create(logfile);
            stream.Close();
        }

        using (var w = File.AppendText(logfile))
        {
            Log(message, w, ignoreTimestamp);
            w.Close();
        }

        if (StatusUpdated != null)
            StatusUpdated(message, new EventArgs());
    }

    private static void Log(string logMessage, TextWriter w, bool ignoreTimeStamp = false)
    {
        if (ignoreTimeStamp)
        {
            w.WriteLine(logMessage);
        }
        else
        {
            w.WriteLine("Log Entry : {0} {1} ", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine(logMessage);
            w.WriteLine("-------------------------------");
        }
    }
}
 