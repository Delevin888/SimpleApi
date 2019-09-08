using AutoMapper.Attributes;
using SimpleApi.Data;
using SimpleApi.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Services.User.Dtos
{
    [MapsFrom(typeof(UserInfo), ReverseMap = true)]
    public class UserInfoInputDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号")]
        [Required(ErrorMessage = "{0}为必填项")]
        [MaxLength(20, ErrorMessage = "{0}长度不正确"), RegularExpression("^[A-z][A-z0-9]{2,19}$", ErrorMessage = "{0}是数字和字母的组合,首位必须为字母")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}为必填项")]
        [MaxLength(16, ErrorMessage = "{0}长度不正确")]
        public string Password { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public UserGender Gender { get; set; } = UserGender.未知;
    }
}
