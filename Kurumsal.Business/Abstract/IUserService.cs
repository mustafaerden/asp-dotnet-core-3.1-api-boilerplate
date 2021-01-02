using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        // Kullanicinin Rollerini Getiren Metot;
        List<string> GetClaims(User user);
        List<OperationClaim> GetRoles(int userId);
        void Add(User user);
        User GetByEmail(string email);
    }
}
