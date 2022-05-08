using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

using Wisp.Info;

using WispHopeLast.Items;

namespace Wisp.Items
{
    internal class Crimson : ItemsChecker
    {
        public Crimson(Ability baseAbility, MainMenu mainMenu, Init init) : base(baseAbility, mainMenu, init)
        {

        }

        public override bool CanBeCasted(Hero friendTarget)
        {
            if (!Base.IsValid)
            {
                return false;
            }

            if (MainMenu.WispUseItemInRange.Value)
            {
                if (!Owner.IsInRange(friendTarget, 1185))
                {
                    return false;
                }
            }

            return base.CanBeCasted(friendTarget);
        }

        public override bool UseAbilityNoTarget(Hero friendTarget)
        {
            if (!friendTarget.HasModifier("modifier_item_crimson_guard_nostack"))
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
