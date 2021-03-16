using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IAskBadgeDAO : IDAO
    {
        public AskBadgeDAOModel GetBadgeByUserId(int userid);
        public int CreateBadge(AskBadgeDAOModel badgeModel);
        public void UpdateBadge(AskBadgeDAOModel daoModel);
        public void DeleteBadge(AskBadgeDAOModel daoModel);
    }
}
