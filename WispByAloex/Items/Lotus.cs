using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

using Wisp.Info;

using WispByAloex.Items;

namespace Wisp.Items
{
    internal class Lotus : ItemsChecker
    {
        public Lotus(Ability baseAbility, MainMenu mainMenu, Init init) : base(baseAbility, mainMenu, init)
        {

        }

        public override bool CanBeCasted(Hero friendTarget)
        {
            return base.CanBeCasted(friendTarget);
        }

        public override bool UseAbilityOnFriendTarget(Hero friendTarget)
        {
            if (!Base.IsValid)
            {
                return false;
            }

            if (!friendTarget.HasModifier("modifier_item_lotus_orb_active")
                && Owner.IsInRange(friendTarget, Base.CastRange))
            {
                return base.UseAbilityOnFriendTarget(friendTarget);
            }
            else
            {
                return false;
            }
        }
    }
}
