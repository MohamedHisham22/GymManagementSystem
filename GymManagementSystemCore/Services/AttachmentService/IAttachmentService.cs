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
        string? UploadMemberPhoto(string folderName , IFormFile file); //if the directory will be the same for all kind of uploads you can remove folderName parameter and set them directly in the implementation class because they will be static and will never change

        bool DeleteMemberPhoto(string folderName , string fileName);
    }
}
