using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Data.Migration
{
    public interface IDataMigrationManager
    {

        Task<IEnumerable<string>> GetFeaturesThatNeedUpdateAsync();

        Task UpdateAllFeaturesAsync();

        Task UpdateAsync(string feature);

        Task UpdateAsync(IEnumerable<string> feature);

        Task Uninstall(string feature);

    }
}
