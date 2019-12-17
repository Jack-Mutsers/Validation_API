using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Repository
{
    public class ValidationRepository : RepositoryBase<Validation>, IValidationRepository
    {
        public ValidationRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public Validation CheckAccessToken(Guid token)
        {
            DateTime dt = DateTime.Now;
            return FindByCondition(val => val.access_token.Equals(token) && val.expiration_date > dt)
                    .FirstOrDefault();
        }

        public Validation GetvalidationByUser(Guid userId)
        {
            DateTime dt = DateTime.Now;
            return FindByCondition(val => val.userId.Equals(userId) && val.expiration_date > dt)
                    .FirstOrDefault();
        }

        public void CreateValidation(Validation val)
        {
            Create(val);
        }

        public IEnumerable<Validation> validationByUser(Guid userId)
        {
            return FindByCondition(a => a.userId.Equals(userId)).ToList();
        }

        public void updateTokenExpirationTime(Validation validationEntity)
        {
            Update(validationEntity);
        }
    }
}
