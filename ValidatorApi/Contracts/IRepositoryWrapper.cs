using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IValidationRepository Validation { get; }
        IUserRepository User { get; }
        void Save();
    }
}
