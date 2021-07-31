using System;

namespace ecovave.model
{
    public class Schedule : Audit
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public int RecyclingTypeId { get; set; }
        public decimal QuantityKg { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int StatusId { get; set; }
        public bool EsDeleted { get; set; }
    }
    public class ScheduleDto
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public int RecyclingTypeId { get; set; }
        public decimal QuantityKg { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int StatusId { get; set; }
        public bool EsDeleted { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedIp { get; set; }
    }

    public class ScheduleRequest
    {

    }
    public class ScheduleResponse
    {

    }
}
