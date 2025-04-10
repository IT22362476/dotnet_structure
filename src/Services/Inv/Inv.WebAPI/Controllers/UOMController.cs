using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inv.Application.DTOs.UOM;
using Inv.Domain.Entities;
using Inv.Persistence.Contexts;
using Inv.Shared;
using Inv.WebAPI.Controllers;
using JwtTokenAuthentication.Permission;
using JwtTokenAuthentication.Constants;
using Inv.Infrastructure.Services;

namespace Inv.WebAPI.Controllers
{
     /// <summary>
    /// Controller for managing uoms.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    public class UOMController : GenericController<UOM, ApplicationDbContext, GetUOMDto, CreateUOMDto, UpdateUOMDto>
    {
        private readonly ILogger<UOMController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UOMController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public UOMController(ApplicationDbContext context, ILogger<UOMController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
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
        [HttpGet(Name = "GetUOM")]
         [AuthorizeMultiplePermissions(Permissions.Btn_SetViewUOM)]
        public override async Task<ActionResult<IEnumerable<GetUOMDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewUOM)]
        public override async Task<ActionResult<GetUOMDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new asset type.
        /// </summary>
        /// <param name="createDto">The DTO representing the asset type to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateUOM")]
         [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveUOM)]
        public override async Task<ActionResult<GetUOMDto>> Create([FromBody] CreateUOMDto createDto,[FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "UOMName" });
        }

        /// <summary>
        /// Updates an existing asset type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the asset type updates.</param>
        ///  <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditUOM)]
        public override async Task<ActionResult<GetUOMDto>> Update([FromBody] UpdateUOMDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "UOMName" });
        }

        /// <summary>
        /// Deletes an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteUOM)]
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
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewUOM)]
        public async Task<ActionResult<IEnumerable<GetUOMDto>>> GetByCustomCriteria([FromQuery] string criteria)
        {
            // Example of custom filtering logic
            var entities = await _context.Set<UOM>()
                .Where(a => a.UOMName.Contains(criteria)) // Filter by name containing the criteria
                .ToListAsync();

            var dtos = _mapper.Map<List<GetUOMDto>>(entities);
            return Ok(dtos);
        }
    }
}
