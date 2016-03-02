using System;
using System.IO;
using System.Linq;

namespace ArgData
{
    /// <summary>
    /// Represents a GP.EXE file that will be read from or written to.
    /// </summary>
    public class GpExeFile
    {
        /// <summary>
        /// Initializes an instance of GpExeFile.
        /// </summary>
        /// <param name="exePath">Path to the GP.EXE file</param>
        public GpExeFile(string exePath)
        {
            ExePath = exePath;

            ValidateFileSupport(exePath);

            SetDataPositions();
        }

        private void ValidateFileSupport(string exePath)
        {
            var exeInfo = GetFileInfo(exePath);

            if (exeInfo != GpExeVersionInfo.European105 && exeInfo != GpExeVersionInfo.Us105)
            {
                string msg = $"The specified file is of type {exeInfo}. ArgData currently supports European105 and Us105.";
                throw new Exception(msg);
            }
        }

        /// <summary>
        /// Gets info about the specified F1GP executable.
        /// </summary>
        /// <param name="exePath">Path of the file to get info about.</param>
        /// <returns>GpExeVersionInfo enum describing the file.</returns>
        public static GpExeVersionInfo GetFileInfo(string exePath)
        {
            var fileInfo = new FileInfo(exePath);

            switch (fileInfo.Length)
            {
                case 321878:
                    return GpExeVersionInfo.European105;

                case 321716:
                    return GpExeVersionInfo.Us105;

                default:
                    return GpExeVersionInfo.Unknown;
            }
        }

        private void SetDataPositions()
        {
            var fileInfo = GetFileInfo(ExePath);

            switch (fileInfo)
            {
                case GpExeVersionInfo.European105:
                    PlayerHorsepowerPosition = 19848;
                    TeamHorsepowerPosition = 158380;
                    CarColorsPosition = 158500;
                    RaceGripLevelsPosition = 158460;
                    QualifyingGripLevelsPosition = 158420;
                    DriverNumbersPosition = 154936;
                    PitCrewColorsPosition = 159421;
                    HelmetColorsPosition = 158795;
                    break;

                case GpExeVersionInfo.Us105:
                    PlayerHorsepowerPosition = 19848;
                    TeamHorsepowerPosition = 158336;
                    CarColorsPosition = 158456;
                    RaceGripLevelsPosition = 158416;
                    QualifyingGripLevelsPosition = 158376;
                    DriverNumbersPosition = 154892;
                    PitCrewColorsPosition = 159377;
                    HelmetColorsPosition = 158751;
                    break;
            }
        }

        private int PlayerHorsepowerPosition { get; set; }
        private int TeamHorsepowerPosition { get; set; }
        private int CarColorsPosition { get; set; }
        private int RaceGripLevelsPosition { get; set; }
        private int QualifyingGripLevelsPosition { get; set; }
        private int DriverNumbersPosition { get; set; }
        private int PitCrewColorsPosition { get; set; }
        private int HelmetColorsPosition { get; set; }

        /// <summary>
        /// Gets the path to the GP.EXE file.
        /// </summary>
        public string ExePath { get; }

        internal int GetPlayerHorsepowerPosition()
        {
            return PlayerHorsepowerPosition;
        }

        internal const int ColorsPerTeam = 16;

        internal int GetCarColorsPosition(int teamIndex)
        {
            return CarColorsPosition + (teamIndex * ColorsPerTeam);
        }

        internal int GetCarColorsPosition()
        {
            return CarColorsPosition;
        }

        internal int GetPitCrewColorsPosition(int teamIndex)
        {
            return PitCrewColorsPosition + (teamIndex * ColorsPerTeam);
        }

        internal int GetPitCrewColorsPosition()
        {
            return PitCrewColorsPosition;
        }

        internal int GetDriverNumbersPosition()
        {
            return DriverNumbersPosition;
        }

        internal int GetDriverNumbersPosition(int driverIndex)
        {
            return DriverNumbersPosition + driverIndex;
        }

        internal int GetTeamHorsepowerPosition(int teamIndex)
        {
            return TeamHorsepowerPosition + (teamIndex * 2);
        }

        internal int GetRaceGripLevelPosition()
        {
            return RaceGripLevelsPosition;
        }

        internal int GetRaceGripLevelPositions(int driverNumber)
        {
            return RaceGripLevelsPosition + driverNumber - 1;
        }

        //internal int GetRaceGripLevelPositions(int driverIndex)
        //{
        //    return RaceGripLevelsPosition + driverIndex;
        //}

        internal int GetQualifyingGripLevelPositions(int driverIndex)
        {
            return QualifyingGripLevelsPosition + driverIndex;
        }

        internal int GetHelmetColorsPosition(int helmetIndex)
        {
            int bytesForPreviousHelmets = _bytesPerHelmet.Take(helmetIndex).Sum(b => b);

            return HelmetColorsPosition + bytesForPreviousHelmets;
        }

        internal int GetHelmetColorsPositionByteCountToRead(int helmetIndex)
        {
            return _bytesPerHelmet[helmetIndex];
        }

        private readonly byte[] _bytesPerHelmet =
        {
            16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
            16, 16, 14, 16, 14, 16, 16, 16, 16, 16,
            16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
            16, 16, 16, 16, 16, 14, 14, 14, 14, 14
        };
    }
}
