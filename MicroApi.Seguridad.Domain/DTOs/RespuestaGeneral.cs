using System;
using System.Collections.Generic;

namespace MicroApi.Seguridad.Domain.DTOs
{
    public class RespuestaGeneral
    {
        public string Status { get; set; }
        public string Answer { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string RequestId { get; set; }
        public string LocalizedMessage { get; set; }
    }
}