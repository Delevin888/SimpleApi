using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleApi.Core;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Framework
{
    public class ModelValidAttribute : ActionFilterAttribute
    {
        private string[] _ignoreAttr { get; set; }

        public ModelValidAttribute(params string[] ignoreAttr)
        {
            _ignoreAttr = ignoreAttr;
            Order = -9;
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (_ignoreAttr != null && _ignoreAttr.Length > 0)
            {
                foreach (var item in _ignoreAttr)
                {
                    context.ModelState.Remove(item);
                }
            }

            if (!context.ModelState.IsValid)
            {
                var errorMsgs = (from item in context.ModelState
                                 where item.Value.Errors.Any() && !string.IsNullOrEmpty(item.Value.Errors[0].ErrorMessage)
                                 select item.Value.Errors[0].ErrorMessage).ToList();
                if (errorMsgs.Count > 0)
                {
                    context.Result = new JsonResult(OperateResult.Error(errorMsgs.FirstOrDefault()));
                    return Task.CompletedTask;
                }
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
