using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Accounting.Controllers
{
   public class NatureController:Repository.Repository
    {
        public List<Models.Nature> GetNatures()
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getNatureList",true))
                {
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    return (info.Select(i => new Models.Nature
                    {
                        Id = Convert.ToInt16(i.Field<object>("id")),
                        Title = Convert.ToString(i.Field<object>("title"))
                    })).ToList();
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
                return null;
            }
            
        }
    }
}
