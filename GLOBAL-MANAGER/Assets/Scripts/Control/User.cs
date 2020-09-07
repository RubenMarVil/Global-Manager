using UnityEngine;

namespace Assets.Scripts.Control
{
    public enum UserLevels
    {
        BASIC = 10,
        INTERMEDIATE = 15,
        ADVANCED = 20,
        NO_LEVEL
    }

    public enum KnowledgeLevels
    {
        LOW,
        HIGH
    }

    public enum AnswersToQuestions
    {
        RIGHT,
        ALMOST_RIGHT,
        NOT_RIGHT
    }

    public struct Level
    {
        public int min;
        public int max;
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public UserLevels UserLevel { get; set; }
        public int Score { get; set; }
        public int NumProjects { get; set; }
        public bool IsMan { get; set; }
        public KnowledgeLevels Colocalized_Global { get; set; }
        public KnowledgeLevels CulturalKnowledge { get; set; }
        public KnowledgeLevels LanguageKnowledge { get; set; }
        public KnowledgeLevels TimeKnowledge { get; set; }
        public AnswersToQuestions SiteQuestion { get; set; }
        public AnswersToQuestions FollowTheSunQuestion { get; set; }
        public AnswersToQuestions OffshoringQuestion { get; set; }
        public AnswersToQuestions OutsourcingQuestion { get; set; }

        private Level BasicLevel = new Level() { min = 0, max = 6 };
        private Level IntermediateLevel = new Level() { min = 6, max = 15 };
        private Level AdvancedLevel = new Level() { min = 15, max = 19 };

        private int[,] BasicUpdateLevel = { {-2, -1, +1},
                                            {-1,  0, +1},
                                            { 0, +1, +2} };
        private int[,] IntermediateUpdateLevel = { {-4, -3,  0},
                                                   {-2,  0, +2},
                                                   {+1, +2, +3} };
        private int[,] AdvancedUpdateLevel = { {-3, -2, -1},
                                               {-2,  0, +1},
                                               {-1, +1, +2} };

        public User(string name, int age)
        {
            Name = name;
            Age = age;
            Score = 0;
            NumProjects = 0;
        }

        public User() { }

        public void SetSex(string sex)
        {
            if(sex == "Man")
            {
                IsMan = true;
            }
            else if(sex == "Woman")
            {
                IsMan = false;
            }
        }

        public void SetUserLevel(string userLevel)
        {
            switch(userLevel)
            {
                case "BASIC": UserLevel = UserLevels.BASIC; break;
                case "INTERMEDIATE": UserLevel = UserLevels.INTERMEDIATE; break;
                case "ADVANCED": UserLevel = UserLevels.ADVANCED; break;
            }
        }

        public void CalculateScore()
        {
            if(Colocalized_Global.Equals(KnowledgeLevels.HIGH)) { Score += 2; }

            if(CulturalKnowledge.Equals(KnowledgeLevels.HIGH)) { Score += 3; }

            if(LanguageKnowledge.Equals(KnowledgeLevels.HIGH)) { Score += 3; }

            if(TimeKnowledge.Equals(KnowledgeLevels.HIGH)) { Score += 3; }

            if(SiteQuestion.Equals(AnswersToQuestions.RIGHT)) { Score += 2; }
            else if(SiteQuestion.Equals(AnswersToQuestions.ALMOST_RIGHT)) { Score += 1; }

            if (FollowTheSunQuestion.Equals(AnswersToQuestions.RIGHT)) { Score += 2; }
            else if (FollowTheSunQuestion.Equals(AnswersToQuestions.ALMOST_RIGHT)) { Score += 1; }

            if (OffshoringQuestion.Equals(AnswersToQuestions.RIGHT)) { Score += 2; }
            else if (OffshoringQuestion.Equals(AnswersToQuestions.ALMOST_RIGHT)) { Score += 1; }

            if (OutsourcingQuestion.Equals(AnswersToQuestions.RIGHT)) { Score += 2; }
            else if (OutsourcingQuestion.Equals(AnswersToQuestions.ALMOST_RIGHT)) { Score += 1; }

            CalculateLevelUser();
        }

        private void CalculateLevelUser()
        {
            if(BasicLevel.min <= Score && Score < BasicLevel.max)
            {
                UserLevel = UserLevels.BASIC;
            }
            else if(IntermediateLevel.min <= Score && Score <= IntermediateLevel.max)
            {
                UserLevel = UserLevels.INTERMEDIATE;
            }
            else if(AdvancedLevel.min < Score && Score <= AdvancedLevel.max)
            {
                UserLevel = UserLevels.ADVANCED;
            }
            else
            {
                UserLevel = UserLevels.NO_LEVEL;
            }
        }

        public int UpdateScoreLevel(int performance, int resilience)
        {
            int change = 0;

            switch(UserLevel)
            {
                case UserLevels.BASIC: 
                    change = BasicUpdateLevel[resilience, performance]; 
                    break;
                case UserLevels.INTERMEDIATE: 
                    change = IntermediateUpdateLevel[resilience, performance]; 
                    break;
                case UserLevels.ADVANCED: 
                    change = AdvancedUpdateLevel[resilience, performance]; 
                    break;
            }

            Score += change;

            if(Score < BasicLevel.min)
            {
                Score = BasicLevel.min;
            }
            else if(Score > AdvancedLevel.max)
            {
                Score = AdvancedLevel.max;
            }

            NumProjects++;

            CalculateLevelUser();

            return change;
        }
    }
}
