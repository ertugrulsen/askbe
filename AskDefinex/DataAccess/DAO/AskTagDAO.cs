using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace AskDefinex.DataAccess.DAO
{
    public class AskTagDAO : BaseDAO<IDatabaseManager>, IAskTagDAO
    {
        public AskTagDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AskTagDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }

        public int CreateTag(AskTagDAOModel tagModel)
        {
            if (null == tagModel)
                return 0;

            int newId = base.InsertWithTemplate("AskTagDAO.CreateTag", tagModel);

            return newId;
        }
        public void UpdateTag(AskTagDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskTagDAO.UpdateTag", daoModel);
        }
        public void DeleteTag(AskTagDAOModel daoModel)
        {
            base.UpdateWithTemplate("AskTagDAO.DeleteTag", daoModel);
        }
        public List<AskTagDAOModel> GetAllTags()
        {
            List<AskTagDAOModel> model = base.SelectWithTemplate<AskTagDAOModel>("AskTagDAO.GetAllTags").ToList();

            return model;
        }
        public AskTagDAOModel GetTagById(int id)
        {
            AskTagDAOModel model = base.SelectWithTemplate<AskTagDAOModel>("AskTagDAO.GetTagById", new { Id = id }).FirstOrDefault();

            if (model == null)
                return null;

            return model;
        }
        public int CreateQuestion(AskTagDAOModel tagModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
