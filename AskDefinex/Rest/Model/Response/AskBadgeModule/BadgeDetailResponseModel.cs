namespace AskDefinex.Rest.Model.Response
{
    public class BadgeDetailResponseModel : CommonResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
