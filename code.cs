private static DispatcherTimer dispatcher = new DispatcherTimer(); // inside your class of your app

//As I am using a checkbox to activate the device. 
//As logitech developer have not created a event listener to subscribe it to an event and continuously listen to events from the Wheel.
//So I am using DispatcherTimer as an event listener and to update my screen.

private void WHEEL_Checked(object sender, RoutedEventArgs e)
{
 if(WHEEL.IsChecked == true)
            {
                dispatcher.Interval = TimeSpan.FromMilliseconds(1);
                dispatcher.Tick += Start;
                dispatcher.Start();
            }
}

void Start()
{
 Dispatcher.BeginInvoke(new Action(() => { Update(); }));
}


void Update()
{
 LogitechGSDK.LogiSteeringInitialize(true);
            try
            { 
            if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
                {
                    LogitechGSDK.DIJOYSTATE2ENGINES eNGINES = LogitechGSDK.LogiGetStateCSharp(0);
                    LogitechGSDK.LogiPlaySpringForce(0, 0, 0, 0);
                    String wheel = (((eNGINES.lX * 5)) / 360).ToString();
                    Rotate.Text = wheel; 
                }
                else if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(1))
                {
                    LogitechGSDK.DIJOYSTATE2ENGINES eNGINES = LogitechGSDK.LogiGetStateCSharp(1);
                    LogitechGSDK.LogiPlaySpringForce(1, 0, 0, 0);
                    String wheel = (((eNGINES.lX * 5)) / 360).ToString();
                    Rotate.Text = wheel;                   
                }
                else if (LogitechGSDK.LogiIsConnected(0) | LogitechGSDK.LogiIsConnected(1) == false)
                {
                    WHEEL.IsChecked = false;                    
                    MessageBox.Show("Device not connected!! \nTry reconnecting or check if Logitech GameHub is running or not?");                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
}
