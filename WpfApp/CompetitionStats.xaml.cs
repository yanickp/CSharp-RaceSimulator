using Controller;
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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for CompetitionStats.xaml
    /// </summary>
    public partial class CompetitionStats : Window
    {
        public CompetitionStats()
        {
            InitializeComponent();

            StartTime.Content = $"start tijd: {Data.CurrentRace.StartTime}";

            Data.Competition.CompetitionChanged += Competition_CompetitionChanged;
            PointsList.ItemsSource = Data.Competition.GetParticipantPoints();
        }

        private void Competition_CompetitionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                PointsList.ItemsSource = Data.Competition.GetParticipantPoints();
            });
        }

    }
}
