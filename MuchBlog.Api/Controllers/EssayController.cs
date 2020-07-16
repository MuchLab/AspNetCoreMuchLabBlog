using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Api.Dtos;
using MuchBlog.Api.Filters;
using MuchBlog.Domain.Helpers;
using MuchBlog.Domain.Helpers.Parameters;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Entities;
using Newtonsoft.Json;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/essays")]
    [ServiceFilter(typeof(CheckUserExistFilterAttribute))]
    public class EssayController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public EssayController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new Exception(nameof(repositoryWrapper));
            _mapper = mapper ?? throw new Exception(nameof(mapper));
        }

        [HttpGet(Name = nameof(GetEssaysAsync))]
        public async Task<ActionResult<IEnumerable<EssayDto>>> GetEssaysAsync(Guid userId, [FromQuery]EssayResourceParameters parameters)
        {
            var essayList = await _repositoryWrapper.Essay.GetEssays(userId, parameters);
            CreatePagedMeta(essayList, userId);
            var essayDtoList = _mapper.Map<IEnumerable<EssayDto>>(essayList);
            var essayDtoListWithPagination = new
            {
                data = essayDtoList,
                meta = new
                {
                    total = _repositoryWrapper.Essay.EssaysCount(userId),
                    page_size = parameters.PageSize,
                    page_number = parameters.PageNumber
                }
            };
            return Ok(essayDtoListWithPagination);
        }

        [HttpGet("{essayId}", Name = nameof(GetEssayAsync))]
        public async Task<ActionResult<EssayDto>> GetEssayAsync(Guid userId, Guid essayId)
        {
            var essay = await _repositoryWrapper.Essay.GetByIdAsync(userId, essayId);
            
            if (essay == null)
            {
                return NotFound();
            }

            var essayDto = _mapper.Map<EssayDto>(essay);
            return Ok(essayDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEssay(Guid userId, EssayForCreationDto essayForCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var essay = _mapper.Map<Essay>(essayForCreationDto);
            essay.Id = Guid.NewGuid();
            essay.CreateDate = essay.LastModified = DateTimeOffset.Now;
            essay.UserId = userId;
            _repositoryWrapper.Essay.Create(essay);
            if (!await _repositoryWrapper.Essay.SaveAsync())
            {
                throw new Exception("Create Essay Failed.");
            }

            var essayDto = _mapper.Map<EssayDto>(essay);
            return CreatedAtRoute(nameof(GetEssayAsync), new {userId, essayId = essay.Id}, essayDto);
        }

        [HttpPut("{essayId}")]
        public async Task<IActionResult> UpdateEssay(Guid userId, Guid essayId, EssayForUpdateDto essayForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var essay = await _repositoryWrapper.Essay.GetByIdAsync(userId, essayId);
            if (essay == null)
            {
                return NotFound();
            }

            _mapper.Map(essayForUpdateDto, essay, typeof(EssayForUpdateDto), typeof(Essay));
            essay.LastModified = DateTimeOffset.Now;
            _repositoryWrapper.Essay.Update(essay);
            if (!await _repositoryWrapper.Essay.SaveAsync())
            {
                throw new Exception("Update Essay Failed.");
            }

            return NoContent();
        }
        [HttpPatch("{essayId}")]
        public async Task<IActionResult> PartiallyUpdateEssay(Guid userId, Guid essayId, JsonPatchDocument<EssayForUpdateDto> patchDocument)
        {
            var essay = await _repositoryWrapper.Essay.GetByIdAsync(userId, essayId);
            var essayToPatch = _mapper.Map<EssayForUpdateDto>(essay);
            patchDocument.ApplyTo(essayToPatch, error =>
            {
                ModelState.AddModelError("EssayPatchError", error.ErrorMessage);
            });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(essayToPatch, essay, typeof(EssayForUpdateDto), typeof(Essay));
            essay.LastModified = DateTimeOffset.Now;
            if (!await _repositoryWrapper.Essay.SaveAsync())
            {
                throw new Exception("Update Essay Failed.");
            }

            return NoContent();
        }

        [HttpDelete("{essayId}")]
        public async Task<IActionResult> DeleteEssay(Guid userId, Guid essayId)
        {
            var essay = await _repositoryWrapper.Essay.GetByIdAsync(userId, essayId);
            if (essay == null)
            {
                return NotFound();
            }
            _repositoryWrapper.Essay.Delete(essay);
            if (!await _repositoryWrapper.Essay.SaveAsync())
            {
                throw new Exception("Delete Essay Failed");
            }

            return NoContent();
        }
        private void CreatePagedMeta(PageList<Essay> pagedList, Guid userId)
        {
            var paginationMetadata = new
            {
                totalCount = pagedList.TotalCount,
                pageSize = pagedList.PageSize,
                currentPage = pagedList.CurrentPage,
                totalPages = pagedList.TotalPages,
                previousPageLink = pagedList.HasPrevious ? Url.Link(nameof(GetEssaysAsync), new
                {
                    userId,
                    pageNumber = pagedList.CurrentPage - 1,
                    pageSize = pagedList.PageSize,
                }) : null,
                nextPageLink = pagedList.HasNext ? Url.Link(nameof(GetEssaysAsync), new
                {
                    userId,
                    pageNumber = pagedList.CurrentPage + 1,
                    pageSize = pagedList.PageSize
                }) : null
            };
            //将分页元数据添加到消息响应头中
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
        }
    }
}