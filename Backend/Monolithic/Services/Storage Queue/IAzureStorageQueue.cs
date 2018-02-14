using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ContosoMaintenance.WebAPI.Services.StorageQueue
{
    public interface IAzureStorageQueue
    {
        Task AddMessage(string message);
    }
}
