using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamerListMVC.Helpers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        public int? MaxBytes { get; set; }

        public FileSizeAttribute(int maxBytes)
        {
            MaxBytes = maxBytes;
            if (MaxBytes.HasValue)
            {
                ErrorMessage = $"Please upload a file of less than {MaxBytes/1000000} MB.";
            }
        }

        //protected override ValidationResult IsValid(object value, ValidationContext context)
        //{
        //    HttpPostedFileBase file = value as HttpPostedFileBase;
        //    if(file != null)
        //    {
        //        //bool result = true;
        //        if (MaxBytes.HasValue)
        //        {
        //            //result &= (file.ContentLength < MaxBytes.Value);
        //            return new ValidationResult(ErrorMessageString);
        //        }
        //        //return result;
        //    }
        //    return ValidationResult.Success;
        //}

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                bool result = true;
                if (MaxBytes.HasValue)
                {
                    result &= (file.ContentLength < MaxBytes.Value);
                    
                }
                return result;
            }
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metaData, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filesize",
                //ErrorMessage = FormatErrorMessage(metaData.DisplayName)
                ErrorMessage = ErrorMessageString
            };
            //rule.ValidationParameters["maxbytes"] = MaxBytes;
            rule.ValidationParameters.Add("maxbytes", MaxBytes);
            yield return rule;
        }
    }
}