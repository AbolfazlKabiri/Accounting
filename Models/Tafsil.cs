using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows;

namespace Accounting.Models
{
    class Tafsil : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string TitleInAutomateDocument { get; set; }
        public bool IsActive { get; set; }
        public bool IsCostCenter { get; set; }
        public int EntityTypeId {get;set; }
        public string TafsiliGroupBindingString { get; set; }
        public DataTable TafsiliGroupBinding { get; set; }
        public string PeopleGroupBindingString { get; set; }
        public DataTable PeopleGroupBinding { get; set; }
        public string TafsilMap { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }
        public string EconomyCode { get; set; }
        public short AccountTypeId { get; set; }
        public decimal MaxCredit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Code))
                yield return new ValidationResult(Application.Current.FindResource("validation_codeIsEmpty") as string);
            if(string.IsNullOrWhiteSpace(Title))
                yield return new ValidationResult(Application.Current.FindResource("validation_titleIsEmpty") as string);
            if(EntityTypeId == 0)
                yield return new ValidationResult(Application.Current.FindResource("validation_invalidEntityTypeId") as string);
            

        }
    }
}
