using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleUI
{
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> _log;
        private readonly IConfiguration _config;

        public TestService(ILogger<TestService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void Run()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                _log.LogError("Run num {runNumber}", i);
            }
        }
    }
}
