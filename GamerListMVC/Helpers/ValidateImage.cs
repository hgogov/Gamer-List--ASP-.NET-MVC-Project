using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace GamerListMVC.Helpers
{
    public class ValidateImage
    {
        public static bool IsValidImage(HttpPostedFileBase file)
        {
            bool isValid = false;

            if (file == null && file.ContentLength > 1 * 1024 * 1024)
            {
                return isValid;
            }

            //if (IsFileTypeValid(file))
            //{
            //    isValid = true;
            //}

            return isValid;
        }

        public static string IsFileSizeValid(HttpPostedFileBase file)
        {
            if (file == null && file.ContentLength > 1 * 1024 * 1024)
            {
                return "true";
            }
            return "false";
        }

        public static string IsFileTypeValid(HttpPostedFileBase file)
        {
            string isValid = "false";

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    if (IsOneOfValidFormats(img.RawFormat))
                    {
                        isValid = "true";
                    }
                }
            }
            catch(Exception ex)
            {
                isValid = "false";
            }

            return isValid;
        }

        private static bool IsOneOfValidFormats(ImageFormat format)
        {
            var formats = GetValidFormats();

            foreach (var validFormat in formats)
            {
                if (format.Equals(validFormat))
                {
                    return true;
                }
            }
            return false;
        }

        private static List<ImageFormat> GetValidFormats()
        {
            var formats = new List<ImageFormat>()
            {
                ImageFormat.Png,
                ImageFormat.Jpeg
            };

            return formats;
        }
    }
}