using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Extensions;
namespace WispByAloex.Extensions;

internal static class AbilityExtensions
{
    public static float GetAbilitySpecialDataWithTalent(this Ability ability, string name, AbilityId talent, uint level = 0)
    {
        if (ability.Owner is not Unit unit)
        {
            return 0;
        }

        var special = ability.AbilitySpecialData.First(x => x.Name == name);

        var bonus = 0f;
        if (unit.Spellbook.Spells.Any(x => x.Id == talent && x.Level == 1))
        {
            bonus += special.Bonuses.FirstOrDefault(x => x.Name == talent.ToString())?.Value ?? 0;
        }

        if (level == 0)
        {
            level = ability.Level;
        }

        return special.GetValue(level - 1) + bonus;
    }
}