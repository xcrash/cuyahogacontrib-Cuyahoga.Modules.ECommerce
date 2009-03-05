using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Domain.Catalogue.Interfaces {
    public interface IAttributeSource {
        string GetValue(string referenceName);
        void SetValue(string referenceName, string val);
    }
}