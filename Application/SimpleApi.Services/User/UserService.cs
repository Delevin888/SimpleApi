using AutoMapper.QueryableExtensions;
using SimpleApi.Core;
using SimpleApi.Data;
using SimpleApi.Data.Domain;
using SimpleApi.Services.User.Dtos;
using SimpleApi.UnitOfWork;
using System;
using System.Linq;

namespace SimpleApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<SADbContext> _unitOfWork;
        public UserService(IUnitOfWork<SADbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public OperateResult AddUser(UserInfoInputDto input)
        {
            if (_unitOfWork.GetRepository<UserInfo>().Table.Any(x => x.Account == input.Account))
                return OperateResult.Error("用户已存在");
            //逻辑....
            var entity = AutoMapper.Mapper.Map<UserInfo>(input);
            _unitOfWork.GetRepository<UserInfo>().Insert(entity);
            _unitOfWork.SaveChanges();
            return OperateResult.Succeed("ok", entity);
        }

        public bool DelteUser(long id)
        {
            //逻辑....
            _unitOfWork.GetRepository<UserInfo>().Delete(id);
            var row = _unitOfWork.SaveChanges();
            return row > 0;
        }

        public UserInfoItemDto GetUser(long id)
        {
            var user = _unitOfWork.GetRepository<UserInfo>().Table
                .Where(x => x.Id == id)
                .ProjectTo<UserInfoItemDto>()
                .FirstOrDefault();
            return user;
        }

        public UserInfoItemDto LatestUser()
        {
            return _unitOfWork.GetRepository<UserInfo>().Table
                .OrderByDescending(x => x.Id)
                .ProjectTo<UserInfoItemDto>()
                .FirstOrDefault();
        }
    }
}
