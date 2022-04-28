namespace RunAholicAPI.Validation;

public class AthleteValidator : AbstractValidator<Athlete>
{
    public AthleteValidator()
    {
        // RuleFor(a => a.FirstName).NotNull().WithMessage("FirstName is not included in the body");
        RuleFor(a => a.FirstName).NotEmpty().WithMessage("FirstName is empty/not included");
        
        RuleFor(a => a.LastName).NotEmpty().WithMessage("LastName is empty/not included");
        
        RuleFor(a => a.City).NotEmpty().WithMessage("City is empty/not included");
        
        RuleFor(a => a.Country).NotEmpty().WithMessage("Country is empty/not included");
        
        RuleFor(a => a.Age).NotEmpty().WithMessage("Age is empty/not included");
        RuleFor(a => a.Age).GreaterThan(6).WithMessage("Age has to be older than 6");
    }
}