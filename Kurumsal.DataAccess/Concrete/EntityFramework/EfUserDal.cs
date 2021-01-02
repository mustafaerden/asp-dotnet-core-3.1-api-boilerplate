using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, MoodraContext>, IUserDal
    {
        // User in tum rollerini ceken metot;
        public List<string> GetClaims(User user)
        {
            using (var context = new MoodraContext())
            {
                return context.OperationClaims
                    .Include(operation => operation.UserOperationClaims)
                    .ThenInclude(u => u.User)
                    .Where(u => u.Id == user.Id)
                    .Select(operation => operation.Name)
                    .ToList();
            }
        }

        public List<OperationClaim> GetRoles(int userId)
        {
            using (var context = new MoodraContext())
            {
                return context.UserOperationClaims
                    .Where(u => u.UserId == userId)
                    .Include(o => o.OperationClaim)
                    .Select(o => o.OperationClaim)
                    .ToList();
            }
        }
    }
}
