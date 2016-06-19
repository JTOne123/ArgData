﻿using System;
using ArgData.Entities;
using FluentAssertions;
using Xunit;

namespace ArgData.Tests
{
    public class SetupFacts
    {
        [Fact]
        public void Read_ReadingSetupFile1_ReturnsExpectedValues()
        {
            var path = ExampleDataHelper.GetExampleDataPath("setup-w40-39_bb0_tyC_ra24-32-39-46-53-61");

            var setup = SetupReader.ReadSingle(path);

            setup.FrontWing.Should().Be(40);
            setup.RearWing.Should().Be(39);
            setup.GearRatio1.Should().Be(24);
            setup.GearRatio2.Should().Be(32);
            setup.GearRatio3.Should().Be(39);
            setup.GearRatio4.Should().Be(46);
            setup.GearRatio5.Should().Be(53);
            setup.GearRatio6.Should().Be(61);
            setup.TyresCompound.Should().Be(SetupTyreCompound.C);
            setup.BrakeBalanceValue.Should().Be(0);
            setup.BrakeBalanceDirection.Should().Be(SetupBrakeBalanceDirection.Front);
        }

        [Fact]
        public void Read_ReadingSetupFile2_ReturnsExpectedValues()
        {
            var path = ExampleDataHelper.GetExampleDataPath("setup-w0-15_bb32f_tyA_ra17-31-39-46-50-56");

            var setup = SetupReader.ReadSingle(path);

            setup.FrontWing.Should().Be(0);
            setup.RearWing.Should().Be(15);
            setup.GearRatio1.Should().Be(17);
            setup.GearRatio2.Should().Be(31);
            setup.GearRatio3.Should().Be(39);
            setup.GearRatio4.Should().Be(46);
            setup.GearRatio5.Should().Be(50);
            setup.GearRatio6.Should().Be(56);
            setup.TyresCompound.Should().Be(SetupTyreCompound.A);
            setup.BrakeBalanceValue.Should().Be(32);
            setup.BrakeBalanceDirection.Should().Be(SetupBrakeBalanceDirection.Front);
        }

        [Fact]
        public void Read_ReadingSetupFile3_ReturnsExpectedValues()
        {
            var path = ExampleDataHelper.GetExampleDataPath("setup-w24-8_bb5f_tyB_ra25-34-42-50-57-64");

            var setup = SetupReader.ReadSingle(path);

            setup.FrontWing.Should().Be(24);
            setup.RearWing.Should().Be(8);
            setup.GearRatio1.Should().Be(25);
            setup.GearRatio2.Should().Be(34);
            setup.GearRatio3.Should().Be(42);
            setup.GearRatio4.Should().Be(50);
            setup.GearRatio5.Should().Be(57);
            setup.GearRatio6.Should().Be(64);
            setup.TyresCompound.Should().Be(SetupTyreCompound.B);
            setup.BrakeBalanceValue.Should().Be(5);
            setup.BrakeBalanceDirection.Should().Be(SetupBrakeBalanceDirection.Front);
        }

        [Fact]
        public void Read_ReadingSetupFile4_ReturnsExpectedValues()
        {
            var path = ExampleDataHelper.GetExampleDataPath("setup-w64-60_bb6r_tyD_ra16-30-38-45-49-55");

            var setup = SetupReader.ReadSingle(path);

            setup.FrontWing.Should().Be(64);
            setup.RearWing.Should().Be(60);
            setup.GearRatio1.Should().Be(16);
            setup.GearRatio2.Should().Be(30);
            setup.GearRatio3.Should().Be(38);
            setup.GearRatio4.Should().Be(45);
            setup.GearRatio5.Should().Be(49);
            setup.GearRatio6.Should().Be(55);
            setup.TyresCompound.Should().Be(SetupTyreCompound.D);
            setup.BrakeBalanceValue.Should().Be(6);
            setup.BrakeBalanceDirection.Should().Be(SetupBrakeBalanceDirection.Rear);
        }

