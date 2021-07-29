using System;

namespace ecovave.model
{
    public class Delivery : Audit
    {
        public int DeliveryId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public decimal QuantityKg { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class DeliveryDto
    {
        public int DeliveryId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public decimal QuantityKg { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIp { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedIp { get; set; }
    }
}
