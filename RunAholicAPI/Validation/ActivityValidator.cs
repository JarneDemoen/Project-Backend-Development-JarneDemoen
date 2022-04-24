namespace RunAholicAPI.Validation;

public class ActivityValidator : AbstractValidator<Activity>
{
    public ActivityValidator()
    {
        RuleFor(a => a.Name).NotEmpty().WithMessage("Name is empty");
        RuleFor(a => a.Name).NotNull().WithMessage("Name is not included in the body");
        RuleFor(a => a.StartDateLocal).NotNull().WithMessage("StartDateLocal is not included in the body");
        RuleFor(a => a.StartDateLocal).NotEmpty().WithMessage("StartDateLocal is empty");
        RuleFor(a => a.ElapsedTimeInSec).NotNull().WithMessage("ElapsedTimeInSec is not included in the body");
        RuleFor(a => a.ElapsedTimeInSec).NotEmpty().WithMessage("ElapsedTimeInSec is empty");
        RuleFor(a => a.ElapsedTimeInSec).GreaterThanOrEqualTo(0).WithMessage("ElapsedTimeInSec can't be negative");
        RuleFor(a => a.DistanceInMeters).NotNull().WithMessage("DistanceInMeters is not included in the body");
        RuleFor(a => a.DistanceInMeters).NotEmpty().WithMessage("DistanceInMeters is empty");
        RuleFor(a => a.DistanceInMeters).GreaterThanOrEqualTo(0).WithMessage("DistanceInMeters can't be negative");
        RuleFor(a => a.AthleteId).NotNull().WithMessage("AthleteId is not included in the body");
        RuleFor(a => a.AthleteId).NotEmpty().WithMessage("AthleteId is empty");

    }
}