using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmix.Data.Enums
{
    public enum GenderEnum
    {
        None,
        Male,
        Female,
        Transgender
    }
    public enum RecurringTypeEnum
    {
        NoRecurring = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4
    }

    public enum DailyRecurringTypeEnum
    {
        NoRecurring = 0,
        Everyday = 1,
        EveryWeekday = 2,
    }

    public enum StatusEnum
    {
        None = 0,
        Pending = 1,
        Approved = 2,
        Archived = 3
    } 

    public enum RequsetPriorityEnum
    {
        VeryHigh = 1,
        High = 2,
        Medium = 3,
        Low = 4,
        VeryLow = 5
    }

    public enum RequestStatusEnum
    {
        Awaiting = 1,
        Approved = 2,
        SentForReview =3,
        Declined = 4
    }

    public enum OrderProgressEnum
    {
        New = 1,
        Pending = 2,
        Approved = 3,
        Scheduled = 4,
        PreperationScheduled = 5,
        Compounding = 6,
        Completed= 7,
        Dispatched = 8
    }

    public enum OrderPreperationStatusEnum
    {
        YetToStart=0,
        Started = 1,
        Paused = 2,
        Completed = 3
    }

    public enum PriorityEnum
    {
        Critical = 1,
        High = 2,
        Medium = 3,
        Low = 4
    }

    public enum PressureUnitEnum
    {
        Pascal=1,
        Bar=2
    }

    public enum TemperatureUnitEnum
    {
        Celcius = 1,
        Farenheit = 2
    }

    public enum IsolatorOperationTypeEnum
    {
        ManualClosed = 1,
        AutomaticOpen = 2
    }

    public enum IsolatorProcedureTypeEnum
    {
        Start =1,
        Stop=2
    }

    public enum StockStatusEnum
    {
        Available = 1,
        FullyUsed = 2,
        Expired = 3,
        Disposed= 4
    }
}
