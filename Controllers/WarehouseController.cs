using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms_api.DTO;
using wms_api.Entities;
using wms_api.Helpers;
using wms_api.Repositories;

namespace wms_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateWarehouseDTO> _validator;
        private readonly IWarehouseRepository _repository;

        public WarehouseController(IMapper mapper, IValidator<CreateWarehouseDTO> validator, IWarehouseRepository repository)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<GetWarehouseDTO>>> GetAll()
        {
            var warehouses = await _repository.Find();

            return Ok(warehouses.Select(_mapper.Map<GetWarehouseDTO>));
        }

        [HttpPost]
        [RequestSizeLimit(1024 * 1024 * 10)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create([FromForm] CreateWarehouseDTO warehouseDTO)
        {
            var result = await _validator.ValidateAsync(warehouseDTO);

            if (result.IsValid)
            {
                var warehouse = _mapper.Map<Warehouse>(warehouseDTO);
                warehouse.FileContent = await FileHelper.GetFileByteArray(warehouseDTO.File);

                await _repository.Add(warehouse);

                return NoContent();
            }

            return BadRequest(result.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var warehouse = await _repository.GetById(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            await _repository.Delete(warehouse);

            return NoContent();
        }

        [HttpGet("{id}/download")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DownloadFile(int id)
        {
            var warehouse = await _repository.GetById(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            Response.Headers.Add("Content-Disposition", $"attachment;filename={warehouse.FileName}");
            return File(warehouse.FileContent, warehouse.FileMimeType);
        }

        [HttpGet("find-closest/{lat}/{lon}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsManager")]
        public async Task<ActionResult> FindClosest(float lat, float lon)
        {
            var warehouses = await _repository.GetClosestsByCoordinates(lat, lon);

            return Ok(warehouses.Select(_mapper.Map<GetWarehouseDTO>));
        }
    }
}
