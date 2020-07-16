using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Domain.Services.IServices;

namespace MuchBlog.Api.Filters
{
    public class CheckEssayExistFilterAttribute:ActionFilterAttribute
    {
        private readonly IEssayRepository _essayRepository;

        public CheckEssayExistFilterAttribute(IEssayRepository essayRepository)
        {
            _essayRepository = essayRepository;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var essayParam = context.ActionArguments
                .Single(m => m.Key == "essayId");
            
            var essayId = (Guid)essayParam.Value;

            if (!await _essayRepository.IsExistAsync(essayId))
            {
                context.Result = new NotFoundResult();
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}