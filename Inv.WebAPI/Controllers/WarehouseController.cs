using Asp.Versioning;
using AutoMapper;
using Inv.Application.DTOs.Warehouse;
using Inv.Domain.Entities;
using Inv.Infrastructure.Services;
using Inv.Persistence.Contexts;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inv.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing asset types.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    [ApiController]
    public class WarehouseController : GenericController<Warehouse, ApplicationDbContext, GetWarehouseDto, CreateWarehouseDto, UpdateWarehouseDto>
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public WarehouseController(ApplicationDbContext context, ILogger<WarehouseController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
: base(context, mapper, theNumbersService)
        {
            _context = context; // Assigning context to the private field
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;

        }

        /// <summary>
        /// Retrieves all asset types.
        /// </summary>
        /// <returns>A list of asset types.</returns>
        [HttpGet(Name = "GetWarehouse")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewWarehouse)]
        public override async Task<ActionResult<IEnumerable<GetWarehouseDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to retrieve.</param>
        [HttpGet("{id}")]
         [AuthorizeMultiplePermissions(Permissions.Btn_SetViewWarehouse, Permissions.Btn_SetViewWarehouse)]
        public override async Task<ActionResult<GetWarehouseDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new asset type.
        /// </summary>
        /// <param name="createDto">The DTO representing the asset type to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateWarehouse")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveWarehouse)]
        public override async Task<ActionResult<GetWarehouseDto>> Create([FromBody] CreateWarehouseDto createDto, string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "WHName" });
        }

        /// <summary>
        /// Updates an existing asset type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the asset type updates.</param>
        ///  <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditWarehouse)]
        public override async Task<ActionResult<GetWarehouseDto>> Update([FromBody] UpdateWarehouseDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "WHName" } );
        }

        /// <summary>
        /// Deletes an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteWarehouse)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        /// <summary>
        /// Retrieves asset types based on custom criteria.
        /// </summary>
        /// <param name="criteria">The criteria for filtering asset types.</param>
        [HttpGet("custom")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewWarehouse)]
        public async Task<ActionResult<IEnumerable<GetWarehouseDto>>> GetByCustomCriteria([FromQuery] string criteria)
        {
            // Example of custom filtering logic
            var entities = await _context.Set<Warehouse>()
                .Where(a => a.WHName.Contains(criteria)) // Filter by name containing the criteria
                .ToListAsync();

            var dtos = _mapper.Map<List<GetWarehouseDto>>(entities);
            return Ok(dtos);
        }
    }
}
