using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;

namespace Wisp.Spells
{
    internal class TetherBrake
    {
        public Ability Ability { get; }
        public Hero Owner { get; }
        public TetherBrake(Ability ability)
        {
            Ability = ability;
            Owner = Ability.Owner as Hero;
        }

        public bool CanBeCasted()
        {
            if (!Ability.IsValid)
            {
                return false;
            }

            if (!Ability.IsHidden)
            {
                return false;
            }

            if (Owner.IsStunned() && Owner.IsHexed() && Owner.IsMuted() && Owner.IsSilenced())
            {
                return false;
            }

            return true;
        }
        public bool UseAbility()
        {
            Ability.Cast();
            return true;
        }
    }
}