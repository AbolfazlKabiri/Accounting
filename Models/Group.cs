using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Accounting.Models
{
   public class Group: IValidatableObject
    {
        [Display(Name = "شناسه حساب")]
        [Hidden]
        public int Id { get; set; }
        [Display(Name ="كد حساب")]
        public string Code { get; set; }
        [Display(Name ="عنوان حساب")]
        public string Title { get; set; }
        [Display(Name ="شناسه ماهيت")]
        [Hidden]
        public short NatureId { get; set; }
        [Display(Name = "ماهيت حساب")]
        public string NatureTitle { get; set; }
        [Hidden]
        public DateTime SaveDateTime { get; set; }
        [Display(Name = "حساب پيشفرض")]
        public bool IsDefault { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Code))
                yield return new ValidationResult(Application.Current.FindResource("validation_codeIsEmpty") as string);
            if(string.IsNullOrWhiteSpace(Title))
                yield return new ValidationResult(Application.Current.FindResource("validation_titleIsEmpty") as string);
            if(NatureId <= 0)
                yield return new ValidationResult(Application.Current.FindResource("validation_invalidNatureId") as string);

        }
    }
}
