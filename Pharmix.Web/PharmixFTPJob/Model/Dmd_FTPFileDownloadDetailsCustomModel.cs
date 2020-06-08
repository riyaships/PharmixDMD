using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmixFTPJob.Model
{
   public class Dmd_FTPFileDownloadDetailsCustomModel
    {
        public int FTPFileDownloadID { get; set; }
        public string ChagetsetName { get; set; }
        public DateTime? DirectoryCreatedOn { get; set; }
    }
}
