using Microsoft.AspNetCore.Mvc;
using POC.Saga.Infrastructure;
using POC.Saga.Infrastructure.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Saga.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IEventDispatcher _dispatcher;

        public InvitationsController(IEventDispatcher dispatcher)
            => _dispatcher = dispatcher;

        [HttpPost("confirm")]
        public async Task<IActionResult> Post(
            [FromBody] ConfirmInvitationRequest request,
            CancellationToken token)
        {
            var id = Guid.NewGuid();
            _dispatcher.Push(new ConfirmInvitationRequested(
                id,
                request.InvitationId,
                request.Password));
            await _dispatcher.DispatchAsync(token);
            return Accepted(id);
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
