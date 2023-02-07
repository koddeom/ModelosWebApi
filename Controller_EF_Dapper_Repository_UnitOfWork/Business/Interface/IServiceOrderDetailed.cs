﻿using Controller_EF_Dapper_Repository_UnityOfWork.Business.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Business.Interface
{
    public interface IServiceOrderDetailed
    {
        Task<OrderDetailed> Get(Guid id);

        Task<ObjectResult> SaveOrder(List<Guid> orderProductsId, OrderBuyer orderBuyer);
    }
}