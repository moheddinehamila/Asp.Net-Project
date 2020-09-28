using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Data.Infrastructure
{
   
        public class DataBaseFactory : Disposable, IDataBaseFactory
        {
            DariContext ctxt;
            public DataBaseFactory()
            {
                ctxt = new DariContext();
            }
            public DariContext Ctxt { get { return ctxt; } }
            public override void DisposeCore()
            {
                if (ctxt != null)
                    ctxt.Dispose();
            }
        }
    }

