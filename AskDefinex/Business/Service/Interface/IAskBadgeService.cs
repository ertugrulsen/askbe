using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskBadgeModule;
using DefineXwork.Library.Business;

namespace AskDefinex.Business.Service.Interface
{
    public interface IAskBadgeService : IService
    {
        public BadgeDetailModel GetBadgeByUserId(int userid);
        public int CreateBadge(BadgeCreateModel badgeModel);
        public void UpdateBadge(BadgeUpdateModel updateModel);
        public void DeleteBadge(BadgeDeleteModel deleteModel);
    }
}
