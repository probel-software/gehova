using System;
using System.Collections.Generic;
using System.Text;

namespace Probel.Lorm
{
    public static class SqlHelpers
    {
        #region Methods

        public static string ToSQLiteDateString(this DateTime src) => src.ToString("yyyy-MM-dd");

        public static string UpdateParameters(this string sql, string parameterName, int parameterCount)
        {
            var toReplace = " (";
            for (var i = 0; i < parameterCount; i++)
            {
                toReplace += $"@{parameterName}{i}, ";
            }
            sql = sql.Replace($"@{parameterName}", toReplace);

            if (sql.EndsWith(", "))
            {
                sql = sql.Remove(sql.Length - 2, 2);
            }
            sql += ")";
            return sql;
        }

        #endregion Methods
    }
}
