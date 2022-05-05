using System;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;

namespace Thaw_Mix_Dashboard
{
    class SerialComms
    {
        SerialPort serialPort1 = new SerialPort("COM21", 9600, Parity.None, 8, StopBits.One);
        //SerialPort serialPort2 = new SerialPort("COM22", 9600, Parity.None, 8, StopBits.One);
        //SerialPort serialPort3 = new SerialPort("COM23", 9600, Parity.None, 8, StopBits.One);
        //SerialPort serialPort4 = new SerialPort("COM24", 9600, Parity.None, 8, StopBits.One);
        //SerialPort serialPort5 = new SerialPort("COM25", 9600, Parity.None, 8, StopBits.One);
        //SerialPort serialPort6 = new SerialPort("COM26", 9600, Parity.None, 8, StopBits.One);
        //SerialPort serialPort7 = new SerialPort("COM27", 9600, Parity.None, 8, StopBits.One);
        //SerialPort serialPort8 = new SerialPort("COM28", 9600, Parity.None, 8, StopBits.One);

        public event EventHandler<string> DataReceived;
        public string Name = "";
        public int Index = 0;
        public Scanner Scanner1 = null;
        public Scanner Scanner2 = null;
        public Scanner Scanner3 = null;
        public Scanner Scanner4 = null;
        public Scanner Scanner5 = null;
        public Scanner Scanner6 = null;
        public Scanner Scanner7 = null;
        public Scanner Scanner8 = null;

        public SerialComms()
        {
            serialPort1.Handshake = Handshake.XOnXOff;
            //serialPort2.Handshake = Handshake.XOnXOff;
            //serialPort3.Handshake = Handshake.XOnXOff;
            //serialPort4.Handshake = Handshake.XOnXOff;
            //serialPort5.Handshake = Handshake.XOnXOff;
            //serialPort6.Handshake = Handshake.XOnXOff;
            //serialPort7.Handshake = Handshake.XOnXOff;
            //serialPort8.Handshake = Handshake.XOnXOff;
            serialPort1.DataReceived += SerialPort_DataReceived;
            //serialPort2.DataReceived += SerialPort_DataReceived;
            //serialPort3.DataReceived += SerialPort_DataReceived;
            //serialPort4.DataReceived += SerialPort_DataReceived;
            //serialPort5.DataReceived += SerialPort_DataReceived;
            //serialPort6.DataReceived += SerialPort_DataReceived;
            //serialPort7.DataReceived += SerialPort_DataReceived;
            //serialPort8.DataReceived += SerialPort_DataReceived;
        }

