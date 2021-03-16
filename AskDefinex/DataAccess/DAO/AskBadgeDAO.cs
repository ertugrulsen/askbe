using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Linq;

namespace AskDefinex.DataAccess.DAO
{
    public class AskBadgeDAO : BaseDAO<IDatabaseManager>, IAskBadgeDAO
    {
        public AskBadgeDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AskBadgeDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }
        public AskBadgeDAOModel GetBadgeByUserId(int userid)
        {

            AskBadgeDAOModel model = base.SelectWithTemplate<AskBadgeDAOModel>("AskBadgeDAO.GetBadgeByUserId", new { UserId = userid }).FirstOrDefault();

            if (model == null)
                return null;

            return model;

        }
        public int CreateBadge(AskBadgeDAOModel badgeModel)
        {
            if (null == badgeModel)
                return 0;

            int newId = base.InsertWithTemplate("AskBadgeDAO.CreateBadge", badgeModel);

            return newId;
        }
        public void UpdateBadge(AskBadgeDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskBadgeDAO.UpdateBadge", daoModel);
        }
        public void DeleteBadge(AskBadgeDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskBadgeDAO.DeleteBadge", daoModel);
        }
    }
}
