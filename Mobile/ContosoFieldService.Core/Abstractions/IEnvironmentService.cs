using System;

namespace ContosoFieldService.Abstractions
{
    public interface IEnvironmentService
    {
        bool IsRunningInRealWorld();
    }
}
