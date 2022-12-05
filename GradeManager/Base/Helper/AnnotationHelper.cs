using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Base.Helper
{
    public class AnnotationHelper
    {
        /*
         * https://stackoverflow.com/questions/45667126/how-to-get-table-name-of-mapped-entity-in-entity-framework-core
         */
        private static string GetName(IEntityType entityType, string defaultSchemaName = "dbo")
        {
            //var schemaName = entityType.GetSchema();
            //var tableName = entityType.GetTableName();
            var schema = entityType.FindAnnotation("Relational:Schema").Value;
            string tableName = entityType.GetAnnotation("Relational:TableName").Value.ToString();
            string schemaName = schema == null ? defaultSchemaName : schema.ToString();
            string name = string.Format("[{0}].[{1}]", schemaName, tableName);
            return name;

        }

        public static string TableName<T>(DbSet<T> dbSet) where T : class
        {
            var entityType = dbSet.EntityType;
            return GetName(entityType);
        }
    }
}
