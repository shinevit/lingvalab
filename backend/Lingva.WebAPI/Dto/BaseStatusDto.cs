using System;
using System.Collections.Generic;
using System.Linq;
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

        public int StatusCode { get; set; }
        public string Message { get; set; }

        public void AddResponseInfo(int statusCode, string messege)
        {
            this.StatusCode = statusCode;
            this.Message = messege;
        }
        public void CreateSucess()
        {
            this.StatusCode = SUCCESS_STATUS_CODE;
            this.Message = SUCCESS_MESSAGE;
        }
        public void CreateSucess(string messege)
        {
            this.StatusCode = SUCCESS_STATUS_CODE;
            this.Message = messege;
        }
        public void CreateError()
        {
            this.StatusCode = ERROR_STATUS_CODE;
            this.Message = ERROR_MESSAGE;
        }
        public void CreateError(string messege)
        {
            this.StatusCode = ERROR_STATUS_CODE;
            this.Message = messege;
        }
        public void CreateInternalError()
        {
            this.StatusCode = ERROR_STATUS_CODE;
            this.Message = ERROR_MESSAGE;
        }
        public void CreateInternalError(string messege)
        {
            this.StatusCode = ERROR_STATUS_CODE;
            this.Message = messege;
        }
    }
}
