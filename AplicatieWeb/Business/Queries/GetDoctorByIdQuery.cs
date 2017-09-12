using Common.Layer.CqrsCore;
using System;

namespace Business.Queries
{
    public class GetDoctorByIdQuery : IQuery
    {
        public GetDoctorByIdQuery(string id)
        {
            Id = new Guid(id);
        }
        public Guid Id { get; private set; }
    }
}
