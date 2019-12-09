using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IValidationRepository
    {
        Validation CheckAccessToken(Guid token);
        Validation GetvalidationByUser(Guid userId);
        void updateTokenExpirationTime(Validation validationEntity);
        void CreateValidation(Validation val); 
        IEnumerable<Validation> validationByUser(Guid userId);
    }
}
