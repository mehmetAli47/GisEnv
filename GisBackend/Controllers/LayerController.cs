using GisBackend.Features.Layers.DeleteLayer;
using GisBackend.Features.Layers.CreateLayer;
using GisBackend.Features.Layers.GetAllLayers;
using GisBackend.Features.Layers.GetLayerById;
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
            var result = await _mediator.Send(query);
            
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [Route("getById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetLayerByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [Route("createLayer")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateLayerCommand command)
        {
            var result = await _mediator.Send(command);
            
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [Route("deleteLayer/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _mediator.Send(new DeleteLayerCommand(id));
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
