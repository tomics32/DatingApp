﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Application.interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
