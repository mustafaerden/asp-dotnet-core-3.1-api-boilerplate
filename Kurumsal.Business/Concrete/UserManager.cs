using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public List<string> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public List<OperationClaim> GetRoles(int userId)
        {
            return _userDal.GetRoles(userId);
        }
    }
}
