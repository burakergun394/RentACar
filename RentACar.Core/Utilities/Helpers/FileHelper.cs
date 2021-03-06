using Microsoft.AspNetCore.Http;
using RentACar.Core.Utilities.Business;
using RentACar.Core.Utilities.Messages;
using RentACar.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Utilities.Helpers
{
    public static class FileHelper
    {
        private static string CurrentDirectory = "wwwroot";
        private static string SubFolderName = "images";
        public static IResult Add(IFormFile file)
        {
            var result = BusinessRules.Run(CheckIsFileEmpty(file), CheckFileTypeValid(Path.GetExtension(file.FileName)));

            if (result != null)
                return result;

            var path = NewPath(file);
     
            return new SuccessResult(path);
        }

        public static IResult Delete(string file)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"{CurrentDirectory}/", file).Replace("/", "\\");

            var result = BusinessRules.Run(CheckFileExist(path));

            if (result != null)
                return result;

            System.IO.File.Delete(path);

            return new SuccessResult();
        }

        public static IResult Update(IFormFile file, string oldFile)
        {
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), $"{CurrentDirectory}/", oldFile).Replace("/", "\\");

            var result = BusinessRules.Run(CheckFileExist(oldPath), CheckIsFileEmpty(file), CheckFileTypeValid(Path.GetExtension(file.FileName)));

            if (result != null)
                return result;

            System.IO.File.Delete(oldPath);

            var path = NewPath(file);

            return new SuccessResult(path);
        }

        private static string NewPath(IFormFile file)
        {
            var randomFileNameWithoutExtension = $"{Guid.NewGuid()}_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}";

            var randomFileName = randomFileNameWithoutExtension + Path.GetExtension(file.FileName).ToString();

            var path = Path.Combine(Directory.GetCurrentDirectory(), $"{CurrentDirectory}/{SubFolderName}", randomFileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{SubFolderName}/{randomFileName}";
        }

        private static IResult CheckIsFileEmpty(IFormFile file)
        {
            if (file.Length == 0 && file == null)
                return new ErrorResult(FileMessages.FileEmpty);

            return new SuccessResult();
        }

        private static IResult CheckFileTypeValid(string type)
        {
            if (type == ".jpg" || type == ".jpeg" || type == ".png")
                return new SuccessResult();

            return new ErrorResult(FileMessages.InvalidType); ;
        } 

        private static IResult CheckFileExist(string path)
        {
            if (System.IO.File.Exists(path))
                return new SuccessResult();

            return new ErrorResult(FileMessages.NotFound);
        }
    }
}
