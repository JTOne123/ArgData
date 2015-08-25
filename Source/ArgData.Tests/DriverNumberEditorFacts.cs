﻿using System;
using FluentAssertions;
using Xunit;

namespace ArgData.Tests
{
    public class DriverNumberEditorFacts
    {
        [Theory]
        [InlineData(GpExeInfo.European105)]
        [InlineData(GpExeInfo.Us105)]
        public void ReadingOriginalDriverNumbersReturnsExpectedValues(GpExeInfo exeInfo)
        {
            var driverNumberEditor = ExampleDataHelper.DriverNumberEditorForDefault(exeInfo);

            byte[] driverNumbers = driverNumberEditor.ReadDriverNumbers();

            driverNumbers[0].Should().Be(1);
            driverNumbers[12].Should().Be(14, "Grouillard is number 14 in slot 13 (i.e. index 12)");
            driverNumbers[13].Should().Be(0);
            driverNumbers[30].Should().Be(31);
            driverNumbers[31].Should().Be(0, "Coloni only has one driver");
            driverNumbers[32].Should().Be(32);
        }

        [Theory]
        [InlineData(GpExeInfo.European105)]
        [InlineData(GpExeInfo.Us105)]
        public void WritingNumbersStoresExpectedValues(GpExeInfo exeInfo)
        {
            using (var context = ExampleDataContext.ExeCopy(exeInfo))
            {
                byte[] driverNumbers = CreateIncrementingDriverNumberArray(40);
                var driverNumberEditor = new DriverNumberEditor(context.ExeFile);

                driverNumberEditor.WriteDriverNumbers(driverNumbers);

                byte[] readNumbers = driverNumberEditor.ReadDriverNumbers();
                readNumbers[0].Should().Be(1);
                readNumbers[39].Should().Be(40);
            }
        }

        private byte[] CreateIncrementingDriverNumberArray(byte count)
        {
            byte[] driverNumbers = new byte[count];
            for (byte b = 0; b < count; b++)
            {
                driverNumbers[b] = Convert.ToByte(b + 1);
            }

            return driverNumbers;
        }

        [Theory]
        [InlineData(GpExeInfo.European105)]
        [InlineData(GpExeInfo.Us105)]
        public void FewerThan_39_DriversThenThrowException(GpExeInfo exeInfo)
        {
            using (var context = ExampleDataContext.ExeCopy(exeInfo))
            {
                byte[] tooFewDriverNumbers = CreateIncrementingDriverNumberArray(39);
                var driverNumberEditor = new DriverNumberEditor(context.ExeFile);

                Action act = () => driverNumberEditor.WriteDriverNumbers(tooFewDriverNumbers);

                act.ShouldThrow<Exception>();
            }
        }

        [Theory]
        [InlineData(GpExeInfo.European105)]
        [InlineData(GpExeInfo.Us105)]
        public void MoreThan_40_DriverNumbersThrowsException(GpExeInfo exeInfo)
        {
            using (var context = ExampleDataContext.ExeCopy(exeInfo))
            {
                byte[] tooManyDriverNumbers = CreateIncrementingDriverNumberArray(41);
                var driverNumberEditor = new DriverNumberEditor(context.ExeFile);

                Action act = () => driverNumberEditor.WriteDriverNumbers(tooManyDriverNumbers);

                act.ShouldThrow<Exception>();
            }
        }

        [Theory]
        [InlineData(GpExeInfo.European105)]
        [InlineData(GpExeInfo.Us105)]
        public void IfLessThan_26_ActiveDriversThenThrowException(GpExeInfo exeInfo)
        {
            using (var context = ExampleDataContext.ExeCopy(exeInfo))
            {
                byte[] first20 = CreateIncrementingDriverNumberArray(20);
                byte[] full = new byte[40];
                first20.CopyTo(full, 0);
                var driverNumberEditor = new DriverNumberEditor(context.ExeFile);

                Action act = () => driverNumberEditor.WriteDriverNumbers(full);

                act.ShouldThrow<Exception>();
            }
        }

        [Theory]
        [InlineData(GpExeInfo.European105)]
        [InlineData(GpExeInfo.Us105)]
        public void DriverNumberHigherThan40ThrowsException(GpExeInfo exeInfo)
        {
            using (var context = ExampleDataContext.ExeCopy(exeInfo))
            {
                byte[] driverNumbers = CreateIncrementingDriverNumberArray(40);
                var driverNumberEditor = new DriverNumberEditor(context.ExeFile);

                driverNumbers[10] = 41;
                Action act = () => driverNumberEditor.WriteDriverNumbers(driverNumbers);

                act.ShouldThrow<Exception>();
            }
        }
    }
}
