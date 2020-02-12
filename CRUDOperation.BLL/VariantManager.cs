using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.BLL
{
    public class VariantManager:Manager<Variant>,IVariantManager
    {
        private IVariantRepository _variantrepository;

        public VariantManager(IVariantRepository variantRepository):base(variantRepository)
        {
            _variantrepository = variantRepository;
        }
    }
}
