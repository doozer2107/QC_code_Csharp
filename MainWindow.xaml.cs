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
using SharpDX;
using SharpDX.XInput;
using System.Threading;

namespace XBoxControl
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller XBox;
        private State myState;
        Thread myThread;
        private delegate void GetControllerValue(string triggerValue);
        GetControllerValue getControllerValue;
        GetControllerValue getAnlogVal;
        
        public MainWindow()
        {
            InitializeComponent();
            XBox = new Controller(UserIndex.One);
            getControllerValue += PrintValue;
            getAnlogVal += PrintLeftAnalogX;
        }

        private void button_GetValue_Click(object sender, RoutedEventArgs e)
        {
            myThread = new Thread(MyThreadFunktion);
            myThread.Start();

        }
        private void MyThreadFunktion()
        {
            while (true)
            {
                myState = XBox.GetState();
                string LeftTriggerValue = myState.Gamepad.LeftTrigger.ToString();
                string LeftAnalogX = myState.Gamepad.LeftThumbX.ToString();
                PrintValue(LeftTriggerValue);
                PrintLeftAnalogX(LeftAnalogX);
                if (myState.Gamepad.Buttons == GamepadButtonFlags.A)
                {
                    break;
                }
                Console.WriteLine(LeftTriggerValue);
                Thread.Sleep(100);
            }
            button_2_Click(null, null);
        }
        private void PrintValue(string triggerValue)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(getControllerValue, triggerValue);
            }
            else
            {
                textBox_Value.Text = triggerValue;
            }
        }
        private void PrintLeftAnalogX(string leftAnalogVal)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(getAnlogVal, leftAnalogVal);
            }
            else
            {
                textBox_LeftAnalogX.Text = leftAnalogVal;
            }
        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            myThread.Abort();
        
        }
    }
}
