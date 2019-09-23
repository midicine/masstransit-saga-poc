using MassTransit;
using Microsoft.AspNetCore.Mvc;
using POC.Saga.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Saga.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IBus _bus;

        public InvitationsController(IBus bus) => _bus = bus;

        [HttpPost("confirm")]
        public async Task<IActionResult> Post(
            [FromBody] ConfirmInvitationRequest request,
            CancellationToken token)
        {
            var id = Guid.NewGuid();
            await _bus.Publish(new ConfirmInvitationRequested(id, request.InvitationId, request.Password), token);
            return Accepted(id);
        }
    }

    public class ConfirmInvitationRequest
    {
        public Guid InvitationId { get; set; }
        public string Password { get; set; }
    }
}
