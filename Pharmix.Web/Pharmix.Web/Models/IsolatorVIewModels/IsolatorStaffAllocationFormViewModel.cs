using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmix.Data.Enums;

namespace Pharmix.Web.Models.IsolatorVIewModels
{
    public class IsolatorStaffAllocationViewModel
    {
        public int IsolatorStaffAllocationId { get; set; }
        public int IsolatorId { get; set; }
        public int IsolatorShiftId { get; set; }
        public string ShiftDescription { get; set; }
        public string StaffId { get; set; }
        public DateTime AllocatedDate { get; set; }
        public SelectList IsolatorStaffList { get; set; }
        public bool IsRecurring { get; set; }
        public RecurringTypeEnum RecurringType { get; set; } = RecurringTypeEnum.Daily;
        public DateTime? RecurrenceEndDate { get; set; }
        public DailyRecurringTypeEnum DailyRecurringType { get; set; } = DailyRecurringTypeEnum.EveryWeekday;
        public MultiSelectList DayList { get; set; }
        public int[] SelectedDays { get; set; }
        public bool IsModelReadonly { get; set; }
        //public CalendarRecurrence Recurrence { get; set; } = new CalendarRecurrence();
    }

    //public class CalendarRecurrence
    //{
    //    public int RecurringType { get; set; } = (int)RecurringTypeEnum.Daily;
    //    public DailyRecurrence Daily { get; set; } = new DailyRecurrence();
    //    public WeeklyRecurrence Weekly { get; set; } = new WeeklyRecurrence();
    //    //public MonthlyRecurrence Monthly { get; set; } = new MonthlyRecurrence();
    //    public DateTime? RecurrenceEndDate { get; set; }
    //}

    //public class DailyRecurrence
    //{
    //    public bool EveryWeekday { get; set; }
    //    public bool Everyday { get; set; }
    //}

    //public class WeeklyRecurrence
    //{
    //    public MultiSelectList DayList { get; set; }
    //    public int[] SelectedDays { get; set; }
    //}

    //public class MonthlyRecurrence
    //{
    //    public MultiSelectList WeekList { get; set; }
    //    public int[] SelectedWeeks { get; set; }

    //    public MultiSelectList DayList { get; set; }
    //    public int[] SelectedDays { get; set; }
    //}
}