        public void Open()
        {
            //scanner 1
            try
            {
                serialPort1.Open();
            }
            catch (Exception)
            {
                //intentionally left blank
            }
            ////scanner 2
            //try
            //{
            //    serialPort2.Open();
            //}
            //catch (Exception)
            //{
            //    //intentionally left blank
            //}
            ////scanner 3
            //try
            //{
            //    serialPort3.Open();
            //}
            //catch (Exception)
            //{
            //    //intentionally left blank
            //}
            ////scanner 4
            //try
            //{
            //    serialPort4.Open();
            //}
            //catch (Exception)
            //{
            //    //intentionally left blank
            //}
            ////scanner 5
            //try
            //{
            //    serialPort5.Open();
            //}
            //catch (Exception)
            //{
            //    //intentionally left blank
            //}
            ////scanner 6
            //try
            //{
            //    serialPort6.Open();
            //}
            //catch (Exception)
            //{
            //    //intentionally left blank
            //}
            ////scanner 7
            //try
            //{
            //    serialPort7.Open();
            //}
            //catch (Exception)
            //{
            //    //intentionally left blank
            //}
            ////scanner 8
            //try
            //{
            //    serialPort8.Open();
            //}
            //catch (Exception)
            //{
            //    //intentionally left blank
            //}

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var rawData = "";
            var rawData1 = "";
            var rawData2 = "";
            var rawData3 = "";
            var rawData4 = "";
            var rawData5 = "";
            var rawData6 = "";
            var rawData7 = "";
            var rawData8 = "";

            //scanner 1
            try
            {
                rawData1 = serialPort1.ReadExisting();
            }
            catch (Exception)
            {

                rawData1 = "";
            }
            ////scanner 2
            //try
            //{
            //    rawData2 = serialPort2.ReadExisting();
            //}
            //catch (Exception)
            //{

            //    rawData2 = "";
            //}
            ////scanner 3
            //try
            //{
            //    rawData3 = serialPort3.ReadExisting();
            //}
            //catch (Exception)
            //{

            //    rawData3 = "";
            //}
            ////scanner 4
            //try
            //{
            //    rawData4 = serialPort4.ReadExisting();
            //}
            //catch (Exception)
            //{

            //    rawData4 = "";
            //}
            ////scanner 5
            //try
            //{
            //    rawData5 = serialPort5.ReadExisting();
            //}
            //catch (Exception)
            //{

            //    rawData5 = "";
            //}
            ////scanner 6
            //try
            //{
            //    rawData6 = serialPort6.ReadExisting();
            //}
            //catch (Exception)
            //{

            //    rawData6 = "";
            //}
            ////scanner 7
            //try
            //{
            //    rawData7 = serialPort7.ReadExisting();
            //}
            //catch (Exception)
            //{

            //    rawData7 = "";
            //}
            ////scanner 8
            //try
            //{
            //    rawData8 = serialPort8.ReadExisting();
            //}
            //catch (Exception)
            //{

            //    rawData8 = "";
            //}

            if (rawData1 != "") rawData = rawData1;
            else if (rawData2 != "") rawData = rawData2;
            else if (rawData3 != "") rawData = rawData3;
            else if (rawData4 != "") rawData = rawData4;
            else if (rawData5 != "") rawData = rawData5;
            else if (rawData6 != "") rawData = rawData6;
            else if (rawData7 != "") rawData = rawData7;
            else if (rawData8 != "") rawData = rawData8;

            DataReceived?.Invoke(this, rawData.Trim());

            //initial testing with omron scanner. they allow two "post amble" characters
            //string scannerName = rawData.Substring(rawData.IndexOf('/') + 1, 1);
            //string barcodeValue = rawData.Substring(0, rawData.IndexOf('/'));

            //codecorp 2600
            string scannerName = rawData.Substring(0,1);
            string barcodeValue = rawData.Substring(rawData.IndexOf('/') + 1);

            switch (scannerName)
            {
                case "1":
                    if (Scanner1.userValue == barcodeValue) logOut(Scanner1);
                    else handleBarcodeValue(Scanner1, barcodeValue);
                    break;
                case "2":
                    if (Scanner2.userValue == barcodeValue) logOut(Scanner2);
                    else handleBarcodeValue(Scanner2, barcodeValue);
                    break;
                case "3":
                    if (Scanner3.userValue == barcodeValue) logOut(Scanner3);
                    else handleBarcodeValue(Scanner3, barcodeValue);
                    break;
                case "4":
                    if (Scanner4.userValue == barcodeValue) logOut(Scanner4);
                    else handleBarcodeValue(Scanner4, barcodeValue);
                    break;
                case "5":
                    if (Scanner5.userValue == barcodeValue) logOut(Scanner5);
                    else handleBarcodeValue(Scanner5, barcodeValue);
                    break;
                case "6":
                    if (Scanner6.userValue == barcodeValue) logOut(Scanner6);
                    else handleBarcodeValue(Scanner6, barcodeValue);
                    break;
                case "7":
                    if (Scanner7.userValue == barcodeValue) logOut(Scanner7);
                    else handleBarcodeValue(Scanner7, barcodeValue);
                    break;
                case "8":
                    if (Scanner8.userValue == barcodeValue) logOut(Scanner8);
                    else handleBarcodeValue(Scanner8, barcodeValue);
                    break;
            }                       
        }

