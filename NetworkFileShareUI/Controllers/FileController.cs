using Application.DTO.AccessLevel;
using Application.DTO.File;
using Application.DTO.Folder;
using Application.DTO.SubFolder;
using Application.Helpers;
using Application.Helpers.Contract;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetworkFileShareUI.ViewModels;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;

namespace NetworkFileShareUI.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly string _baseAddress = "";
        private readonly string _FSAddress = "";

        public FileController(IApiHelper apiHelper, IConfiguration configuration)
        {
            _apiHelper = apiHelper;
            _baseAddress = configuration.GetSection("NetworkApiAddress").Value;
            //_baseAddress = configuration.GetSection("NetworkApiAddressDebug").Value;//debug
            _FSAddress = configuration.GetSection("FileServerAddress").Value;
        }
        public async Task<IActionResult> Folder(Guid Id)
        {
            try
            {
                FolderViewModel model = new FolderViewModel();
                var results = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/{Id}");
                if (results.IsSuccessfulResult())
                {
                    model.Folder = _apiHelper.Deserialize<FolderDTO>(results.Content);
                    var fileResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/{Id}/Files");
                    if (fileResult.IsSuccessfulResult())
                        model.Files = _apiHelper.Deserialize<List<FileDTO>>(fileResult.Content);
                    var subFolderResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/{Id}/SubFolders");
                    if (subFolderResult.IsSuccessfulResult())
                        model.SubFolders = _apiHelper.Deserialize<List<SubFolderDTO>>(subFolderResult.Content);
                    var accesslevelResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/Accesslevels");
                    if (accesslevelResult.IsSuccessfulResult())
                        model.AccessLevels = _apiHelper.Deserialize<List<AccessLevelDTO>>(accesslevelResult.Content);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("_tempDebug",ex.Message);
            }
        }
        [HttpPost]
        [RequestSizeLimit(5368709120)]
        public async Task<IActionResult> UploadFile(IFormCollection data) 
        {
            string result = "success";
            foreach (var item in data.Files)
            {
                if(DirectoryHelper.AddFile(WebUtility.UrlDecode(data["FolderPath"]), item))
                {
                    CreateFileDTO newfile = new CreateFileDTO();
                    newfile.Name = item.FileName;
                    newfile.FileLength = DirectoryHelper.GetFileLength(item.Length);
                    newfile.FileExt = Path.GetExtension(newfile.Name);
                    newfile.AccessLevelId = Guid.Parse(data["AccessLevel"].ToString());
                    newfile.FolderId = Guid.Parse(data["Folder"].ToString());
                    var Createresult = await _apiHelper.Call(ApiHelper.HttpMethods.Post, $"{_baseAddress}/Files", _apiHelper.Serialize(newfile));
                    if (!Createresult.IsSuccessfulResult())
                        result = "error";
                }else
                    result = "error";
            }
            return Json(result);
        }
        public async Task<IActionResult> RefreshFileList(Guid Id) 
        {
            var fileResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/{Id}/Files");
            if (fileResult.IsSuccessfulResult())
                return Json(new { hasvalue = true, files = Application.Helpers.ViewHelper.RenderViewAsync(this, "_FileList", _apiHelper.Deserialize<List<FileDTO>>(fileResult.Content), true) });
            else
                return Json(new { hasvalue = false });
        }
        public async Task<IActionResult> SubmitDeleteFile(Guid Id,Guid FolderId) 
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Delete, $"{_baseAddress}/Files/{Id}");
            if (result.IsSuccessfulResult())
            {
                if (_apiHelper.Deserialize<bool>(result.Content))
                    TempData["FolderSuccess"] = "file deleted successfully";
                else
                    TempData["FolderError"] = "Error occured!try again";
            }
            else
                TempData["FolderError"] = "Error occured!try again";
            return RedirectToAction("Folder", "File",new { Id = FolderId });
        }
        public async Task<IActionResult> ViewFile(Guid Id) 
        {
            FileViewModel model = new FileViewModel();
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Files/{Id}");
            if (result.IsSuccessfulResult()) 
            {
                var file = _apiHelper.Deserialize<FileDTO>(result.Content);
                var Navresult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Files/{Id}/Navigation");
                if(Navresult.IsSuccessfulResult()) 
                {
                    var navs = _apiHelper.Deserialize<string[]>(Navresult.Content);
                    model.PreFile = navs[0];
                    model.NextFile = navs[1];
                }
                model.FileType = FindFileType(file.FileExt);
                if(file.FolderTypeName.ToLower() == "simple")
                    model.FileSource = $"{_FSAddress}/{file.FolderPath.Split('\\').LastOrDefault()}/{file.Name}";
                else
                    model.FileSource = $"{_FSAddress}/{file.FolderId.ToString()}/{file.Name}";
                model.FolderId = file.FolderId;
                model.Id = file.Id;
                model.Name = file.Name;
                model.FileExt = file.FileExt.Replace(".","");
            }
            return View(model);
        }
        public async Task<IActionResult> DownloadFile(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Files/{Id}");
            if (result.IsSuccessfulResult())
            {
                var file = _apiHelper.Deserialize<FileDTO>(result.Content);
                var currentFile = System.IO.File.ReadAllBytes(Path.Combine(file.FolderPath, file.Name));
                var content = new System.IO.MemoryStream(currentFile);
                return File(content, "APPLICATION/octet-stream", file.Name);
            }
            else
                return RedirectToAction("Error", new { code = (int)result.ResultCode });
        }
        private string FindFileType(string Ext) 
        {
            string[] movies = { ".avi", ".mp4", ".divx", ".wmv", ".flv", ".mkv", ".vob", ".m4v", ".ts" };
            string[] photos = { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".webp" };
            string[] musics = { ".wav", ".mid", ".midi", ".wma", ".mp3", ".ogg", ".rma" };
            string[] documents = { ".doc", ".docx", ".pdf", ".txt", ".xls", ".xlsx", ".ppt", ".pptx" };
            string[] compresses = { ".zip", ".rar", ".7z", ".gz" };
            if (movies.Contains(Ext.Trim().ToLower()))
                return "movies";
            else if (photos.Contains(Ext.Trim().ToLower()))
                return "photos";
            else if (musics.Contains(Ext.Trim().ToLower()))
                return "musics";
            else if (documents.Contains(Ext.Trim().ToLower()))
                return "documents";
            else if (compresses.Contains(Ext.Trim().ToLower()))
                return "compresses";
            else
                return "unknown";
        }
    }
}
