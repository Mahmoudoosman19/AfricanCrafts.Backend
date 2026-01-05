using Common.Domain.Primitives;
using MediatR.NotificationPublishers;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities;

public class RefundProof : Entity<Guid>, IAuditableEntity
{
    public Guid WalletTransactionId { get; private set; } // Made nullable

    [ForeignKey(nameof(WalletTransactionId))]
    public virtual WalletTransaction WalletTransaction { get; set; } 

    public string RefundProofImgId { get; private set; }

    public string RefundProofImgUrl { get; private set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public void SetWalletTransactionId(Guid walletTransactionId) // Adjusted setter for nullable
    {
        WalletTransactionId = walletTransactionId;
    }

    public void SetRefundProofImgId(string refundProofImgId)
    {
        RefundProofImgId = refundProofImgId;
    }

    public void SetRefundProofImgUrl(string refundProofImgUrl)
    {
        RefundProofImgUrl = refundProofImgUrl;
    }
}
