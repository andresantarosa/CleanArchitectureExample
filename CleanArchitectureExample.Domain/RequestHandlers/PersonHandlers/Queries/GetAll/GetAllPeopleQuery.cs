using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Queries.GetAll
{
    public class GetAllPeopleQuery:IRequest<GetAllPeopleQueryViewModel>
    {
    }
}
