using ProductManagement.Api.Context;
using ProductManagement.Api.ViewModel.Product;
using ProductManagement.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Api.ViewModel;

namespace ProductManagement.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Member Declaration
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constractor
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Add Update Product
        public async Task<int> AddUpdateProduct(AddUpdateProductViewModel model)
        {
            TblProduct TblProduct = new TblProduct();
            if (model.ProductID > 0)
            {
                TblProduct = await _context.TblProduct.Where(u => u.ProductID == model.ProductID).FirstOrDefaultAsync();
                if (TblProduct == null)
                {
                    return -1;
                }
            }
            TblProduct.ProductName = model.ProductName?.Trim();
            TblProduct.Description = model.Description;
            TblProduct.Cost = model.Cost;
            TblProduct.IsActive = true;

            if (TblProduct.ProductID == 0)
            {
                TblProduct.CreatedDate = DateTime.Now;
                await _context.TblProduct.AddAsync(TblProduct);
                await _context.SaveChangesAsync();
            }
            else
            {
                TblProduct.ModifiedDate = DateTime.Now;
            }

            //Adding Data in categoryMapping table
            if (model.CategoryIDs.Any())
            {
                var list = await _context.TblCategoryMapping.Where(r => r.ProductID == TblProduct.ProductID).ToListAsync();
                _context.RemoveRange(list);
                await _context.SaveChangesAsync();

                foreach (var item in model.CategoryIDs)
                {
                    TblCategoryMapping tblCategoryMapping = new TblCategoryMapping();
                    tblCategoryMapping.CategoryID = item;
                    tblCategoryMapping.ProductID = TblProduct.ProductID;
                    await _context.TblCategoryMapping.AddAsync(tblCategoryMapping);
                    await _context.SaveChangesAsync();
                }
            }

            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Get Product Detail By ID
        public async Task<ProductViewModel> GetProductDetailByID(int id)
        {
            var categoryDetail = await (from u in _context.TblProduct
                                        where u.ProductID == id && u.IsActive == true && u.IsDeleted == false
                                        select new ProductViewModel
                                        {
                                            ProductID = u.ProductID,
                                            ProductName = u.ProductName,
                                            Cost = u.Cost,
                                            Description = u.Description,
                                            IsActive = u.IsActive,
                                            CategoryIDs = (from p in _context.TblCategoryMapping
                                                           join c in _context.TblCategory on p.CategoryID equals c.CategoryID
                                                           where p.ProductID == u.ProductID
                                                           select new DropdownViewModel
                                                           {
                                                               value = p.CategoryID,
                                                               name = c.CategoryName,
                                                           }).ToList(),
                                        }).FirstOrDefaultAsync();
            return categoryDetail;

        }
        #endregion

        #region Is Product Exists
        public async Task<bool> IsProductExists(int ProductID, string ProductName)
        {
            return await (from r in _context.TblProduct
                          where (ProductID == 0 || r.ProductID == ProductID)
                          && (string.Equals(r.ProductName.Trim(), ProductName))
                          && r.IsActive == true && r.IsDeleted == false
                          select r.ProductID).AnyAsync();
        }
        #endregion

        #region Bind Product List
        public async Task<ListResponseViewModel<ProductViewModel>> GetProductList(ListRequestViewModel model)
        {
            ListResponseViewModel<ProductViewModel> responsemodel = new ListResponseViewModel<ProductViewModel>();
            responsemodel.page = model.page == 0 ? 1 : model.page;
            responsemodel.size = model.size == 0 ? 10 : model.size;

            var Productlist = (from u in _context.TblProduct
                                where (model.searchText == null ||
                                      model.searchText == string.Empty ||
                                      u.ProductName.ToLower().Contains(model.searchText.ToLower()))
                                      && (model.isActive == null || model.isActive == u.IsActive)
                                      && u.IsDeleted == false
                                orderby u.CreatedDate descending
                                select new ProductViewModel
                                {
                                    ProductID = u.ProductID,
                                    ProductName = u.ProductName,
                                    Cost = u.Cost,
                                    Description = u.Description,
                                    IsActive = u.IsActive,
                                    CategoryIDs = (from p in _context.TblCategoryMapping
                                                   join c in _context.TblCategory on p.CategoryID equals c.CategoryID
                                                   where p.ProductID == u.ProductID
                                                   select new DropdownViewModel
                                                   {
                                                       value = p.CategoryID,
                                                       name = c.CategoryName,
                                                   }).ToList(),
                                });
            responsemodel.totalcount = Productlist.Count();
            Productlist = Productlist.Skip((responsemodel.page - 1) * responsemodel.size).Take(responsemodel.size);
            responsemodel.data = await Productlist.ToListAsync();
            return responsemodel;
        }
        #endregion

        #region Delete Product
        public async Task<int> DeleteProduct(int[] ids)
        {
            var IdsToDelete = ids.ToList();
            var list = await _context.TblProduct.Where(r => IdsToDelete.Contains(r.ProductID)).ToListAsync();
            list = list.Select(m => { m.IsDeleted = true; m.IsActive = false; return m; }).ToList();
            await _context.SaveChangesAsync();

            var mappinglist = await _context.TblCategoryMapping.Where(r => IdsToDelete.Contains(r.ProductID)).ToListAsync();
            _context.RemoveRange(mappinglist);
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Change Status
        public async Task<int> ChangeStatus(int id, bool isActive)
        {
            TblProduct TblProduct = new TblProduct();
            if (id > 0)
            {
                TblProduct = await _context.TblProduct.Where(u => u.ProductID == id && u.IsDeleted == false).FirstOrDefaultAsync();
                if (TblProduct != null)
                {
                    TblProduct.IsActive = isActive;
                    await _context.SaveChangesAsync();
                    return 1;
                }
            }
            return 0;
        }
        #endregion
    }
}
