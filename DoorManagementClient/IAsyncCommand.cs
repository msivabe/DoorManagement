using System.Threading.Tasks;
using System.Windows.Input;

namespace DoorManagementClient
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
