using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inv.Application.DTOs.UOMConversion;
using Inv.Domain.Entities;
using Inv.Persistence.Contexts;
using Inv.Shared;
using Inv.Application.Features.UOMConversion.Queries;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using static Dapper.SqlMapper;
using Microsoft.EntityFrameworkCore;
using Inv.Infrastructure.Services;

namespace Inv.WebAPI.Controllers
{
        /// <summary>
    /// Controller for managing uom conversions.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    public class UOMConversionController : GenericController<UOMConversion, ApplicationDbContext, GetUOMConversionDto, CreateUOMConversionDto, UpdateUOMConversionDto>
    {
        private readonly ILogger<UOMConversionController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UOMConversionController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public UOMConversionController(ApplicationDbContext context, ILogger<UOMConversionController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
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
        [HttpGet(Name = "GetUOMConversion")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewUOMConversion)]
        public override async Task<ActionResult<IEnumerable<GetUOMConversionDto>>> GetAll()
        {
            var entity = await _context.Set<UOMConversion>()
                .Include(e => ((UOMConversion)(object)e).UOM)
                .Include(e => ((UOMConversion)(object)e).UOMTo)
                .ToListAsync(); // Replace `Id` with the correct primary key property

            if (entity == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<GetUOMConversionDto>>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Retrieves an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewUOMConversion)]
        public override async Task<ActionResult<GetUOMConversionDto>> Get(int id)
        {
            var entity = await _context.Set<UOMConversion>()
                .Include(e => ((UOMConversion)(object)e).UOM)
                .Include(e => ((UOMConversion)(object)e).UOMTo)
                .FirstOrDefaultAsync(e => e.UOMConvSerialID == id); // Replace `Id` with the correct primary key property

            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetUOMConversionDto>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Creates a new asset type.
        /// </summary>
        /// <param name="createDto">The DTO representing the asset type to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateUOMConversion")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveUOMConversion)]
        public override async Task<ActionResult<GetUOMConversionDto>> Create([FromBody] CreateUOMConversionDto createDto,[FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto);
        }

        /// <summary>
        /// Updates an existing asset type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the asset type updates.</param>
        ///  <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditUOMConversion)]
        public override async Task<ActionResult<GetUOMConversionDto>> Update([FromBody] UpdateUOMConversionDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "ConversionDescription" } );
        }

        /// <summary>
        /// Deletes an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteUOMConversion)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
        /// <summary>
        /// Get a uom conversion by paginated query.
        /// </summary>
        /// <param name="getUomConversionQuery">The query to search for a uom conversion.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewUOMConversion)]
        public async Task<ActionResult<Result<PaginatedResult<GetUomConversionsWithPaginationDto>>>> GetUomConversionsWithPagination([FromQuery] GetUomConversionsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Use the mediator to send the query (you may need to adjust this based on your actual query structure)
            return await _mediator.Send(query, cancellationToken);

            // Return the result
        }
    }
}
