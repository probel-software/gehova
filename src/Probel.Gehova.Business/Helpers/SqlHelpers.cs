using System;
using System.Collections.Generic;
using System.Text;

namespace Probel.Gehova.Business.Helpers
{
    public static class SqlHelpers
    {
        public static string ToSQLiteDate(this DateTime src) => src.ToString("yyyy-MM-dd");
    }
}
