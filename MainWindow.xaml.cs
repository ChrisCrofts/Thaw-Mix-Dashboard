using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.IO.Ports;
using System.ComponentModel;

namespace Thaw_Mix_Dashboard
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        SerialPort serialPort = new SerialPort();
        SerialComms port = new SerialComms();
        public Scanner Scanner = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            port.Open();
            AddExistingScanners();

            //get list of specimens that haven't been checked out after CheckedInTimeMinutes minutes
            CheckedInSpecimens();
            //refresh list 
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = Properties.Settings.Default.CheckedInSpecimenRefresh;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<Specimen> _specimenList;

        public List<Specimen> specimenList
        {
            get
            {
                return _specimenList;
            }
            set
            {
                _specimenList = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("specimenList"));
                }
            }
        }

        private string _CheckedInSpecimensList = "The following specimens have been checked in. Specimens in red have been checked in longer than " + Properties.Settings.Default.CheckedInTimeHours + " hours:";

        public string CheckedInSpecimensList
        {
            get
            {
                return _CheckedInSpecimensList;
            }
            set
            {
                _CheckedInSpecimensList = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CheckedInSpecimensList"));
                }
            }
        }


        #region Methods

        public int AddExistingScanners()
        {
            //string filePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string filePath = @"C:\temp\Scanners";
            string filePath = Properties.Settings.Default.ScannerLocation;
            string[] files = Directory.GetFiles(filePath, "Scanner*.json");
            int scannerCount = 1;

            foreach (var file in files)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string json = sr.ReadToEnd();
                    var jsonConvert = JsonConvert.DeserializeObject<dynamic>(json);
                    string c = jsonConvert.Color.Value;
                    string n = jsonConvert.Name.Value;
                    string num = jsonConvert.Number.Value;
                    
                    switch (num)
                    {
                        case "1":
                            port.Scanner1 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                        case "2":
                            port.Scanner2 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                        case "3":
                            port.Scanner3 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                        case "4":
                            port.Scanner4 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                        case "5":
                            port.Scanner5 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                        case "6":
                            port.Scanner6 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                        case "7":
                            port.Scanner7 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                        case "8":
                            port.Scanner8 = DisplayScanner((Color)ColorConverter.ConvertFromString(c), n);
                            break;
                    }
                    scannerCount++;
                    sr.Close();
                }
            }
            return scannerCount - 1;
        }

        private Scanner DisplayScanner(Color color, string Name)
        {
            Scanner scanner = new Scanner(color, Name);
            wrapPanel.Children.Add(scanner);
            return scanner;
        }

        //get checked in specimens without check out event
        public void CheckedInSpecimens()
        {
            SQLLIBBLD01Controller sqllibbld01Controller = new SQLLIBBLD01Controller();
            specimenList = sqllibbld01Controller.GetCheckedInSpecimens();
        }   

        //refresh list of specimens without check out event
        public void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckedInSpecimens();
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
            this.Close();
        }
    }
}