        private void logOut(Scanner scanner)
        {
            scanner.borderColor = "Black";
            scanner.specimenValue = "";
            scanner.userValue = "Log in to begin";
            scanner.timer.Stop();
            scanner.timerValue = "00:00";
            scanner.messageValue = "";
        }

        public void handleBarcodeValue(Scanner scanner, string barcodeValue)
        {
            scanner.borderColor = "Black";

            //tracking number - 
            if (barcodeValue.Length > 8 && barcodeValue.Length < 11 && int.TryParse(barcodeValue, out int n))
            {

                //make sure user is logged in, specimen is frozen and hasn't been checked in, then check in
                if (scanner.userValue == "Log in to begin")
                {
                    //MessageBox.Show("Please scan your employee badge before attempting to check in a specimen.", "Please log in", MessageBoxButton.OK, MessageBoxImage.Error);
                    scanner.messageValue = "Please scan your employee badge";
                    SystemSounds.Exclamation.Play();
                    scanner.borderColor = "Red";
                    return;
                }

                //get specimen temp id
                bool isFrozen = scanner.validTempId(barcodeValue);
                //get most recent in or out event (49, 50, 51. scan attempt, in, out)
                int mostRecentEvent = scanner.mostRecentInOrOut(Properties.Settings.Default.ApplicationID, barcodeValue);

                if (isFrozen && mostRecentEvent != 50)
                {

                    //added for 2.0.1.1
                    //check out of "Check into Thaw Mix" if currently checked into one of those
                    if (scanner.mostRecentInOrOut(Properties.Settings.Default.ThawMixAppId, barcodeValue) == 50)
                    {
                        scanner.checkOut(Properties.Settings.Default.ThawMixAppId, barcodeValue, scanner.userValue);
                    }

                    scanner.checkIn(barcodeValue, scanner.userValue);
                    scanner.specimenValue = barcodeValue;
                    scanner.messageValue = barcodeValue + " checked in";
                    scanner.beginTimer();
                }
                else if (isFrozen && mostRecentEvent != 51)
                {
                    scanner.checkOut(Properties.Settings.Default.ApplicationID, barcodeValue, scanner.userValue);
                    scanner.specimenValue = barcodeValue;
                    scanner.messageValue = barcodeValue + " checked out";
                    scanner.beginTimer();
                }
                else if (!isFrozen)
                {
                    scanner.scanAttempt(barcodeValue, scanner.userValue, "ScanAttempt»Not a frozen specimen");
                    //MessageBox.Show(barcodeValue + " is not a frozen specimen. Scan next specimen to skip.", "Not frozen", MessageBoxButton.OK, MessageBoxImage.Error);
                    SystemSounds.Exclamation.Play();
                    scanner.beginTimer();
                    scanner.messageValue = barcodeValue + " not a frozen sample!";
                    scanner.borderColor = "Red";
                    return;
                }
            }
            //employee id - if valid employee, begin logout timer
            else if (barcodeValue.Length < 9 && scanner.userValue == "Log in to begin")
            {
                if (!scanner.isValidEmployee(barcodeValue))
                {
                    SystemSounds.Exclamation.Play();
                    scanner.messageValue = "You don't have access to this app!";
                    scanner.borderColor = "Red";
                    return;
                }
                scanner.userValue = barcodeValue;
                scanner.messageValue = "Welcome!";
            }
            //employee id - someone else is already logged in
            else if (barcodeValue.Length < 9 && scanner.userValue != "Log in to begin")
            {
                if (!scanner.isValidEmployee(barcodeValue))
                {
                    SystemSounds.Exclamation.Play();
                    scanner.messageValue = scanner.userValue + " needs to log out first!";
                    scanner.borderColor = "Red";
                    return;
                }
                scanner.userValue = barcodeValue;
                scanner.messageValue = "Welcome!";
            }
            //invalid
            else
            {
                SystemSounds.Exclamation.Play();
                if (scanner.userValue != "Log in to begin") { 
                    scanner.beginTimer();
                }
                scanner.messageValue = "Invalid barcode!";
                scanner.borderColor = "Red";
            }
        }
    }
}