        [Fact]
        public void Read_NotSingleSetupFile_Throws()
        {
            var path = ExampleDataHelper.GetExampleDataPath("GP-EU105.EXE");

            Action act = () => SetupReader.ReadSingle(path);

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Read_MultipleSetupsNotSeparate_ReturnsFalse()
        {
            var path = ExampleDataHelper.GetExampleDataPath("LOTSETUP");

            var list = SetupReader.ReadMultiple(path);

            list.SeparateSetups.Should().BeFalse();
        }

        [Fact]
        public void Read_MultipleSetupsSeparate_ReturnsTrue()
        {
            var path = ExampleDataHelper.GetExampleDataPath("LOTSETUS");

            var list = SetupReader.ReadMultiple(path);

            list.SeparateSetups.Should().BeTrue();
        }

        [Fact]
        public void Read_MultipleSetups_ReturnsCorrectWingLevels()
        {
            var path = ExampleDataHelper.GetExampleDataPath("LOTSETUP");

            var list = SetupReader.ReadMultiple(path);

            byte index = 1;

            foreach (var setup in list.QualifyingSetups)
            {
                setup.FrontWing.Should().Be(index);
                setup.RearWing.Should().Be(index);
                index++;
            }

            foreach (var setup in list.RaceSetups)
            {
                setup.FrontWing.Should().Be(index);
                setup.RearWing.Should().Be(index);
                index++;
            }
        }

        [Fact]
        public void Read_NotMultipleSetupFile_Throws()
        {
            var path = ExampleDataHelper.GetExampleDataPath("GP-EU105.EXE");

            Action act = () => SetupReader.ReadMultiple(path);

            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void WriteSingle_KnownValues_AreStoredCorrectly()
        {
            var setup = new Setup
            {
                FrontWing = 19,
                RearWing = 60,
                BrakeBalanceValue = 5,
                BrakeBalanceDirection = SetupBrakeBalanceDirection.Rear,
                GearRatio1 = 25,
                GearRatio2 = 28,
                GearRatio3 = 33,
                GearRatio4 = 38,
                GearRatio5 = 40,
                GearRatio6 = 46,
                TyresCompound = SetupTyreCompound.D
            };

            using (var context = ExampleDataContext.GetTempFileName(".set"))
            {
                SetupWriter.WriteSingle(setup, context.FilePath);

                var setupOnDisk = SetupReader.ReadSingle(context.FilePath);

                setupOnDisk.FrontWing.Should().Be(setup.FrontWing);
                setupOnDisk.RearWing.Should().Be(setup.RearWing);
                setupOnDisk.GearRatio1.Should().Be(setup.GearRatio1);
                setupOnDisk.GearRatio2.Should().Be(setup.GearRatio2);
                setupOnDisk.GearRatio3.Should().Be(setup.GearRatio3);
                setupOnDisk.GearRatio4.Should().Be(setup.GearRatio4);
                setupOnDisk.GearRatio5.Should().Be(setup.GearRatio5);
                setupOnDisk.GearRatio6.Should().Be(setup.GearRatio6);
                setupOnDisk.TyresCompound.Should().Be(setup.TyresCompound);
                setupOnDisk.BrakeBalanceValue.Should().Be(setup.BrakeBalanceValue);
                setupOnDisk.BrakeBalanceDirection.Should().Be(setup.BrakeBalanceDirection);
            }
        }

        [Theory]
        [InlineData("setup-w24-8_bb5f_tyB_ra25-34-42-50-57-64")]
        [InlineData("setup-w0-15_bb32f_tyA_ra17-31-39-46-50-56")]
        public void WriteSingle_KnownSetup_StoredCorrectly(string knownSetupFile)
        {
            var setup = SetupReader.ReadSingle(ExampleDataHelper.GetExampleDataPath(knownSetupFile));

            using (var context = ExampleDataContext.GetTempFileName(".set"))
            {
                SetupWriter.WriteSingle(setup, context.FilePath);

                var setupOnDisk = SetupReader.ReadSingle(context.FilePath);

                setupOnDisk.FrontWing.Should().Be(setup.FrontWing);
                setupOnDisk.RearWing.Should().Be(setup.RearWing);
                setupOnDisk.GearRatio1.Should().Be(setup.GearRatio1);
                setupOnDisk.GearRatio2.Should().Be(setup.GearRatio2);
                setupOnDisk.GearRatio3.Should().Be(setup.GearRatio3);
                setupOnDisk.GearRatio4.Should().Be(setup.GearRatio4);
                setupOnDisk.GearRatio5.Should().Be(setup.GearRatio5);
                setupOnDisk.GearRatio6.Should().Be(setup.GearRatio6);
                setupOnDisk.TyresCompound.Should().Be(setup.TyresCompound);
                setupOnDisk.BrakeBalanceValue.Should().Be(setup.BrakeBalanceValue);
                setupOnDisk.BrakeBalanceDirection.Should().Be(setup.BrakeBalanceDirection);
            }
        }

        [Fact]
        public void WriteMultiple()
        {
            var setups = CreateSetupListForTest(true);

            using (var context = ExampleDataContext.GetTempFileName(".set"))
            {
                SetupWriter.WriteMultiple(setups, context.FilePath);

                var setupsOnDisk = SetupReader.ReadMultiple(context.FilePath);

                setupsOnDisk.SeparateSetups.Should().BeTrue();

                byte index = 1;

                for (int qi = 0; qi <= 15; qi++)
                {
                    var setup = setupsOnDisk.QualifyingSetups[qi];
                    setup.FrontWing.Should().Be(index);
                    setup.RearWing.Should().Be(Convert.ToByte(index + 10));
                    setup.GearRatio1.Should().Be(30);
                    setup.GearRatio2.Should().Be(31);
                    setup.GearRatio3.Should().Be(32);
                    setup.GearRatio4.Should().Be(33);
                    setup.GearRatio5.Should().Be(34);
                    setup.GearRatio6.Should().Be(45);
                    setup.TyresCompound.Should().Be(SetupTyreCompound.B);
                    setup.BrakeBalanceValue.Should().Be(10);
                    setup.BrakeBalanceDirection.Should().Be(SetupBrakeBalanceDirection.Front);
                    index++;
                }

                for (int ri = 0; ri <= 15; ri++)
                {
                    var setup = setupsOnDisk.RaceSetups[ri];
                    setup.FrontWing.Should().Be(index);
                    setup.RearWing.Should().Be(Convert.ToByte(index + 10));
                    setup.GearRatio1.Should().Be(30);
                    setup.GearRatio2.Should().Be(31);
                    setup.GearRatio3.Should().Be(32);
                    setup.GearRatio4.Should().Be(33);
                    setup.GearRatio5.Should().Be(34);
                    setup.GearRatio6.Should().Be(45);
                    setup.TyresCompound.Should().Be(SetupTyreCompound.C);
                    setup.BrakeBalanceValue.Should().Be(25);
                    setup.BrakeBalanceDirection.Should().Be(SetupBrakeBalanceDirection.Rear);

                    index++;
                }
            }
        }

        private SetupList CreateSetupListForTest(bool separate)
        {
            var setups = new SetupList
            {
                SeparateSetups = separate
            };

            byte index = 1;

            for (int qi = 0; qi <= 15; qi++)
            {
                var setup = setups.QualifyingSetups[qi];
                setup.FrontWing = index;
                setup.RearWing = Convert.ToByte(index + 10);
                setup.GearRatio1 = 30;
                setup.GearRatio2 = 31;
                setup.GearRatio3 = 32;
                setup.GearRatio4 = 33;
                setup.GearRatio5 = 34;
                setup.GearRatio6 = 45;
                setup.TyresCompound = SetupTyreCompound.B;
                setup.BrakeBalanceValue = 10;
                setup.BrakeBalanceDirection = SetupBrakeBalanceDirection.Front;

                index++;
            }

            for (int ri = 0; ri <= 15; ri++)
            {
                var setup = setups.RaceSetups[ri];
                setup.FrontWing = index;
                setup.RearWing = Convert.ToByte(index + 10);
                setup.GearRatio1 = 30;
                setup.GearRatio2 = 31;
                setup.GearRatio3 = 32;
                setup.GearRatio4 = 33;
                setup.GearRatio5 = 34;
                setup.GearRatio6 = 45;
                setup.TyresCompound = SetupTyreCompound.C;
                setup.BrakeBalanceValue = 25;
                setup.BrakeBalanceDirection = SetupBrakeBalanceDirection.Rear;

                index++;
            }

            return setups;
        }
    }
}