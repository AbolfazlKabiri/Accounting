using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows;

namespace Accounting.Models
{
   public class Moin:IValidatableObject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int KolId { get; set; }
        public string KolTitle { get; set; }
        public short NatureId { get; set; }
        public string KolCode { get; set; }
        public string NatureTitle { get; set; }
        public DateTime SaveDateTime { get; set; }
        public bool IsDefault { get; set; }
        public string TafsiliGroupBindingString { get; set; }
        public string MoinMap { get; set; }
        public bool IsPermanent { get; set; }
        public DataTable TafsiliGroupBinding { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
             if (string.IsNullOrWhiteSpace(Code))
                yield return new ValidationResult(Application.Current.FindResource("validation_codeIsEmpty") as string);
            if(string.IsNullOrWhiteSpace(Title))
                yield return new ValidationResult(Application.Current.FindResource("validation_titleIsEmpty") as string);
            if(NatureId <= 0)
                yield return new ValidationResult(Application.Current.FindResource("validation_invalidNatureId") as string);
            if(KolId <= 0)
                yield return new ValidationResult(Application.Current.FindResource("validation_invalidGroupId") as string);

        }

        public void SetDataTable(List<long> list)
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
            TafsiliGroupBinding = dt;
        }

    }
}
