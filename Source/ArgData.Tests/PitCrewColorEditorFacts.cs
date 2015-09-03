﻿using System;
using ArgData.Entities;
using ArgData.Tests.DefaultData;
using FluentAssertions;
using Xunit;

namespace ArgData.Tests
{
    public class PitCrewColorEditorFacts
    {
        [Theory]
        [InlineData(GpExeVersionInfo.European105)]
        [InlineData(GpExeVersionInfo.Us105)]
        public void ReadingOriginalPitCrewColorsReturnsExpectedValues(GpExeVersionInfo exeVersionInfo)
        {
            var expectedPitCrewColors = new DefaultPitCrewColors();
            string exampleDataPath = ExampleDataHelper.GpExePath(exeVersionInfo);
            var exeEditor = new GpExeFile(exampleDataPath);
            var pitCrewColorEditor = new PitCrewColorEditor(exeEditor);

            var pitCrewColors = pitCrewColorEditor.ReadPitCrewColors();

            for (int i = 0; i < 14; i++)
            {
                pitCrewColors[i].ShouldBeEquivalentTo(expectedPitCrewColors[i]);
            }
        }

        [Theory]
        [InlineData(GpExeVersionInfo.European105)]
        [InlineData(GpExeVersionInfo.Us105)]
        public void ReadingSingleOriginalPitCrewColorReturnsExpectedValues(GpExeVersionInfo exeVersionInfo)
        {
            var expectedPitCrew = new DefaultPitCrewColors()[0];
            string exampleDataPath = ExampleDataHelper.GpExePath(exeVersionInfo);
            var exeEditor = new GpExeFile(exampleDataPath);
            var pitCrewColorEditor = new PitCrewColorEditor(exeEditor);

            var pitCrew = pitCrewColorEditor.ReadPitCrewColors(0);

            pitCrew.ShirtPrimary.Should().Be(expectedPitCrew.ShirtPrimary);
            pitCrew.ShirtSecondary.Should().Be(expectedPitCrew.ShirtSecondary);
            pitCrew.PantsPrimary.Should().Be(expectedPitCrew.PantsPrimary);
            pitCrew.PantsSecondary.Should().Be(expectedPitCrew.PantsSecondary);
            pitCrew.Socks.Should().Be(expectedPitCrew.Socks);
        }

        [Theory]
        [InlineData(GpExeVersionInfo.European105)]
        [InlineData(GpExeVersionInfo.Us105)]
        public void WriteAndReadPitCrews(GpExeVersionInfo exeVersionInfo)
        {
            var pitCrewList = new PitCrewList();
            for (int i = 0; i < Constants.NumberOfSupportedTeams; i++)
            {
                byte b = Convert.ToByte(i + 1);
                pitCrewList[i] = new PitCrew(new[] { b, b, b, b, b, b, b, b, b, b, b, b, b, b, b, b });
            }

            using (var context = ExampleDataContext.ExeCopy(exeVersionInfo))
            {
                var pitCrewColorEditor = new PitCrewColorEditor(context.ExeFile);

                pitCrewColorEditor.WritePitCrewColors(pitCrewList);

                var pitCrewColors = new PitCrewColorEditor(context.ExeFile).ReadPitCrewColors();

                byte expectedColor = 1;
                foreach (PitCrew pitCrew in pitCrewColors)
                {
                    pitCrew.ShirtPrimary.Should().Be(expectedColor);

                    expectedColor++;
                }
            }
        }

        [Theory]
        [InlineData(GpExeVersionInfo.European105)]
        [InlineData(GpExeVersionInfo.Us105)]
        public void WriteAndReadPitCrew(GpExeVersionInfo exeVersionInfo)
        {
            using (var context = ExampleDataContext.ExeCopy(exeVersionInfo))
            {
                var pitCrewList = new PitCrewList();
                var exeEditor = new GpExeFile(context.FilePath);
                var pitCrew = new PitCrew
                {
                    ShirtPrimary = 1,
                    ShirtSecondary = 2,
                    PantsPrimary = 3,
                    PantsSecondary = 4,
                    Socks = 5
                };
                pitCrewList[0] = pitCrew;

                var pitCrewColorEditor = new PitCrewColorEditor(exeEditor);

                pitCrewColorEditor.WritePitCrewColors(pitCrewList[0], 0);

                var pitCrewColors = new PitCrewColorEditor(exeEditor).ReadPitCrewColors();
                var actualPitCrew = pitCrewColors[0];

                actualPitCrew.ShirtPrimary.Should().Be(1);
                actualPitCrew.ShirtSecondary.Should().Be(2);
                actualPitCrew.PantsPrimary.Should().Be(3);
                actualPitCrew.PantsSecondary.Should().Be(4);
                actualPitCrew.Socks.Should().Be(5);
            }
        }
    }
}
