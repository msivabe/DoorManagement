using GateManagement.Domain;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DoorManagementClient.ViewModel
{
    public class DoorAddOrEditControlViewModel: INotifyPropertyChanged
    {
        private IDoorManagementServiceIntegration _doorManagementServiceIntegration;

        public delegate void UpdateMessageEventDelegate(string message);

        public event UpdateMessageEventDelegate UpdateMessageEvent;

        public delegate void RefreshDoorsListEventDelegate();

        public event RefreshDoorsListEventDelegate RefreshDoorsListEvent;

        public DoorAddOrEditControlViewModel(IDoorManagementServiceIntegration doorManagementServiceIntegration)
        {
            _doorManagementServiceIntegration = doorManagementServiceIntegration;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Door _selectedDoor;
        public Door SelectedDoor
        {
            get
            {
                return _selectedDoor;
            }
            set
            {
                _selectedDoor = value;

                if(_selectedDoor!=null && _selectedDoor.Id!=null)
                {
                    IsUpdate = true;
                }

                OnPropertyChanged(nameof(SelectedDoor));
            }
        }


        private bool _isUpdate;
        public bool IsUpdate
        {
            get
            {
                return _isUpdate;
            }
            set
            {
                _isUpdate = value;

                if(_isUpdate)
                {
                    AddOrUpdateText = "Update";
                }
                else
                {
                    AddOrUpdateText = "Add";
                }

                OnPropertyChanged(nameof(IsUpdate));
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;

             
                OnPropertyChanged(nameof(IsEnabled));
            }
        }


        private string _addOrUpdateText;
        public string AddOrUpdateText
        {
            get
            {
                return _addOrUpdateText;
            }
            set
            {
                _addOrUpdateText = value;
                OnPropertyChanged(nameof(AddOrUpdateText));

            }
        }

        private ICommand _toggleLockUnlockStateCmd;
        public ICommand ToggleLockUnlockStateCmd
        {
            get
            {
                if (_toggleLockUnlockStateCmd == null)
                    _toggleLockUnlockStateCmd = new ToggleLockUnlockStateCommand(ToggleLockUnlockState);
                return _toggleLockUnlockStateCmd;
            }
            set
            {
                _toggleLockUnlockStateCmd = value;
            }
        }

        private ICommand _addOrUpdateCmd;
        public ICommand AddOrUpdateCmd
        {
            get
            {
                if (_addOrUpdateCmd == null)
                    _addOrUpdateCmd = new AddOrUpdateCommand(AddOrUpdateDoor);
                return _addOrUpdateCmd;
            }
            set
            {
                _addOrUpdateCmd = value;
            }
        }


        private ICommand _cancelCmd;
        public ICommand CancelCmd
        {
            get
            {
                if (_cancelCmd == null)
                    _cancelCmd = new AddOrUpdateCommand(Cancel);
                return _cancelCmd;
            }
            set
            {
                _cancelCmd = value;
            }
        }

        private ICommand _toggleOpenCloseStateCmd;
        public ICommand ToggleOpenCloseStateCmd
        {
            get
            {
                if (_toggleOpenCloseStateCmd == null)
                    _toggleOpenCloseStateCmd = new  ToggleOpenCloseStateCommand(ToggleOpenCloseState);
                return _toggleOpenCloseStateCmd;
            }
            set
            {
                _toggleOpenCloseStateCmd = value;
            }
        }


        private void ToggleOpenCloseState()
        {
            if(SelectedDoor!=null)
            {
                if (SelectedDoor.OpenCloseStatus == OpenCloseState.OPEN)
                    SelectedDoor.OpenCloseStatus = OpenCloseState.CLOSE;
                else if (SelectedDoor.OpenCloseStatus == OpenCloseState.CLOSE)
                    SelectedDoor.OpenCloseStatus = OpenCloseState.OPEN;
            }
        }

        private void ToggleLockUnlockState()
        {
            if (SelectedDoor != null)
            {
                if (SelectedDoor.LockUnlockStatus ==  LockUnlockState.LOCK)
                    SelectedDoor.LockUnlockStatus = LockUnlockState.UNLOCK;
                else if (SelectedDoor.LockUnlockStatus == LockUnlockState.UNLOCK)
                    SelectedDoor.LockUnlockStatus = LockUnlockState.LOCK;
            }
        }


        private void AddOrUpdateDoor()
        {

            if(IsUpdate)
            {
                UpdateDoor();
            }
            else
            {
                AddDoor();
            }

            RefreshDoorsListEvent?.Invoke();
        }

        private void Cancel()
        {
            RefreshDoorsListEvent?.Invoke();
        }

        private void AddDoor()
        {
            try
            {
                UpdateMessageEvent?.Invoke("");
                 int res =   _doorManagementServiceIntegration.AddDoor(SelectedDoor).Result;

                if(res ==0)
                UpdateMessageEvent?.Invoke("Door added successfully!");
                else
                    UpdateMessageEvent?.Invoke("Add door failed!");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Add door failed", ex);
                UpdateMessageEvent?.Invoke("Add door failed!");
            }
        }


        private void UpdateDoor()
        {
            try
            {
                UpdateMessageEvent?.Invoke("");
                var res = _doorManagementServiceIntegration.UpdateDoor(SelectedDoor).Result;
                if (res == 0)
                    UpdateMessageEvent?.Invoke("Door Update succeeded!");
                else
                    UpdateMessageEvent?.Invoke("Update door failed!");
            }
            catch(Exception ex)
            {
                Serilog.Log.Error("Update door failed", ex);
                UpdateMessageEvent?.Invoke("Update door failed!");
            }
        }

        internal class AddOrUpdateCommand : ICommand
        {
            private Action _action;
            public AddOrUpdateCommand(Action action)
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

        internal class CanceleCommand : ICommand
        {
            private Action _action;
            public CanceleCommand(Action action)
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

        internal class ToggleOpenCloseStateCommand : ICommand
        {
            private Action _action;
            public ToggleOpenCloseStateCommand(Action action)
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

        internal class ToggleLockUnlockStateCommand : ICommand
        {
            private Action _action;
            public ToggleLockUnlockStateCommand(Action action)
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

}
