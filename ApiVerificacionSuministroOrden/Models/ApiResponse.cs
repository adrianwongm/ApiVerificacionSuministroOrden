using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiVerificacionSuministroOrden.Models
{
    public class ApiResponse<T>
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ApiResponse(bool isValid, string message, T data)
        {
            IsValid = isValid;
            Message = message;
            Data = data;
        }
    }

}