using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class Dmd_FTPFileDownloadDetails
    {
        [Key]
        public int FTPFileDownloadID { get; set; }
        public string ChagetsetName { get; set; }
        public DateTime? DirectoryCreatedOn { get; set; }
    }
}
