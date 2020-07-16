using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Api.Dtos;
using MuchBlog.Domain.Helpers;
using MuchBlog.Domain.Helpers.Parameters;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Domain.Services.IServices;
using MuchBlog.Infrastructure.Entities;
using Newtonsoft.Json;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/essaysummary")]
    public class EssaySummaryController:ControllerBase
    {
        private readonly IEssayRepository _essayRepository;
        private readonly IMapper _mapper;

        public EssaySummaryController(IEssayRepository essayRepository, IMapper mapper)
        {
            _essayRepository = essayRepository ?? throw new Exception(nameof(essayRepository));
            _mapper = mapper ?? throw new Exception(nameof(mapper));
        }
        [HttpGet(Name = nameof(GetEssaySummaryAsync))]
        public async Task<IActionResult> GetEssaySummaryAsync
            ([FromQuery] EssayResourceParameters parameters)
        {
            var pagedList = await _essayRepository.GetAll(parameters);
            CreatePagedMeta(pagedList);
            var essayDtoList = _mapper.Map<IEnumerable<EssayDto>>(pagedList);
            //开始进行数据塑形，把页元信息添加到Body里
            //var shapedData = essayDtoList.ShapeData()
            var essayDtoListWithPagination = new
            {
                data = essayDtoList,
                meta = new
                {
                    total = _essayRepository.EssaysCount(), 
                    page_size = parameters.PageSize, 
                    page_number = parameters.PageNumber
                }
            };
            return Ok(essayDtoListWithPagination);
        }

        private void CreatePagedMeta(PageList<Essay> pagedList)
        {
            var paginationMetadata = new
            {
                totalCount = pagedList.TotalCount,
                pageSize = pagedList.PageSize,
                currentPage = pagedList.CurrentPage,
                totalPages = pagedList.TotalPages,
                //前一页的链接
                previousPageLink = pagedList.HasPrevious ? Url.Link(nameof(GetEssaySummaryAsync), new
                {
                    pageNumber = pagedList.CurrentPage - 1,
                    pageSize = pagedList.PageSize,
                }) : null,
                //后一页的链接
                nextPageLink = pagedList.HasNext ? Url.Link(nameof(GetEssaySummaryAsync), new
                {
                    pageNumber = pagedList.CurrentPage + 1,
                    pageSize = pagedList.PageSize,
                }) : null
            };
            //将分页元数据添加到消息响应头中
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
        }
    }
}