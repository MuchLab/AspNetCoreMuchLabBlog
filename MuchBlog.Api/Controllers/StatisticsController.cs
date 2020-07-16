using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Domain.Helpers.Parameters;
using MuchBlog.Domain.Services.IService;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController:ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public StatisticsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserEssayCount()
        {
            var users = await _repositoryWrapper.User.GetAllAsync();
            var userEssayCountList = new List<UserEssayCountParameter>();
            foreach (var user in users)
            {
                var count = _repositoryWrapper.Essay.EssaysCount(user.Id);
                userEssayCountList.Add( new UserEssayCountParameter
                {
                    Type = user.UserName,
                    Value = count
                });
            }
            var classificationEssayCountList = new List<ClassificationEssayCountParameter>();
            var classifications = await _repositoryWrapper.Classification.GetAllAsync();
            foreach (var classification in classifications)
            {
                var count = _repositoryWrapper.Essay.EssaysCountByClassification(classification.Id);
                classificationEssayCountList.Add(new ClassificationEssayCountParameter
                {
                    Action = classification.Name,
                    Pv = count
                });
            }

            var data = new
            {
                userEssayCount = userEssayCountList,
                classificationEssayCount = classificationEssayCountList
            };
            return Ok(data);
        }
    }
}