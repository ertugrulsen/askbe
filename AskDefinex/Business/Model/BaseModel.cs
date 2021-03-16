using System;

namespace AskDefinex.Business.Model
{
    public class BaseModel
    {
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
    }
}
