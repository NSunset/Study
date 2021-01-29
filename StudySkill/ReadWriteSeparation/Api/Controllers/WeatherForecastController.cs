﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lib;
using lib.Data.Default;
using lib.EFDbContext.Default;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DefaultDbContextFactory _defaultContextFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , DefaultDbContextFactory defaultContextFactory)
        {
            _logger = logger;
            _defaultContextFactory = defaultContextFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            object list = null, list1 = null;
            for (int i = 0; i < 30000; i++)
            {
                await Task.Run(() =>
                {
                    var dbContext = _defaultContextFactory.GetDefaultDbContext(WriteAndRead.Read);
                    var query = from u in dbContext.User
                                select u;
                    list = query.ToList();
                });

                await Task.Run(() =>
                 {
                     var dbContext1 = _defaultContextFactory.GetDefaultDbContext(WriteAndRead.Read);
                     var query1 = from u in dbContext1.User
                                  select u;
                     list1 = query1.ToList();
                 });
            }
            return Ok(new { list, list1 });
        }
    }
}
