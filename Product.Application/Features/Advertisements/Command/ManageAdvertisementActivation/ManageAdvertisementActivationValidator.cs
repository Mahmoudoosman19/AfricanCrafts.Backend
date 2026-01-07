namespace Product.Application.Features.Advertisement.Command.ManageAdvertisementActivation
{
    public class ManageAdvertisementActivationValidator : AbstractValidator<ManageAdvertisementActivationCommand>
    {
        private readonly IGenericRepository<Domain.Entities.Advertisement> _advertisementRepository;
        public ManageAdvertisementActivationValidator(IGenericRepository<Domain.Entities.Advertisement> repository)
        {
            _advertisementRepository = repository;
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(Messages.EmptyField)
             .EntityExist(_advertisementRepository).WithMessage(Messages.NotFound);

        }


    }
}

