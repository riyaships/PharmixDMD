using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmix.Data.Entities.Context;
using Pharmix.Data.Entities.ViewModels;
using Pharmix.Data.Enums;
using Pharmix.Services.Mappers;
using Pharmix.Web.Entities;
using Pharmix.Web.Entities.ViewModels;
using Pharmix.Web.Entities.ViewModels.IntegrationOrder;
using Pharmix.Web.Extensions;
using Pharmix.Web.Models;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmix.Web.Entities.ViewModels.Production;
using X.PagedList;

namespace Pharmix.Web.Services
{
    public class IntegrationOrderService : IIntegrationOrderService
    {
        private readonly IRepository _repository;
        public IntegrationOrderService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<IntegrationOrder> GetIntegrationOrders()
        {
            return _repository.GetAll<IntegrationOrder>().Where(p => !p.IsArchived);
        }

        public IEnumerable<IntegrationOrderClassification> GetOrderClassifications()
        {
            return _repository.GetAll<IntegrationOrderClassification>().Where(p => !p.IsArchived);
        }

        public IEnumerable<IntegrationOrderProgress> GetOrderProgresses()
        {
            return _repository.GetAll<IntegrationOrderProgress>().Where(p => !p.IsArchived);
        }

        public IntegrationOrder GetIntegrationOrder(int id)
        {
            return _repository.GetById<IntegrationOrder>(id);
        }

