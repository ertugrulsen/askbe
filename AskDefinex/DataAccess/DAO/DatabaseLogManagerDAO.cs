using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Model.Data;
using DefineXwork.Library.DataAccess;

namespace AskDefinex.DataAccess.DAO
{
    public class DatabaseLogManagerDAO : BaseDAO<IDatabaseManager>, IDatabaseLogManagerDAO
    {

        // Sadece frameworkten gelen Database Log Manager üzerinden log yazımı işlemi için kullanılır

        public DatabaseLogManagerDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public DatabaseLogManagerDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }

        public void WriteLog(DataBaseLogDAOModel databaseLog)
        {
            base.InsertWithTemplate("DatabaseLogDAO.AddLog", databaseLog);
        }
    }
}
