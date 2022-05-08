
using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;

using Wisp;
using Wisp.Info;

namespace WispHopeLast.Items
{
    internal abstract class ItemsChecker : FinalAbilItems
    {
        protected ItemsChecker(Ability baseAbility, MainMenu mainMenu, Init init) : base(baseAbility, mainMenu, init)
        {
            
        }
    }
}
