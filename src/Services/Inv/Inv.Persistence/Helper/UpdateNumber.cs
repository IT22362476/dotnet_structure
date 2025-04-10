using Inv.Application.Interfaces.Repositories;
using Inv.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inv.Persistence.Helper
{
    public static class UpdateNumber
    {
        // Helper method to update the last number of a given entity
        public static async Task UpdateLastNumber(IUnitOfWork unitOfWork, string entityName, int? comSerialID)
        {
            var param = await unitOfWork.Repository<TheNumber>()
                .Entities
                .FirstOrDefaultAsync(p => p.TheNumberName == entityName && p.ComSerialID == comSerialID);
            if (param == null)
            {
                // Create a new record if it doesn't exist
                var numberEntity = new TheNumber
                {
                    TheNumberName = entityName,
                    LastNumber = 1,
                    ComSerialID = comSerialID,
                    CreatedDate = DateTime.UtcNow,
                    Active = true,
                    IsDeleted = false
                };

                await unitOfWork.Repository<TheNumber>().AddAsync(numberEntity);
            }
            if (param != null)
            {
                param.LastNumber++;
                await unitOfWork.Repository<TheNumber>().UpdateAsync(param, param.TheNumberSerialID);
            }
        }

        // Rest of your code...
    }
}
