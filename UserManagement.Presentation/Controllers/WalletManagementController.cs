using Common.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.wallet.Command.CreateWallet;
using UserManagement.Application.Features.wallet.Queries.GetWallet;
using UserManagement.Application.Features.wallet.Queries.GetWalletTransactions;
using UserManagement.Application.Features.Wallet.Queries.GetAdminWalletTransactions;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/UserManagement/[controller]")]
    public class WalletManagementController(ISender sender) : ApiController(sender)
    {
        [HttpGet("Get-User-wallet")]
        public async Task<IActionResult> GetUserWallet([FromRoute] GetWalletQuery query, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }

        [HttpGet("Get-User-Wallet-Transaction")]
        public async Task<IActionResult> GetUserWalletTransaction([FromQuery] WalletTransactionQuery query, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }
        [HttpPost("Create-User-Wallet")]
        public async Task<IActionResult> CreateVendorWallet([FromBody] CreateWalletCommand query, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }

        [HttpGet("Get-Admin-Wallet-Transactions")]
        public async Task<IActionResult> CreateVendorWallet([FromQuery] GetAdminWalletTransactionsQuery query, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(query);
            return HandleResult(response);
        }
    }
}
