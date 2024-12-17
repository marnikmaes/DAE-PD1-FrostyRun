using System;
using System.IO;

namespace FrostyRun.InterfaceElements
{
    public class HighScoreManager
    {
        private string _filePath;

        public int HighScore { get; private set; }

        public HighScoreManager(string filePath)
        {
            _filePath = filePath;
            HighScore = 0; // Default high score
            LoadHighScore(); // Load the high score from the file on initialization
        }

        // Load the high score from the file
        public void LoadHighScore()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string score = File.ReadAllText(_filePath);
                    HighScore = int.Parse(score); // Parse the high score as an integer
                }
                catch (Exception)
                {
                    HighScore = 0; // If there's an error reading the file, reset the high score
                }
            }
            else
            {
                HighScore = 0; // No high score file found, start with 0
            }
        }

        // Save the current high score to the file
        public void SaveHighScore(int currentScore)
        {
            if (currentScore > HighScore)
            {
                HighScore = currentScore; // Update the high score if the current score is higher
                File.WriteAllText(_filePath, HighScore.ToString()); // Write the high score to the file
            }
        }
    }
}
