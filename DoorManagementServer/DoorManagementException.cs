using System;

namespace DoorManagementServer
{
    public class DoorManagementException:Exception 
    {
        public DoorManagementException()
        {

        }

        public DoorManagementException(string message)
            : base(message)
        {

        }

        public DoorManagementException(string message,Exception exception)
           : base(message, exception)
        {

        }

    }
}
