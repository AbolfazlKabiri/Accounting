using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Accounting.Models
{
   public class Kol:IValidatableObject
    {
        
        public int Id { get; set; }
       
        public string Code { get; set; }
       
        public string Title { get; set; }
       
        public short NatureId { get; set; }
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }
        public string GroupCode { get; set; }
        public string NatureTitle { get; set; }
     
        public DateTime SaveDateTime { get; set; }
        
        public bool IsDefault { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Code))
                yield return new ValidationResult(Application.Current.FindResource("validation_codeIsEmpty") as string);
            if(string.IsNullOrWhiteSpace(Title))
                yield return new ValidationResult(Application.Current.FindResource("validation_titleIsEmpty") as string);
            if(NatureId <= 0)
                yield return new ValidationResult(Application.Current.FindResource("validation_invalidNatureId") as string);
            if(GroupId <= 0)
                yield return new ValidationResult(Application.Current.FindResource("validation_invalidGroupId") as string);

        }
    }
}
