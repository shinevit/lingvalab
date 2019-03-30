using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Dto
{
    public class BaseStatusDto
    {
        private const int SUCCESS_STATUS_CODE = 200;
        private const int ERROR_STATUS_CODE = 404;
        private const int INTERNAL_ERROR_STATUS_CODE = 500;
        private const string INTERNAL_ERROR_MESSAGE = "Internal error";
        private const string SUCCESS_MESSAGE = "Successfully";
        private const string ERROR_MESSAGE = "Error";

        public int StatusCode { get; private set; }
        public string Message { get; private set; }

        public void AddResponseInfo(int statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        public static BaseStatusDto CreateSuccessDto(string message = SUCCESS_MESSAGE)
        {
            BaseStatusDto result = new BaseStatusDto();

            result.CreateSuccess(message);

            return result;
        }
        public void CreateSuccess(string message = SUCCESS_MESSAGE)
        {
            this.StatusCode = SUCCESS_STATUS_CODE;
            this.Message = message;
        }

        public static BaseStatusDto CreateErrorDto(string message = ERROR_MESSAGE)
        {
            BaseStatusDto result = new BaseStatusDto();

            result.CreateError(message);

            return result;

        }
        public void CreateError(string message = ERROR_MESSAGE)
        {
            this.StatusCode = ERROR_STATUS_CODE;
            this.Message = message;
        }

        public static BaseStatusDto CreateInternalErrorDto(string message = INTERNAL_ERROR_MESSAGE)
        {
            BaseStatusDto result = new BaseStatusDto();

            result.CreateInternalError(message);

            return result;

        }
        public void CreateInternalError(string message = INTERNAL_ERROR_MESSAGE)
        {
            this.StatusCode = INTERNAL_ERROR_STATUS_CODE;
            this.Message = message;
        }
    }
}
