using GisBackend.Application.Layers.Commands;
using GisBackend.Application.Layers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GisBackend.Controllers
{
    [ApiController]
    [Route("api/layers")]
    public class LayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("getAllLayers")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new GetAllLayersQuery();
            var layers = await _mediator.Send(query);
            return Ok(layers);
        }

        [Route("createLayer")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateLayerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
