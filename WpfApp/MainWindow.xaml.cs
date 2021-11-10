using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RaceStats raceStats;
        private CompetitionStats CompetitionStats;

        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            SubscribeEvents();
        }

        private void OnDriversChanged(object sender, DriverChangeEventArgs e)
        {
            Display(e.Track);
        }

        private void OnRaceFinished(object sender, RaceFinishedEventArgs e)
        {
            //werkt niet om een bepaalde reden, code komt hier wel...
            // raceStats = new RaceStats();
            // raceStats.Show();
        }

        private void OnStartNextRace(object sender, EventArgs e)
        {
            UnsubscribeEvents();
            imageCache.Clear();
            Data.NextRace();

            if (Data.CurrentRace != null)
            {
                SubscribeEvents();
                Display(Data.CurrentRace.track);
            }
        }

        private void Display(Track track)
        {
            Track.Dispatcher.BeginInvoke(
                 DispatcherPriority.Render,
                 new Action(() =>
                {
                    Track.Source = null;
                    Track.Source = Draw.DrawTrack(track);
                })
            );
        }

        private void SubscribeEvents()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceIsFinnished += OnRaceFinished;
            Data.CurrentRace.NextRace += OnStartNextRace;
            
            Dispatcher.Invoke(() =>
            {
                Data.CurrentRace.DriversChanged += ((DataContext)DataContext).OnDriversChanged;
                Data.CurrentRace.DriversChanged += ((DataContext)DataContext).OnDriversChanged;
            });
            
        }

        private void UnsubscribeEvents()
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceIsFinnished -= OnRaceFinished;
            Data.CurrentRace.NextRace -= OnStartNextRace;
        }

        private void MenuItem_RaceStats_Click(object sender, RoutedEventArgs e)
        {
            raceStats = new RaceStats();
            raceStats.Show();
        }

        private void MenuItem_CompetionStats_Click(object sender, RoutedEventArgs e)
        {
            CompetitionStats = new CompetitionStats();
            CompetitionStats.Show();
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}