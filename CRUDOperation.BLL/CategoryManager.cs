using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Abstractions.Repositories;
using CRUDOperation.BLL.Base;
using CRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.BLL
{
    public class CategoryManager:Manager<Category>,ICategoryManager
    {
        private ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository):base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
    }
}
