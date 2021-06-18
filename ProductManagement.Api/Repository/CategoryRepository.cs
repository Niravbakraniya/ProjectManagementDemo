using ProductManagement.Api.Context;
using ProductManagement.Api.ViewModel.Category;
using ProductManagement.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Api.ViewModel;

namespace ProductManagement.Api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Member Declaration
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constractor
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Add Update Category
        public async Task<int> AddUpdateCategory(AddUpdateCategoryViewModel model)
        {
            TblCategory TblCategory = new TblCategory();
            if (model.CategoryID > 0)
            {
                TblCategory = await _context.TblCategory.Where(u => u.CategoryID == model.CategoryID).FirstOrDefaultAsync();
                if (TblCategory == null)
                {
                    return -1;
                }
            }
            TblCategory.CategoryName = model.CategoryName?.Trim();
            TblCategory.Description = model.Description;
            TblCategory.IsActive = true;
            if (TblCategory.CategoryID == 0)
            {
                TblCategory.CreatedDate = DateTime.Now;
                await _context.TblCategory.AddAsync(TblCategory);
            }
            else
            {
                TblCategory.ModifiedDate = DateTime.Now;
            }
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Get Category Detail By ID
        public async Task<CategoryViewModel> GetCategoryDetailByID(int id)
        {
            var categoryDetail = await (from u in _context.TblCategory
                                        where u.CategoryID == id && u.IsActive == true && u.IsDeleted == false
                                        select new CategoryViewModel
                                        {
                                            CategoryID = u.CategoryID,
                                            CategoryName = u.CategoryName,
                                            Description = u.Description,
                                            IsActive = u.IsActive,
                                        }).FirstOrDefaultAsync();
            return categoryDetail;

        }
        #endregion

        #region Is Category Exists
        public async Task<bool> IsCategoryExists(int CategoryID, string CategoryName)
        {
            return await (from r in _context.TblCategory
                          where (CategoryID == 0 || r.CategoryID == CategoryID)
                          && (string.Equals(r.CategoryName.Trim(), CategoryName))
                          && r.IsActive == true && r.IsDeleted == false
                          select r.CategoryID).AnyAsync();
        }
        #endregion

        #region Bind Category List
        public async Task<ListResponseViewModel<CategoryViewModel>> GetCategoryList(ListRequestViewModel model)
        {
            ListResponseViewModel<CategoryViewModel> responsemodel = new ListResponseViewModel<CategoryViewModel>();
            responsemodel.page = model.page == 0 ? 1 : model.page;
            responsemodel.size = model.size == 0 ? 10 : model.size;

            var Categorylist = (from u in _context.TblCategory
                           where (model.searchText == null ||
                                 model.searchText == string.Empty ||
                                 u.CategoryName.ToLower().Contains(model.searchText.ToLower()))
                                 && (model.isActive == null || model.isActive == u.IsActive)
                                 && u.IsDeleted == false
                           orderby u.CreatedDate descending
                           select new CategoryViewModel
                           {
                               CategoryID = u.CategoryID,
                               CategoryName = u.CategoryName,
                               Description = u.Description,
                               IsActive = u.IsActive,
                           });
            responsemodel.totalcount = Categorylist.Count();
            Categorylist = Categorylist.Skip((responsemodel.page - 1) * responsemodel.size).Take(responsemodel.size);
            responsemodel.data = await Categorylist.ToListAsync();
            return responsemodel;
        }
        #endregion

        #region Delete Category
        public async Task<int> DeleteCategory(int[] ids)
        {
            var IdsToDelete = ids.ToList();
            var list = await _context.TblCategory.Where(r => IdsToDelete.Contains(r.CategoryID)).ToListAsync();
            list = list.Select(m => { m.IsDeleted = true; m.IsActive = false; return m; }).ToList();            
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Change Status
        public async Task<int> ChangeStatus(int id, bool isActive)
        {
            TblCategory TblCategory = new TblCategory();
            if (id > 0)
            {
                TblCategory = await _context.TblCategory.Where(u => u.CategoryID == id && u.IsDeleted == false).FirstOrDefaultAsync();
                if (TblCategory != null)
                {
                    TblCategory.IsActive = isActive;
                    await _context.SaveChangesAsync();
                    return 1;
                }
            }
            return 0;
        }
        #endregion

        #region Get CategoryNameList For DropDown
        public async Task<List<DropdownViewModel>> GetCategoryNameList()
        {
            List<DropdownViewModel> model = new List<DropdownViewModel>();
            model = await (from u in _context.TblCategory
                           where u.IsActive == true && u.IsDeleted == false
                           select new DropdownViewModel
                           {
                               value = u.CategoryID,
                               name = u.CategoryName,
                           }).ToListAsync();
            return model;
        }
        #endregion
    }
}
