using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inv.Domain.Entities;
using Inv.Persistence.Contexts;
using JwtTokenAuthentication.Permission;
using JwtTokenAuthentication.Constants;
using Inv.Application.DTOs.CusPriceCategory;
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
    public class CusPriceCategoryController : GenericController<CusPriceCategory, ApplicationDbContext, GetCusPriceCategoryDto, CreateCusPriceCategoryDto, UpdateCusPriceCategoryDto>
    {
        private readonly ILogger<CusPriceCategoryController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="CusPriceCategoryController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public CusPriceCategoryController(ApplicationDbContext context, ILogger<CusPriceCategoryController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
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
        [HttpGet(Name = "GetCusPriceCategory")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCusPriceCategory)]
        public override async Task<ActionResult<IEnumerable<GetCusPriceCategoryDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to retrieve.</param>
        [HttpGet("{id}")]
         [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCusPriceCategory, Permissions.Btn_SetViewCusPriceCategory)]
        public override async Task<ActionResult<GetCusPriceCategoryDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new asset type.
        /// </summary>
        /// <param name="createDto">The DTO representing the asset type to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateCusPriceCategory")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveCusPriceCategory)]
        public override async Task<ActionResult<GetCusPriceCategoryDto>> Create([FromBody] CreateCusPriceCategoryDto createDto, string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "CusPriceCatName" });
        }

        /// <summary>
        /// Updates an existing asset type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the asset type updates.</param>
        ///  <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditCusPriceCategory)]
        public override async Task<ActionResult<GetCusPriceCategoryDto>> Update([FromBody] UpdateCusPriceCategoryDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "CusPriceCatName" });
        }

        /// <summary>
        /// Deletes an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteCusPriceCategory)]
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
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCusPriceCategory)]
        public async Task<ActionResult<IEnumerable<GetCusPriceCategoryDto>>> GetByCustomCriteria([FromQuery] string criteria)
        {
            // Example of custom filtering logic
            var entities = await _context.Set<CusPriceCategory>()
                .Where(a => a.CusPriceCatName.Contains(criteria)) // Filter by name containing the criteria
                .ToListAsync();

            var dtos = _mapper.Map<List<GetCusPriceCategoryDto>>(entities);
            return Ok(dtos);
        }
    }
}
