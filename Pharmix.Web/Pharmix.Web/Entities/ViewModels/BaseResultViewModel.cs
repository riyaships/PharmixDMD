using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Entities.ViewModels
{
    public class BaseResultViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Extra { get; set; }
    }
}
