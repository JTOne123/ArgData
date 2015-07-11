﻿using ArgData.IO;

namespace ArgData
{
    /// <summary>
    /// Edits the race or qualifying grip level for computer drivers.
    /// </summary>
    public class GripLevelEditor
    {
        private readonly GpExeFile _exeFile;

        /// <summary>
        /// Initializes a new instance of a GripLevelEditor.
        /// </summary>
        /// <param name="exeFile">GpExeFile to edit.</param>
        public GripLevelEditor(GpExeFile exeFile)
        {
            _exeFile = exeFile;
        }

        /// <summary>
        /// Reads the race grip level of the driver at the specified index. Lower value indicates higher grip.
        /// </summary>
        /// <param name="driverIndex">Index of driver to read race grip level for.</param>
        /// <returns>Grip level.</returns>
        public byte ReadRaceGripLevel(int driverIndex)
        {
            int position = _exeFile.GetRaceGripLevelPositions(driverIndex);
            byte value = new FileReader(_exeFile.ExePath).ReadByte(position);

            return value;
        }

        /// <summary>
        /// Reads the qualifying grip level of the driver at the specified index. Lower value indicates higher grip.
        /// </summary>
        /// <param name="driverIndex">Index of driver to read qualifying grip level for.</param>
        /// <returns>Grip level.</returns>
        public byte ReadQualifyingGripLevel(int driverIndex)
        {
            int position = _exeFile.GetQualifyingGripLevelPositions(driverIndex);
            byte value = new FileReader(_exeFile.ExePath).ReadByte(position);

            return value;
        }

        /// <summary>
        /// Writes the race grip level for the driver at the specified index. Lower value indicates higher grip.
        /// </summary>
        /// <param name="driverIndex">Index of driver to write race grip level for.</param>
        /// <param name="gripLevel">Grip level.</param>
        public void WriteRaceGripLevel(int driverIndex, byte gripLevel)
        {
            int position = _exeFile.GetRaceGripLevelPositions(driverIndex);
            new FileWriter(_exeFile.ExePath).WriteByte(gripLevel, position);
        }

        /// <summary>
        /// Writes the qualifying grip level for the driver at the specified index. Lower value indicates higher grip.
        /// </summary>
        /// <param name="driverIndex">Index of driver to write qualifying grip level for.</param>
        /// <param name="gripLevel">Grip level.</param>
        public void WriteQualifyingGripLevel(int driverIndex, byte gripLevel)
        {
            int position = _exeFile.GetQualifyingGripLevelPositions(driverIndex);
            new FileWriter(_exeFile.ExePath).WriteByte(gripLevel, position);
        }
    }
}
