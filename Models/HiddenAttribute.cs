using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Models
{
    [AttributeUsage(AttributeTargets.Property)]
   public class HiddenAttribute:System.Attribute
    {
        public HiddenAttribute()
        {
        }
    }
}
