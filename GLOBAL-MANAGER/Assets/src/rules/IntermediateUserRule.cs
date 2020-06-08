using Assets.src.domain;
using NRules.Fluent.Dsl;

namespace Assets.src.rules
{
    [Name("IntermediateUsersRule")]
    [Tag("UserLevel")]
    public class IntermediateUserRule : Rule
    {
        public override void Define()
        {
            User user = null;

            When()
                .Or(x => x
                    .Match<User>(() => user, u => u.CulturalKnowledge == KnowledgeLevels.HIGH,
                                             u => u.LanguageKnowledge == KnowledgeLevels.HIGH,
                                             u => u.timeKnowledge == KnowledgeLevels.LOW)
                    .Match<User>(() => user, u => u.CulturalKnowledge == KnowledgeLevels.HIGH,
                                             u => u.LanguageKnowledge == KnowledgeLevels.LOW,
                                             u => u.timeKnowledge == KnowledgeLevels.HIGH)
                    .Match<User>(() => user, u => u.CulturalKnowledge == KnowledgeLevels.LOW,
                                             u => u.LanguageKnowledge == KnowledgeLevels.HIGH,
                                             u => u.timeKnowledge == KnowledgeLevels.HIGH));

            Then()
                .Do(ctx => AddIntermediateLevel(user));
        }

        private static void AddIntermediateLevel(User user)
        {
            user.UserLevel = UserLevels.INTERMEDIATE;
        }
    }
}
