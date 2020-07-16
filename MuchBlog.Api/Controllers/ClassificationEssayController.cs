using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Api.Dtos;
using MuchBlog.Api.Filters;
using MuchBlog.Domain.Dtos;
using MuchBlog.Domain.Services.IService;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/classifications/{classificationId}/essays")]
    [ServiceFilter(typeof(CheckClassificationExistFilterAttribute))]
    public class ClassificationEssayController:ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public ClassificationEssayController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentException(nameof(repositoryWrapper));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EssayDto>>> GetClassificationsAsync(Guid classificationId)
        {
            var classifications = await _repositoryWrapper.Essay.GetEssaysByClassificationId(classificationId);
            var classificationDtoList = _mapper.Map<IEnumerable<EssayDto>>(classifications);
            return Ok(classificationDtoList);
        }
    }
}