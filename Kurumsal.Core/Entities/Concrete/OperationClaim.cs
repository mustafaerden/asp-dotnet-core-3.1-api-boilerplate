using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    // Rollerin Oldugu Tablo;
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
