using System;
using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Sql.Server.Access.Interfaces;
using System.Collections.Generic;
using Domain.Model.Models;
using Vanguard;
using System.Linq;

namespace Business.QueryHandlers
{
    public class GetSearchQueryHandler : IQueryHandler<GetSearchQuery, GetSearchQueryResult>
    {
        private readonly IDoctorRepository doctorRepository;
        public GetSearchQueryHandler(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public GetSearchQueryResult Retrieve(GetSearchQuery query)
        {
            Guard.ArgumentNotNull(query, nameof(query));

            string[] strings = query.SearchString.Split(' ');
            List<Doctor> allResults = new List<Doctor>();
            Dictionary<Guid, string> doctors = new Dictionary<Guid, string>();
            foreach(var s in strings)
            {
                var results = doctorRepository.GetBy(d => d.UserDetails.FirstName.ToLower().Contains(s.ToLower())
                            || d.UserDetails.LastName.ToLower().Contains(s.ToLower())).ToList();
                allResults.AddRange(results);
            }
            foreach (var result in allResults)
            {
                string name = string.Concat(result.UserDetails.FirstName, " " + result.UserDetails.LastName);
                try {
                    doctors.Add(result.Id, name);
                }
                catch (Exception e)
                {

                }
            }
            return new GetSearchQueryResult(doctors);
        }
    }
}
