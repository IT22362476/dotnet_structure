using Asp.Versioning;
using AutoMapper;
using Inv.Application.DTOs.BrandItemType;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Domain.Entities;
using Inv.Infrastructure.Services;
using Inv.Persistence.Contexts;
using Inv.Shared;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inv.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing BrandItemType Item Type 
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    [ApiController]
    public class BrandItemTypeController : GenericController<BrandItemType, ApplicationDbContext, GetBrandItemTypeDto, CreateBrandItemTypeDto, UpdateBrandItemTypeDto>
    {
        private readonly ILogger<BrandItemTypeController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="BrandItemTypeController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public BrandItemTypeController(ApplicationDbContext context, ILogger<BrandItemTypeController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
                : base(context, mapper, theNumbersService)
        {
            _context = context; // Assigning context to the private field
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;

        }

        /// <summary>
        /// Retrieves allBrand Item Types.
        /// </summary>
        /// <returns>A list ofBrand Item Types.</returns>
        [HttpGet(Name = "GetAllBrandItemType")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewBrandItemType)]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<IEnumerable<GetBrandItemTypeDto>>> GetAll()
        {
            return await base.GetAll();
        }
        /// <summary>
        /// Retrieves all BrandItemType entities with optional navigation properties.
        /// </summary>
        /// <returns>A list of BrandItemType DTOs.</returns>
        [HttpGet("GetBrandItemType")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewBrandItemType)]
        public async Task<ActionResult<IEnumerable<GetBrandItemTypeDto>>> GetBrandItemType()
        {
            var includes = new string[] { "Brand", "ItemType" };

            // Initialize the query for BrandItemType
            var query = _context.Set<BrandItemType>().AsQueryable();

            // Apply each navigation property as an include, if specified
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Execute query and map to DTOs
            var entities = await query.ToListAsync();
            var dtos = _mapper.Map<List<GetBrandItemTypeDto>>(entities);

            return Ok(dtos);
        }
        /// <summary>
        /// Retrieves a Brand Item Type by ID.
        /// </summary>
        /// <param name="id">The ID of theBrand Item Type to retrieve.</param>
        [HttpGet("{id}")]
         [AuthorizeMultiplePermissions(Permissions.Btn_SetViewBrandItemType)]
        public override async Task<ActionResult<GetBrandItemTypeDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new brand item type.
        /// </summary>
        /// <param name="brandItemTypeCommand">The command containing the brand and associated item types to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing a list of created brand item type DTOs.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveBrandItemType)]
        public async Task<ActionResult<Result<int>>> CreateBrandItemType(
            [FromBody] CreateBrandItemTypeCommand brandItemTypeCommand,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(brandItemTypeCommand, cancellationToken);
        }
        /// <summary>
        /// Update a new brand item type.
        /// </summary>
        /// <param name="brandItemTypeCommand">The command containing the brand and associated item types to update.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing a list of update brand item type DTOs.</returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditBrandItemType)]
        public async Task<ActionResult<Result<int>>> UpdateBrandItemType(
            [FromBody] UpdateBrandItemTypeCommand brandItemTypeCommand,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(brandItemTypeCommand, cancellationToken);
        }
        // Override the generic Create to do nothing or handle as needed.
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override Task<ActionResult<GetBrandItemTypeDto>> Create(CreateBrandItemTypeDto createDto,string[]? uniquePropertyNames = null)
        {
            throw new NotImplementedException("Use CreateBrandItemType method instead.");
        }

        /// <summary>
        /// Updates an existing Brand Item Type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the Brand Item Type updates.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditBrandItemType)]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<GetBrandItemTypeDto>> Update([FromBody] UpdateBrandItemTypeDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto);
        }

        /// <summary>
        /// Deletes a Brand Item Type by ID.
        /// </summary>
        /// <param name="id">The ID of the Brand Item Type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteBrandItemType)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
 
    }
}
