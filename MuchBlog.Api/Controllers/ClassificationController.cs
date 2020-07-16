using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Domain.Dtos;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/classifications")]
    public class ClassificationController:ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public ClassificationController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentException(nameof(repositoryWrapper));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassificationDto>>> GetClassificationsAsync()
        {
            var classifications = await _repositoryWrapper.Classification.GetAllAsync();
            var classificationDtoList = _mapper.Map<IEnumerable<ClassificationDto>>(classifications);
            return Ok(classificationDtoList);
        }
        [HttpGet("{classificationId}",Name = nameof(GetClassificationAsync))]
        public async Task<ActionResult<IEnumerable<ClassificationDto>>> GetClassificationAsync(Guid classificationId)
        {
            var classification = await _repositoryWrapper.Classification.GetByIdAsync(classificationId);
            if (classification==null)
            {
                return NotFound();
            }
            var classificationDto = _mapper.Map<ClassificationDto>(classification);

            return Ok(classificationDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClassification(ClassificationForCreateDto classificationForCreate)
        {
            var classification = _mapper.Map<Classification>(classificationForCreate);
            classification.Id = Guid.NewGuid();
            _repositoryWrapper.Classification.Create(classification);
            if (!await _repositoryWrapper.Classification.SaveAsync())
            {
                throw new Exception("Create Classification Failed.");
            }

            var classificationDto = _mapper.Map<ClassificationDto>(classification);
            return CreatedAtRoute(nameof(GetClassificationAsync), new {classificationId = classification.Id},
                classificationDto);
        }
        [HttpPut("{classificationId}")]
        public async Task<IActionResult> UpdateClassification(Guid classificationId, ClassificationForUpdateDto classificationForUpdate)
        {
            var classification = await _repositoryWrapper.Classification.GetByIdAsync(classificationId);
            if (classification==null)
            {
                return NotFound();
            }
            _mapper.Map(classificationForUpdate, classification, typeof(ClassificationForUpdateDto), typeof(Classification));
            _repositoryWrapper.Classification.Update(classification);
            if (!await _repositoryWrapper.Classification.SaveAsync())
            {
                throw new Exception("Update Classification Failed.");
            }
            return NoContent();
        }
        [HttpDelete("{classificationId}")]
        public async Task<IActionResult> DeleteClassification(Guid classificationId)
        {
            var classification = await _repositoryWrapper.Classification.GetByIdAsync(classificationId);
            if (classification == null)
            {
                return NotFound();
            }
            _repositoryWrapper.Classification.Delete(classification);
            if (!await _repositoryWrapper.Classification.SaveAsync())
            {
                throw new Exception("Update Classification Failed.");
            }
            return NoContent();
        }
    }
}