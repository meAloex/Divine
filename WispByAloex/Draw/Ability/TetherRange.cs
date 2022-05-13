using Wisp.Info;
using Wisp;
using Divine.Particle;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Extensions;

using Divine.Numerics;

namespace WispByAloex.Draw.Ability
{
    internal class TetherRange
    {
        private Init Init;
        private MainMenu MainMenu;
        private MainSettings MainSettings;

        public TetherRange(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;
        }

        public void DrawTetherRange()
        {
            ParticleManager.CreateRangeParticle("ShowTetherRange",
                Init.MyEntity,
                Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).CastRange,
                Color.Snow);
        }

        public void Dispose()
        {
            ParticleManager.DestroyParticle("ShowTetherRange");
        }
    }
}
