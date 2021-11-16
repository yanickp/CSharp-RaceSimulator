using System;
using System.Collections.Generic;
using Controller;
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

    [TestFixture]
    public class Controller_Race_SetStartPositionParticipantsShould
    {
        private Race _race;

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();

            _race = new Race(Data.CurrentRace.track, Data.CurrentRace.Participants);
        }

        [Test]
        public void ResetStats_should()
        {
            Data.CurrentRace.ResetParticipantsStats();
            foreach (IParticipant p in _race.Participants)
            {
                    Assert.AreEqual(p.Equipment.Preformance, p.Equipment.startPreformance);
                    Assert.AreEqual(p.Equipment.startQuality, p.Equipment.startQuality);
            }
        }

        [Test]
        public void AddParticipants_error()
        {
            Data.Competition.Participants = new List<IParticipant>();
            Data.AddParticipants(999);
            Assert.AreEqual(2, Data.Competition.Participants.Count);
        }

        [Test]
        public void Participant_Count_should()
        {
            Data.Competition.Participants = new List<IParticipant>();
            Data.AddParticipants(5);
            Assert.AreEqual(5, Data.Competition.Participants.Count);
        }

        [Test]
        public void MaxParticipants_should()
        {
            Data.Competition.Participants = new List<IParticipant>();
            Data.AddParticipants(Enum.GetNames(typeof(IParticipant.TeamColors)).Length);
            Assert.AreEqual(Enum.GetNames(typeof(IParticipant.TeamColors)).Length, Data.Competition.Participants.Count);
        }

        [Test]
        public void InvalidTrack_should()
        {
            Section.SectionTypes[] s =
            {
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Finish,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
            };
            bool testTrack = Data.ValidateTrack(s);
            
            Assert.AreEqual(testTrack, false);
        }

        [Test]
        public void TrackWithoutStart_should()
        {
            Section.SectionTypes[] s =
            {
                Section.SectionTypes.Straight,
                Section.SectionTypes.Finish,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
            };
            bool testTrack = Data.ValidateTrack(s);

            Assert.AreEqual(testTrack, false);
        }

        [Test]
        public void TrackValid_should()
        {
            Section.SectionTypes[] s =
            {
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Finish,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
            };
            bool testTrack = Data.ValidateTrack(s);
            
            Assert.AreEqual(testTrack, true);
        }
        
        [Test]
        public void TrackNotRound_should()
        {
            Section.SectionTypes[] s =
            {
                
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Finish,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
            };
            bool testTrack = Data.ValidateTrack(s);
            
            Assert.AreEqual(testTrack, false);
        }
        
        
    }
    
    [TestFixture]
    public class Controller_Race_RandomizeEquipmentShould
    {
        private Race _race;

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();

            _race = new Race(Data.CurrentRace.track, Data.CurrentRace.Participants);
        }

        [Test]
        public void RandomizeEquipment_ParticipantsEquipment_AreNotEqual()
        {
            IParticipant participant1 = _race.Participants[0];
            IParticipant participant2 = _race.Participants[1];
            Assert.AreNotEqual(participant1.Equipment, participant2.Equipment);
        }
    }
}