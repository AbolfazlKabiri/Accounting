using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Models
{
   public class TafsilAccountTemplateModelBinding:IValidatableObject
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string EntityTitle { get; set; }
        public long TafsiliGroupId { get; set; }
        public string TafsiliGroupTitle { get; set; }
        public int PeopleGroupId { get; set; }
        public string PeopleGroupTitle { get; set; }
        public string Path { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(EntityId == 0)
                yield return new ValidationResult("");
            if(TafsiliGroupId == 0)
                yield return new ValidationResult("");
           
        }
    }
}
