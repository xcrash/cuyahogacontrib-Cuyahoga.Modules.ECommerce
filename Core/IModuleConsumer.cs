using System;

namespace Cuyahoga.Modules.ECommerce.Core {
    public interface IModuleConsumer {
        Cuyahoga.Core.Domain.ModuleBase Module { get; set;}
    }
}
