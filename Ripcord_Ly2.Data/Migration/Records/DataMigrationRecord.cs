using Ripcord_Ly2.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ripcord_Ly2.Data.Migration.Records
{
    public class DataMigrationRecord
    {
        public DataMigrationRecord()
        {
            DataMigrations = new List<DataMigration>();
        }

        public List<DataMigration> DataMigrations { get; set; }
    }

    public class DataMigration
    {
        public string DataMigrationClass { get; set; }

        public int? Version { get; set; }
    }

}
