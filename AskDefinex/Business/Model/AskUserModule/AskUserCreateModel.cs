namespace AskDefinex.Business.Model
{
    public class AskUserCreateModel : BaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }

    }
}
