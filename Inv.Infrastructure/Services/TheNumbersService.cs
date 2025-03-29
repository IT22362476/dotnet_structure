using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inv.Infrastructure.Services
{
    public class TheNumbersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TheNumbersService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            if (httpContextAccessor != null)
            {
                _httpContextAccessor = httpContextAccessor;
            }
        }

        /// <summary>
        /// Fetch and increment the LastNumber for a given entity name.
        /// </summary>
        /// <param name="entityName">The name of the entity.</param>
        /// <returns>The updated LastNumber.</returns>
        public async Task<int> GetAndIncrementLastNumberAsync(string entityName, CancellationToken cancellationToken = default)
        {
            var numberEntity = await _unitOfWork.Repository<TheNumber>()
                .Entities
                .FirstOrDefaultAsync(n => n.TheNumberName == entityName);
            int userID = 0;
            var token = _httpContextAccessor?.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var applicationUserId = jwt.Claims.First(c => c.Type == "userSerialID");
                userID = applicationUserId.Value == null ? 0 : Convert.ToInt32(applicationUserId.Value);
            }
            if (numberEntity == null)
            {
                try
                {
                    // Create a new record if it doesn't exist
                    numberEntity = new TheNumber
                    {
                        TheNumberName = entityName,
                        LastNumber = 1,
                        CreatedBy = userID,
                        CreatedDate = DateTime.UtcNow,
                        Active = true,
                        IsDeleted = false
                    };

                    await _unitOfWork.Repository<TheNumber>().AddAsync(numberEntity);
                    await _unitOfWork.Save(cancellationToken);

                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                }
            }
            else
            {
                try
                {
                    // Increment LastNumber
                    numberEntity.LastNumber += 1;
                    numberEntity.CreatedBy = userID;

                    await _unitOfWork.Repository<TheNumber>().UpdateAsync(numberEntity, numberEntity.TheNumberSerialID);
                    await _unitOfWork.Save(cancellationToken);

                }
                catch (Exception ex) { 
                    await _unitOfWork.Rollback();
                }

            }

            return numberEntity.LastNumber;
        }
    }

}
