﻿namespace MyAccountAPI.Domain.Model.Customers
{
    using System;
    using System.Threading.Tasks;

    public interface ICustomerReadOnlyRepository
    {
        Task<Customer> Get(Guid id);
    }
}
