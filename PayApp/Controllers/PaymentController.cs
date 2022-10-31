using System.Threading.Tasks;
using Application.Payment.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PayApp.Dto;

namespace PayApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PayByNumber([FromBody] PaymentDto paymentDto)
        {
            var command = new PayByNumberCommand
            {
                PhoneNumber = paymentDto.PhoneNumber,
                Sum = paymentDto.Sum
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}