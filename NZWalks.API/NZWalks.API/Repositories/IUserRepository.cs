﻿using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        public Task<User> AuthenticateAsync(string username, string password);
    }
}
