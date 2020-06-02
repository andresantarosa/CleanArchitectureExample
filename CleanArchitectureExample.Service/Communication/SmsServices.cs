using CleanArchitectureExample.Domain.Interfaces.Services.Communication;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Service.Communication
{
    public class SmsServices : ISmsServices
    {
        public async Task SendSms(string phoneNumber, string text)
        {
            //Send SMS
        }
    }
}
