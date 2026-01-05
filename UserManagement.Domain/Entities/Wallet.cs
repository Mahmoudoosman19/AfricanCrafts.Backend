using Common.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities
{
    public class Wallet : Entity<Guid>, IAuditableEntity
    {
        private List<WalletTransaction> _walletTransaction = [];
        public decimal CurrentBalance { get; private set; }
        public decimal TotalEarnings { get; private set; }
        public Guid UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public long NumberOfCompletedTransactions { get; private set; }
        public ICollection<WalletTransaction> walletTransaction => _walletTransaction;
        public void SetCurrentBalance(decimal currentBalance)
        {
            CurrentBalance = currentBalance;
        }
        public void subtractCurrentBalance(decimal currentBalance)
        {
            CurrentBalance -= currentBalance;
        }
        public void SetTotalEarnings(decimal totalEarnings)
        {
            TotalEarnings += totalEarnings;
        }
        public void subtractTotalEarnings(decimal totalEarnings)
        {
            TotalEarnings -= totalEarnings;
        }
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
        public void SetNumberOfCompletedTransactions()
        {
            ++NumberOfCompletedTransactions;
        }
        public void AddTransaction(WalletTransaction item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _walletTransaction.Add(item);
        }
        public void AddRangeTransaction(List<WalletTransaction> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            _walletTransaction.AddRange(items);
        }
        public void RemoveTransaction(WalletTransaction item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _walletTransaction.Remove(item);
        }
    }
}
