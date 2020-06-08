namespace Assets.src.domain
{
    enum UserLevels
    {
        NO_KNOWLEDGE,
        BASIC,
        INTERMEDIATE,
        ADVANCED
    }

    enum KnowledgeLevels
    {
        LOW,
        HIGH
    }

    class User
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public UserLevels UserLevel { get; set; }
        public KnowledgeLevels CulturalKnowledge { get; set; }
        public KnowledgeLevels LanguageKnowledge { get; set; }
        public KnowledgeLevels timeKnowledge { get; set; }

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
