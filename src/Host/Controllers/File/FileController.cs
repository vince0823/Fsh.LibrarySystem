using FSH.Learn.Application.Common.Exceptions;
using FSH.Learn.Application.Common.Util;
using FSH.Learn.Infrastructure.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FSH.Learn.Host.Controllers.File;
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    public FileController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    /// <summary>
    /// 上传文件 - 限制大小100M
    /// </summary>
    /// <param name="path">文件分类的文件夹名称</param>
    /// <returns></returns>
    [HttpPost]
    [RequestSizeLimit(100 * 1024 * 1024)]
    public ApiResult FileUpload(string path = "Default", IFormFile uploadFile = null)
    {
        ApiResult result = new ApiResult();
        string message = string.Empty;

        try
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                throw new ExceptionUtil("请选择文件");
            }

            string domain = Configuration.GetSection("Domain").Value;
            string dircstr = $"/Files/{path}/";
            var resultUrl = new List<string>();
            foreach (var file in files)
            {
                string filename = Path.GetFileName(file.FileName);
                if (filename is null) continue;
                string fileext = Path.GetExtension(filename).ToLower();
                string folderpath = AppContext.BaseDirectory;
                FileUtil.CreateDir(folderpath + dircstr);

                // 重新命名文件
                string pre = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string after = FileUtil.GetRandom(1000, 9999).ToString();
                string fileloadname = dircstr + pre + "_" + after + ProExt(fileext);
                using (var stream = new FileStream(folderpath + fileloadname, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                string lastfilename = $"/{path}/" + pre + "_" + after + ProExt(fileext);
                resultUrl.Add(domain + lastfilename);
            }

            result = result.SetSuccessResult("成功上传", resultUrl);
        }
        catch (Exception ex)
        {
            message = ex.Message;
            result = result.SetFailedResult((int)HttpStatusCode.InternalServerError, message);
        }

        return result;
    }

    #region 校验文件类型
    readonly string[] badext = { "exe", "msi", "bat", "com", "sys", "aspx", "asax", "ashx" };
    private string ProExt(string ext)
    {
        if (ext is null) return "";
        if (badext.Contains(ext)) throw new Exception("危险文件");
        if (ext.First() == '.') return ext;
        return "." + ext;
    }
    #endregion
}
