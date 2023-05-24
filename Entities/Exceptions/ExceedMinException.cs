namespace Entities.Exceptions
{
    public sealed class ExceedMinException : ExceedException
    {
        public ExceedMinException():base("Exceeded minimum balance for unidentified accounts.")
        {
            
        }
    }
}
