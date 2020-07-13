using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Services.Communication;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Service.Communication
{
    public class SmsServices : ISmsServices
    {
        private readonly IEventDispatcher _eventDispatcher;

        public SmsServices(IEventDispatcher eventDispatcher)
        {
            this._eventDispatcher = eventDispatcher;
        }

        public async Task SendSms(string phoneNumber, string text)
        {
            await Task.Delay(1000);
            Console.WriteLine("SMS Sent");
            //SMS sent

        }
    }
}
