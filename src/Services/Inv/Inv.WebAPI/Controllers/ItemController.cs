using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inv.Application.DTOs.Item;
using Inv.Application.Features.Item.Command;
using Inv.Domain.Entities;
using Inv.Persistence.Contexts;
using Inv.Shared;
using JwtTokenAuthentication.Permission;
using JwtTokenAuthentication.Constants;
using Inv.Application.Features.Item.Queries;
using Inv.Application.DTOs.UOMConversion;
using Inv.Infrastructure.Services;

namespace Inv.WebAPI.Controllers
{
   
    /// <summary>
    /// Controller for managing item.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    public class ItemController : GenericController<Item, ApplicationDbContext, GetItemDto, CreateItemDto, UpdateItemDto>
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public ItemController(ApplicationDbContext context, ILogger<ItemController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
: base(context, mapper, theNumbersService)
        {
            _context = context; // Assigning context to the private field
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;

        }

        /// <summary>
        /// Retrieves all item codes.
        /// </summary>
        /// <returns>A list of item codes.</returns>
        [HttpGet("codes/all")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewItem)]
        public async Task<ActionResult<IEnumerable<GetItemCodeDto>>> GetItemCodes()
        {
            var entity = await _context.Set<Item>()
                           .Where(i => i.ApprovedBy>0)
                           .ToListAsync(); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<GetItemCodeDto>>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>A list of items.</returns>
        [HttpGet(Name = "GetItems")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewItem)]
        public override async Task<ActionResult<IEnumerable<GetItemDto>>> GetAll()
        {
            var entity = await _context.Set<Item>()
                           .Include(e => ((Item)(object)e).ItemType)
                           .Where(i => i.ApprovedBy > 0)
                           .ToListAsync(); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<GetItemDto>>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Retrieves an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewItem)]
        public override async Task<ActionResult<GetItemDto>> Get(int id)
        {
            var entity = await _context.Set<Item>()
                                .Include(e => ((Item)(object)e).Brand)
                                .Include(e => ((Item)(object)e).Model)
                                 .Include(e => ((Item)(object)e).MainCategory)
                                .Include(e => ((Item)(object)e).SubCategory)
                                .FirstOrDefaultAsync(e => e.ItemSerialID == id); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetItemDto>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="createDto">The DTO representing the item to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateGetItems")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveItems)]
        public override async Task<ActionResult<GetItemDto>> Create([FromBody] CreateItemDto createDto,[FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto,new[] { "ItemCode" });
        }
        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="itemCommand">The command containing the item to create.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing item.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveItems)]
        public async Task<ActionResult<Result<int>>> CreateItem(
            [FromBody] CreateItemCommand itemCommand,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(itemCommand, cancellationToken);
        }
        /// <summary>
        /// update a item.
        /// </summary>
        /// <param name="itemCommand">The command containing the item to update.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A result containing item.</returns>
        [HttpPatch("Update")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditItems)]
        public async Task<ActionResult<Result<int>>> UpdateItem(
            [FromBody] UpdateItemCommand itemCommand,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(itemCommand, cancellationToken);
        }
        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="updateDto">The DTO representing the item updates.</param>
        ///  <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditItems)]
        public override async Task<ActionResult<GetItemDto>> Update([FromBody] UpdateItemDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "ItemCode" });
        }

        /// <summary>
        /// Deletes an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteItem)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        /// <summary>
        /// Retrieves items based on custom criteria.
        /// </summary>
        /// <param name="criteria">The criteria for filtering items.</param>
        [HttpGet("custom")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
         [AuthorizeMultiplePermissions(Permissions.Btn_SetViewItem)]
        public async Task<ActionResult<IEnumerable<GetItemDto>>> GetByCustomCriteria([FromQuery] string criteria)
        {
            // Example of custom filtering logic
            var entities = await _context.Set<Item>()
                .Where(a => a.ItemDes.Contains(criteria)) // Filter by name containing the criteria
                .ToListAsync();

            var dtos = _mapper.Map<List<GetItemDto>>(entities);
            return Ok(dtos);
        }
        /// <summary>
        /// Get a uom conversion by paginated query.
        /// </summary>
        /// <param name="query">The query to search for a uom conversion.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewItem)]
        public async Task<ActionResult<Result<PaginatedResult<GetItemsWithPaginationDto>>>> GetItemWithPagination([FromQuery] GetItemWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Use the mediator to send the query (you may need to adjust this based on your actual query structure)
            return await _mediator.Send(query, cancellationToken);

            // Return the result
        }
        /// <summary>
        /// Get a uom conversion by paginated query.
        /// </summary>
        /// <param name="query">The query to search for a uom conversion.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("approved/paged")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetApprovalItems)]
        public async Task<ActionResult<Result<PaginatedResult<GetItemsWithPaginationDto>>>> GetApprovedItemWithPagination([FromQuery] GetApprovedItemWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Use the mediator to send the query (you may need to adjust this based on your actual query structure)
            return await _mediator.Send(query, cancellationToken);

            // Return the result
        }
    }
}
