﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamerListMVC.Helpers
{
    public class FileTypeAttribute : ValidationAttribute, IClientValidatable
    {
        private const string _DefaultErrorMessage = "Only the following file types are allowed: {0}";
        private IEnumerable<string> _ValidTypes { get; set; }

        public FileTypeAttribute(string validTypes)
        {
            _ValidTypes = validTypes.Split(',').Select(s => s.Trim().ToLower());
            ErrorMessage = string.Format(_DefaultErrorMessage, string.Join(" , ", _ValidTypes));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                if (file != null && !_ValidTypes.Any(e => file.FileName.EndsWith(e)))
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filetype",
                ErrorMessage = ErrorMessageString
            };
            rule.ValidationParameters.Add("validtypes", string.Join(",", _ValidTypes));
            yield return rule;
        }
    }
}