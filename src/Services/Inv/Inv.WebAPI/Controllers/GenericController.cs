
using Asp.Versioning;
using AutoMapper;
using Inv.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inv.WebAPI.Controllers
{
    /// <summary>
    /// A generic controller to handle common CRUD operations.
    /// Supports multiple API versions and maps entities to DTOs.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TContext">The database context type.</typeparam>
    /// <typeparam name="TGetDto">The DTO type used for returning data in GET requests.</typeparam>
    /// <typeparam name="TCreateDto">The DTO type used for creating new records.</typeparam>
    /// <typeparam name="TUpdateDto">The DTO type used for updating existing records.</typeparam>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    public class GenericController<TEntity, TContext, TGetDto, TCreateDto, TUpdateDto> : ControllerBase
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;
        private static readonly string[] PrimaryKeyNames =
        {
            "SerialID", "UOMSerialID", "UOMConvSerialID", "SubCategorySerialID",
            "ModelSerialID", "MainCategorySerialID", "ItemTypeSerialID", "ItemSerialID",
            "FeedingMechaSerialID","BITSerialID", "BrandSerialID",
            "GRNSerialID","GRNLineItemSerialID","InvoiceSerialID","InvoiceItemSerialID"
            ,"SupplierSerialId","POSerialID","POItemSerialID",
            "WHSerialID","RackSerialID","ZoneSerialID","BinLctnSerialID","StoreSerialID","CusPriceCatSerialID","CompatibleItemSerialID"
        };

        /// <summary>
        /// Initializes a new instance of the generic controller.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entities and DTOs.</param>
        public GenericController(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CRUD Operations

        [HttpGet(Name = "GetAll")]
        public virtual async Task<ActionResult<IEnumerable<TGetDto>>> GetAll()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            var dtos = _mapper.Map<List<TGetDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TGetDto>> Get(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<TGetDto>(entity);
            return Ok(dto);
        }

   
        // [HttpPost]
        // public virtual async Task<ActionResult<TGetDto>> Create([FromBody] TCreateDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        // {
        //     var entity = _mapper.Map<TEntity>(createDto);

        //     if (uniquePropertyNames?.Length > 0 && await CheckDuplicatesAsync(createDto, uniquePropertyNames))
        //     {
        //         return Conflict(new { message = $"A record already exists." });
        //     }

        //     // Fetch and increment LastNumber for the entity name
        //     var lastNumber = await _theNumbersService.GetAndIncrementLastNumberAsync(typeof(TEntity).Name);
        //     Console.WriteLine($"Generated LastNumber for {typeof(TEntity).Name}: {lastNumber}");
                 
        //     _context.Set<TEntity>().Add(entity);
        //     await _context.SaveChangesAsync();

        //     var dto = _mapper.Map<TGetDto>(entity);
        //     var primaryKeyValue = GetPrimaryKeyValue(entity);

        //     if (primaryKeyValue == null)
        //         throw new InvalidOperationException("No primary key property found for the entity.");

        //     return CreatedAtAction(nameof(Get), new { id = primaryKeyValue }, dto);
        // }

        [HttpPut]
        public virtual async Task<ActionResult<TGetDto>> Update([FromBody] TUpdateDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            if (updateDto == null) return BadRequest("Invalid update data.");

            var idValue = GetIdValueFromDto(updateDto);

            if (idValue == null) return BadRequest("The update DTO does not contain a valid ID.");
            var entity = await FindEntityByIdAsync(idValue);
            if (entity == null) return NotFound("Entity not found.");
            var primaryKeyPropertyName = GetPrimaryKeyPropertyName(entity);
            if (uniquePropertyNames?.Length > 0 && await CheckDuplicatesAsync(updateDto, uniquePropertyNames, primaryKeyPropertyName))
            {
                return Conflict(new { message = $"A record already exists." });
            }

            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = idValue }, updateDto);

        }
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return NotFound();

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Helper Methods

        private object? GetIdValueFromDto(TUpdateDto dto)
        {
            var idProperty = typeof(TUpdateDto).GetProperties()
                .FirstOrDefault(prop => PrimaryKeyNames.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));
            return idProperty?.GetValue(dto);
        }

        private async Task<TEntity?> FindEntityByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        private object? GetPrimaryKeyValue(TEntity entity)
        {
            var primaryKeyProperty = entity.GetType().GetProperties()
                .FirstOrDefault(prop => PrimaryKeyNames.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));
            return primaryKeyProperty?.GetValue(entity);
        }

        private async Task<bool> CheckDuplicatesAsync(TUpdateDto updateDto, string[] uniquePropertyNames, string keyPropertyName)
        {
            if (updateDto == null)
                throw new ArgumentNullException(nameof(updateDto));
            if (uniquePropertyNames == null || uniquePropertyNames.Length == 0)
                throw new ArgumentException("Unique property names must be provided.", nameof(uniquePropertyNames));
            if (string.IsNullOrWhiteSpace(keyPropertyName))
                throw new ArgumentException("Key property name must be provided.", nameof(keyPropertyName));

            // Extract the key property value from the updateDto (used for excluding the current entity)
            var keyPropertyInfo = updateDto.GetType().GetProperty(keyPropertyName);
            if (keyPropertyInfo == null)
                throw new ArgumentException($"Key property '{keyPropertyName}' does not exist on type '{typeof(TUpdateDto).Name}'.");

            var keyPropertyValue = keyPropertyInfo.GetValue(updateDto);
            List<bool> boolList = new List<bool>();

            // Add conditions for each unique property with exact match for strings or other types
            foreach (var propertyName in uniquePropertyNames)
            {
                // Get the DbSet for the entity type
                IQueryable<TEntity> query = _context.Set<TEntity>();

                if (keyPropertyValue != null)
                {
                    // Exclude the current entity from the query (same way as before)
                    query = query.Where(e => !EF.Property<object>(e, keyPropertyName).Equals(keyPropertyValue));
                }

                var propertyInfo = updateDto.GetType().GetProperty(propertyName);
                if (propertyInfo == null)
                    throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(TUpdateDto).Name}'.");

                var propertyValue = propertyInfo.GetValue(updateDto);

                if (propertyValue != null)
                {
                    if (propertyValue is string strValue)
                    {
                        // Use exact match for string properties
                        query = query.Where(e => EF.Property<string>(e, propertyName) == strValue);
                        boolList.Add(await query.AnyAsync());
                    }
                    else
                    {
                        // Handle non-string properties as exact matches
                        query = query.Where(e => EF.Property<object>(e, propertyName).Equals(propertyValue));
                        boolList.Add(await query.AnyAsync());
                    }
                }
                else
                {
                    // Handle null values explicitly
                    query = query.Where(e => EF.Property<object>(e, propertyName) == null);
                    boolList.Add(await query.AnyAsync());
                }
            }

            // Check if any of the bool values in the list are true
            bool hasDuplicate = boolList.Any(b => b == true);
            return hasDuplicate;
        }

        private async Task<bool> CheckDuplicatesAsync(TCreateDto createDto, string[] uniquePropertyNames)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));
            if (uniquePropertyNames == null || uniquePropertyNames.Length == 0)
                throw new ArgumentException("Unique property names must be provided.", nameof(uniquePropertyNames));

            // store results in a list to query if any of the results are true or false
            List<bool> boolList = new List<bool>();


            foreach (var propertyName in uniquePropertyNames)
            {
                // Start with all entities of the type
                var query = _context.Set<TEntity>().AsQueryable();

                // Get the property info
                var propertyInfo = createDto.GetType().GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(TEntity).Name}'.");
                }

                // Get the property value
                var propertyValue = propertyInfo.GetValue(createDto);
                if (propertyValue != null)
                {
                    if (propertyValue is string strValue)
                    {
                        // Use exact match for string properties
                        query = query.Where(e => EF.Property<string>(e, propertyName) == strValue);
                        boolList.Add(await query.AnyAsync());
                    }
                    else
                    {
                        // Handle non-string properties as exact matches
                        query = query.Where(e => EF.Property<object>(e, propertyName).Equals(propertyValue));
                        boolList.Add(await query.AnyAsync());
                    }

                }
                else
                {
                    // Handle null values explicitly if needed
                    query = query.Where(e => EF.Property<object>(e, propertyName) == null);
                    boolList.Add(await query.AnyAsync());
                }
            }

            // Check if any of the bool values in the list are true
            bool hasDuplicate = boolList.Any(b => b == true);
            return hasDuplicate;
        }
        
        private string? GetPrimaryKeyPropertyName(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Common naming conventions for primary keys
            var primaryKeyProperty = entity.GetType().GetProperties()
                .FirstOrDefault(prop => PrimaryKeyNames.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));

            return primaryKeyProperty?.Name;
        }

        #endregion
    }
}



