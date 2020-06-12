using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCRB.DAL.Configuration;

namespace TCRB.DAL
{
    public interface IDataAccessWrapper
    {
        IConfigurationDataAccess ConfigurationDataAccess { get; }

        void SaveChanges();
        Task SaveChangesAsync();
        int Count<T>(IQueryable<T> query);
    }
}
