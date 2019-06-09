using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Accounting.Models
{
   public class TafsiliGroup:IValidatableObject
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public bool Editable { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
              if (string.IsNullOrWhiteSpace(Code))
                yield return new ValidationResult(Application.Current.FindResource("validation_codeIsEmpty") as string);
            if(string.IsNullOrWhiteSpace(Title))
                yield return new ValidationResult(Application.Current.FindResource("validation_titleIsEmpty") as string);
        }
    }
}
