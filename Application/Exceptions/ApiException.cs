using System.Globalization;

namespace Application.Exceptions
{
    public class ApiException:Exception
    {
        public ApiException():base() { }

        public ApiException(string message):base(message) // base keyword is for: pass to message parameter(string) to constructor Exception
        {
        }

        public ApiException(string message,params object[] args):base(String.Format(CultureInfo.CurrentCulture,message,args))
        {   
        }
    }
}
