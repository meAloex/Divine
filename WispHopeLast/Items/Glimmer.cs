using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

using Wisp.Info;

using WispHopeLast.Items;

namespace Wisp.Items
{
    internal class Glimmer : ItemsChecker
    {
        public Glimmer(Ability baseAbility, MainMenu mainMenu, Init init) : base(baseAbility, mainMenu, init)
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

            if (!friendTarget.HasModifier("modifier_item_glimmer_cape_fade")
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
