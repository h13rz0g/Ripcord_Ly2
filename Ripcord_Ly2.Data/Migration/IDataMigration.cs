using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql.Core.Sql;

namespace Ripcord_Ly2.Data.Migration
{
    public interface IDataMigration
    {
        SchemaBuilder SchemaBuilder { get; set; }
    }
}
