using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{
    public enum ProjectDifficultyLevels
    {
        VERY_LOW = 10,
        LOW = 5,
        MEDIUM = 3,
        HIGH = 0,
        VERY_HIGH = -2
    }

    public class GameConfiguration
    {
        public int CodGame { get; set; }
        public string Player { get; set; }
        public int NumSites { get; set; }
        public string ClientCountry { get; set; }
        public string CommonLanguage { get; set; }
        public ProjectDifficultyLevels ProjectDifficulty { get; set; }
        public float InitialBudget { get; set; }
        public float InitialDuration { get; set; }
        public List<SiteConfiguration> SitesList { get; set; }
        public List<CommunicationConfiguration> CommunicationsList { get; set; }
        public List<ProjectCharacteristic> ProjectCharacteristicsList { get; set; }
        public float StressValue { get; set; }
        public float ProgressValue { get; set; }
        public float BudgetValue { get; set; }
        public float DurationValue { get; set; }
        public int TotalNegativeEvents { get; set; }
        public int CorrectNegativeEvents { get; set; }

        public void setProjectDifficulty(string difficulty)
        {
            switch(difficulty)
            {
                case "VERY HIGH":
                    ProjectDifficulty = ProjectDifficultyLevels.VERY_HIGH;
                    break;
                case "HIGH":
                    ProjectDifficulty = ProjectDifficultyLevels.HIGH;
                    break;
                case "MEDIUM":
                    ProjectDifficulty = ProjectDifficultyLevels.MEDIUM;
                    break;
                case "LOW":
                    ProjectDifficulty = ProjectDifficultyLevels.LOW;
                    break;
                case "VERY LOW":
                    ProjectDifficulty = ProjectDifficultyLevels.VERY_LOW;
                    break;
            }
        }
    }
}
