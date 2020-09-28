using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Data.Infrastructure
{
    public interface IDataBaseFactory : IDisposable
    {
        DariContext Ctxt { get; }
    }
}
