namespace AskDefinex.Rest.Model.Request.AskBadgeModule
{
    public class BadgeCreateRequestModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
