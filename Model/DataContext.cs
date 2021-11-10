using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class DataContext : INotifyPropertyChanged
    {
        public string TrackName { get; set; }
        public List<IParticipant> Participants { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnDriversChanged(object sender, DriverChangeEventArgs e)
        {
            TrackName = e.Track.Name;
            Participants = e.Participants;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}