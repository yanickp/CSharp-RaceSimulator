using Model;
using NUnit.Framework;

namespace ModelTests
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;


        [SetUp]
        public void SetUp()
        {
            _competition = new Competition("testComp");
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Section.SectionTypes[] sect1 =
            {
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner
            };
            Track track1 = new Track("Racetrack1", sect1);

            _competition.Tracks.Enqueue(track1);

            var result = _competition.NextTrack();

            Assert.AreEqual(track1, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Section.SectionTypes[] sect1 =
            {
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
            };
            Track track1 = new Track("Racetrack1", sect1);

            _competition.Tracks.Enqueue(track1);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Section.SectionTypes[] sect1 =
            {
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
            };
            Track track1 = new Track("Racetrack1", sect1);
            Track track2 = new Track("Racetrack2", sect1);

            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.AreEqual(track2, result);
        }
    }
}