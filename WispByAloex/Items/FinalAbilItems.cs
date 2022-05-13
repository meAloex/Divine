
using Divine.Entity;
using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Helpers;
using Divine.Menu.Items;

using Wisp;
using Wisp.Info;

namespace WispByAloex.Items
{
    internal abstract class FinalAbilItems
    {
        public Ability Base { get; }
        public Hero Owner { get; }
        public MainMenu MainMenu { get; }
        public Init Init { get; }

        protected FinalAbilItems(Ability baseAbility, MainMenu mainMenu, Init init)
        {
            Base = baseAbility;
            Owner = Base.Owner as Hero;
            MainMenu = mainMenu;
            Init = init;
        }

        public virtual bool UseAbilityNoTarget(Hero friendTarget)
        {
            if (!CanBeCasted(friendTarget))
            {
                return false;
            }
            
            if (!CheckerLimitHP(friendTarget))
            {
                return false;
            }
            
            Base.Cast();
            return true;
        }

        public virtual bool UseAbilityOnOwner(Hero friendTarget)
        {
            if (!CanBeCasted(friendTarget))
            {
                return false;
            }

            if (!CheckerLimitHP(friendTarget))
            {
                return false;
            }

            Base.Cast(Owner);
            return true;
        }

        public virtual bool UseAbilityOnFriendTarget(Hero friendTarget)
        {
            if (!CanBeCasted(friendTarget))
            {
                return false;
            }

            if (!CheckerLimitHP(friendTarget))
            {
                return false;
            }

            Base.Cast(friendTarget);
            return true;
        }

        public virtual bool CheckerLimitHP(Hero friendTarget)
        {
            foreach (var HeroesID in Init.DicUIInfo)
            {
                if (HeroesID.Value.EntInfo == friendTarget
                    && HeroesID.Value.EntInfo.IsAlive
                    && friendTarget.HasModifier("modifier_wisp_tether_haste")
                    && HeroesID.Value.LimitHp < HeroesID.Value.EntInfo.Health)
                {
                    return false;
                }
            }

            return true;
        }

        public virtual bool CanBeCasted(Hero friendTarget)
{
            if (!Base.IsValid)
            {
                return false;
            }

            if ($"{Base.Id}" == "wisp_overcharge")
            {
                if (MainMenu.WispAbilityToggler?.GetValue(Base.Id) == false)
                {
                    return false;
                }
            }
            else
            {
                if (MainMenu.WispItemsToggler?.GetValue(Base.Id) == false)
                {
                    return false;
                }
            }

            if (Base.ManaCost > Owner.Mana)
            {
                return false;
            }

            if (Base.Cooldown != 0)
            {
                return false;
            }
            
            if (Owner.IsStunned() && Owner.IsHexed()
                && Owner.IsMuted() && Owner.IsSilenced()
                && Owner.IsInvulnerable())
            {
                return false;
            }

            if (!friendTarget.HasModifier("modifier_wisp_tether_haste"))
            {
                return false;
            }

            return true;
        }
    }
}
