using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

using Wisp.Info;

using WispHopeLast.Items;

namespace Wisp.Items
{
    internal class Salve : ItemsChecker
    {
        public Salve(Ability baseAbility, MainMenu mainMenu, Init init) : base(baseAbility, mainMenu, init)
        {

        }

        public override bool CanBeCasted(Hero friendTarget)
        {
            return base.CanBeCasted(friendTarget);
        }

        public override bool UseAbilityOnOwner(Hero friendTarget)
        {
            if (!Base.IsValid)
            {
                return false;
            }

            if (!Owner.HasModifier("modifier_ice_blast")
                && !Owner.HasModifier("modifier_flask_healing")
                && !friendTarget.HasModifier("modifier_ice_blast"))
            {
                return base.UseAbilityOnOwner(friendTarget);
            }
            else
            {
                return false;
            }
        }
    }
}
