namespace WorkWearGroup.API.Models
{
    public class ServiceResult<TData>
    {
        private ServiceResult(TData data)
        {
            Data = data;
        }

        private ServiceResult(ErrorCategory errorCategory, string errorMessage)
            : this(errorCategory, new ErrorContent(errorMessage))
        {
        }

        private ServiceResult(ErrorCategory errorCategory, ErrorContent error)
        {
            Error = error;
            ErrorCategory = errorCategory;
        }

        public TData Data { get; }

        public ErrorContent Error { get; }

        public ErrorCategory ErrorCategory { get; }

        public bool IsSuccess()
        {
            return (Error == null);
        }

        public static ServiceResult<TData> Success(TData data)
        {
            return new ServiceResult<TData>(data);
        }

        public static ServiceResult<TData> Failed(ErrorCategory errorCategory, string errorMessage)
        {
            return new ServiceResult<TData>(errorCategory, errorMessage);
        }

        public static ServiceResult<TData> Failed(ErrorCategory errorCategory, ErrorContent error)
        {
            return new ServiceResult<TData>(errorCategory, error);
        }
    }
}
