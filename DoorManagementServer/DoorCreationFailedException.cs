using System;

namespace DoorManagementServer
{
    public class DoorCreationFailedException : DoorManagementException
    {
        public DoorCreationFailedException()
        {
        }

        public DoorCreationFailedException(string message) : base(message)
        {
        }

        public DoorCreationFailedException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
