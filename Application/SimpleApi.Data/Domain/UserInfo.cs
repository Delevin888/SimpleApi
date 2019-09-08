using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimpleApi.Data.Domain
{
    public class UserInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Required, MaxLength(20)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(32)]
        public string Password { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public UserGender Gender { get; set; } = UserGender.未知;
        /// <summary>
        /// 注册时间
        /// </summary>
        [Required]
        public DateTime RegisterTime { get; set; } = DateTime.Now;
    }
}
