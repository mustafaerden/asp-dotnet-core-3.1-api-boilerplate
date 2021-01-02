using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        // Kullanicinin Rollerini Ceken Metot;
        List<string> GetClaims(User user);
        List<OperationClaim> GetRoles(int userId);
    }
}
