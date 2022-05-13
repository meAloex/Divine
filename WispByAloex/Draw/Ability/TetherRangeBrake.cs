
using Wisp.Info;
using Wisp;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Extensions;
using Divine.Particle;
using Divine.Numerics;

namespace WispByAloex.Draw.Ability
{
    internal class TetherRangeBrake
    {
        private Init Init;
        private MainMenu MainMenu;
        private MainSettings MainSettings;

        public TetherRangeBrake(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;
        }

        public void DrawTetherRangeBrake()
        {
            ParticleManager.CreateRangeParticle("ShowTetherRangeBrake",
                Init.MyEntity,
                1000,
                Color.Red);
        }

        public void Dispose()
        {
            ParticleManager.DestroyParticle("ShowTetherRangeBrake");
        }
    }
}
