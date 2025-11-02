using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.AttachmentService
{
    public interface IAttachmentService
    {
        string? UploadMemberPhoto(string folderName , IFormFile file); 

        bool DeleteMemberPhoto(string folderName , string fileName);
    }
}
