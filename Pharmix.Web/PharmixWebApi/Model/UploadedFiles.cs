using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Model
{
    public class UploadedFiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
