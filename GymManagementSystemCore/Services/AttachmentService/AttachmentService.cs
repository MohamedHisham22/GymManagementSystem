using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long maxAllowedSize = 10 * 1024 * 1024; //10mb max

        public string? UploadMemberPhoto(string folderName, IFormFile file)
        {
            try 
            {
                if (string.IsNullOrEmpty(folderName) || file is null || file.Length == 0) return null;
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension)) return null;
                if (file.Length > maxAllowedSize) return null;
                var PhotoStoringLocation = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folderName); // wwwroort/uploads/memberProfile
                if (!Directory.Exists(PhotoStoringLocation))
                {
                    Directory.CreateDirectory(PhotoStoringLocation);
                }
                var FullUniqueFileName = Guid.NewGuid().ToString() + extension;
                var fullPath = Path.Combine(PhotoStoringLocation, FullUniqueFileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);
                return FullUniqueFileName;
            }
            catch 
            {
                return null;
            }
            
        }
        public bool DeleteMemberPhoto(string folderName, string fileName)
        {
            try 
            {
                if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileName)) return false;

                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folderName, fileName);

                if (!File.Exists(photoPath)) return false;
                File.Delete(photoPath);
                return true;
            }
            catch 
            {
                return false;
            }
           
        }

    }
}
