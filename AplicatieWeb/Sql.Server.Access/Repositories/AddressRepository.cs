﻿using Domain.Model.Models;
using Sql.Server.Access.Interfaces;

namespace Sql.Server.Access.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
    }
}
