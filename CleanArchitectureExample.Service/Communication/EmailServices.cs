using CleanArchitectureExample.Domain.Interfaces.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Service.Communication
{
    public class EmailServices : IEmailServices
    {
        public async Task SendEmail(List<string> recipient, string subject, string message)
        {
            //SendEmail
        }
    }
}
