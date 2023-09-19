using System;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name) : base($"Entity \"{ name }\" not found.") { }
    }
}
