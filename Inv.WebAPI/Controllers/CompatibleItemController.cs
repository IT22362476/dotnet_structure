using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inv.Domain.Entities;
using Inv.Persistence.Contexts;
using Inv.Application.DTOs.CompatibleItem;
using JwtTokenAuthentication.Permission;
using JwtTokenAuthentication.Constants;
using Inv.Infrastructure.Services;

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
    public class CompatibleItemController : GenericController<CompatibleItem, ApplicationDbContext, GetCompatibleItemDto, CreateCompatibleItemDto, UpdateCompatibleItemDto>
    {
        private readonly ILogger<CompatibleItemController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="CompatibleItemController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public CompatibleItemController(ApplicationDbContext context, ILogger<CompatibleItemController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
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
        [HttpGet(Name = "GetCompatibleItem")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCompatibleItems)]
        public override async Task<ActionResult<IEnumerable<GetCompatibleItemDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to retrieve.</param>
        [HttpGet("{id}")]
         [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCompatibleItems, Permissions.Btn_SetViewCompatibleItems)]
        public override async Task<ActionResult<GetCompatibleItemDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new asset type.
        /// </summary>
        /// <param name="createDto">The DTO representing the asset type to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateCompatibleItem")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveCompatibleItems)]
        public override async Task<ActionResult<GetCompatibleItemDto>> Create([FromBody] CreateCompatibleItemDto createDto, string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto);
        }

        /// <summary>
        /// Updates an existing asset type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the asset type updates.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>

        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditCompatibleItems)]
        public override async Task<ActionResult<GetCompatibleItemDto>> Update([FromBody] UpdateCompatibleItemDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto);
        }

        /// <summary>
        /// Deletes an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteCompatibleItems)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

      /*  /// <summary>
        /// Retrieves asset types based on custom criteria.
        /// </summary>
        /// <param name="criteria">The criteria for filtering asset types.</param>
        [HttpGet("custom")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [AuthorizeMultiplePermissions(Permissions.CompatibleItem_View)]
        public async Task<ActionResult<IEnumerable<GetCompatibleItemDto>>> GetByCustomCriteria([FromQuery] string criteria)
        {
            // Example of custom filtering logic
            var entities = await _context.Set<CompatibleItem>()
                .Where(a => a.CompatibleItemSerialID.Contains(criteria)) // Filter by name containing the criteria
                .ToListAsync();

            var dtos = _mapper.Map<List<GetCompatibleItemDto>>(entities);
            return Ok(dtos);
        }*/
    }
}
