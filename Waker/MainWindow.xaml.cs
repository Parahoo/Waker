using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Waker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (UserSetting.Load(out setting) == false)
                setting.Save();

            Jobs = new ObservableCollection<AlarmJob>(setting.AlarmJobs);
            JobsTooltipListBox.ItemsSource = Jobs;

            RefreshJobBtn_Click(null, null);
        }

        UserSetting setting = new UserSetting();

        public ObservableCollection<AlarmJob> Jobs
        {
            get { return (ObservableCollection<AlarmJob>)GetValue(JobsProperty); }
            set { SetValue(JobsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Jobs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JobsProperty =
            DependencyProperty.Register("Jobs", typeof(ObservableCollection<AlarmJob>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<AlarmJob>()));


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void SysSetBtn_Click(object sender, RoutedEventArgs e)
        {
            settingtoolbar.IsOpen = true;
        }

        DoingAlarmJobs doingAlarmJobs = new DoingAlarmJobs();
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartBtn.IsEnabled = false;
            doingAlarmJobs.Jobs = new List<AlarmJob>(Jobs);

            doingAlarmJobs.DoJob((AlarmJob job) => {
                ClockControl.Start(job.Time);
            });
        }

        private void ClockControl_WakeFinished(object sender, EventArgs e)
        {
            doingAlarmJobs.DoJob((AlarmJob job) => {
                ClockControl.Start(job.Time);
            });
        }

        private void RefreshJobBtn_Click(object sender, RoutedEventArgs e)
        {
            StartBtn.IsEnabled = true; 
            if(Jobs.Count > 0)
                ClockControl.Reset(Jobs[0].Time);
            else
                ClockControl.Reset("00:00:00");
        }

        private void SetJobsBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkListSettingWindow workListSettingWindow = new WorkListSettingWindow();
            workListSettingWindow.Jobs = Jobs;
            workListSettingWindow.ShowDialog();

            setting.AlarmJobs = new List<AlarmJob>(Jobs);
            setting.Save();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
