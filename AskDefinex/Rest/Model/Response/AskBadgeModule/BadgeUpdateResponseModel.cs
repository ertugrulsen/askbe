namespace AskDefinex.Rest.Model.Response.AskBadgeModule
{
    public class BadgeUpdateResponseModel : CommonResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
