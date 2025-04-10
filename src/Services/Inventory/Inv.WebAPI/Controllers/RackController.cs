using Asp.Versioning;
using AutoMapper;
using Inv.Application.DTOs.Rack;
using Inv.Application.Features.BrandItemType.Command;
using Inv.Application.Features.Rack.Command;
using Inv.Application.Features.Rack.Queries;
using Inv.Domain.Entities;
using Inv.Infrastructure.Services;
using Inv.Persistence.Contexts;
using Inv.Shared;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public class RackController : GenericController<Rack, ApplicationDbContext, GetRackDto, CreateRackDto, UpdateRackDto>
    {
        private readonly ILogger<RackController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="RackController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public RackController(ApplicationDbContext context, ILogger<RackController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
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
        [HttpGet(Name = "GetRack")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewRack)]
        public override async Task<ActionResult<IEnumerable<GetRackDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves the Rack Code.
        /// </summary>
        /// <returns>Retrieves the Rack Code.</returns>
        [HttpGet("GetNextRackCode")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewRack)]
        public async Task<ActionResult<Result<string>>> GetNextRackCode(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetNextRackCodeQuery(), cancellationToken);
         }

        /// <summary>
        /// Retrieves an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewRack, Permissions.Btn_SetViewRack)]
        public override async Task<ActionResult<GetRackDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new asset type.
        /// </summary>
        /// <param name="createDto">The DTO representing the asset type to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateRack")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveRack)]
        public override async Task<ActionResult<GetRackDto>> Create([FromBody] CreateRackDto createDto, string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "RackName" });
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="rackCommand">The command containing the item to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing item.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveRack)]
        public async Task<ActionResult<Result<int>>> CreateRack([FromBody] CreateRackCommand rackCommand,CancellationToken cancellationToken)
        {
            return await _mediator.Send(rackCommand, cancellationToken);
        }

        /// <summary>
        /// Update a new brand item type.
        /// </summary>
        /// <param name="updateRack">The command containing the brand and associated item types to update.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing a list of update brand item type DTOs.</returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditRack)]
        public async Task<ActionResult<Result<int>>> UpdateRack(
            [FromBody] UpdateRackCommand updateRack,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(updateRack, cancellationToken);
        }

        /// <summary>
        /// Updates an existing asset type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the asset type updates.</param>
        ///  <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditRack)]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger

        public override async Task<ActionResult<GetRackDto>> Update([FromBody] UpdateRackDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "RackCode","RackName" } );
        }

        /// <summary>
        /// Deletes an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteRack)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
        /// <summary>
        /// Get a uom conversion by paginated query.
        /// </summary>
        /// <param name="getitemQuery">The query to search for a uom conversion.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewRack)]
        public async Task<ActionResult<Result<PaginatedResult<GetRacksWithPaginationDto>>>> GetItemWithPagination([FromQuery] GetRackWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Use the mediator to send the query (you may need to adjust this based on your actual query structure)
            return await _mediator.Send(query, cancellationToken);
            // Return the result
        }
    }
}
