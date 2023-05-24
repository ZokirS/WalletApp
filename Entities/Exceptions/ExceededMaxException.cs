namespace Entities.Exceptions
{
    public sealed class ExceededMaxException : ExceedException
    {
        public ExceededMaxException() : base("Exceeded maximum balance for unidentified accounts.")
        {
            
        }
    }
}
