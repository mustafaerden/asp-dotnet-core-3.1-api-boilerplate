namespace Core.Entities.Concrete
{
    // Hangi User da Hangi Rollerin oldugunu belirten tablo;
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public OperationClaim OperationClaim { get; set; }
        public int OperationClaimId { get; set; }
    }
}
