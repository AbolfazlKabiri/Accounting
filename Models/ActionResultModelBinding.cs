using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Models
{
    public enum ActionResult
    {
        Success = 1,
        Failed = 0
    }
   public class ActionResultModelBinding
    {
        public string Message { get; set; }
        public ActionResult Status { get; set; }
    }
}
