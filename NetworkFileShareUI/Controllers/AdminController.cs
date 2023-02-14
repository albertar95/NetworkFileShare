using Application.Helpers.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.Helpers;
using Application.DTO.User;
using Application.DTO.Folder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Application.Models;
using System.Security.Claims;
using Domain;
using static NuGet.Packaging.PackagingConstants;
using Microsoft.AspNetCore.Authorization;
using NetworkFileShareUI.ViewModels;
using Application.DTO.AccessLevel;
using Application.DTO.FolderIcon;
using Application.DTO.FolderType;
using Application.DTO.FolderColor;
using System.Reflection;

namespace NetworkFileShareUI.Controllers
{
    [Authorize()]
    public class AdminController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly string _baseAddress = "";

        public AdminController(IApiHelper apiHelper,IConfiguration configuration)
        {
            _apiHelper = apiHelper;
            _baseAddress = configuration.GetSection("NetworkApiAddress").Value;
            //_baseAddress = configuration.GetSection("NetworkApiAddressDebug").Value;//debug
        }
        //generals
        public async Task<IActionResult> Index() 
        {
            IndexViewModel model = new IndexViewModel();
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Users/{User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid).Value}/Folders?IncludePublics=true");
            if (result.IsSuccessfulResult())
                model.Folders = _apiHelper.Deserialize<List<FolderDTO>>(result.Content);
            var folderIconResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderIcons");
            if (folderIconResult.IsSuccessfulResult())
                model.FolderIcons = _apiHelper.Deserialize<List<FolderIconDTO>>(folderIconResult.Content);
            return View(model);
        }
        public async Task<IActionResult> FilterFolders(string FolderIcon = "",string FolderName = "") 
        {
            List<FolderDTO> folders = new List<FolderDTO>();
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, 
                $"{_baseAddress}/Users/{User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid).Value}/Folders?IncludePublics=true&FilterByIconId={FolderIcon}&FilterByFolderName={FolderName}");
            if (result.IsSuccessfulResult())
                folders = _apiHelper.Deserialize<List<FolderDTO>>(result.Content);
            return Json(new { hasvalue = true, html = ViewHelper.RenderViewAsync(this,"_FolderFilter",folders,true).Result });
        }
        public IActionResult Profile()//needs work
        {
            return View();
        }
        public IActionResult Error(int code) 
        {
            return View(code);
        }
        //users
        public async Task<IActionResult> Users()
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get,$"{_baseAddress}/Users");
            if (result.IsSuccessfulResult())
                return View(_apiHelper.Deserialize<List<UserDTO>>(result.Content));
            else
                return RedirectToAction("Error",new {code = (int)result.ResultCode });
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitLogin(LoginCredential credential)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Post,$"{_baseAddress}/Users/Login",_apiHelper.Serialize(credential));
            if(result.IsSuccessfulResult())
            {
                var user = _apiHelper.Deserialize<UserDTO>(result.Content);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.Username));
                if(user.IsAdmin)
                    claims.Add(new Claim(ClaimTypes.Role, "true"));
                else
                    claims.Add(new Claim(ClaimTypes.Role, "false"));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))),new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5)});
                return RedirectToAction("Index", "Admin");
            }else
            {
                TempData["LoginError"] = result.Content;
                return RedirectToAction("Login");
            }
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitAddUser(CreateUserDTO user)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Post, $"{_baseAddress}/Users", _apiHelper.Serialize(user));
            if (result.IsSuccessfulResult())
            {
                if(_apiHelper.Deserialize<bool>(result.Content))
                    TempData["UserSuccess"] = "user created successfully";
                else
                    TempData["UserError"] = "Error occured!try again";
            }
            else
                TempData["UserError"] = "Error occured!try again";
            return RedirectToAction("Users", "Admin");
        }
        public async Task<IActionResult> EditUser(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Users/{Id}");
            if (result.IsSuccessfulResult())
                return View(_apiHelper.Deserialize<UserDTO>(result.Content));
            else
                return RedirectToAction("Error", new { code = (int)result.ResultCode });
        }
        [HttpPost]
        public async Task<IActionResult> SubmitEditUser(UpdateUserDTO user)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Patch, $"{_baseAddress}/Users", _apiHelper.Serialize(user));
            if (result.IsSuccessfulResult())
            {
                if(_apiHelper.Deserialize<bool>(result.Content))
                    TempData["UserSuccess"] = "user edited successfully";
                else
                    TempData["UserError"] = "Error occured!try again";
            }
            else
                TempData["UserError"] = "Error occured!try again";
            return RedirectToAction("Users", "Admin");
        }
        public async Task<IActionResult> SubmitDeleteUser(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Delete, $"{_baseAddress}/Users/{Id}");
            if (result.IsSuccessfulResult())
            {
                if(_apiHelper.Deserialize<bool>(result.Content))
                    TempData["UserSuccess"] = "user deleted successfully";
                else
                    TempData["UserError"] = "Error occured!try again";
            }
            else
                TempData["UserError"] = "Error occured!try again";
            return RedirectToAction("Users", "Admin");
        }
        public async Task<IActionResult> UserDetail(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Users/{Id}");
            if (result.IsSuccessfulResult())
                return View(_apiHelper.Deserialize<UserDTO>(result.Content));
            else
                return RedirectToAction("Error", new { code = (int)result.ResultCode });
        }
        //folders
        public async Task<IActionResult> Folders()
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Users/{User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid).Value}/Folders");
            if (result.IsSuccessfulResult())
                return View(_apiHelper.Deserialize<List<FolderDTO>>(result.Content));
            else
                return RedirectToAction("Error", new { code = (int)result.ResultCode });
        }
        public async Task<IActionResult> AddFolder()
        {
            AddFolderViewModel model = new AddFolderViewModel();
            var accesslevelResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get,$"{_baseAddress}/Folders/Accesslevels");
            if (accesslevelResult.IsSuccessfulResult())
                model.AccessLevels = _apiHelper.Deserialize<List<AccessLevelDTO>>(accesslevelResult.Content);
            var folderIconResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderIcons");
            if (folderIconResult.IsSuccessfulResult())
                model.FolderIcons = _apiHelper.Deserialize<List<FolderIconDTO>>(folderIconResult.Content);
            var folderTypeResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderTypes");
            if (folderTypeResult.IsSuccessfulResult())
                model.FolderTypes = _apiHelper.Deserialize<List<FolderTypeDTO>>(folderTypeResult.Content);
            var folderColorResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderColors");
            if (folderColorResult.IsSuccessfulResult())
                model.FolderColors = _apiHelper.Deserialize<List<FolderColorDTO>>(folderColorResult.Content);
            return View(model);
        }
        public async Task<IActionResult> AddAdminFolder()
        {
            AddFolderViewModel model = new AddFolderViewModel();
            var accesslevelResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/Accesslevels");
            if (accesslevelResult.IsSuccessfulResult())
                model.AccessLevels = _apiHelper.Deserialize<List<AccessLevelDTO>>(accesslevelResult.Content);
            var folderIconResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderIcons");
            if (folderIconResult.IsSuccessfulResult())
                model.FolderIcons = _apiHelper.Deserialize<List<FolderIconDTO>>(folderIconResult.Content);
            var folderTypeResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderTypes");
            if (folderTypeResult.IsSuccessfulResult())
                model.FolderTypes = _apiHelper.Deserialize<List<FolderTypeDTO>>(folderTypeResult.Content);
            var folderColorResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderColors");
            if (folderColorResult.IsSuccessfulResult())
                model.FolderColors = _apiHelper.Deserialize<List<FolderColorDTO>>(folderColorResult.Content);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitAddFolder(CreateFolderDTO folder)
        {
            folder.UserId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid).Value);
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Post, $"{_baseAddress}/Folders", _apiHelper.Serialize(folder));
            if (result.IsSuccessfulResult())
            {
                if(_apiHelper.Deserialize<bool>(result.Content))
                    TempData["FolderSuccess"] = "folder created successfully";
                else
                    TempData["FolderError"] = "Error occured!try again";
            }
            else
                TempData["FolderError"] = "Error occured!try again";
            return RedirectToAction("Folders", "Admin");
        }
        public async Task<IActionResult> EditFolder(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/{Id}");
            if (result.IsSuccessfulResult())
            {
                UpdateFolderViewModel model = new UpdateFolderViewModel();
                var accesslevelResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/Accesslevels");
                if (accesslevelResult.IsSuccessfulResult())
                    model.AccessLevels = _apiHelper.Deserialize<List<AccessLevelDTO>>(accesslevelResult.Content);
                var folderIconResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderIcons");
                if (folderIconResult.IsSuccessfulResult())
                    model.FolderIcons = _apiHelper.Deserialize<List<FolderIconDTO>>(folderIconResult.Content);
                var folderColorResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderColors");
                if (folderColorResult.IsSuccessfulResult())
                    model.FolderColors = _apiHelper.Deserialize<List<FolderColorDTO>>(folderColorResult.Content);
                model.Folder = _apiHelper.Deserialize<FolderDTO>(result.Content);
                return View(model);
            }
            else
                return RedirectToAction("Error", new { code = (int)result.ResultCode });
        }
        public async Task<IActionResult> EditServerFolder(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/{Id}");
            if (result.IsSuccessfulResult())
            {
                UpdateFolderViewModel model = new UpdateFolderViewModel();
                var accesslevelResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/Accesslevels");
                if (accesslevelResult.IsSuccessfulResult())
                    model.AccessLevels = _apiHelper.Deserialize<List<AccessLevelDTO>>(accesslevelResult.Content);
                var folderIconResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderIcons");
                if (folderIconResult.IsSuccessfulResult())
                    model.FolderIcons = _apiHelper.Deserialize<List<FolderIconDTO>>(folderIconResult.Content);
                var folderColorResult = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/FolderColors");
                if (folderColorResult.IsSuccessfulResult())
                    model.FolderColors = _apiHelper.Deserialize<List<FolderColorDTO>>(folderColorResult.Content);
                model.Folder = _apiHelper.Deserialize<FolderDTO>(result.Content);
                return View(model);
            }
            else
                return RedirectToAction("Error", new { code = (int)result.ResultCode });
        }
        [HttpPost]
        public async Task<IActionResult> SubmitEditFolder(UpdateFolderDTO folder)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Patch, $"{_baseAddress}/Folders", _apiHelper.Serialize(folder));
            if (result.IsSuccessfulResult())
            {
                if(_apiHelper.Deserialize<bool>(result.Content))
                    TempData["FolderSuccess"] = "folder edited successfully";
                else
                    TempData["FolderError"] = "Error occured!try again";
            }
            else
                TempData["FolderError"] = "Error occured!try again";
            return RedirectToAction("Folders", "Admin");
        }
        public async Task<IActionResult> SubmitDeleteFolder(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Delete, $"{_baseAddress}/Folders/{Id}");
            if (result.IsSuccessfulResult())
            {
                if(_apiHelper.Deserialize<bool>(result.Content))
                    TempData["FolderSuccess"] = "folder deleted successfully";
                else
                    TempData["FolderError"] = "Error occured!try again";
            }
            else
                TempData["FolderError"] = "Error occured!try again";
            return RedirectToAction("Folders", "Admin");
        }
        public async Task<IActionResult> FolderDetail(Guid Id)
        {
            var result = await _apiHelper.Call(ApiHelper.HttpMethods.Get, $"{_baseAddress}/Folders/{Id}");
            if (result.IsSuccessfulResult())
                return View(_apiHelper.Deserialize<FolderDTO>(result.Content));
            else
                return RedirectToAction("Error", new { code = (int)result.ResultCode });
        }
    }
}