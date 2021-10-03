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
            Track result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Section.SectionTypes[] s =
            {
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.RightCorner
            };
            Track track1 = new Track("1", s);

            _competition.Tracks.Enqueue(track1);

            Track result = _competition.NextTrack();

            Assert.AreEqual(track1, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Section.SectionTypes[] s =
            {
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
            };
            Track track1 = new Track("1", s);

            _competition.Tracks.Enqueue(track1);

            Track result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Section.SectionTypes[] s =
            {
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
            };
            Track track1 = new Track("1", s);
            Track track2 = new Track("2", s);

            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);

            Track result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.AreEqual(track2, result);
        }
    }
}