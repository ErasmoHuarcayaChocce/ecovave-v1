using System;

namespace ecovave.model
{
    public class User : Audit
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string MobilphoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public bool IsActived { get; set; }
        public bool EsDeleted { get; set; }
    }
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string MobilphoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public bool IsActived { get; set; }
        public bool EsDeleted { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreateIp { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedIp { get; set; }
    }

    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string MobilphoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public bool IsActived { get; set; }
        public bool EsDeleted { get; set; }
    }
}
