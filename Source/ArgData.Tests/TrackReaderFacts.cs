﻿using FluentAssertions;
using Xunit;

namespace ArgData.Tests
{
    public class TrackReaderFacts
    {
        // these tests are a form of integration tests, making sure that all data reading glues together correctly

        [Fact]
        public void PhoenixDataIntegration()
        {
            var trackData = TrackFactsHelper.GetTrackPhoenix();
            var track = TrackReader.Read(trackData.Path);

            track.TrackSections.Count.Should().Be(67);
            track.PitLaneSections.Count.Should().Be(13);
            track.BestLineSegments.Count.Should().Be(40);
            track.LapCount.Should().Be(81);
        }

        [Fact]
        public void MexicoIntegration()
        {
            var trackData = TrackFactsHelper.GetTrackMexico();
            var track = TrackReader.Read(trackData.Path);

            track.LapCount.Should().Be(69);
        }
    }
}