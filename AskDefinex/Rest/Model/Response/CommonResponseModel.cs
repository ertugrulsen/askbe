using System;

namespace AskDefinex.Rest.Model.Response
{
    public class CommonResponseModel
    {
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
    }
}
