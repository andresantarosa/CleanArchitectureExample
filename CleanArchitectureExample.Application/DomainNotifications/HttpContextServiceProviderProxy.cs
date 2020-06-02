using CleanArchitectureExample.Domain.Core.DomainNotification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Application.DomainNotifications
{
    public class HttpContextServiceProviderProxy : IContainer
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpContextServiceProviderProxy(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public T GetService<T>(Type type)
        {
            return (T)contextAccessor.HttpContext.RequestServices.GetService(type);
        }
    }
}
