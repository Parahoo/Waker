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
using System.Windows.Threading;

namespace Waker
{
    /// <summary>
    /// ClockControl.xaml 的交互逻辑
    /// </summary>
    public partial class ClockControl : UserControl
    {
        public ClockControl()
        {
            InitializeComponent();
        }


        public TimeSpan RemainTime
        {
            get { return (TimeSpan)GetValue(RemainTimeProperty); }
            set { SetValue(RemainTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemainTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemainTimeProperty =
            DependencyProperty.Register("RemainTime", typeof(TimeSpan), typeof(ClockControl), new PropertyMetadata(TimeSpan.FromMinutes(10)));


        public event EventHandler WakeFinished
        {
            add { AddHandler(WakeFinishedEvent, value); }
            remove { RemoveHandler(WakeFinishedEvent, value); }
        }

        public static readonly RoutedEvent WakeFinishedEvent = EventManager.RegisterRoutedEvent(
        "WakeFinished", RoutingStrategy.Bubble, typeof(EventHandler), typeof(ClockControl));


        DispatcherTimer TickTimer = new DispatcherTimer();
        public void Start(string wake)
        {
            TimeSpan wakeTime = TimeSpan.Parse(wake);
            if (TickTimer.IsEnabled)
                TickTimer.Stop();

            DateTime startTime = DateTime.Now;

            TickTimer = new DispatcherTimer();
            TickTimer.Interval = TimeSpan.FromMilliseconds(200);
            TickTimer.Tick += (object sender, EventArgs e)=> {
                var remain = wakeTime - (DateTime.Now - startTime);
                if (remain < TimeSpan.Zero)
                    remain = TimeSpan.Zero;

                RemainTime = remain;

                if (remain <= TimeSpan.Zero)
                {
                    TickTimer.Stop();
                    SoundPlayer.Source = new Uri(@"pack://siteoforigin:,,,/clocksound.mp3");
                    SoundPlayer.Play();

                    RaiseEvent(new RoutedEventArgs(WakeFinishedEvent));
                }
            };


            TickTimer.Start();
        }

        public void Reset(string remain)
        {
            if (TickTimer.IsEnabled)
                TickTimer.Stop();

            RemainTime = TimeSpan.Parse(remain);
        }
    }
}
