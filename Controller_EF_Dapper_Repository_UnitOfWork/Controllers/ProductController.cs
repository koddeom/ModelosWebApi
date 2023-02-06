using AutoMapper;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.Extensions.ErroDetailedExtension;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.UnitOfWork.Interface;
using Controller_EF_Dapper_Repository_UnityOfWork.Domain.Database;
using Controller_EF_Dapper_Repository_UnityOfWork.Domain.Database.Entities.Product;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Products.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly ApplicationDbContext _dbContext;

        public ProductController(ILogger<ProductController> logger,
                                 ApplicationDbContext dbContext,
                                 IMapper mapper,
                                 IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //------------------------------------------------------------------------------------
        //EndPoints
        //------------------------------------------------------------------------------------
        [HttpGet, Route("{id:guid}")]
        public async Task<IActionResult> ProductGet([FromRoute] Guid id)
        {
            var products = await _unitOfWork.Products.Get(id);
            var productResponseDTO = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

            return (IActionResult)productResponseDTO;
        }

        [HttpGet, Route("")]
        public async Task<IEnumerable<IActionResult>> ProductGetAll()
        {
            var products = await _unitOfWork.Products.GetAll();
            var productResponseDTO = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

            return (IEnumerable<IActionResult>)productResponseDTO;
        }

        [HttpGet, Route("{/solds")]
        public IActionResult ProductSoldGet()
        {
            var products = _unitOfWork.Products.GetAllProductsSolds();
            var productResponseDTO = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
            return (IActionResult)productResponseDTO;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> ProductPost(ProductRequestDTO productRequestDTO)
        {
            //Usuario fixo, mas  poderia vir de um identity
            string user = "doe joe";

            Product product = _mapper.Map<Product>(productRequestDTO);

            //Recupero a categoria de forma sincrona
            Category category = await _unitOfWork.Categories.Get(productRequestDTO.CategoryId);
            product.Category = category;
            //-----------------------------------------
            product.CreatedBy = user;
            product.CreatedOn = DateTime.Now;

            if (!product.IsValid)
            {
                return new ObjectResult(Results.ValidationProblem(product.Notifications.ConvertToErrorDetails()));
            }
            else
            {
                await _unitOfWork.Products.Add(product);
                var productResponseDTO = _mapper.Map<ProductResponseDTO>(product);

                return new ObjectResult(Results.Created($"/products/{product.Id}", product.Id));
            }
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> ProductPutAsync([FromRoute] Guid id,
                                         ProductRequestDTO productRequestDTO)
        {
            //Usuario fixo, mas  poderia vir de um identity
            string user = "doe joe";

            //Recupero o produto do banco
            var product = await _unitOfWork.Products.Get(id);

            //nao encontrado
            if (product == null) return new ObjectResult(Results.NotFound());

            //Recupero a categoria de forma sincrona
            Category category = await _unitOfWork.Categories.Get(productRequestDTO.CategoryId);
            product.Category = category;

            //nao encontrado
            if (category == null) return new ObjectResult(Results.NotFound());

            product.Name = productRequestDTO.Name;
            product.Price = productRequestDTO.Price;
            product.Active = true;
            product.Category = category;
            //-----------------------------------------
            product.EditedBy = user;
            product.EditedOn = DateTime.Now;

            if (!product.IsValid)
            {
                return new ObjectResult(Results.ValidationProblem(product.Notifications.ConvertToErrorDetails()));
            }
            else
            {
                _unitOfWork.Products.Update(product);
                return new ObjectResult(Results.Ok());
            }
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> ProductDeleteAsync([FromRoute] Guid id)
        {
            //Recupero o produto do banco
            var product = await _unitOfWork.Products.Get(id);

            //nao encontrado
            if (product == null) return new ObjectResult(Results.NotFound());

            _unitOfWork.Products.Delete(product);
            return new ObjectResult(Results.Ok());
        }
    }
}