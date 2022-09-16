﻿using Prodavalnik.Core.IReposotories;

namespace Prodavalnik.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IProductRepository Product { get; }
        Task CompliteAsync();
        void Dispose();

    }
}
