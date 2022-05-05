using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Threading;


namespace Thaw_Mix_Dashboard
{
    public partial class Scanner : UserControl, INotifyPropertyChanged
    {
        public Scanner(Color color, string name)
        {
            InitializeComponent();
            DataContext = this;
            txtLogout.Background = new SolidColorBrush(color);
            userValue = "Log in to begin";
            this.Name = name;
        }

        public DispatcherTimer timer;
        private TimeSpan time;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _specimenValue;

        public string specimenValue
        {
            get
            {
                return _specimenValue;
            }
            set
            {
                _specimenValue = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("specimenValue"));
                }
            }
        }

        private string _userValue;

        public string userValue
        {
            get
            {
                return _userValue;
            }
            set
            {
                _userValue = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("userValue"));
                }
            }
        }

        private string _messageValue;

        public string messageValue
        {
            get
            {
                return _messageValue;
            }
            set
            {
                _messageValue = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("messageValue"));
                }
            }
        }

        private string _timerValue = "00:00";

        public string timerValue
        {
            get
            {
                return _timerValue;
            }
            set
            {
                _timerValue = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("timerValue"));
                }
            }
        }

        private string _borderColor = "Black";

        public string borderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("borderColor"));
                }
            }
        }

        #region Methods
        //check for valid employee ID, begin logout timer
        public bool isValidEmployee(string scannedValue)
        {
            ANSRController ansrController = new ANSRController();
            if (Properties.Settings.Default.DeptAccess.Contains(ansrController.GetEmployeeDepartment(scannedValue)))
            {
                beginTimer();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void beginTimer()
        {
            //resets timer if already going
            if (timer != null)
            {
                timer.Stop();
            }
            
            //begin timer
            time = TimeSpan.FromSeconds(Properties.Settings.Default.LogOutTime);
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                timerValue = time.ToString(@"mm\:ss");
                if (time == TimeSpan.Zero) timer.Stop();
                time = time.Add(TimeSpan.FromSeconds(-1));

            }, Dispatcher);
        }
        //check if temp id is Frozen
        public bool validTempId(string scannedValue)
        {
            ESPController espController = new ESPController();
            if (espController.GetTemperatureID(scannedValue).Equals("3"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //get most recent event
        public int mostRecentInOrOut(int applicationID, string scannedValue)
        {
            SQLLIBBLD01Controller sqllibbld01Controller = new SQLLIBBLD01Controller();
            return sqllibbld01Controller.GetMostRecentInOrOut(applicationID, scannedValue);
        }
        //check specimen in
        public bool checkIn(string scannedValue, string emp)
        {
            SQLLIBBLD01Controller sqllibbld01Controller = new SQLLIBBLD01Controller();
            return sqllibbld01Controller.CheckIn(scannedValue, emp);
        }
        //check specimen out
        public bool checkOut(int applicationID, string scannedValue, string emp)
        {
            SQLLIBBLD01Controller sqllibbld01Controller = new SQLLIBBLD01Controller();
            return sqllibbld01Controller.CheckOut(applicationID, scannedValue, emp);
        }
        //scan attempt
        public bool scanAttempt(string scannedValue, string emp, string details)
        {
            SQLLIBBLD01Controller sqllibbld01Controller = new SQLLIBBLD01Controller();
            return sqllibbld01Controller.ScanAttempt(scannedValue, emp, details);
        }
        //check for expired timer
        private void txtLogout_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLogout.Text.Equals("00:00"))
            {
                borderColor = "Black";
                specimenValue = "";
                userValue = "Log in to begin";
                timerValue = "00:00";
                messageValue = "";
            }
        }
        #endregion
    }
}
