﻿#region Using

using C4rm4x.WebApi.Framework.Log;
using C4rm4x.WebApi.Monitoring.Core.Controllers;
using System.Collections.Generic;

#endregion

namespace C4rm4x.WebApi.Monitoring.Counter.Controllers
{
    /// <summary>
    /// Basic implementation of an ApiController responsible for counting 
    /// relevant pieces of information generated by your system
    /// </summary>

    public class CounterController :
        MonitorController<long, CounterResultDto>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="counters">The list of all instances that implement interface ICounter</param>
        public CounterController(
            ILog logger,
            IEnumerable<ICounter> counters)
            : base(logger, counters, Transform)
        { }

        private static CounterResultDto Transform(
            ComponentDto component, long counter)
        {
            return new CounterResultDto(component, counter);
        }
    }
}
