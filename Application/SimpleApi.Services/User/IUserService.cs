using SimpleApi.Core;
using SimpleApi.Data.Domain;
using SimpleApi.Services.User.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApi.Services.User
{
    public interface IUserService
    {
        UserInfoItemDto GetUser(long id);
        OperateResult AddUser(UserInfoInputDto input);
        bool DelteUser(long id);
        UserInfoItemDto LatestUser();
    }
}
