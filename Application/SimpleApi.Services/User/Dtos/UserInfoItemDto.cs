using AutoMapper.Attributes;
using SimpleApi.Data;
using SimpleApi.Data.Domain;
using System;

namespace SimpleApi.Services.User.Dtos
{
    [MapsFrom(typeof(UserInfo), ReverseMap = true)]
    public class UserInfoItemDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public UserGender Gender { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
    }
}
