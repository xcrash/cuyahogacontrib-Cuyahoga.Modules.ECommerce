using System;
using System.Collections.Generic;
using System.Text;
using Cuyahoga.Web.HttpModules;

namespace Cuyahoga.Modules.ECommerce.Service.Interfaces {
    interface IAuthenticationService {
        bool AuthenticateUser(AuthenticationModule authModule, string username, string password);
    }
}
