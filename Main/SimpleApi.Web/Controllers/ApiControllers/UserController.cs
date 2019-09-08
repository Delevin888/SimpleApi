using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleApi.Core;
using SimpleApi.Data;
using SimpleApi.Framework;
using SimpleApi.Services.User;
using SimpleApi.Services.User.Dtos;
using SimpleApi.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SimpleApi.Web.Controllers
{
    public class UserController : BaseApiController
    {
        private IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService
            , ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// 按照ID获取用户
        /// GET /api/user/item?id=$id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("item")]
        public IActionResult GetUser(long id)
        {
            _logger.LogDebug("获取用户信息:{0}", id);
            return Ok(_userService.GetUser(id));
        }

        /// <summary>
        /// 添加用户
        /// POST /api/user/adduser {"Account":"Admin","Password":"123456","Gender":1}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("adduser")]
        [ModelValid]
        public IActionResult AddUser([FromBody]UserInfoInputDto input)
        {
            var result = _userService.AddUser(input);
            return Ok(result);
        }

        /// <summary>
        /// 获取最新的用户 --服务定位器获取Service
        /// GET /api/user/newuser
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("newuser")]
        public IActionResult LatestUser()
        {
            var userService = ServiceLocator.GetService<IUserService>();
            return Ok(OperateResult.Succeed("ok", userService.LatestUser()));
        }

        /// <summary>
        /// 获取当前IP --扩展
        /// GET /api/user/ipaddress
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ipaddress")]
        public IActionResult CurrentIpAddress()
        {
            return Ok(OperateResult.Succeed("ok", Request.HttpContext.GetIpAddress()));
        }
    }
}