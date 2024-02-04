using WebServerApp_Week3.Interfaces;

namespace WebServerApp_Week3
{
    public class Processor
    {
        IValidator validatr;

        public Processor(IValidator Validator)
        {
            validatr = Validator;
        }

        public bool Validate(string value)
        {
            return validatr.IsValid(value);
        }
    }
}
