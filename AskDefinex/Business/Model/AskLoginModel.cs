using System.Collections.Generic;

namespace AskDefinex.Business.Model
{
    public class AskLoginModel
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }



    }
}
