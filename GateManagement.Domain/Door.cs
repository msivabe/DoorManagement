using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateManagement.Domain
{
    public enum OpenCloseState { OPEN =0,CLOSE };
    public enum LockUnlockState {LOCK = 0, UNLOCK  };

   
    public class Door:INotifyPropertyChanged
    {
        private string _id;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value == null)
                {
                    _id = Guid.NewGuid().ToString();
                }
                else
                {
                    _id = value;

                }
                }
        }

        private string _doorLabel;
        public string DoorLabel
        {
            get
            {
                return _doorLabel;
            }
            set
            {
                if(value!=_doorLabel)
                {
                    _doorLabel = value;
                    OnPropertyChanged(nameof(DoorLabel));
                }
            }
        }

        private OpenCloseState _openCloseStatus;
        public OpenCloseState OpenCloseStatus
        {
            get
            {
                return _openCloseStatus;
            }
            set
            {
                if(value != _openCloseStatus)
                {
                    _openCloseStatus = value;
                    OnPropertyChanged(nameof(OpenCloseStatus));
                }
            }
        }

        private LockUnlockState _lockUnlockStatus;
        public LockUnlockState LockUnlockStatus
        {
            get
            {
                return _lockUnlockStatus;
            }
            set
            {
                if(value != _lockUnlockStatus)
                {
                    _lockUnlockStatus = value;
                    OnPropertyChanged(nameof(LockUnlockStatus));
                }
            }
        }
        public override string ToString()
        {
            return $"Id={Id};DoorLabel={DoorLabel};OpenCloseStatus={OpenCloseStatus};LockUnlockStatus={LockUnlockStatus}";
        }

        public override bool Equals(object obj)
        {
           if(obj is Door door)
            {
                return door.Id == this.Id && door.DoorLabel == this.DoorLabel && door.LockUnlockStatus == this.LockUnlockStatus && door.OpenCloseStatus == this.OpenCloseStatus;
            }
            return false;
        }


        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

    }
}
