using AutoMapper;
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
            //Usuario fixo, mas  poderia vir de um identity
            string userName = "doe joe";

            //01 Recupero a Order
            var orderDetailed = (OrderDetailed)await _unitOfWork.Orders.GetDetailedOrder(id);
            orderDetailed.ClientName = userName;

            OrderDetailedResponseDTO orderDetailedResponseDTO = _mapper.Map<OrderDetailedResponseDTO>(orderDetailed);
            return new ObjectResult(orderDetailedResponseDTO);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> OrderPost(OrderRequestDTO orderRequestDTO)
        {
            OrderBuyer orderBuyer = new OrderBuyer();

            orderBuyer.Id = Guid.NewGuid();
            orderBuyer.Name = "Doe Joe Client";

            IActionResult result = await _unitOfWork.Orders.SaveOrder(orderRequestDTO.ProductListIds, orderBuyer);

            return new ObjectResult(result);
        }
    }
}