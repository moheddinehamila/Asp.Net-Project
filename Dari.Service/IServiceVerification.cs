using Dari.Domain;
using System.Collections.Generic;

namespace Dari.Service
{
    public interface IServiceVerification : IService<Verification>
    {
        IEnumerable<Verification> GetByuserid(string UserId);

    }
}