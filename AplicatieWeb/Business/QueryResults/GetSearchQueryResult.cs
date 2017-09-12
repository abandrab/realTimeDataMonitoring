using Common.Layer.CqrsCore;
using System;
using System.Collections.Generic;

namespace Business.QueryResults
{
    public class GetSearchQueryResult : IQueryResult
    {
        public GetSearchQueryResult(Dictionary<Guid, string> doctors)
        {
            this.Doctors = doctors;  
        }
        public Dictionary<Guid, string> Doctors { get; private set; }
    }
}
