using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.DatabaseContext;
using CRUDOperation.Models;
using CRUDOperation.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.Repositories
{
    public class VariantRepository:EFRepository<Variant>,IVariantRepository
    {
        private CRUDOperationDbContext _db;

        public VariantRepository(CRUDOperationDbContext dbContext):base(dbContext)
        {
            _db = dbContext as CRUDOperationDbContext;
        }
    }
}
