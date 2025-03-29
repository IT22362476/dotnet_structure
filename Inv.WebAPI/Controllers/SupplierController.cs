using Asp.Versioning;
using AutoMapper;
using Inv.Application.DTOs.Supplier;
using Inv.Domain.Entities;
using Inv.Infrastructure.Services;
using Inv.Persistence.Contexts;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inv.WebAPI.Controllers
{

    /// <summary>
    /// Controller for managing suppliers.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    [ApiController]
    public class SupplierController : GenericController<Supplier, ApplicationDbContext, GetSupplierDto, CreateSupplierDto, UpdateSupplierDto>
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public SupplierController(ApplicationDbContext context, ILogger<SupplierController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
                : base(context, mapper, theNumbersService)
        {
            _context = context; // Assigning context to the private field
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;

        }

        /// <summary>
        /// Retrieves all suppliers.
        /// </summary>
        /// <returns>A list of suppliers.</returns>
        [HttpGet(Name = "GetSupplier")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSupplier)]
        public override async Task<ActionResult<IEnumerable<GetSupplierDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves an supplier by ID.
        /// </summary>
        /// <param name="id">The ID of the supplier to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSupplier, Permissions.Btn_SetViewSupplier)]
        public override async Task<ActionResult<GetSupplierDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new supplier.
        /// </summary>
        /// <param name="createDto">The DTO representing the supplier to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateSupplier")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveSupplier)]
        public override async Task<ActionResult<GetSupplierDto>> Create([FromBody] CreateSupplierDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "SupplierName" });
        }

        /// <summary>
        /// Updates an existing supplier.
        /// </summary>
        /// <param name="updateDto">The DTO representing the supplier updates.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditSupplier)]
        public override async Task<ActionResult<GetSupplierDto>> Update([FromBody] UpdateSupplierDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "SupplierName" });
        }
        /// <summary>
        /// Deletes an supplier by ID.
        /// </summary>
        /// <param name="id">The ID of the supplier to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteSupplier)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

    }
}
