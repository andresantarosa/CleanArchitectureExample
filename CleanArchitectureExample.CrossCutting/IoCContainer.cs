using AutoMapper;
using CleanArchitectureExample.Application.DomainNotifications;
using CleanArchitectureExample.Application.Events;
using CleanArchitectureExample.Application.Orchestration;
using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories.ReadOnlyRepository;
using CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork;
using CleanArchitectureExample.Domain.Interfaces.Services.Communication;
using CleanArchitectureExample.Persistence.Context;
using CleanArchitectureExample.Persistence.Repositories;
using CleanArchitectureExample.Persistence.Repositories.ReadOnlyRepository;
using CleanArchitectureExample.Persistence.UnitOfWork;
using CleanArchitectureExample.Service.Communication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchitectureExample.CrossCutting
{
    public class IoCContainer
    {
        public static void InitializeContainer(IServiceCollection services)
        {
            string connectionString = "Data Source=localhost;Initial Catalog=CleanArchitectureExample;Persist Security Info=False;User ID=sa;Password=123456;";

            //Inject external tools
            services.AddAutoMapper(AppDomain.CurrentDomain.Load("CleanArchitectureExample.Domain"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("CleanArchitectureExample.Domain"));

            //Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<ApplicationDbContextReadOnly>(options => options.UseSqlServer(connectionString));


            //Inject internal work classes
            services.AddScoped<IOrquestrator, Orchestrator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainNotifications, DomainNotifications>();
            services.AddSingleton<IContainer, HttpContextServiceProviderProxy>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();


            //Entity Framework Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookLoanRepository, BookLoanRepository>();

            //ReadOnly repositories
            services.AddScoped<IBookLoanReadOnlyRepository, BookLoanReadOnlyRepository>();

            //Services
            services.AddScoped<ISmsServices, SmsServices>();
            services.AddScoped<IEmailServices, EmailServices>();
        }
    }
}