#region GenericController
/*
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Inv.WebAPI.Controllers
{
    /// <summary>
    /// A generic controller to handle common CRUD operations.
    /// Supports multiple API versions and maps entities to DTOs.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TContext">The database context type.</typeparam>
    /// <typeparam name="TGetDto">The DTO type used for returning data in GET requests.</typeparam>
    /// <typeparam name="TCreateDto">The DTO type used for creating new records.</typeparam>
    /// <typeparam name="TUpdateDto">The DTO type used for updating existing records.</typeparam>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class GenericController<TEntity, TContext, TGetDto, TCreateDto, TUpdateDto> : ControllerBase
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        private static readonly string[] PrimaryKeyNames =
{
            "SerialID", "UOMSerialID", "UOMConvSerialID", "SubCategorySerialID", "RentAssetRegSerialID",
            "ModelSerialID", "MainCategorySerialID", "ItemTypeSerialID", "ItemSerialID", "IntAssetRegSerialID",
            "InAssetLocSerialID", "FeedingMechaSerialID", "ExAssetLocSerialID", "DetachedAssetSerialID",
            "BITSerialID", "BrandSerialID", "BedTypeSerialID", "AttchmntSerialID", "AssetTypeSerialID",
            "AssetTranferSerialID", "AssetSubTypeSerialID", "AssetCompoSerialID", "AssetAttachSerialID",
            "AssetAdjustValueSerialID"
        };
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entities and DTOs.</param>
        public GenericController(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A list of all entities mapped to the Get DTO type.</returns>
        [HttpGet(Name = "GetAll")]
        public virtual async Task<ActionResult<IEnumerable<TGetDto>>> GetAll()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            var dtos = _mapper.Map<List<TGetDto>>(entities);
            return Ok(dtos);
        }
        /// <summary>
        /// Retrieves a specific entity by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity mapped to the Get DTO type.</returns>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TGetDto>> Get(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TGetDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Generic create method with optional duplicate validation for multiple properties.
        /// </summary>
        /// <param name="createDto">The DTO to create an entity.</param>
        /// <param name="uniquePropertyNames">The optional array of property names to check for uniqueness.</param>
        public virtual async Task<ActionResult<TGetDto>> Create(
            [FromBody] TCreateDto createDto,
            [FromQuery] string[]? uniquePropertyNames = null)
        {
            // Map the DTO to the entity
            var entity = _mapper.Map<TEntity>(createDto);

            // Check for duplicates if uniquePropertyNames is provided
            if (uniquePropertyNames != null && uniquePropertyNames.Length > 0)
            {
                var query = _context.Set<TEntity>().AsQueryable();

                foreach (var propertyName in uniquePropertyNames)
                {
                    var propertyValue = entity.GetType()
                        .GetProperty(propertyName)?
                        .GetValue(entity);

                    if (propertyValue != null)
                    {
                        query = query.Where(e =>
                            EF.Property<object>(e, propertyName).Equals(propertyValue));
                    }
                }

                var exists = await query.AnyAsync();
                if (exists)
                {
                    return Conflict(new { message = $"A record values for '{string.Join(", ", uniquePropertyNames)}' already exists." });
                }
            }

            // Add and save the new entity
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            // Map the created entity to the DTO
            var dto = _mapper.Map<TGetDto>(entity);
            // Retrieve the primary key value dynamically
            var primaryKeyNames = PrimaryKeyNames;

            var primaryKeyProperty = entity.GetType().GetProperties()
                .FirstOrDefault(prop => primaryKeyNames.Contains(prop.Name, StringComparer.OrdinalIgnoreCase));

            if (primaryKeyProperty == null)
                throw new InvalidOperationException("No primary key property found for the entity.");

            var primaryKeyValue = primaryKeyProperty.GetValue(entity);
            // Return the created response
            return CreatedAtAction(nameof(Create), new { id = primaryKeyValue }, dto); // Replace "Id" if necessary
        }


        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="updateDto">The DTO containing the updated data for the entity.</param>
        /// <returns>An HTTP status indicating success or failure.</returns>
        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("The provided update data is invalid.");
            }

            // Attempt to retrieve the ID value from the DTO
            var idValue = GetIdValueFromDto(updateDto);
            if (idValue == null)
            {
                return BadRequest("The update DTO does not contain a valid 'id' property.");
            }

            // Fetch the entity from the database using the ID
            var entity = await FindEntityByIdAsync(idValue);
            if (entity == null)
            {
                return NotFound("Entity not found.");
            }

            // Map the updated DTO values onto the existing entity and save
            _mapper.Map(updateDto, entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gets the ID value from the DTO based on predefined property names.
        /// </summary>
        /// <param name="dto">The DTO object.</param>
        /// <returns>The value of the ID property, or null if not found.</returns>
        private object GetIdValueFromDto(TUpdateDto dto)
        {
            var idPropertyNames = PrimaryKeyNames;

            var idProperty = typeof(TUpdateDto).GetProperties().FirstOrDefault(prop => idPropertyNames.Contains(prop.Name));
            return idProperty?.GetValue(dto);
        }

        /// <summary>
        /// Finds an entity by its ID in the database context.
        /// </summary>
        /// <param name="id">The ID value of the entity to find.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        private async Task<TEntity> FindEntityByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        /// <summary>
        /// Deletes a specific entity by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>An HTTP status indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
*/
/*[HttpPost]
   public virtual async Task<ActionResult<TGetDto>> Create([FromBody] TCreateDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
   {
       var entity = _mapper.Map<TEntity>(createDto);

       if (uniquePropertyNames?.Length > 0 && await CheckDuplicatesAsync(entity, uniquePropertyNames))
       {
           return Conflict(new { message = $"A record with the specified properties already exists." });
       }

       _context.Set<TEntity>().Add(entity);
       await _context.SaveChangesAsync();

       var dto = _mapper.Map<TGetDto>(entity);
       var primaryKeyValue = GetPrimaryKeyValue(entity);

       if (primaryKeyValue == null)
           throw new InvalidOperationException("No primary key property found for the entity.");

       return CreatedAtAction(nameof(Get), new { id = primaryKeyValue }, dto);
   }

   [HttpPut]
   public virtual async Task<IActionResult> Update([FromBody] TUpdateDto updateDto)
   {
       if (updateDto == null) return BadRequest("Invalid update data.");

       var idValue = GetIdValueFromDto(updateDto);
       if (idValue == null) return BadRequest("The update DTO does not contain a valid ID.");

       var entity = await FindEntityByIdAsync(idValue);
       if (entity == null) return NotFound("Entity not found.");

       _mapper.Map(updateDto, entity);
       await _context.SaveChangesAsync();

       return NoContent();
   }*/
#endregion