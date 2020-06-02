using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Services.Communication
{
    public interface ISmsServices
    {
        Task SendSms(string phoneNumber, string text);
    }
}
