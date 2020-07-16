using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Api.Filters;
using MuchBlog.Domain.Dtos;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/essays/{essayId}/classifications")]
    [ServiceFilter(typeof(CheckEssayExistFilterAttribute))]
    public class EssayClassificationController:ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public EssayClassificationController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentException(nameof(repositoryWrapper));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassificationDto>>> GetClassificationsAsync(Guid essayId)
        {
            var classifications = await _repositoryWrapper.Classification.GetAllAsync(essayId);
            var classificationDtoList = _mapper.Map< IEnumerable<ClassificationDto>>(classifications);
            return Ok(classificationDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> SelectClassification(Guid essayId, string[] className)
        {
            var addClassifications = await _repositoryWrapper.Classification.FindClassificationsByName(className);
            var classifications = await _repositoryWrapper.Classification.GetAllAsync(essayId);
            if (!addClassifications.Any(x=>classifications.Any(y=>y.Name!=x.Name)))
            {
                return NoContent();
            }
            List< EssayClassification > addList = new List<EssayClassification>();
            List< EssayClassification > deleteList = new List<EssayClassification>();
            foreach (var classification in addClassifications)
            {
                if (!classifications.Any(y=>y.Id== classification.Id))
                {
                    addList.Add(new EssayClassification
                    {
                        EssayId = essayId,
                        ClassificationId = classification.Id
                    });
                }
            }
            foreach (var classification in classifications)
            {
                if (!addClassifications.Any(y => y.Id == classification.Id))
                {
                    deleteList.Add(new EssayClassification
                    {
                        EssayId = essayId,
                        ClassificationId = classification.Id
                    });
                }
            }

            if (addClassifications.Count()==0&&deleteList.Count==0)
            {
                return NoContent();
            }
            _repositoryWrapper.Classification.SelectClassification(addList, deleteList);
            if (!await _repositoryWrapper.Classification.SaveAsync())
            {
                throw new Exception("Create Relation Failed.");
            }

            return NoContent();
        }
    }
}