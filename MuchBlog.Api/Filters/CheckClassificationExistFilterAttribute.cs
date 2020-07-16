using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MuchBlog.Domain.Services.IServices;

namespace MuchBlog.Api.Filters
{
    public class CheckClassificationExistFilterAttribute:ActionFilterAttribute
    {
        private readonly IClassificationRepository _classificationRepository;

        public CheckClassificationExistFilterAttribute(IClassificationRepository classificationRepository)
        {
            _classificationRepository = classificationRepository;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var classificationParam = context.ActionArguments
                .Single(m => m.Key == "classificationId");;

            var classificationId = (Guid)classificationParam.Value;

            if (!await _classificationRepository.IsExistAsync(classificationId))
            {
                context.Result = new NotFoundResult();
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}