namespace TodoList.Services.Shared
{
    public class ServiceResult
    {
        public ResultCode ResultCode { get; internal set; }
        public string Message { get; internal set; }
        public List<string> Errors { get; internal set; }
        public bool HasErrors => Errors is { Count: > 0 };
        public ServiceResult()
        {
            Errors = new List<string>();
        }
    }

    public class ServiceResult<TModel> : ServiceResult
    {
        public TModel Result { get; internal set; }
    }

    public enum ResultCode
    {
        Success,
        Created,
        Updated,
        BadRequest,
        InternalServerError,
        NotFound
    }

    public static class ServiceResultExtension
    {
        public static ServiceResult BadRequestWithMessage(this ServiceResult serviceResult, string message)
        {
            serviceResult.ResultCode = ResultCode.BadRequest;
            serviceResult.Message = message;
            return serviceResult;
        }
        public static ServiceResult WithResultCode(this ServiceResult serviceResult, ResultCode resultCode, string message = "")
        {
            serviceResult.ResultCode = resultCode;
            serviceResult.Message = message;
            return serviceResult;
        }
        public static ServiceResult<TModel> ToResourceCreatedResult<TModel>(this ServiceResult<TModel> serviceResult, TModel result)
        {
            serviceResult.ResultCode = ResultCode.Created;
            serviceResult.Result = result;
            return serviceResult;
        }
        public static ServiceResult<TModel> ToSuccessResult<TModel>(this ServiceResult<TModel> serviceResult, TModel result)
        {
            serviceResult.ResultCode = ResultCode.Success;
            serviceResult.Result = result;
            return serviceResult;
        }
    }
}
