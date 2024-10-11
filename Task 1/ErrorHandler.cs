namespace Task_1
{
    public class ErrorHandler
    {
        public delegate void ErrorDelegate(Exception exception);

        public event ErrorDelegate Errored;
    }
}