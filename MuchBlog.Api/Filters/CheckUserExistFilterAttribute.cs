using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MuchBlog.Domain.Services.IService;

namespace MuchBlog.Api.Filters
{
    /// <summary>
    /// 检查User是否存在的特性
    /// 由于检查User是否存在这一逻辑使用量比较多，
    /// 所以可以考虑把该逻辑写到一个过滤器里，这样可以实现复用
    /// 继承自ActionFilterAttribute，即可用于Controller上（对Controller的所有Action都有效），
    /// 或者Action上（对单个Action有效）
    /// </summary>
    public class CheckUserExistFilterAttribute:ActionFilterAttribute
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CheckUserExistFilterAttribute(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userParam = context.ActionArguments.Single(m => m.Key == "userId");
            Guid userId = (Guid) userParam.Value;
            if (!await _repositoryWrapper.User.IsExistAsync(userId))
            {
                context.Result = new NotFoundResult();
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}