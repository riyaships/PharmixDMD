using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmix.Data.Entities.Context;

namespace Pharmix.Web.Entities
{
    [Table("Reminder", Schema = "task")]
    public class Reminder : BaseEntity
    {
        [Key]
        public int ReminderId { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? TodDateTime { get; set; }

        public DateTime? DueDate { get; set; }

        public int ReminderTypeId { get; set; }//Notification/Task/Reminder
        public int ReminderEntityTypeId { get; set; } //Service Due/Calibration due for devices/Event Reminder for user

        public int ReminderModuleId { get; set; } //Users/Isolators/Devices 
        public string ReminderModuleKeyId { get; set; }//This will be UserId, IsolatorId or DeviceId

        public virtual ICollection<ReminderInvitation> TrustContacts { get; set; } = new HashSet<ReminderInvitation>();
    }
    [Table("ReminderInvitation", Schema = "task")]
    public class ReminderInvitation : BaseEntity
    {
        [Key]
        public int ReminderInvitationId { get; set; }

        public string SenderUserName { get; set; }

        public int ReminderId { get; set; }
        [ForeignKey("ReminderId")]
        public virtual Reminder Reminder { get; set; }
        public virtual ICollection<ReminderInvitationMember> ReminderInvitationMembers { get; set; } = new HashSet<ReminderInvitationMember>();

    }
    [Table("ReminderInvitationMember", Schema = "task")]
    public class ReminderInvitationMember : BaseEntity
    {
        public int ReminderInvitationMemberId { get; set; }

        public string RecipientUserName { get; set; }

        public bool? HasRead { get; set; }
        public DateTime? ReadDate { get; set; }

        public int ReminderInvitationId { get; set; }
        [ForeignKey("ReminderInvitationId")]
        public virtual ReminderInvitation ReminderInvitation { get; set; }

        public int ReminderProgressId { get; set; }
        [ForeignKey("ReminderProgressId")]
        public virtual ReminderProgress ReminderProgress { get; set; }
    }

    [Table("ReminderProgress", Schema = "task")]
    public class ReminderProgress : BaseEntity
    {
        [Key]
        public int ReminderProgressId { get; set; }
        public int ReminderProgressPercent { get; set; }
        public DateTime? LastProgressDate { get; set; }
        public int ReminderProgressStatusId { get; set; }
        [ForeignKey("ReminderProgressStatusId")]
        public virtual ReminderProgressStatus ReminderProgressStatus { get; set; }
        
    }
    [Table("ReminderProgressStatus", Schema = "task")]
    public class ReminderProgressStatus : BaseEntity
    {
        [Key]
        public int ReminderProgressStatusId { get; set; }
        public string Description { get; set; }
    }

    /*
  Every reminder has a subject, description, location, start time and end time, recurring info.

This will belong to either User or Isolator or any Devices which will be Reminder Modules.

For Isolator, there may Service Due Date, there may be Calibration due date.
For Devices there may be CalibrationDueDate

For Users, may be event reminder date.

We just need to use the calendar control and do a single page component in which we can do this.

     */
}