using System;

namespace DoorManagementClient
{
    public class DoorManagementAppException:Exception 
    {
        public DoorManagementAppException()
        {

        }

        public DoorManagementAppException(string message)
            : base(message)
        {

        }

        public DoorManagementAppException(string message,Exception exception)
           : base(message, exception)
        {

        }

    }
}
