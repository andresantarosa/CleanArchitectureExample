using CleanArchitectureExample.Domain.Interfaces.Services.Communication;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Service.Communication
{
    public class EmailServices : IEmailServices
    {
        private AsyncRetryPolicy retryPolice;

        public EmailServices()
        {
            retryPolice = Policy.Handle<TimeoutException>().WaitAndRetryAsync(5, i => TimeSpan.FromMilliseconds(500));
        }

        public async Task SendEmail(List<string> recipient, string subject, string message)
        {
            var rand = 0;
            await retryPolice.ExecuteAsync(async () =>
            {
                Console.WriteLine("Trying to send email");
                // Force exception for demo purpose
                if (rand <=2)
                {
                    rand++;
                    throw new TimeoutException();
                }
                    
                await Task.Delay(1000);
                Console.WriteLine("Email Sent");
            });
        }
    }
}
