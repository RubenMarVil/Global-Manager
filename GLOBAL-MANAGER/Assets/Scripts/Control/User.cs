using UnityEngine;

namespace Assets.src.domain
{
    public enum UserLevels
    {
        BASIC,
        INTERMEDIATE,
        ADVANCED,
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
        public KnowledgeLevels Colocalized_Global { get; set; }
        public KnowledgeLevels CulturalKnowledge { get; set; }
        public KnowledgeLevels LanguageKnowledge { get; set; }
        public KnowledgeLevels TimeKnowledge { get; set; }
        public AnswersToQuestions SiteQuestion { get; set; }
        public AnswersToQuestions FollowTheSunQuestion { get; set; }
        public AnswersToQuestions OffshoringQuestion { get; set; }
        public AnswersToQuestions OutsourcingQuestion { get; set; }

        private Level BasicLevel;
        private Level IntermediateLevel;
        private Level AdvancedLevel;

        public User(string name, int age)
        {
            Name = name;
            Age = age;
            Score = 0;

            BasicLevel.min = 0;         BasicLevel.max = 6;
            IntermediateLevel.min = 6;  IntermediateLevel.max = 15;
            AdvancedLevel.min = 15;     AdvancedLevel.max = 19;
        }

        public User() { }

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
            if(BasicLevel.min <= Score && Score <= BasicLevel.max)
            {
                UserLevel = UserLevels.BASIC;
            }
            else if(IntermediateLevel.min < Score && Score <= IntermediateLevel.max)
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
    }
}
