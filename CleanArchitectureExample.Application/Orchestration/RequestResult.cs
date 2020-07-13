using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Application.Orchestration
{
    public class RequestResult
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public object Data { get; set; }
    }
}
