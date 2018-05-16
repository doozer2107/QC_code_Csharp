using System;
using System.Collections.Generic;
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
using System.Threading;
using SharpDX;
using SharpDX.XInput;

namespace ScannXboxController
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller XBox;
        private State myState;
        Thread myThread;
        private delegate void GetControllerValue(string str);
        GetControllerValue printLeftTriggerValDel;
        GetControllerValue printLeftThumbXDel;
        GetControllerValue printLeftThumbYDel;
        GetControllerValue printRightTriggerValDel;
        GetControllerValue printRightThumbXDel;
        GetControllerValue printRightThumbYDel;
        GetControllerValue ListBoxAddADel;
        string LeftTriggerVal;
        string LeftThumbX;
        string LeftThumbY;
        string RightTriggerVal;
        string RightThumbX;
        string RightThumbY;
        public MainWindow()
        {
            InitializeComponent();
            XBox = new Controller(UserIndex.One);
            printLeftTriggerValDel += PrintLeftTriggerVal;
            printLeftThumbXDel += PrintLeftThumbX;
            printLeftThumbYDel += PrintLeftThumbY;
            printRightTriggerValDel += PrintRightTriggerVal;
            printRightThumbXDel += PrintRightThumbX;
            printRightThumbYDel += PrintRightThumbY;
            ListBoxAddADel += ListBoxAddA;
        }

        private void button_Start_Click(object sender, RoutedEventArgs e)
        {
            myThread = new Thread(MyThreadFunction);
            myThread.Start();
        }
        private void MyThreadFunction()
        {
            while (true)
            {
                Thread.Sleep(100);//jeder 500ms einen neuen Wert aktualisieren
                myState = XBox.GetState();
                LeftTriggerVal = myState.Gamepad.LeftTrigger.ToString();
                LeftThumbX = myState.Gamepad.LeftThumbX.ToString();
                LeftThumbY = myState.Gamepad.LeftThumbY.ToString();
                RightTriggerVal = myState.Gamepad.RightTrigger.ToString();
                RightThumbX = myState.Gamepad.RightThumbX.ToString();
                RightThumbY = myState.Gamepad.RightThumbY.ToString();
                PrintLeftTriggerVal(LeftTriggerVal);
                PrintLeftThumbX(LeftThumbX);
                PrintLeftThumbY(LeftThumbY);
                PrintRightTriggerVal(RightTriggerVal);
                PrintRightThumbX(RightThumbX);
                PrintRightThumbY(RightThumbY);
                if (myState.Gamepad.Buttons == GamepadButtonFlags.A)
                {
                    ListBoxAddA("A");
                }
            }
        }

        private void ListBoxAddA(string a)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(ListBoxAddADel, a);
            }
            else
            {
                listBox_Result.Items.Add(a);
            }
        }

        private void PrintRightThumbY(string rightThumbY)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(printRightThumbYDel, rightThumbY);
            }
            else
            {
                textBox_RightThumbY.Text = rightThumbY;
            }
        }

        private void PrintRightThumbX(string rightThumbX)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(printRightThumbXDel, rightThumbX);
            }
            else
            {
                textBox_RightThumbX.Text = rightThumbX;
            }
        }

        private void PrintRightTriggerVal(string rightTriggerVal)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(printRightTriggerValDel, rightTriggerVal);
            }
            else
            {
                textBox_RightTriggerVal.Text = rightTriggerVal;
            }
        }

        private void PrintLeftThumbY(string leftThumbY)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(printLeftThumbYDel, leftThumbY);
            }
            else
            {
                textBox_LeftThumbY.Text = leftThumbY;
            }
        }

        private void PrintLeftThumbX(string leftThumbX)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(printLeftThumbXDel, leftThumbX);
            }
            else
            {
                textBox_LeftThumbX.Text = leftThumbX;
            }
        }

        private void PrintLeftTriggerVal(string leftTriggerVal)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(printLeftTriggerValDel, leftTriggerVal);
            }
            else
            {
                textBox_LeftTriggerVal.Text = leftTriggerVal;
            }
        }

        private void button_Stop_Click(object sender, RoutedEventArgs e)
        {
            myThread.Abort();
        }
    }
}
