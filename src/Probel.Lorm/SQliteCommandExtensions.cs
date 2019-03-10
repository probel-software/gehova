using Microsoft.Data.Sqlite;
using Microsoft.HockeyApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Lorm
{
    public static class SQliteCommandExtensions
    {
        #region Fields

        private static readonly LormConfigurator _configurator = new LormConfigurator();

        #endregion Fields

        #region Methods

        private static DbType GetDbType(Type type)
        {
            DbType result;

            if (type == typeof(string)) { result = DbType.String; }
            else if (type == typeof(DateTime)) { result = DbType.Date; }
            else if (type == typeof(long)) { result = DbType.Int64; }
            else if (type == typeof(bool)) { result = DbType.Boolean; }
            else { throw new NotSupportedException($"The argument type '{type}' is not supported"); }

            return result;
        }

        private static SqliteParameter GetParameter<TType>(string parameterName, TType value)
        {
            var r = new SqliteParameter
            {
                Value = value,
                ParameterName = parameterName,
                DbType = GetDbType(typeof(TType))
            };
            return r;
        }

        private static SqliteParameter GetParameter(string parameterName, object value, DbType dbtype)
        {
            var r = new SqliteParameter
            {
                Value = value,
                ParameterName = parameterName,
                DbType = dbtype
            };
            return r;
        }

        private static IDbCommand PrepareQuery(IDbConnection connection, string sql, object parameters = null)
        {
            var obj = parameters;
            var cmd = CreateCommand(connection, sql);

            if (connection.State != ConnectionState.Open) { connection.Open(); }
            if (parameters != null)
            {
                foreach (var p in obj.GetType().GetProperties())
                {
                    var value = p.GetValue(obj, null);
                    var name = p.Name;
                    cmd.AddParameter(name, value, value.GetType());
                }
            }
            return cmd;
        }

        public static void AddParameter(this IDbCommand cmd, string parameterName, object value, DbType dbtype)
        {
            if (cmd is SqliteCommand c)
            {
                c.Parameters.Add(GetParameter(parameterName, value, dbtype));
            }
            else { throw new NotSupportedException($"Command of type is '{cmd.GetType()}' is not supported."); }
        }

        public static void AddParameter(this IDbCommand cmd, string parameterName, object value, Type type) => AddParameter(cmd, parameterName, value, GetDbType(type));

        public static void AddParameter<TValue>(this IDbCommand cmd, string parameterName, TValue value)
        {
            if (cmd is SqliteCommand c)
            {
                c.Parameters.Add(GetParameter(parameterName, value));
            }
            else { throw new NotSupportedException($"Command of type is '{cmd.GetType()}' is not supported."); }
        }

        public static void AddParameters<TValue>(this IDbCommand cmd, string parameterName, IEnumerable<TValue> values)
        {
            cmd.CommandText = cmd.CommandText.UpdateParameters(parameterName, values.Count());
            for (var i = 0; i < values.Count(); i++)
            {
                cmd.AddParameter($"{parameterName}{i}", values.ElementAt(i));
            }
        }

        public static IDbCommand CreateCommand(this IDbConnection connection, string sql)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            return cmd;
        }

        public static void Execute(this IDbConnection connection, string sql, dynamic parameters = null)
        {
            using (var cmd = PrepareQuery(connection, sql, (object)parameters))
            {
                try { cmd.ExecuteNonQuery(); }
                catch (Exception ex)
                {
                    HockeyClient.Current.TrackException(ex);
                    throw;
                }
            }
        }

        public static IEnumerable<TResult> Query<TResult>(this IDbConnection connection, string sql, dynamic parameters = null)
        {
            using (var cmd = PrepareQuery(connection, sql, (object)parameters))
            {
                try
                {
                    var r = cmd.ExecuteReader();
                    var mapper = _configurator.Get<TResult>();
                    return (IEnumerable<TResult>)mapper(r);
                }
                catch (Exception ex)
                {
                    HockeyClient.Current.TrackException(ex);
                    throw;
                }
            }
        }

        public static TResult Scalar<TResult>(this IDbConnection connection, string sql, dynamic parameters = null)
        {
            using (var cmd = PrepareQuery(connection, sql, (object)parameters))
            {
                var r = cmd.ExecuteScalar();
                return (TResult)r;
            }
        }

        #endregion Methods
    }
}