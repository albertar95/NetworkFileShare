using Application.DTO.File;
using Application.Features.File.Request;
using Application.Features.User.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetworkApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("Folders/{folderId}/Files")]
        public async Task<IActionResult> GetFiles(Guid folderId)
        {
            return Ok(await _mediator.Send(new GetFileListRequest() { FolderId = folderId }));
        }
        [HttpGet("Files/{id}")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            return Ok(await _mediator.Send(new GetFileRequest() { Id = id }));
        }
        [HttpPost("Files")]
        public async Task<IActionResult> AddFile([FromBody]CreateFileDTO file)
        {
            return Ok(await _mediator.Send(new CreateFileRequest() { File = file }));
        }
        [HttpDelete("Files/{id}")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteFileRequest() { Id = id }));
        }
    }
}
