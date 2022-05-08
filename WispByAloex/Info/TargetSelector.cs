using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Game;
using Divine.Menu.EventArgs;
using Divine.Menu.Items;
using Divine.Particle;
using Divine.Update;
using Divine.Numerics;

namespace Wisp.Info
{
    internal class TargetSelector
    {
        public static Hero CurrentTarget { get; set; }
        public MainMenu MainMenu { get; }
        public Init Init { get; }
        public TargetSelector(Init init, MainMenu mainMenu)
        {
            Init = init;
            MainMenu = mainMenu;
            MainMenu.TargetIndicator.ValueChanged += TargetIndicator_ValueChanged;
        }

        private void TargetIndicator_ValueChanged(MenuSelector selector, SelectorEventArgs e)
        {
            if (e.NewValue == "Friend Team")
            {
                UpdateManager.CreateIngameUpdate(25, TargetUpdater);
            }
            else
            {
                CurrentTarget = null;
                ParticleManager.DestroyParticle("TargetParticle");
            }
        }

        private Hero GetNearestToMouse()
        {
            var mousePos = GameManager.MousePosition;

            Hero friendtarget = EntityManager.GetEntities<Hero>()
                .Where(x => x.Distance2D(mousePos) < MainMenu.TargetSlider
                        && x.IsAlive
                        && (!x.IsIllusion || x.HasModifier("modifier_morphling_replicate"))
                        && !x.IsEnemy(Init.MyEntity)
                        && (x.Name != "npc_dota_hero_wisp")
                        && x.IsVisible)
                .OrderBy(x => x.Distance2D(mousePos))
                .FirstOrDefault();

            return friendtarget;
        }

        private void TargetUpdater()
        {
            CurrentTarget = GetNearestToMouse();
            if (CurrentTarget == null)
            {
                ParticleManager.DestroyParticle("TargetParticle");
            }
            else
            {
                ParticleManager.CreateTargetLineParticle("TargetParticle", Init.MyEntity, CurrentTarget.Position, Divine.Numerics.Color.Red);
            }
            
        }

        public void Dispose()
        {
            MainMenu.TargetIndicator.ValueChanged -= TargetIndicator_ValueChanged;
            UpdateManager.DestroyIngameUpdate(TargetUpdater);
            ParticleManager.DestroyParticle("TargetParticle");
        }
    }
}
