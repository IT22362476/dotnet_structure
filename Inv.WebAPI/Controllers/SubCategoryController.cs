using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inv.Domain.Entities;
using Inv.Persistence.Contexts;
using Inv.WebAPI.Controllers;
using Inv.Application.DTOs.SubCategory;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using Inv.Infrastructure.Services;

namespace Inv.WebAPI.Controllers
{
      /// <summary>
    /// Controller for managing sub categories.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/inv/[controller]")]
    public class SubCategoryController : GenericController<SubCategory, ApplicationDbContext, GetSubCategoryDto, CreateSubCategoryDto, UpdateSubCategoryDto>
    {
        private readonly ILogger<SubCategoryController> _logger;
        private readonly IMediator _mediator; // Assuming you're using MediatR
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context; // Private field for the context
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="SubCategoryController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public SubCategoryController(ApplicationDbContext context, ILogger<SubCategoryController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
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
        [HttpGet(Name = "GetSubCategory")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSubCategory)]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<IEnumerable<GetSubCategoryDto>>> GetAll()
        {
            return await base.GetAll();
        }
        /// <summary>
        /// Retrieves all BrandItemType entities with optional navigation properties.
        /// </summary>
        /// <returns>A list of BrandItemType DTOs.</returns>
        [HttpGet("GetSubCategories")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSubCategory)]
        public async Task<ActionResult<IEnumerable<GetSubCategoryDto>>> GetSubCategories()
        {
            var includes = new string[] { "MainCategory" };

            // Initialize the query for BrandItemType
            var query = _context.Set<SubCategory>().AsQueryable();

            // Apply each navigation property as an include, if specified
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Execute query and map to DTOs
            var entities = await query.ToListAsync();
            var dtos = _mapper.Map<List<GetSubCategoryDto>>(entities);

            return Ok(dtos);
        }
        /// <summary>
        /// Retrieves an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to retrieve.</param>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSubCategory)]
        public override async Task<ActionResult<GetSubCategoryDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Creates a new asset type.
        /// </summary>
        /// <param name="createDto">The DTO representing the asset type to be created.</param>
        /// <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPost(Name = "CreateSubCategory")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveSubCategory)]
        public override async Task<ActionResult<GetSubCategoryDto>> Create([FromBody] CreateSubCategoryDto createDto,[FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "SubCategoryName" });
        }

        /// <summary>
        /// Updates an existing asset type.
        /// </summary>
        /// <param name="updateDto">The DTO representing the asset type updates.</param>
        ///  <param name="uniquePropertyNames">Optional: Array of property names to check for uniqueness.</param>
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetEditSubCategory)]
        public override async Task<ActionResult<GetSubCategoryDto>> Update([FromBody] UpdateSubCategoryDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "SubCategoryName" });
        }

        /// <summary>
        /// Deletes an asset type by ID.
        /// </summary>
        /// <param name="id">The ID of the asset type to delete.</param>
        [HttpDelete("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteSubCategory)]
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
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewSubCategory)]
        public async Task<ActionResult<IEnumerable<GetSubCategoryDto>>> GetByCustomCriteria([FromQuery] string criteria)
        {
            // Example of custom filtering logic
            var entities = await _context.Set<SubCategory>()
                .Where(a => a.SubCategoryName.Contains(criteria)) // Filter by name containing the criteria
                .ToListAsync();

            var dtos = _mapper.Map<List<GetSubCategoryDto>>(entities);
            return Ok(dtos);
        }
     }
}
