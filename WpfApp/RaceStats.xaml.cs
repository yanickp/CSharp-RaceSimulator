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
    /// Interaction logic for RaceStats.xaml
    /// </summary>
    public partial class RaceStats : Window

    { 
       private string _currentTrack;

    public RaceStats()
        {
            InitializeComponent();
            _currentTrack = Data.CurrentRace.track.Name;
            CurrentTrack.Content = _currentTrack;

            TimeBrokenList.ItemsSource = Data.CurrentRace.FinishedParticipants;
            PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.GetParticipantPoints();

            /*
             
            Data.CurrentRace.StartNextRace += OnNextRace;

            Data.Competition.ParticipantTimeBroken.PropertyChanged += OnParticipantTimeBrokenChanged;
            

            Data.Competition.ParticipantPerformanceBeforeAndAfter.PropertyChanged += OnParticipantPerformanceBeforeAndAfterChanged;
            
            */
        }

        private void TimeBrokenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
