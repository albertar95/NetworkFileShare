using Application.DTO.File;
using Application.DTO.Folder;
using Microsoft.AspNetCore.Http;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class DirectoryHelper
    {
        public static string Generate() 
        {
            var Current = new DirectoryInfo(ApplicationServiceRegistration.FileSystemAddress).GetDirectories();
            if (Current.Any())
            {
                var newName = $"{ApplicationServiceRegistration.FileSystemAddress}\\{int.Parse(Current.OrderByDescending(p => p.Name).FirstOrDefault().Name) + 1}";
                Directory.CreateDirectory(newName);
                return newName;
            }
            else
            {
                var newName = $"{ApplicationServiceRegistration.FileSystemAddress}\\1";
                Directory.CreateDirectory(newName);
                return newName;
            }

        }
        public static void Remove(string path) 
        {
           Directory.Delete(path, true);
        }
        public static bool AddFile(string path,IFormFile formFile) 
        {
            if (Directory.Exists(path))
            {
                using (Stream stream = new FileStream(Path.Combine(path, formFile.FileName), FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                return File.Exists(Path.Combine(path, formFile.FileName));
            }
            else
                return false;
        }
        public static void RemoveFile(string path) 
        {
            File.Delete(path);
        }
        public static List<CreateFileDTO> ProbeFolder(Guid folderId,Guid accessLevelId,string path) 
        {
            List<CreateFileDTO> result = new List<CreateFileDTO>();
            DirectoryInfo di = new DirectoryInfo(NormilizePath(path));
            foreach (var file in di.GetFiles())
            {
                result.Add(new CreateFileDTO()
                {
                    AccessLevelId = accessLevelId,
                    FolderId = folderId,
                    FileExt = Path.GetExtension(file.Name),
                    Name = file.Name,
                    FileLength = GetFileLength(file.Length)
                });
            }
            return result;
        }
        public static void CreateVirtualDirectory(string path,string Id) 
        {
            ServerManager iisManager = new ServerManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "inetsrv\\config\\applicationHost.config"));
            Microsoft.Web.Administration.Application mySite = iisManager.Sites["Default Web Site"].Applications[0];
            if (mySite.VirtualDirectories[$"/FS/{Id}"] == null)
            {
                mySite.VirtualDirectories.Add($"/FS/{Id}", path);
                iisManager.CommitChanges();
            }
        }
        public static void DeleteVirtualDirectory(string path,string Id) 
        {
            ServerManager iisManager = new ServerManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "inetsrv\\config\\applicationHost.config"));
            VirtualDirectory vd = iisManager.Sites["Default Web Site"].Applications[0].VirtualDirectories[$"/FS/{Id}"];
            if(vd != null)
            {
                iisManager.Sites["Default Web Site"].Applications[0].VirtualDirectories.Remove(vd);
                iisManager.CommitChanges();
            }
        }
        public static string NormilizePath(string path) 
        {
            string result = "";
            int counter = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path.ToArray()[i] != '\\')
                {
                    result += path.ToArray()[i];
                    counter++;
                }
                else
                {
                    if (path.ToArray()[counter + 1] == '\\')
                    {
                        i++;
                        counter++;
                    }
                    result += "\\\\";
                    counter++;
                }
            }
            return result;
        }
        public static string GetFileLength(long length)
        {
            if (length < 1024)
                return $"{length} bytes";
            else if (length >= 1024 && length < 1048576)
                return $"{((decimal)length / (decimal)1024).ToString("#.##")} KB";
            else if (length >= 1048576 && length < 1073741824)
                return $"{((decimal)length / (decimal)1048576).ToString("#.##")} MB";
            else if (length >= 1073741824 && length < 1099511627776)
                return $"{((decimal)length / (decimal)1073741824).ToString("#.##")} GB";
            else
                return $"{length} bytes";
        }
    }
}
