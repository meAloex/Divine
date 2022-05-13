using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

using Wisp.Info;

using WispByAloex.Items;

namespace Wisp.Spells
{
    internal class Overcharge : ItemsChecker
    {

        public Overcharge(Ability baseAbility, MainMenu mainMenu, Init init) : base(baseAbility, mainMenu, init)
        {

        }

        public override bool CanBeCasted(Hero friendTarget)
        {
            return base.CanBeCasted(friendTarget);
        }

        public override bool UseAbilityNoTarget(Hero friendTarget)
        {
            if (!Owner.HasModifier("modifier_ice_blast"))
            {
                return base.UseAbilityNoTarget(friendTarget);
            }
            else
            {
                return false;
            }
        }
    }
}
