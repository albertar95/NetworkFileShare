using Application.DTO.Folder;
using Application.DTO.User;
using Application.Features.AccessLevel.Request;
using Application.Features.Folder.Request;
using Application.Features.User.Request;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetworkApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FolderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("Users/{userId}/Folders")]
        public async Task<IActionResult> GetFolders(Guid userId, [FromQuery]bool IncludePublics = false, [FromQuery] string? FilterByIconId = "", [FromQuery] string? FilterByFolderName = "")
        {
            return Ok(await _mediator.Send(new GetFolderListRequest() { UserId = userId, IncludePublics = IncludePublics, FolderIconFilter = FilterByIconId, FolderNameFilter = FilterByFolderName }));
        }
        [HttpGet("Folders/{id}")]
        public async Task<IActionResult> GetFolder(Guid id)
        {
            return Ok(await _mediator.Send(new GetFolderRequest() { Id = id }));
        }
        [HttpPost("Folders")]
        public async Task<IActionResult> AddFolder([FromBody] CreateFolderDTO folder)
        {
            return Ok(await _mediator.Send(new AddFolderRequest() { Folder = folder }));
        }
        [HttpPatch("Folders")]
        public async Task<IActionResult> UpdateFolder([FromBody] UpdateFolderDTO folder)
        {
            return Ok(await _mediator.Send(new UpdateFolderRequest() { Folder = folder }));
        }
        [HttpDelete("Folders/{id}")]
        public async Task<IActionResult> DeleteFolder(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteFolderRequest() { Id = id }));
        }
        [HttpGet("Folders/Accesslevels")]
        public async Task<IActionResult> GetAccesslevels()
        {
            return Ok(await _mediator.Send(new GetAccesslevelListRequest()));
        }
        [HttpGet("Folders/FolderIcons")]
        public async Task<IActionResult> GetFolderIcons()
        {
            return Ok(await _mediator.Send(new GetFolderIconListRequest()));
        }
        [HttpGet("Folders/FolderTypes")]
        public async Task<IActionResult> GetFolderTypes()
        {
            return Ok(await _mediator.Send(new GetFolderTypeListRequest()));
        }
        [HttpGet("Folders/FolderColors")]
        public async Task<IActionResult> GetFolderColors()
        {
            return Ok(await _mediator.Send(new GetFolderColorListRequest()));
        }
        [HttpGet("Folders/{id}/SubFolders")]
        public async Task<IActionResult> GetRootFolderSubFolders(Guid id)
        {
            return Ok(await _mediator.Send(new GetSubFoldersRequest() { RootFolderId = id }));
        }
        [HttpGet("Folders/{rootId}/SubFolders/{id}")]
        public async Task<IActionResult> GetSubFolderSubFolders(Guid rootId,Guid id)
        {
            return Ok(await _mediator.Send(new GetSubFoldersRequest() { RootFolderId = rootId, ParentFolderId = id }));
        }
    }
}