        public BaseResultViewModel<string> ApproveOrder(int OrderId, string user, int location)
        {
            var result = new BaseResultViewModel<string>
            {
                IsSuccess = false,
                Message = "",
                Extra = null
            };

            try
            {
                var getStatusProgress = _repository.GetAll<IntegrationOrderProgress>().Where(p => !p.IsArchived && p.Name.Contains("Approve", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (getStatusProgress != null)
                {
                    var findOrder = _repository.GetById<IntegrationOrder>(OrderId);
                    findOrder.OrderLastProgressId = getStatusProgress.Id;
                    _repository.SaveExisting(findOrder);

                    //Save to tracker
                    var tracker = new IntegrationOrderTracking
                    {
                        IntegrationOrderId = findOrder.Id,
                        OrderCurrentLocationId = location,
                        OrderLastProgressId = findOrder.OrderLastProgressId
                    };
                    tracker.SetCreateDetails(user);
                    var saveTracking = _repository.SaveNew(tracker);

                    result.IsSuccess = true;
                    result.Message = "Success: Order approved";
                }
                else
                {
                    result.Message = "No Approve Order status describe on database, please check database";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public BaseResultViewModel<string> SaveUpdateIntegrationOrder(IntegrationOrderViewModel integrationOrderViewModel, string user)
        {
            var result = new BaseResultViewModel<string>
            {
                IsSuccess = false,
                Message = "",
                Extra = null
            };

            try
            {
                if (integrationOrderViewModel.Id > 0)
                {       //Edit 
                    var integrationOrder = _repository.GetById<IntegrationOrder>(integrationOrderViewModel.Id);

                    integrationOrder.Name = integrationOrderViewModel.Name; //DateTime.Now.ToString("ddmmyyyy hh:mm:ss");
                    integrationOrder.IntegratedSystemId = integrationOrderViewModel.IntegratedSystemId;
                    integrationOrder.AdministeredDate = integrationOrderViewModel.AdministeredDate;
                    integrationOrder.AllocatedIsolatorId = integrationOrderViewModel.AlocatedIsolatorId;
                    integrationOrder.BookedInDate = integrationOrderViewModel.BookedInDate;
                    integrationOrder.ExternalBarcode = integrationOrderViewModel.ExternalBarcode;
                    integrationOrder.ExternalOrderId = integrationOrderViewModel.ExternalOrderId;
                    integrationOrder.OrderLastProgressId = (int)OrderProgressEnum.Scheduled;
                    integrationOrder.RequiredDate = integrationOrderViewModel.RequiredDate;
                    integrationOrder.ScheduledDate = integrationOrderViewModel.ScheduledDate; 

                    _repository.SaveExisting<IntegrationOrder>(integrationOrder, user);
                }
                else
                {
                    var integrationOrder = new IntegrationOrder
                    {
                        Name = integrationOrderViewModel.Name,
                        IsArchived = false,
                        IntegratedSystemId = integrationOrderViewModel.IntegratedSystemId,
                        AdministeredDate = integrationOrderViewModel.AdministeredDate,
                        AllocatedIsolatorId = integrationOrderViewModel.AlocatedIsolatorId,
                        BookedInDate = integrationOrderViewModel.BookedInDate,
                        ExternalBarcode = integrationOrderViewModel.ExternalBarcode,
                        ExternalOrderId = integrationOrderViewModel.ExternalOrderId,
                        OrderLastProgressId = (int)OrderProgressEnum.Scheduled,
                        RequiredDate = integrationOrderViewModel.RequiredDate,
                        ScheduledDate = integrationOrderViewModel.ScheduledDate
                    };
                    //Mapper.Map<IntegrationOrderViewModel, IntegrationOrder>(integrationOrderViewModel);
                    integrationOrder.SetCreateDetails(user);
                    if (integrationOrder.Id == 0)
                    {
                        //integrationOrder.OrderLastProgressId = (int) OrderStatusEnum.New;
                        var saveNew = _repository.SaveNew(integrationOrder, user);
                        result.IsSuccess = true;
                        result.Message = "Success: Create new Integration Order";
                        return result;
                    }

                    //integrationOrder.OrderLastProgressId = (int)OrderStatusEnum.Pending;
                    _repository.SaveExisting<IntegrationOrder>(integrationOrder, user);
                    result.IsSuccess = true;
                    result.Message = "Success: Update Integration Order.";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public GridViewModel GetSearchResult(SearchRequest request)
        {
            var model = IntegrationOrderMapper.CreateGridViewModel();
            var getAllIntegrationOrder = _repository.GetContext().IntegrationOrders
                .Where(p => !p.IsArchived && p.OrderLastProgressId != (int)OrderProgressEnum.Approved);

            var pageResult = QueryListHelper.SortResults(getAllIntegrationOrder, request);
            var serviceRows = pageResult
                .Select(IntegrationOrderMapper.BindGridData);
            model.Rows = serviceRows.ToPagedList(request.Page ?? 1, request.PageSize);

            return model;
        }

        public IntegrationOrderViewModel CreateViewModel(int id)
        {
            var integrationOrder = GetIntegrationOrder(id);
            var model = integrationOrder == null ? new IntegrationOrderViewModel() : Mapper.Map<IntegrationOrder, IntegrationOrderViewModel>(integrationOrder);
            model.Isolators = _repository.GetAll<Isolator>().Where(p => !p.IsArchived);
            model.IntegratedSystem = _repository.GetAll<IntegratedSystem>().Where(p => !p.IsArchived);

            return model;
        }

        public CallSupervisorViewModel CreateCallSupervisorViewModel(int OrderId)
        {
            var model = new CallSupervisorViewModel
            {
                SupervisorRequest = new SupervisorRequestViewModel
                {
                    SupervisorRequetTypes = _repository.GetAll<SupervisorRequestType>().Where(p => !p.IsArchived),
                    CurrentOrderId = OrderId,
                    IntegrationOrders = _repository.GetAll<IntegrationOrder>().Where(p => p.Id == OrderId),
                    Isolators = _repository.GetAll<Pharmix.Web.Entities.Isolator>().Where(p => !p.IsArchived)
                },
                IntegrationOrderComment = new IntegrationOrderCommentViewModel
                {
                    OrderId = OrderId
                }
            };

            return model;
        }

        public BaseResultViewModel<string> CallSupervisor(CallSupervisorViewModel param, string user, int location)
        {
            var result = new BaseResultViewModel<string>
            {
                IsSuccess = false,
                Message = "",
                Extra = null
            };

            try
            {
                var getStatusProgress = _repository.GetAll<IntegrationOrderProgress>().Where(p => !p.IsArchived && p.Name.Contains("Supervisor", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (getStatusProgress != null)
                {
                    //SupervisorRequest
                    var request = new SupervisorRequest()
                    {
                        Title = param.SupervisorRequest.Title,
                        IsArchived = false,
                        IsolatorId = param.SupervisorRequest.IsolatorId,
                        LatestRequestStatus = RequestStatusEnum.Awaiting,
                        Priority = param.SupervisorRequest.Priority,
                        CurrentOrderId = param.IntegrationOrderComment.OrderId,
                        TypeId = param.SupervisorRequest.TypeId,
                        RequestedBy = user
                    };
                    request.SetCreateDetails(user);
                    _repository.SaveNew<SupervisorRequest>(request);

                    //Update progress status of IntegrationOrder
                    var findOrder = _repository.GetById<IntegrationOrder>(param.IntegrationOrderComment.OrderId);
                    findOrder.OrderLastProgressId = getStatusProgress.Id;
                    _repository.SaveExisting(findOrder);

                    //Save to tracker
                    var tracker = new IntegrationOrderTracking
                    {
                        IntegrationOrderId = findOrder.Id,
                        OrderCurrentLocationId = location,
                        OrderLastProgressId = findOrder.OrderLastProgressId,
                        Comment = param.IntegrationOrderComment.Comment
                    };
                    tracker.SetCreateDetails(user);
                    var saveTracking = _repository.SaveNew(tracker);

                    result.IsSuccess = true;
                    result.Message = "Success: Call Supervisor";
                    return result;
                }
                else
                {
                    result.Message = "No Call Supervisor status describe on database, please check database";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Extra = ex.Message;
            }

            return result;
        }

        public BaseResultViewModel<string> SaveActionDelineClassify(IntegrationOrderCommentViewModel param, string user, int location)
        {
            var result = new BaseResultViewModel<string>
            {
                IsSuccess = false,
                Message = "",
                Extra = null
            };

            try
            {
                //0 = Decline Order, 1 = Classify Order
                if (param.State == 0)
                {
                    var getStatusProgress = _repository.GetAll<IntegrationOrderProgress>().Where(p => !p.IsArchived && p.Name.Contains("Decline", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (getStatusProgress != null)
                    {
                        var findOrder = _repository.GetById<IntegrationOrder>(param.OrderId);
                        findOrder.OrderLastProgressId = getStatusProgress.Id;
                        _repository.SaveExisting(findOrder);

                        //Save to tracker
                        var tracker = new IntegrationOrderTracking
                        {
                            IntegrationOrderId = findOrder.Id,
                            OrderCurrentLocationId = location,
                            OrderLastProgressId = findOrder.OrderLastProgressId,
                            Comment = param.Comment
                        };
                        tracker.SetCreateDetails(user);
                        var saveTracking = _repository.SaveNew(tracker);

                        result.IsSuccess = true;
                        result.Message = "Order Declined";
                        return result;
                    }
                    else
                    {
                        result.Message = "No Declined status describe on database, please check database";
                        return result;
                    }

                }
                else if (param.State == 1)
                {
                    var getStatusProgress = _repository.GetAll<IntegrationOrderProgress>().Where(p => !p.IsArchived && p.Name.Contains("Classify", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (getStatusProgress != null)
                    {
                        var findOrder = _repository.GetById<IntegrationOrder>(param.OrderId);
                        findOrder.OrderLastProgressId = getStatusProgress.Id;
                        findOrder.OrderlastClassificationId = param.Classification;
                        _repository.SaveExisting(findOrder);

                        //Save to tracker
                        var tracker = new IntegrationOrderTracking
                        {
                            IntegrationOrderId = findOrder.Id,
                            OrderCurrentLocationId = location,
                            OrderLastProgressId = findOrder.OrderLastProgressId,
                            OrderLastClassificationId = findOrder.OrderlastClassificationId,
                            Comment = param.Comment
                        };
                        tracker.SetCreateDetails(user);
                        var saveTracking = _repository.SaveNew(tracker);

                        result.IsSuccess = true;
                        result.Message = "Order Declined";
                        return result;
                    }
                    else
                    {
                        result.Message = "No Classify Order status describe on database, please check database";
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Extra = ex.Message;
            }

            return result;
        }
    }
}
