using Asp.Versioning;
using AutoMapper;
using Inv.Application.DTOs.Brand;
using Inv.Application.DTOs.GRN;
using Inv.Application.DTOs.Invoice;
using Inv.Domain.Entities;
using Inv.Infrastructure.Services;
using Inv.Persistence.Contexts;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inv.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing invoices.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    [ApiController]
    public class InvoiceController : GenericController<Invoice, ApplicationDbContext, GetInvoiceDto, CreateInvoiceDto, UpdateInvoiceDto>
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public InvoiceController(ApplicationDbContext context, ILogger<InvoiceController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
                : base(context, mapper, theNumbersService)
        {
            _context = context; // Assigning context to the private field
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;

        }
        /// <summary>
        /// Retrieves all invoice.
        /// </summary>
        /// <returns>A list of invoice.</returns>
        [HttpGet(Name = "GetInvoice")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewInvoice)]
        public override async Task<ActionResult<IEnumerable<GetInvoiceDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves an invoice by ID.
        /// </summary>
        /// <param name="id">The ID of the invoice to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewInvoice, Permissions.Btn_SetViewInvoice)]
        public override async Task<ActionResult<GetInvoiceDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new invoice.
        /// </summary>
        /// <param name="createDto">The DTO representing the invoice to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateInvoice")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveInvoice)]
        public override async Task<ActionResult<GetInvoiceDto>> Create([FromBody] CreateInvoiceDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "InvoiceName" });
        }

        /// <summary>
        /// Updates an existing invoice.
        /// </summary>
        /// <param name="updateDto">The DTO representing the invoice updates.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditInvoice)]
        public override async Task<ActionResult<GetInvoiceDto>> Update([FromBody] UpdateInvoiceDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "InvoiceName" });
        }
        /// <summary>
        /// Deletes an invoice by ID.
        /// </summary>
        /// <param name="id">The ID of the invoice to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteInvoice)]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        /// <summary>
        /// Retrieves a GRN by item ID.
        /// </summary>
        /// <param name="id">The ID of the GRN to retrieve.</param>
        [HttpGet("GRN/{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewGRN)]
        public async Task<ActionResult<List<GetInvoiceByGRNDto>>> GetGrnsByItem(int id)
        {
            var entities = await _context.Set<Invoice>()
                .Include(s => s.Supplier)
                .Include(s => s.InvoiceItems)
                .Where(l => l.GRNSerialID==id).ToListAsync();
            if (!entities.Any())
                return NotFound();
            var dto = _mapper.Map<List<GetInvoiceByGRNDto>>(entities);
            return Ok(dto);
        }
    }

}




#region Old

/*[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly TheNumbersService _theNumbersService;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context; // Private field for the context

    public InvoicesController(TheNumbersService theNumbersService, IMapper mapper, ApplicationDbContext context)
    {
        _theNumbersService = theNumbersService;
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> CreateInvoice([FromBody] InvoiceCreateDto createDto)
    {
        // Map DTO to entity
        var invoice = _mapper.Map<Invoice>(createDto);

        // Fetch and increment LastNumber for the "Invoice" entity
        var lastNumber = await _theNumbersService.GetAndIncrementLastNumberAsync(nameof(Invoice));

        // Use LastNumber to generate a unique InvoiceNumber
        invoice.InvoiceNumber = $"INV-{lastNumber:D6}";
        invoice.LastNumber = lastNumber;

        // Add the new invoice
        _context.Set<Invoice>().Add(invoice);
        await _context.SaveChangesAsync();

        // Map entity to a DTO and return it
        var dto = _mapper.Map<InvoiceDto>(invoice);
        return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceSerialID }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoice(int id)
    {
        var invoice = await _context.Set<Invoice>().FindAsync(id);

        if (invoice == null)
            return NotFound();

        return Ok(_mapper.Map<InvoiceDto>(invoice));
    }
}*/
#endregion