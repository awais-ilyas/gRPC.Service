using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserId == 1)
            {
                output.FirstName = "Awais";
                output.LastName = "Ilyas";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Tim";
                output.LastName = "Co";
            }
            else
            {
                output.FirstName = "Jane";
                output.LastName = "Bae";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Awais",
                    LastName = "Ilyas",
                    Age = 30,
                    EmailAddress = "im.ai@gmail.com",
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Jabe",
                    LastName = "Jo",
                    Age = 22,
                    EmailAddress = "mail@gmail.com",
                    IsAlive = true
                },
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
