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
using System.Windows.Shapes;

namespace Waker
{
    /// <summary>
    /// WorkListSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WorkListSettingWindow : Window
    {
        public WorkListSettingWindow()
        {
            InitializeComponent();
        }



        public ObservableCollection<AlarmJob> Jobs
        {
            get { return (ObservableCollection<AlarmJob>)GetValue(JobsProperty); }
            set { SetValue(JobsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Jobs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JobsProperty =
            DependencyProperty.Register("Jobs", typeof(ObservableCollection<AlarmJob>), typeof(WorkListSettingWindow), new PropertyMetadata(new ObservableCollection<AlarmJob>()));


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AlarmJob job = new AlarmJob("0:15:00");
            SetJobProperty(job);
            Jobs.Add(job);
        }

        private void SetJobProperty(AlarmJob job)
        {
            JobPropertyPage jobPropertyPage = new JobPropertyPage();
            jobPropertyPage.DataContext = job;
            jobPropertyPage.Owner = this;

            jobPropertyPage.ShowDialog();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            int sel = JobListBox.SelectedIndex;
            Jobs.RemoveAt(sel);
            if (sel >= JobListBox.Items.Count)
                sel = JobListBox.Items.Count - 1;
            JobListBox.SelectedIndex = sel;
        }

        private void JobListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sel = JobListBox.SelectedIndex;
            int count = JobListBox.Items.Count;
            RemoveBtn.IsEnabled = sel != -1;
            EditBtn.IsEnabled = sel != -1;

            MoveUpBtn.IsEnabled = (sel != -1) && (sel != 0);
            MoveDownBtn.IsEnabled = (sel != -1) && (sel != (count-1));
        }

        private void JobListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (JobListBox.SelectedIndex == -1)
                return;

            AlarmJob job = new AlarmJob();
            job.Time = Jobs[JobListBox.SelectedIndex].Time;
            SetJobProperty(job);
            Jobs[JobListBox.SelectedIndex] = job;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (JobListBox.SelectedIndex == -1)
                return;

            AlarmJob job = new AlarmJob();
            job.Time = Jobs[JobListBox.SelectedIndex].Time;
            SetJobProperty(job);
            Jobs[JobListBox.SelectedIndex] = job;
        }

        private void MoveUpBtn_Click(object sender, RoutedEventArgs e)
        {
            int sel = JobListBox.SelectedIndex;
            if (sel == -1)
                return;
            int exi = sel - 1;
            AlarmJob cur = new AlarmJob();
            cur.Time = Jobs[sel].Time;

            AlarmJob ex = new AlarmJob();
            ex.Time = Jobs[exi].Time;

            Jobs[sel] = ex;
            Jobs[exi] = cur;

            JobListBox.SelectedIndex = exi;
        }

        private void MoveDownBtn_Click(object sender, RoutedEventArgs e)
        {
            int sel = JobListBox.SelectedIndex;
            if (sel == -1)
                return;
            int exi = sel + 1;
            AlarmJob cur = new AlarmJob();
            cur.Time = Jobs[sel].Time;

            AlarmJob ex = new AlarmJob();
            ex.Time = Jobs[exi].Time;

            Jobs[sel] = ex;
            Jobs[exi] = cur;

            JobListBox.SelectedIndex = exi;
        }
    }
}
