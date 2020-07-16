using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Api.Dtos;
using MuchBlog.Api.Filters;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/essays/{essayId}/essayimages")]
    [ServiceFilter(typeof(CheckEssayExistFilterAttribute))]
    public class EssayImageController:ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public EssayImageController(
            IWebHostEnvironment webHostEnvironment, 
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        [HttpGet("{essayImageId}", Name = nameof(GetEssayImageAsync))]
        public async Task<ActionResult<EssayImageDto>> GetEssayImageAsync(Guid userId, Guid essayId, Guid essayImageId)
        {
            var essayImage = await _repositoryWrapper.EssayImage.GetByIdAsync(userId, essayId, essayImageId);
            if (essayImage==null)
            {
                return NotFound();
            }
            var essayImageDto = _mapper.Map<EssayImageDto>(essayImage);
            return Ok(essayImageDto);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Guid userId, Guid essayId, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File is null");
            }

            if (file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                return BadRequest("File size cannot exceed 10M");
            }
            var acceptTypes = new[] { ".jpg", ".jpeg", ".png" };
            if (acceptTypes.All(t => t != Path.GetExtension(file.FileName).ToLower()))
            {
                return BadRequest("File type not valid, only jpg and png are acceptable.");
            }
            var essay = await _repositoryWrapper.Essay.GetByIdAsync(userId, essayId);
            //创建wwwroot/EssayImages根路径
            var uploadsFolderPath = Path
                .Combine(_webHostEnvironment.WebRootPath, "EssayImages", essay.User.UserName);
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var essayImageId = Guid.NewGuid();
            
            var fileName = essayImageId + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var essayImage = new EssayImage
            {
                Id = essayImageId,
                FileName = fileName,
                EssayId = essayId,
                Essay = essay
            };

            _repositoryWrapper.EssayImage.Create(essayImage);
            if (!await _repositoryWrapper.EssayImage.SaveAsync())
            {
                throw new Exception("Create EssayImage Failed.");
            }

            var essayImageDto = _mapper.Map<EssayImageDto>(essayImage);
            return CreatedAtRoute(nameof(GetEssayImageAsync), new {userId, essayId, essayImageId}, essayImageDto);
        }
    }
}