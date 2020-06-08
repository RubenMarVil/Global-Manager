using Assets.src.domain;
using NRules.Fluent.Dsl;

namespace Assets.src.rules
{
    [Name("AdvancedUsersRule")]
    [Tag("UserLevel")]
    public class AdvancedUserRule : Rule
    {
        public override void Define()
        {
            User user = null;

            When()
                .Match<User>(() => user, u => u.CulturalKnowledge == KnowledgeLevels.HIGH, 
                u => u.LanguageKnowledge == KnowledgeLevels.HIGH, u => u.timeKnowledge == KnowledgeLevels.HIGH);

            Then()
                .Do(ctx => AddAdvancedLevel(user));
        }

        private static void AddAdvancedLevel(User user)
        {
            user.UserLevel = UserLevels.ADVANCED;
        }
    }
}
