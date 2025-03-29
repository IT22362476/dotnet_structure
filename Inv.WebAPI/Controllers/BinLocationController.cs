using Asp.Versioning;
using AutoMapper;
using Inv.Application.DTOs.BinLocation;
using Inv.Application.DTOs.Item;
using Inv.Application.Features.BinLocation.Command;
using Inv.Application.Features.BinLocation.Queries;
using Inv.Application.Features.Rack.Command;
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
    /// Controller for managing skuBinAlocations.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    [ApiController]
    public class BinLocationController : GenericController<BinLocation, ApplicationDbContext, GetBinLocationDto, CreateBinLocationDto, UpdateBinLocationDto>
    {
        private readonly ILogger<BinLocationController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinLocationController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public BinLocationController(ApplicationDbContext context, ILogger<BinLocationController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
            : base(context, mapper, theNumbersService)
        {
            _context = context; // Assigning context to the private field
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;
        }

        /// <summary>
        /// Retrieves all skuBinAlocations.
        /// </summary>
        /// <returns>A list of skuBinAlocations.</returns>
        [HttpGet(Name = "GetBinLocation")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSKUBinLoaction)]
        public override async Task<ActionResult<IEnumerable<GetBinLocationDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves an skuBinAlocation by ID.
        /// </summary>
        /// <param name="id">The ID of the skuBinAlocation to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSKUBinLoaction, Permissions.Btn_SetViewSubCategory)]
        public override async Task<ActionResult<GetBinLocationDto>> Get(int id)
        {
            var entity = await _context.Set<BinLocation>()
                              .Include(e => ((BinLocation)(object)e).Rack)
                              .Include(e => ((BinLocation)(object)e).Warehouse)
                              .Include(e => ((BinLocation)(object)e).Store)
                              .Include(e => ((BinLocation)(object)e).Zone)
                             .FirstOrDefaultAsync(e => e.BinLctnSerialID == id); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetBinLocationDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new skuBinAlocation.
        /// </summary>
        /// <param name="createDto">The DTO representing the skuBinAlocation to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateBinLocation")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveSKUBinLoaction)]
        public override async Task<ActionResult<GetBinLocationDto>> Create([FromBody] CreateBinLocationDto createDto, string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "BinLocationName" });
        }

        /// <summary>
        /// Creates a new bin location.
        /// </summary>
        /// <param name="locationCommand">The command containing the bin location to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing binlocation.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveSKUBinLoaction)]
        public async Task<ActionResult<Result<int>>> CreateBinLocation([FromBody] CreateBinLocationCommand locationCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(locationCommand, cancellationToken);
        }

        /// <summary>
        /// Update a update binlocation.
        /// </summary>
        /// <param name="updateBinLocation">The command containing the brand and associated item types to update.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing a update bin location.</returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditSKUBinLoaction)]
        public async Task<ActionResult<Result<int>>> UpdateBinLocation(
            [FromBody] UpdateBinLocationCommand updateBinLocation,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(updateBinLocation, cancellationToken);
        }

        /// <summary>
        /// Updates an existing binlocation.
        /// </summary>
        /// <param name="updateDto">The DTO representing the binlocation updates.</param
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditSKUBinLoaction)]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>

        public override async Task<ActionResult<GetBinLocationDto>> Update([FromBody] UpdateBinLocationDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto,new []{ "BinLctn" });
        }

        /// <summary>
        /// Deletes an skuBinAlocation by ID.
        /// </summary>
        /// <param name="id">The ID of the skuBinAlocation to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteSKUBinLoaction)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
        /// <summary>
        /// Retrieves an skuBinAlocation by item ID.
        /// </summary>
        /// <param name="itemSerialID">The skuBinAlocation to retrieve.</param>
        [HttpGet("GetSkuBinLocation/{itemSerialID}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSKUBinLoaction, Permissions.Btn_SetViewSubCategory)]
        public async Task<ActionResult<GetBinLocationDto>> GetSkuBinlocation(int itemSerialID)
        {
            var entity = await _context.Set<BinLocation>()
                              .Include(e => ((BinLocation)(object)e).Rack)
                              .Include(e => ((BinLocation)(object)e).Warehouse)
                              .Include(e => ((BinLocation)(object)e).Store)
                              .Include(e => ((BinLocation)(object)e).Zone)
                             .FirstOrDefaultAsync(e => e.ItemSerialID == itemSerialID && e.IsVoidBinLocation==false && e.ItemCondition==ItemCondition.Working); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetBinLocationDto>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Get a skuBinAlocation by paginated query.
        /// </summary>
        /// <param name="getitemQuery">The query to search for a skuBinAlocation.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSKUBinLoaction)]
        public async Task<ActionResult<Result<PaginatedResult<GetBinLocationsWithPaginationDto>>>> GetItemWithPagination([FromQuery] GetBinLocationWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Use the mediator to send the query (you may need to adjust this based on your actual query structure)
            return await _mediator.Send(query, cancellationToken);
            // Return the result
        }

    }
}
