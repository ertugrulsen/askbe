using System.Collections.Generic;

namespace AskDefinex.DataAccess.Model.Data
{
    public class UserContextUserDAOModel : BaseDAOModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
    }
}
