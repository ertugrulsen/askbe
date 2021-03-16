using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IAskTagDAO : IDAO
    {
        public int CreateTag(AskTagDAOModel tagModel);
        public void UpdateTag(AskTagDAOModel daoModel);
        public void DeleteTag(AskTagDAOModel daoModel);
    }
}