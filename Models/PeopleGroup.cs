﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
namespace Accounting.Models
{
    public class PeopleGroup : IValidatableObject
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public DateTime SaveDateTime { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrWhiteSpace(Code))
            {
                yield return new ValidationResult(Application.Current.FindResource("validationPeople_codeIsEmpty") as string);
            }
             if(string.IsNullOrWhiteSpace(Title))
            {
                yield return new ValidationResult(Application.Current.FindResource("validationPeople_titleIsEmpty") as string);
            }
        }
    }
}
