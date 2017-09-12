using Domain.Model.Models;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;

namespace Common.Layer.Extensions
{
    public static class IdentityServerServiceFactoryExtentions
    {
        public static IdentityServerServiceFactory UseCustomUsers(this IdentityServerServiceFactory factory, List<UserDetails> users)
        {
            factory.Register(new Registration<List<UserDetails>>(users));
            //factory.UserService = new Registration<IUserService, InMemoryUserService>();

            return factory;
        }
    }
}
