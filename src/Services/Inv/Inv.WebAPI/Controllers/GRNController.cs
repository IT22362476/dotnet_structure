using Asp.Versioning;
using AutoMapper;
using Inv.Application.DTOs.GRN;
using Inv.Application.DTOs.Item;
using Inv.Application.Features.GRN.Commands;
using Inv.Application.Features.GRN.Queries;
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
    /// Controller for managing grns.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    [ApiController]
    public class GRNController : GenericController<GRN, ApplicationDbContext, GetGRNDto, CreateGRNDto, UpdateGRNDto>
    {
        private readonly ILogger<GRNController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="GRNController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GRNController(ApplicationDbContext context, ILogger<GRNController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
                : base(context, mapper, theNumbersService)
        {
            _context = context; // Assigning context to the private field
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;

        }

        /// <summary>
        /// Retrieves all grns.
        /// </summary>
        /// <returns>A list of grns.</returns>
        [HttpGet(Name = "GetGRN")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewGRN)]
        public override async Task<ActionResult<IEnumerable<GetGRNDto>>> GetAll()
        {
            return await base.GetAll();
        }
        
        /// <summary>
        /// Retrieves approved grns.
        /// </summary>
        /// <returns>A list of grns.</returns>
        [HttpGet("paged/approved")]
        public async Task<ActionResult<Result<PaginatedResult<GetPaginatedGRNHeadersDto>>>> GetApprovedGRNs([FromBody] GetApprovedGRNsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// Retrieves an grn by ID.
        /// </summary>
        /// <param name="id">The ID of the grn to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewGRN, Permissions.Btn_SetViewGRN)]
        public override async Task<ActionResult<GetGRNDto>> Get(int id)
        {
            var entity = await _context.Set<GRN>()
                         .Include(e => ((GRN)(object)e).Supplier)
                         .Include(e => ((GRN)(object)e).PurchaseOrder).ThenInclude(x => x.PurchaseOrderItem)
                          .Include(e => ((GRN)(object)e).Invoice)
                         .FirstOrDefaultAsync(e => e.GRNSerialID == id); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetGRNWithInfoDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Retrieves a GRN by item ID.
        /// </summary>
        /// <param name="id">The ID of the GRN to retrieve.</param>
        [HttpGet("item/{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewGRN)]
        public async Task<ActionResult<List<GetGRNDto>>> GetGrnsByItem(int id)
        {
            var entities = await _context.Set<GRN>()
                .Include(e => e.Supplier)
                .Include(e => e.PurchaseOrder)
                    .ThenInclude(po => po.PurchaseOrderItem)
                .Where(e => e.PurchaseOrder.PurchaseOrderItem.Any(i => i.ItemSerialID == id))
                .ToListAsync();

            if (!entities.Any())
                return NotFound();

            var dto = _mapper.Map<List<GetGRNDto>>(entities);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new grn.
        /// </summary>
        /// <param name="createDto">The DTO representing the grn to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateGRN")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveGRN)]
        public override async Task<ActionResult<GetGRNDto>> Create([FromBody] CreateGRNDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "GRNName" });
        }

        /// <summary>
        /// Creates a new grn.
        /// </summary>
        /// <param name="request">The DTO representing the grn to be created.</param>
        /// <param name="cancellationToken"> Cancellation token for async operations.</param>
        [HttpPost("create")]
        //[AuthorizeMultiplePermissions(Permissions.Btn_SetSaveGRN)]
        public async Task<ActionResult<Result<int>>> CreateGRN([FromBody] CreateGRNCommand request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing grn.
        /// </summary>
        /// <param name="updateDto">The DTO representing the grn updates.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditGRN)]
        public override async Task<ActionResult<GetGRNDto>> Update([FromBody] UpdateGRNDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "GRNName" });
        }
        /// <summary>
        /// Deletes an grn by ID.
        /// </summary>
        /// <param name="id">The ID of the grn to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteGRN)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }


    }
}
