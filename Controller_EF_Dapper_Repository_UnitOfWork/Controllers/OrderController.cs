using AutoMapper;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.Database.Entities;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.UnitOfWork.Interface;
using Controller_EF_Dapper_Repository_UnityOfWork.Business.Models.Product;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(ILogger<OrderController> logger,
                               IMapper mapper,
                               IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //------------------------------------------------------------------------------------
        //EndPoints
        //------------------------------------------------------------------------------------

        [HttpGet, Route("{id:guid}")]
        public async Task<ActionResult<OrderDetailedResponseDTO>> OrderGetAsync([FromRoute] Guid id)
        {
            Order order = await _unitOfWork.Orders.Get(id);

            if (order == null)
                return new ObjectResult(Results.NotFound());

            var orderDetailed = await _unitOfWork.Orders.GetDetailedOrder(order);

            var orderDetailedResponseDTO = _mapper.Map<OrderDetailedResponseDTO>(orderDetailed);

            return new ObjectResult(orderDetailedResponseDTO);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> OrderPost(OrderProductsDTO orderRequestDTO)
        {
            OrderBuyer orderBuyer = new OrderBuyer();

            orderBuyer.Id = "cod-3457";
            orderBuyer.Name = "Doe Joe Client";

            IActionResult result = await _unitOfWork.Orders.SaveOrder(orderRequestDTO.ProductListIds, orderBuyer);
            return new ObjectResult(result);
        }
    }
}