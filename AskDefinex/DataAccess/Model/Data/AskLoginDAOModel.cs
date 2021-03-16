using System;
using System.Collections.Generic;

namespace AskDefinex.DataAccess.Model.Data
{
    public class AskLoginDAOModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenCreateDate { get; set; }
    }
}
