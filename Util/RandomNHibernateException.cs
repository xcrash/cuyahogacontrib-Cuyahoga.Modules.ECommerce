using System;
using System.Collections.Generic;
using System.Text;

namespace Cuyahoga.Modules.ECommerce.Util {
    public class RandomNHibernateException : Exception {
        public RandomNHibernateException(string message)
            : base(message) {
        }
    }
}