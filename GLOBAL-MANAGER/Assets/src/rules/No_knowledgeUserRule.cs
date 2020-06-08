using Assets.src.domain;
using NRules.Fluent.Dsl;

namespace Assets.src.rules
{
    [Name("No_KnowledgeUsersRule")]
    [Tag("UserLevel")]
    public class No_KnowledgeUserRule : Rule
    {
        public override void Define()
        {
            User user = null;

            When()
                .Match<User>(() => user, u => u.CulturalKnowledge == KnowledgeLevels.LOW,
                u => u.LanguageKnowledge == KnowledgeLevels.LOW, u => u.timeKnowledge == KnowledgeLevels.LOW);

            Then()
                .Do(ctx => AddNo_KnowledgeLevel(user));
        }

        private static void AddNo_KnowledgeLevel(User user)
        {
            user.UserLevel = UserLevels.NO_KNOWLEDGE;
        }
    }
}
