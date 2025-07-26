using ECommerceProject.Core.Models.Enums;
using System;

namespace ECommerceProject.Core.Base
{
    public abstract class UserBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Year { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public DateTime CreateTime { get; set; }
        public  Role Role { get; set; }


        protected UserBase(DateTime createTime)
        {
            CreateTime = createTime;
        }

    }
}
