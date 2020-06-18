using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TCRB.DAL.Model.Appsetting;

namespace TCRB.DAL
{
    public class TCRBDBContext : ConfigurationManagementContext.ConfigurationManagementContext
    {
        private readonly AppsittingModel _configuration;

        public TCRBDBContext(IOptions<AppsittingModel> configuration)
        {
            _configuration = configuration.Value;
        }

        public TCRBDBContext(DbContextOptions<ConfigurationManagementContext.ConfigurationManagementContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.ConnectionStrings.TCRBDB);
            }
        }
    }
}
