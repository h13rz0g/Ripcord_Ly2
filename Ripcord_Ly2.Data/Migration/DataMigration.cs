using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql.Core.Sql;

namespace Ripcord_Ly2.Data.Migration
{
    public abstract class DataMigration : IDataMigration
    {
        public SchemaBuilder SchemaBuilder { get; set; }

    }
}
