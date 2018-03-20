using System;
using ContosoFieldService.Abstractions;

namespace ContosoFieldService.Services
{
    public abstract class BaseAPIService
    {
        IAuthenticationService authenticationService;


        public BaseAPIService(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
    }
}
