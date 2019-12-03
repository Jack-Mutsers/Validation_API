using Entities.Models;
using System;

namespace Contracts
{
    public interface IValidationRepository
    {
        Validation CheckAccessToken(Guid token);
        Validation GetvalidationByUser(Guid userId);
        void CreateValidation(Validation val);
    }
}
