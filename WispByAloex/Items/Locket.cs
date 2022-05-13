using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

using Wisp.Info;

using WispByAloex.Items;

namespace Wisp.Items
{
    internal class Locket : ItemsChecker
    {

        public Locket(Ability baseAbility, MainMenu mainMenu, Init init) : base(baseAbility, mainMenu, init)
        {

        }

        public override bool CanBeCasted(Hero friendTarget)
        {
            if (!Base.IsValid)
            {
                return false;
            }

            if (Base.CurrentCharges == 0)
            {
                return false;
            }

            return base.CanBeCasted(friendTarget);
        }

        public override bool UseAbilityOnOwner(Hero friendTarget)
        {
            if (!Owner.HasModifier("modifier_ice_blast")
                && !friendTarget.HasModifier("modifier_ice_blast"))
            {
                return base.UseAbilityOnOwner(friendTarget);
            }
            else
            {
                return false;
            }
        }

        public override bool UseAbilityOnFriendTarget(Hero friendTarget)
        {
            if (Owner.HasModifier("modifier_ice_blast")
                && !friendTarget.HasModifier("modifier_ice_blast")
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
