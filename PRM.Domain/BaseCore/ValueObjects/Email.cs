using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PRM.Domain.BaseCore.ValueObjects
{
    public class Email : ValueObject
    {
        public string Address { get; private set; }
        
        private Email() {}

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ValidationException("Email is required!");
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None,
                    TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    var domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw new ValidationException("Timeout Validating Email! \n" + ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ValidationException("Invalid Email! \n" + e.Message);
            }

            if (!EmailValidations.IsValidEmail(email)) throw new ValidationException("Invalid Email!");
            Address = email;
        }

        


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
        }
    }
    
    public static class EmailValidations 
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}