using Autofac;
using GateManagement.Domain;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace DoorManagementClient.ViewModel
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        private IDoorManagementServiceIntegration _doorManagementServiceIntegration;

        private ObservableCollection<Door> _doors;
        public ObservableCollection<Door> Doors
        {
            get
            {
                return _doors;
            }
            set
            {
                _doors = value;
                OnPropertyChanged(nameof(Doors));
            }
        }

        private string _userMessage;
        public string UserMessage
        {
            get
            {
                return _userMessage;
            }
            set
            {
                _userMessage = value;
                OnPropertyChanged(nameof(UserMessage));
            }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }



        private DoorAddOrEditControlViewModel _addOrEditBinding;
        public DoorAddOrEditControlViewModel AddOrEditBinding
        {
            get
            {
                if (_addOrEditBinding == null)
                {
                    _addOrEditBinding = new DoorAddOrEditControlViewModel(Bootstrap.Container.Resolve<IDoorManagementServiceIntegration>());
                    _addOrEditBinding.UpdateMessageEvent += _addOrEditBinding_UpdateMessageEvent;
                    _addOrEditBinding.RefreshDoorsListEvent += _addOrEditBinding_RefreshDoorsListEvent;
                }
                return _addOrEditBinding;
            }
            set
            {
                if(_addOrEditBinding==null)
                {
                    _addOrEditBinding = new DoorAddOrEditControlViewModel(Bootstrap.Container.Resolve<IDoorManagementServiceIntegration>());
                    _addOrEditBinding.UpdateMessageEvent += _addOrEditBinding_UpdateMessageEvent;
                    _addOrEditBinding.RefreshDoorsListEvent += _addOrEditBinding_RefreshDoorsListEvent;
                }
                _addOrEditBinding = value;
                OnPropertyChanged(nameof(AddOrEditBinding));
            }
        }

        private void _addOrEditBinding_RefreshDoorsListEvent()
        {
            BindAllDoors();
        }

        private void _addOrEditBinding_UpdateMessageEvent(string message)
        {
            UserMessage = message;
        }

        private bool _deleteCmdEnabledDisable;
        public bool DeleteCmdEnableDisable
        {
            get
            {
                return _deleteCmdEnabledDisable;
            }
            set
            {
                _deleteCmdEnabledDisable = value;
                OnPropertyChanged(nameof(DeleteCmdEnableDisable));
            }
        }


        public MainWindowViewModel(IDoorManagementServiceIntegration doorManagementServiceIntegration)
        {
            _doorManagementServiceIntegration = doorManagementServiceIntegration;
            Init();
            Add();
        }



        private ICommand _connectCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                    _connectCommand = new ConnectCmd(Connect);
                return _connectCommand;
            }
            set
            {
                _connectCommand = value;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new DeleteCmd(DeleteDoor);
                return _deleteCommand;
            }
            set
            {
                _deleteCommand = value;
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new AddCmd(Add);
                return _addCommand;
            }
            set
            {
                _addCommand = value;
            }
        }

       

        private void Add()
        {
            AddOrEditBinding.SelectedDoor = new Door();
            AddOrEditBinding.SelectedDoor.LockUnlockStatus = LockUnlockState.LOCK;
            AddOrEditBinding.SelectedDoor.OpenCloseStatus = OpenCloseState.CLOSE;
            AddOrEditBinding.IsUpdate = false;
        }

        private void Connect()
        {

            if (BindAllDoors() == 0)
            {
                IsConnected = true;
                AddOrEditBinding.IsEnabled = true;
            }
        }

        private int BindAllDoors()
        {
            Serilog.Log.Information("BindAllDoors request");
            Doors = new ObservableCollection<Door>();
            try
            {
                var result = _doorManagementServiceIntegration.GetAllDoors().Result;

                DeleteCmdEnableDisable = result != null && result.Count() > 0;
                Doors.Clear();
                Doors = new ObservableCollection<Door>(result);
             
                Add();
                return 0;
            }
            catch(Exception ex)
            {
                UserMessage = "BindAllDoors failed";
                Serilog.Log.Error("BindAllDoors failed", ex);
                return -1;
            }
        }

        private void Init()
        {
            UserMessage = "";
        }

        private void DeleteDoor(string doorId)
        {
            UserMessage = "";
            Serilog.Log.Information($"Delete door request received.ID:{doorId}");
            try
            {
                var res = _doorManagementServiceIntegration.DeleteDoor(doorId).Result;

                if (res == 0)
                    UserMessage = "Delete door succeeded!";
                else
                    UserMessage = "Delete door failed";
                BindAllDoors();
            }
            catch(Exception ex)
            {
                UserMessage = "Delete door failed";
                Serilog.Log.Error("Delete door failed", ex);
            }
        }


    }

    internal class ConnectCmd : ICommand
    {
        private Action _action;
        public ConnectCmd(Action action)
        {
            _action = action;
        }
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;



        public void Execute(object parameter)
        {
            _action.Invoke();
        }

        #endregion
    }

    internal class DeleteCmd : ICommand
    {
        private Action<string> _action;
        public DeleteCmd(Action<string> action)
        {
            _action = action;
        }
        #region ICommand Members  

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke((parameter as Door).Id);
        }

        #endregion
    }

    internal class AddCmd : ICommand
    {
        private Action _action;
        public AddCmd(Action action)
        {
            _action = action;
        }
        #region ICommand Members  

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }

        #endregion
    }


}
