namespace AskDefinex.DataAccess.Model.Data
{
    public class DataBaseLogDAOModel : BaseDAOModel
    {
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public string Severity { get; set; }
        public string Source { get; set; }
    }
}
