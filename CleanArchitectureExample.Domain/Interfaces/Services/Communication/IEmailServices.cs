using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Services.Communication
{
    public interface IEmailServices
    {
        Task SendEmail(List<string> recipient, string subject, string message);
    }
}
