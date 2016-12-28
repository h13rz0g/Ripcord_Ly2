
using Microsoft.Extensions.Logging;
using Ripcord_Ly2.Data.Migration.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YesSql.Core.Services;

namespace Ripcord_Ly2.Data.Migration
{
    public class DataMigrationManager : IDataMigrationManager
    {
        private readonly IEnumerable<IDataMigration> _dataMigrations;
        private readonly ISession _session;
        private readonly IStore _store;
        private readonly ILogger _logger;

        private readonly List<string> _processedFeatures;
        private DataMigrationRecord _dataMigrationRecord;

        public DataMigrationManager(
            IEnumerable<IDataMigration> dataMigrations,
            ISession session,
            IStore store,
            ILogger<DataMigrationManager> logger
            )
        {
            this._dataMigrations = dataMigrations;
            this._logger = logger;
            this._session = session;
            this._store = store;


            this._processedFeatures = new List<string>();
        }

        /// <summary>
        /// 获取迁移记录
        /// </summary>
        /// <returns></returns>
        public async Task<DataMigrationRecord> GetDataMigrationRecordAsync()
        {
            if (_dataMigrationRecord == null)
            {
                _dataMigrationRecord = await _session.QueryAsync<DataMigrationRecord>().FirstOrDefault();
                if (_dataMigrationRecord == null)
                {
                    _dataMigrationRecord = new DataMigrationRecord();
                    _session.Save(_dataMigrationRecord);
                }
            }
            return _dataMigrationRecord;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetFeaturesThatNeedUpdateAsync()
        {
            var currentVersions = (await GetDataMigrationRecordAsync()).DataMigrations
                .ToDictionary(r => r.DataMigrationClass);
            var outOfDataMigrations = _dataMigrations.Where(dataMigration =>
            {
                DataMigration record;
                if (currentVersions.TryGetValue(dataMigration.GetType().FullName, out record))
                {
                    return CreateUpgradeLookupTable(dataMigration)
                }
            });

            throw new NotImplementedException();
        }

        public Task Uninstall(string feature)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAllFeaturesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<string> feature)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string feature)
        {
            throw new NotImplementedException();
        }


        private static Dictionary<int, MethodInfo> CreateUpgradeLookupTable(IDataMigration dataMigration)
        {
            return dataMigration.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(GetUpdateMethod)
                .Where(tuple => tuple != null)
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        private static Tuple<int, MethodInfo> GetUpdateMethod(MethodInfo mi)
        {
            const string updatefromPrefix = "update";
            if (mi.Name.StartsWith(updatefromPrefix))
            {
                var version = mi.Name.Substring(updatefromPrefix.Length);
                int versionValue;
                if (int.TryParse(version, out versionValue))
                {
                    return new Tuple<int, MethodInfo>(versionValue, mi);
                }
            }
            return null;
        }


    }
}
