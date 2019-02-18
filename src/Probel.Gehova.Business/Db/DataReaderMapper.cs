using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Probel.Gehova.Business.Db
{
    public static class DataReaderMapper
    {
        #region Methods

        private static bool GetBoolean(this IDataReader reader, string columnName)
        {
            var numericValue = reader.HasColumn(columnName) ? (long)reader[columnName] : default(long);
            return (numericValue > 0);
        }

        public static IEnumerable<AbsenceDisplayModel> AsAbsenceDisplayModel(this IDataReader reader)
        {
            var result = new List<AbsenceDisplayModel>();
            while (reader.Read())
            {
                var absence = new AbsenceDisplayModel
                {
                    FirstName = (reader.HasColumn("FirstName")) ? reader["FirstName"] as string : null,
                    From = (reader.HasColumn("From")) ? DateTime.Parse((string)reader["From"]) : default(DateTime),
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    LastName = (reader.HasColumn("LastName")) ? reader["LastName"] as string : null,
                    PersonId = (reader.HasColumn("PersonId")) ? (long)reader["PersonId"] : default(long),
                    To = (reader.HasColumn("To")) ? DateTime.Parse((string)reader["To"]) : default(DateTime),
                };
                result.Add(absence);
            }
            return result;
        }

        public static IEnumerable<CategoryModel> AsCategoryModel(this IDataReader reader)
        {
            var result = new List<CategoryModel>();
            while (reader.Read())
            {
                var category = new CategoryModel
                {
                    Display = (reader.HasColumn("Display")) ? reader["Display"] as string : null,
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    Key = (reader.HasColumn("Key")) ? reader["Key"] as string : null,
                };
                result.Add(category);
            }
            return result;
        }

        internal static IEnumerable<RawPresenceWeekModel> AsPresenceWeekRaw(this IDataReader reader)
        {
            var result = new List<RawPresenceWeekModel>();
            while (reader.Read())
            {
                var category = new RawPresenceWeekModel
                {
                    Categories = (reader.HasColumn("category")) ? reader["category"] as string : null,
                    CategoryKeys = (reader.HasColumn("category_key")) ? reader["category_key"] as string : null,
                    Day = (reader.HasColumn("day")) ? (long)reader["day"] : default(long),
                    FirstName = (reader.HasColumn("first_name")) ? reader["first_name"] as string : null,
                    LastName = (reader.HasColumn("last_name")) ? reader["last_name"] as string : null,
                    PersonId = (reader.HasColumn("person_id")) ? (long)reader["person_id"] : default(long),
                    PickupRound = (reader.HasColumn("pickup_round")) ? reader["pickup_round"] as string : null,
                    PickupRoundId = (reader.HasColumn("pickup_round_id")) ? (long)reader["pickup_round_id"] : default(long),
                    ReceptionGroupId = (reader.HasColumn("reception_group_id")) ? (long)reader["reception_group_id"] : default(long),
                    ReceptionId = (reader.HasColumn("reception_id")) ? (long)reader["reception_id"] : default(long),
                    Team = (reader.HasColumn("team")) ? reader["team"] as string : null,
                    TeamId = (reader.HasColumn("team_id")) ? (long)reader["team_id"] : default(long),
                };
                result.Add(category);
            }
            return result;
        }

        public static IEnumerable<long> AsLong(this IDataReader reader)
        {
            var result = new List<long>();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    var str = reader.GetString(0);
                    if (long.TryParse(str, out var lg))
                    {
                        result.Add(lg);
                    }
                }
            }

            return result;
        }

        public static IEnumerable<PersonDisplayModel> AsPersonDisplayModel(this IDataReader reader)
        {
            var result = new List<PersonDisplayModel>();
            while (reader.Read())
            {
                var person = new PersonDisplayModel
                {
                    Category = (reader.HasColumn("Category")) ? reader["Category"] as string : null,
                    FirstName = (reader.HasColumn("FirstName")) ? reader["FirstName"] as string : null,
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    CategoryKey = (reader.HasColumn("CategoryKey")) ? reader["CategoryKey"] as string : null,
                    LastName = (reader.HasColumn("LastName")) ? reader["LastName"] as string : null,
                };
                result.Add(person);
            }
            return result;
        }

        public static IEnumerable<PersonFullDisplayModel> AsPersonFullDisplayModel(this IDataReader reader)
        {
            var result = new List<PersonFullDisplayModel>();
            while (reader.Read())
            {
                var person = new PersonFullDisplayModel
                {
                    Category = (reader.HasColumn("Category")) ? reader["Category"] as string : null,
                    FirstName = (reader.HasColumn("FirstName")) ? reader["FirstName"] as string : null,
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    CategoryKey = (reader.HasColumn("CategoryKey")) ? reader["CategoryKey"] as string : null,
                    LastName = (reader.HasColumn("LastName")) ? reader["LastName"] as string : null,
                    ///
                    //CategoryIds = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    PickupRound = (reader.HasColumn("PickupRound")) ? reader["PickupRound"] as string : null,
                    PickupRoundId = (reader.HasColumn("PickupRoundId")) ? (long)reader["PickupRoundId"] : default(long),
                    Team = (reader.HasColumn("Team")) ? reader["Team"] as string : null,
                    TeamId = (reader.HasColumn("TeamId")) ? (long)reader["TeamId"] : default(long),
                };
                result.Add(person);
            }
            return result;
        }

        public static IEnumerable<GroupDisplayModel> AsPickupRoundDisplayModel(this IDataReader reader)
        {
            var result = new List<GroupDisplayModel>();
            while (reader.Read())
            {
                var pickup = new GroupDisplayModel
                {
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    Name = (reader.HasColumn("Name")) ? reader["Name"] as string : null,
                };
                result.Add(pickup);
            }
            return result;
        }

        public static IEnumerable<ReceptionModel> AsReceptionModel(this IDataReader reader)
        {
            var result = new List<ReceptionModel>();
            while (reader.Read())
            {
                var category = new ReceptionModel
                {
                    ReceptionName = (reader.HasColumn("Name")) ? reader["Name"] as string : null,
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                };
                result.Add(category);
            }
            return result;
        }

        public static IEnumerable<SettingModel> AsSettingModel(this IDataReader reader)
        {
            var result = new List<SettingModel>();
            while (reader.Read())
            {
                var setting = new SettingModel
                {
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    Key = (reader.HasColumn("Key")) ? reader["Key"] as string : null,
                    Value = (reader.HasColumn("Value")) ? reader["Value"] as string : null,
                };
                result.Add(setting);
            }
            return result;
        }

        public static IEnumerable<string> AsString(this IDataReader reader)
        {
            var result = new List<string>();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0)) { result.Add(reader.GetString(0)); }
            }

            return result;
        }

        public static IEnumerable<GroupDisplayModel> AsTeamDisplayModel(this IDataReader reader)
        {
            var result = new List<GroupDisplayModel>();
            while (reader.Read())
            {
                var team = new GroupDisplayModel
                {
                    Id = (reader.HasColumn("Id")) ? (long)reader["Id"] : default(long),
                    Name = (reader.HasColumn("Name")) ? reader["Name"] as string : null,
                };
                result.Add(team);
            }
            return result;
        }

        public static IEnumerable<WeekDay> AsWeekday(this IDataReader reader)
        {
            var result = new List<WeekDay>();

            while (reader.Read())
            {
                var weekday = new WeekDay
                {
                    Day = (reader.HasColumn("Day")) ? reader["Day"] as string : null,
                    Team = (reader.HasColumn("Team")) ? reader["Team"] as string : null,
                    FirstName = (reader.HasColumn("FirstName")) ? reader["FirstName"] as string : null,
                    LastName = (reader.HasColumn("LastName")) ? reader["LastName"] as string : null,
                    PickupRound = (reader.HasColumn("PickupRound")) ? reader["PickupRound"] as string : null,
                    Categories = (reader.HasColumn("Categories")) ? reader["Categories"] as string : null,
                };
                result.Add(weekday);
            };
            return result;
        }

        public static bool HasColumn(this IDataReader reader, string columnName)
        {
            foreach (DataRow row in reader.GetSchemaTable().Rows)
            {
                if (row["ColumnName"].ToString() == columnName)
                {
                    if (reader[columnName] != DBNull.Value) { return true; }
                }
            } //Still here? Column not found.
            return false;
        }

        #endregion Methods
    }
}