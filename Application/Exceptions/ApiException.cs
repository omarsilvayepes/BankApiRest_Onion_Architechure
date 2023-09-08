using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Application.Exceptions
{
    public class ApiException:Exception
    {
        public List<string> Errors { get; }
        public ApiException():base() 
        {
            Errors = new List<string>();
        }

        public ApiException(string message):base(message) // base keyword is for: pass to message parameter(string) to constructor Exception
        {
        }

        public ApiException(string message,params object[] args):base(String.Format(CultureInfo.CurrentCulture,message,args))
        {   
        }

        public ApiException(IEnumerable<IdentityError> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.Description);
            }
        }
    }
}
