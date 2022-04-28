namespace RunAholicAPI.Validation;

public class ActivityValidator : AbstractValidator<Activity>
{
    public ActivityValidator()
    {
        RuleFor(a => a.Name).NotEmpty().WithMessage("Name is empty/not included");

        RuleFor(a => a.StartDateLocal).NotEmpty().WithMessage("StartDateLocal is empty/not included");

        RuleFor(a => a.ElapsedTimeInSec).NotEmpty().WithMessage("ElapsedTimeInSec is empty/not included");
        RuleFor(a => a.ElapsedTimeInSec).GreaterThanOrEqualTo(0).WithMessage("ElapsedTimeInSec can't be negative");

        RuleFor(a => a.DistanceInMeters).NotEmpty().WithMessage("DistanceInMeters is empty/not included");
        RuleFor(a => a.DistanceInMeters).GreaterThanOrEqualTo(0).WithMessage("DistanceInMeters can't be negative");
        
        RuleFor(a => a.AthleteId).NotEmpty().WithMessage("AthleteId is empty/not included");
        RuleFor(a => a.AthleteId).Length(24).WithMessage("Invalid AthleteId");

    }
}