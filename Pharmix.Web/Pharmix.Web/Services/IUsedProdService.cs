using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Enums;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels.UsedProduct;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public interface IUsedProdService
    {
        UsedProduct GetUsedProductById(int id);
        IEnumerable<UsedProduct> GetAllUsedProductByOrderId(int id);
        UsedProductListViewModel CreateUsedProdListViewModel(int orderId, int isoId);
        VtmViewModel CreateVtmViewModel(int vtmId);
        UsedProductViewModel CreateViewModel(int id, int orderId, int isoId);
        GridViewModel GetUsedProdSearchResult(SearchRequest request, int orderId);
        UsedProduct MapViewModelToUsedProduct(UsedProductViewModel model, string user, bool proceedSave);
        BaseResultViewModel<string> RemoveUsedProduct(int id, string user);
        IEnumerable<Vtm> GetAllVtms();
        Vtm GetVtmById(int id);
        Vtm SaveVtm(VtmViewModel model, string user);
        BaseResultViewModel<string> DeleteVtm(int id, string user);
        GridViewModel GetVtmSearchResult(SearchRequest request);
        IEnumerable<DmdRoute> GetAllDmdRoutes();
        IEnumerable<DmdSupplier> GetAllDmdSuppliers();
    }

    public class UsedProdService : IUsedProdService
    {
        private IRepository repository;

        public UsedProdService(IRepository repository)
        {
            this.repository = repository;
        }

        public UsedProduct GetUsedProductById(int id)
        {
            return repository.GetById<UsedProduct>(id);
        }

        public IEnumerable<UsedProduct> GetAllUsedProductByOrderId(int id)
        {
            return repository.GetContext().UsedProducts
                .Include(p => p.Vtm)
                .Include(p => p.Stocks)
                .ThenInclude(p => p.StorageLocation)
                .Where(p => p.Stocks != null && p.Stocks.Any(s => s.IntegrationOrderId == id))
                .Where(p => !p.IsArchived);
        }
        
        public IEnumerable<StorageLocation> GetAllStorageLocation()
        {
            return repository.GetContext().StorageLocations.Where(p => !p.IsArchived);
        }

        public IEnumerable<Vtm> GetAllVtms()
        {
            return repository.GetContext().Vtms.Where(p => !p.IsArchived);
        }

        public UsedProductListViewModel CreateUsedProdListViewModel(int orderId, int isoId)
        {
            var order = repository.GetById<IntegrationOrder>(orderId);

            if(order == null) return new UsedProductListViewModel();

            var model = new UsedProductListViewModel
            {
                IntegrationOrderId = orderId,
                IntegrationOrderName = order.Name,
                IsolatorId = isoId,
                VtmViewModel = CreateVtmViewModel(0)
            };
            
            return model;
        }

        public VtmViewModel CreateVtmViewModel(int vtmId)
        {
            var vtm = repository.GetById<Vtm>(vtmId);
            var model = vtm == null ? new VtmViewModel() : Mapper.Map<Vtm, VtmViewModel>(vtm);

            model.DmdRouteList = new SelectList(GetAllDmdRoutes(), "Id", "Route", model.DmdRouteId);
            model.DmdSupplierList = new SelectList(GetAllDmdSuppliers(), "Id", "SupplierName", model.DmdSupplierId);

            return model;
        }

        public UsedProductViewModel CreateViewModel(int id, int orderId, int isoId)
        {
            var prod = repository.GetContext().UsedProducts
                .Include(p => p.Stocks)
                .FirstOrDefault(p => p.UsedProductId == id);

            var model = new UsedProductViewModel();

            if (prod != null)
            {
                model = Mapper.Map<UsedProduct, UsedProductViewModel>(prod);
                var stock = prod.Stocks.FirstOrDefault();
                if (stock != null)
                    model.Stock = Mapper.Map<UsedProductStock, UsedProductStockViewModel>(stock);
            }
            else
            {
                var order = repository.GetById<IntegrationOrder>(orderId);
                model.Stock.IntegrationOrderId = order.Id;
                model.Stock.IntegrationOrderName = order.Name;
                model.Stock.IsolatorId = isoId;
            }
            
            model.VtmList = new SelectList(GetAllVtms(), "VtmId", "DrugName", model.VtmId);
            var locations = GetAllStorageLocation().Select(p => new { Value = p.StorageLocationId, Text = string.Format("{0} ({1})", p.LocationName, p.LocationCode) });
            model.Stock.StorageLocationList = new SelectList(locations, "Value", "Text", model.Stock.StorageLocationId);

            return model;
        }

        public GridViewModel GetUsedProdSearchResult(SearchRequest request, int orderId)
        {
            var model = IntegrationOrderMapper.CreateUsedProdGridViewModel();

            var pageResult = QueryListHelper.SortResults(GetAllUsedProductByOrderId(orderId), request);
            var rows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.CustomName.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(IntegrationOrderMapper.BindUsedProdGridData);
            model.Rows = rows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public UsedProduct MapViewModelToUsedProduct(UsedProductViewModel model, string user, bool proceedSave)
        {
            var prod = GetUsedProductById(model.UsedProductId);
            prod = prod == null ? Mapper.Map<UsedProductViewModel, UsedProduct>(model) : Mapper.Map(model, prod);

            if (!proceedSave) return prod;

            if (prod.UsedProductId > 0)
            {
                prod.SetUpdateDetails(user);
                repository.SaveExisting(prod);
            }
            else
            {
                prod.SetCreateDetails(user);
                repository.SaveNew(prod);
            }

            var stock = repository.GetById<UsedProductStock>(model.Stock.UsedProductStockId);
            stock = stock == null ? Mapper.Map<UsedProductStockViewModel, UsedProductStock>(model.Stock) : Mapper.Map(model.Stock, stock);

            stock.UsedProductId = prod.UsedProductId;
            stock.StockStatusId = (int)StockStatusEnum.Available;

            if (stock.UsedProductStockId > 0)
            {
                stock.SetUpdateDetails(user);
                repository.SaveExisting(stock);
            }
            else
            {
                stock.SetCreateDetails(user);
                repository.SaveNew(stock);
            }

            return prod;
        }

        public BaseResultViewModel<string> RemoveUsedProduct(int id, string user)
        {
            var usedProd = repository.GetById<UsedProduct>(id);

            usedProd.SetArchiveDetails(user);
            repository.SaveExisting(usedProd);

            return new BaseResultViewModel<string>(){ IsSuccess = true, Message = "Used product has been removed successfully."};
        }
        
        public Vtm GetVtmById(int id)
        {
            return repository.GetById<Vtm>(id);
        }

        public Vtm SaveVtm(VtmViewModel model, string user)
        {
            var vtm = GetVtmById(model.VtmId);
            vtm = vtm == null ? Mapper.Map<VtmViewModel, Vtm>(model) : Mapper.Map(model, vtm);
            
            if (vtm.VtmId > 0)
            {
                vtm.SetUpdateDetails(user);
                repository.SaveExisting(vtm);
            }
            else
            {
                vtm.SetCreateDetails(user);
                repository.SaveNew(vtm);
            }

            return vtm;
        }

        public BaseResultViewModel<string> DeleteVtm(int id, string user)
        {
            var usedProd = repository.GetById<Vtm>(id);

            if (usedProd.IsLicensed)
                return new BaseResultViewModel<string>() { IsSuccess = false, Message = "Licensed VTM can not be deleted." };

            usedProd.SetArchiveDetails(user);
            repository.SaveExisting(usedProd);

            return new BaseResultViewModel<string>() { IsSuccess = true, Message = "VTM has been deleted successfully." };
        }

        public GridViewModel GetVtmSearchResult(SearchRequest request)
        {
            var model = IntegrationOrderMapper.CreateVtmGridViewModel();

            var vtms = repository.GetContext().Vtms
                .Include(p => p.DmdRoute)
                .Include(p => p.DmdSupplier)
                .Where(p => !p.IsArchived);

            var pageResult = QueryListHelper.SortResults(vtms, request);
            var serviceRows = pageResult
                .Where(p => string.IsNullOrEmpty(request.SearchText) || p.DrugName.StartsWith(request.SearchText, StringComparison.CurrentCultureIgnoreCase))
                .Select(IntegrationOrderMapper.BindVtmGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public IEnumerable<DmdRoute> GetAllDmdRoutes()
        {
            return repository.GetContext().DmdtRoutes;
        }

        public IEnumerable<DmdSupplier> GetAllDmdSuppliers()
        {
            return repository.GetContext().DmdSuppliers;
        }
    }
}
