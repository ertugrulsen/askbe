namespace AskDefinex.Rest.Model.Response
{
    public class AskUserDetailResponseModel : CommonResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
    }
}
