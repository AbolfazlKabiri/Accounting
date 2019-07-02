using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows;

namespace Accounting.Models
{
  public  class FiscalYear : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Taxable { get; set; }
        public decimal Taxes { get; set; }
        public decimal Duties { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
