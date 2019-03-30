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
<<<<<<< HEAD

        private const string SUCCESS_MESSAGE = "Success";
        private const string ERROR_MESSAGE = "Error";

        public int StatusCode { get; private set; }

        public string Message { get; private set; }

        public void SetResponseInfo(int statusCode, string message)
=======
        private const int INTERNAL_ERROR_STATUS_CODE = 500;
        private const string INTERNAL_ERROR_MESSAGE = "Internal error";
        private const string SUCCESS_MESSAGE = "Successfully";
        private const string ERROR_MESSAGE = "Error";

        public int StatusCode { get; private set; }
        public string Message { get; private set; }

        public void AddResponseInfo(int statusCode, string message)
>>>>>>> origin/feature-movie-info
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
<<<<<<< HEAD

=======
>>>>>>> origin/feature-movie-info
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
<<<<<<< HEAD

=======
>>>>>>> origin/feature-movie-info
        public void CreateError(string message = ERROR_MESSAGE)
        {
            this.StatusCode = ERROR_STATUS_CODE;
            this.Message = message;
        }
<<<<<<< HEAD
    }
}
=======

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
>>>>>>> origin/feature-movie-info
