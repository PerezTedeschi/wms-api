using FluentValidation;
using wms_api.DTO;
using wms_api.Repositories;

namespace wms_api.Validators
{
    public class CreateWarehouseDTOValidator : AbstractValidator<CreateWarehouseDTO>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public CreateWarehouseDTOValidator(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;

            RuleFor(x => x)
                .NotNull();
            RuleFor(x => x.Code)
                .NotEmpty()
                .MustAsync(HaveUniqueCode)
                .WithMessage("The warehouse code you provided is already in use. Please enter a unique code for the warehouse.");
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Address)
                .NotEmpty();
            RuleFor(x => x.State)
                .NotEmpty();
            RuleFor(x => x.Country)
                .NotEmpty();
            RuleFor(x => x.Zip)
                .NotEmpty();            
        }

        private async Task<bool> HaveUniqueCode(string code, CancellationToken cancellation)
        {
            var result = await _warehouseRepository.Find(x => x.Code.Equals(code));

            return !result.Any();
        }
    }
}
