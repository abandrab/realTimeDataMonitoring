using Common.Layer.CqrsCore;
using System;

namespace Business.Queries
{
    public class GetPatientByIdQuery : IQuery
    {
        public GetPatientByIdQuery(string id)
        {
            PatientId = new Guid(id);
        }
        public Guid PatientId { get; private set; }
    }
}
