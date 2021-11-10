using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.GetParticipants();
            
             
            Data.CurrentRace.NextRace += OnNextRace;

            Data.Competition.CompetitionChanged += OnNextRace;
            
            
            
        }

    private void OnNextRace(object sender, EventArgs e)
    {
        if (Data.CurrentRace != null)
        {
            _currentTrack = Data.CurrentRace.track.Name;

            Dispatcher.Invoke(() =>
            {
                CurrentTrack.Content = _currentTrack;
                TimeBrokenList.ItemsSource = Data.Competition.GetParticipants();
                PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.GetParticipants();
            });
        }
    }

    private void OnChanche(object? sender, PropertyChangedEventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            TimeBrokenList.ItemsSource = Data.Competition.GetParticipants();
            PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.GetParticipants();
        });
    }

    private void TimeBrokenList_SelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
    {
        Dispatcher.Invoke(() =>
        {
            TimeBrokenList.ItemsSource = Data.Competition.GetParticipants();
            PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.GetParticipants();
        });
    }
    }
}
