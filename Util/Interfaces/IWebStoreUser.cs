using System;
using System.Collections.Generic;
using Cuyahoga.Modules.ECommerce.Util.Location;
namespace Cuyahoga.Modules.ECommerce.Util.Interfaces {
    public interface IWebStoreUser {
        IUserDetails UserDetails {get; set; }
        IAddress UserAddress { get; set; }
    }
}
