using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Models
{
   public static class Common
    {
        public enum SelectorType
        {
            GroupAccounts,
            KolAccounts,
            TafsiliGroup,
            PeopleGroup
        }

         public static DataTable SetDataTable(List<long> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            DataRow row;
            foreach (var item in list)
            {
                row = dt.NewRow();
                row["id"] = item;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
