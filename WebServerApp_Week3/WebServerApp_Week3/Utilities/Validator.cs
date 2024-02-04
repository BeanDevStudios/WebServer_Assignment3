using WebServerApp_Week3.Interfaces;

namespace WebServerApp_Week3.Utilities
{
    public class Validator : IValidator
    {
        bool IsTruthy = false;

        public Validator() 
        { }

        public Validator(bool isTruthy) 
        {
            IsTruthy = isTruthy;
        }

        public bool IsValid(string value)
        {
            return true;
        }
    }
}
