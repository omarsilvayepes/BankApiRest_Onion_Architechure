using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationException:Exception
    {
        public ValidationException():base("There is one o more errors of validation")
        {
            Errors=new List<string>();
        }
        public List<string> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
