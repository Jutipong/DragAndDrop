using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using TCRB.DAL.Configuration;
using TCRB.DAL.Model.Appsetting;

namespace TCRB.DAL
{
    public class DataAccessWrapper : IDataAccessWrapper
    {
        private readonly TCRBDBContext _context;
        private readonly IOptions<AppsittingModel> _config;
        private readonly ILoggerFactory _loggerFactory;

        private IConfigurationDataAccess _configurationDataAccess;

        public DataAccessWrapper(TCRBDBContext context, IOptions<AppsittingModel> config, ILoggerFactory loggerFactory)
        {
            _context = context;
            _config = config;
            _loggerFactory = loggerFactory;
        }

        public IConfigurationDataAccess ConfigurationDataAccess => _configurationDataAccess ?? (_configurationDataAccess = new ConfigurationDataAccess(_context, _loggerFactory));

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public int Count<T>(IQueryable<T> query)
        {
            return query.Count();
        }
    }
}
