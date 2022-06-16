using FreeCource.Services.PhotoStock.DTOS;
using FreeCource.Shared.ControllerBases;
using FreeCource.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCource.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        //cancellationToken eger request gelen hissede browser sondurulerse photo save prossesi yarida buraxilsin deye qebul edilir ozu avtomatik error atacaq
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);
                var returnPath = "photos/" + photo.FileName;
                return CreateActionResultInstance<PhotoClassDto>(Response<PhotoClassDto>.Success(new PhotoClassDto { url = returnPath },200));
            }
            else
            {
                return CreateActionResultInstance<PhotoClassDto>(Response<PhotoClassDto>.Fail("Photo is empty",400));
            }
        }
        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Directory.GetCurrentDirectory()+ "/wwwroot/photos"+ photoUrl;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return CreateActionResultInstance(Response<NoContent>.Success(204));
            }
            return CreateActionResultInstance(Response<NoContent>.Fail("photo not found!", 404));
        }
    }
}
