namespace TCRB.DAL.Model.Appsetting
{
    public class AppsittingModel
    {
        public string AppVersion { get; set; }
        public string AppName { get; set; }
        public int LoginTimeExpired { get; set; }
        public ConnectionStringModel ConnectionStrings { get; set; }

    }

    public class ConnectionStringModel
    {
        public string TCRBDB { get; set; }
    }
}
