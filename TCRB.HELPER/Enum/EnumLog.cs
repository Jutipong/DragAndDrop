using System.ComponentModel;

namespace TCRB.HELPER
{
    public enum EnumLog
    {
        [Description("WEB_LOG")]
        Web,
        [Description("WEBSERVICE_LOG")]
        WebService,
        [Description("CONSOLE_LOG")]
        Console,
        [Description("BUSINESS_LOG")]
        Business,
        [Description("DATABASE_LOG")]
        Database,
        [Description("DATASERVICE_LOG")]
        DataService

    }
}
