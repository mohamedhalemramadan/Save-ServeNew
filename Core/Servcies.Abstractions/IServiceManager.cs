using Servcies.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
     
        
        public IAuthenticationService AuthenticationService { get; }
        public IRestaurantService RestaurantService { get; }
        public IConsumerService ConsumerService { get; }

    }
}