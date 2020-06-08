using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmixJob.Model
{
    public class UploadedFiles
    {        
        public int UploadedFileID { get; set; }
        public string TableName { get; set; }
        public string FilePath { get; set; }
        public string ChangesetID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string WeekNo { get; set; }
        public string YearNo { get; set; }
        public decimal? ChangesetFileSize { get; set; }

    }
}
