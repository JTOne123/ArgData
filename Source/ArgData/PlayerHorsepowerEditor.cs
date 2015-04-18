﻿using ArgData.IO;

namespace ArgData
{
    /// <summary>
    /// Edit horsepower values for the player.
    /// </summary>
    public class PlayerHorsepowerEditor
    {
        private readonly GpExeFile _exeFile;

        /// <summary>
        /// Initializes a new instance of a PlayerHorsepowerEditor.
        /// </summary>
        /// <param name="exeFile">GpExeFile to edit.</param>
        public PlayerHorsepowerEditor(GpExeFile exeFile)
        {
            _exeFile = exeFile;
        }

        /// <summary>
        /// Reads the horsepower value for the player.
        /// </summary>
        /// <returns>Player horsepower value.</returns>
        public int ReadPlayerHorsepower()
        {
            var fileReader = new FileReader(_exeFile.ExePath);
            ushort rawHorsepower = fileReader.ReadUShort(_exeFile.GetPlayerHorsepowerPosition());

            return (rawHorsepower - 632) / 22;
        }

        /// <summary>
        /// Writes the horsepower value for the player. The default value is 716.
        /// </summary>
        /// <param name="horsepower">Player horsepower value.</param>
        public void WritePlayerHorsepower(int horsepower)
        {
            ushort rawHorsepower = (ushort)((horsepower * 22) + 632);

            new FileWriter(_exeFile.ExePath).WriteUInt16(rawHorsepower, _exeFile.GetPlayerHorsepowerPosition());
        }
    }
}
