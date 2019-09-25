using Microsoft.AspNetCore.Mvc;
using POC.Saga.Infrastructure;
using POC.Saga.Infrastructure.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace POC.Saga.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IBus _bus;

        public InvitationsController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Post(
            [FromBody] ConfirmInvitationRequest request,
            CancellationToken token)
        {
            await _bus.Publish(new ConfirmInvitationRequested
            {
                InvitationId = request.InvitationId,
                Password = request.Password
            }, token);
            return Accepted();
        }
    }

    #region Model
    public class ConfirmInvitationRequest
    {
        public Guid InvitationId { get; set; }
        public string Password { get; set; }
    }
    #endregion
}
