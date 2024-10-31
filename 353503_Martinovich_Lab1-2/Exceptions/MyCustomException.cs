namespace _353503_Martinovich_Lab1.Exceptions
{
    using System;

    public class MyCustomException : Exception
    {
        public MyCustomException() { }

        public MyCustomException(string message)
            : base(message) { }

        public MyCustomException(string message, Exception inner)
            : base(message, inner) { }
    }

}
