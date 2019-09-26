using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;    

namespace Vidly.Models
{
    public class ValidationAge18Attribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId==MembershipType.Unknown|| customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if(customer.Birthdate == null)
            {
                return new ValidationResult("Bith date required");
            }
            if (!Is18(customer.Birthdate))
            {
                return new ValidationResult("Yo must be at leat 18 to be a member");
            }
            return ValidationResult.Success;
        }

        private bool Is18(DateTime? birthDate)
        {
            int year = DateTime.Now.Year-18;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day+1;
            DateTime date = new DateTime(year, month, day);

            return birthDate < date;
        }
    }
}