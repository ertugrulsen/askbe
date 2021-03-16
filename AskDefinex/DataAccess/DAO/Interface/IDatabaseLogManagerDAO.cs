using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;

namespace AskDefinex.DataAccess.DAO.Interface
{
    public interface IDatabaseLogManagerDAO : IDAO
    {
        void WriteLog(DataBaseLogDAOModel databaseLog);
    }
}
