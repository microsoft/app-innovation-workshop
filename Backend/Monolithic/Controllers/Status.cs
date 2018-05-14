using System;
using Microsoft.AspNetCore.Mvc;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/ping")]
    public class Status : Controller
    {
        /// <summary>
        /// Test the application by sending a ping
        /// </summary>
        /// <returns>Pong</returns>
        [HttpGet]
        public string Ping()
        {
            return "pong";
        }

        /// <summary>
        /// Produces a dummy error for demo purposes
        /// </summary>
        [HttpGet]
        [Route("error")]
        public string Error()
        {
            throw new NotImplementedException("This Demo Error was intended.");
        }
    }
}
