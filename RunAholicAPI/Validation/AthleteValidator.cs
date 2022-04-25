namespace RunAholicAPI.Validation;

public class AthleteValidator : AbstractValidator<Athlete>
{
    public AthleteValidator()
    {
        RuleFor(a => a.FirstName).NotEmpty().WithMessage("FirstName is empty");
        RuleFor(a => a.FirstName).NotNull().WithMessage("FirstName is not included in the body");
        
        RuleFor(a => a.LastName).NotEmpty().WithMessage("LastName is empty");
        RuleFor(a => a.LastName).NotNull().WithMessage("LastName is not included in the body");
        
        RuleFor(a => a.City).NotEmpty().WithMessage("City is empty");
        RuleFor(a => a.City).NotNull().WithMessage("City is not included in the body");
        
        RuleFor(a => a.Country).NotEmpty().WithMessage("Country is empty");
        RuleFor(a => a.Country).NotNull().WithMessage("Country is not included in the body");
        
        RuleFor(a => a.Age).NotEmpty().WithMessage("Age is empty");
        RuleFor(a => a.Age).NotNull().WithMessage("Age is not included in the body");
        RuleFor(a => a.Age).GreaterThan(6).WithMessage("Age has to be older than 6");
    }
}