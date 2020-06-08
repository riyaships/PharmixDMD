using System;

namespace Pharmix.Data.Entities.Context
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public string ArchivedUser { get; set; }

        public void SetCreateDetails(string user)
        {
            CreatedDate = DateTime.Now;
            CreatedUser = user;
        }
        public void SetUpdateDetails(string user)
        {
            UpdatedDate = DateTime.Now;
            UpdatedUser = user;
        }
        public void SetArchiveDetails(string user)
        {
            ArchivedDate = DateTime.Now;
            ArchivedUser = user;
            IsArchived = true;
        }
    }
}
