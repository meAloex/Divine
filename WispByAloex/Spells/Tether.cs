using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

namespace Wisp.Spells
{
    internal class Tether
    {
        private readonly MainMenu MainMenu;
        public Ability Ability { get; }
        public Hero Owner { get; }
        public Tether(Ability ability, MainMenu mainMenu)
        {
            Ability = ability;
            MainMenu = mainMenu;
            Owner = Ability.Owner as Hero;
        }

        public bool CanBeCasted(Hero friendTarget)
{
            if (!Ability.IsValid)
            {
                return false;
            }

            if (MainMenu.WispAbilityToggler?.GetValue(Ability.Id) == false)
            {
                return false;
            }

            if (Ability.ManaCost > Owner.Mana)
            {
                return false;
            }

            if (Ability.Cooldown != 0)
            {
                return false;
            }

            if (Owner.IsStunned() && Owner.IsHexed() && Owner.IsMuted() && Owner.IsSilenced())
            {
                return false;
            }

            if (friendTarget.IsInvulnerable())
            {
                return false;
            }

            return true;
        }

        public bool UseAbility(Hero friendTarget, TetherBrake tetherBrake)
        {
            if (Owner.IsInRange(friendTarget, Ability.CastRange))
            {
                if (friendTarget.HasModifier("modifier_wisp_tether_haste") && Owner.HasModifier("modifier_wisp_tether"))
                {
                    return false;
                }
                else if (!friendTarget.HasModifier("modifier_wisp_tether_haste") && Owner.HasModifier("modifier_wisp_tether"))
                {
                    if (tetherBrake.UseAbility())
                    {
                        Ability.Cast(friendTarget);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Ability.Cast(friendTarget);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}