using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;

namespace FSH.Learn.Infrastructure.Filter;
public class ApiResultFilter : ActionFilterAttribute
{
    /// <summary>
    /// Action执行完成,返回结果处理.
    /// </summary>
    /// <param name="actionExecutedContext"></param>
    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
    {
        if (actionExecutedContext.Exception == null)
        {
            // 执行成功 取得由 API 返回的资料.
            ObjectResult result = actionExecutedContext.Result as ObjectResult;
            if (result != null)
            {
                // 重新封装回传格式
                ApiResult robj = new ApiResult();
                robj.SetSuccessResult(result.Value);
                ObjectResult objectResult = new ObjectResult(robj);
                actionExecutedContext.Result = objectResult;
            }
        }

        base.OnActionExecuted(actionExecutedContext);
    }
}

public class ApiResult
{
    public int Code { get; set; }

    /// <summary>
    /// API调用是否成功.
    /// </summary>
    public bool Success { get; set; } = false;

    /// <summary>
    /// 服务器回应消息提示.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 服务器回应的返回值对象(API调用失败则返回异常对象).
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// 服务器回应时间.
    /// </summary>
    public string? ResponseDatetime { get; set; }

    /// <summary>
    /// 设置API调用结果为成功.
    /// </summary>
    /// <param name="resultObject">不需要从Data里面读取返回值对象时，存储简单的值对象或者string.</param>
    /// <returns></returns>
    public ApiResult SetSuccessResult(object resultObject)
    {
        Code = 200;
        ResponseDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
        Success = true;
        Message = "Success";
        Data = resultObject;
        return this;
    }

    /// <summary>
    /// 设置API调用结果为失败.
    /// </summary>
    /// <param name="errorCode">错误代码.</param>
    /// <param name="errorMessage">错误消息.</param>
    /// <returns></returns>
    public ApiResult SetFailedResult(int errorCode, string errorMessage)
    {
        Code = errorCode;
        ResponseDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
        Success = false;
        Message = errorMessage;
        Data = new JArray();
        return this;
    }

    /// <summary>
    /// 设置API调用结果为失败.
    /// </summary>
    /// <param name="errorCode">错误代码.</param>
    /// <param name="errorMessage">错误消息.</param>
    /// <param name="e">异常对象.</param>
    /// <returns></returns>
    public ApiResult SetFailedResult(int errorCode, string errorMessage, Exception e)
    {
        Code = errorCode;
        ResponseDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
        Success = false;
        Message = errorMessage;
        Data = e;
        return this;
    }
}
