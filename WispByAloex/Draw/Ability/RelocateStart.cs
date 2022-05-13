using Divine.Entity.Entities.Abilities.Components;
using Divine.Extensions;
using Divine.Game;
using Divine.Input;
using Divine.Numerics;
using Divine.Particle;
using Divine.Renderer;

using Wisp;
using Wisp.Info;

namespace WispByAloex.Draw.Ability
{
    internal class RelocateStart
    {
        private Init Init;
        private MainMenu MainMenu;
        private MainSettings MainSettings;

        public Divine.Entity.Entities.Abilities.Ability ReloAbil { get; private set; }
        public RectangleF RelocateText { get; private set; }
        public float RelocateTime { get; private set; }
        public RectangleF RelocateTextMouse { get; private set; }

        public RelocateStart(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;
            ReloAbil = Init.MyEntity.GetAbilityById(AbilityId.wisp_relocate);
 
            ParticleManager.ParticleAdded += ParticleManager_ParticleAdded;
        }

        public void ReloRects()
        {
            var pos = Init.MyEntity.Position;
            var worldscreen = RendererManager.WorldToScreen(pos, true);

            if (!worldscreen.IsZero)
            {
                var HBO = Init.MyEntity.HealthBarOffset;
                var z = pos.Z + HBO;

                var origin = new Vector3(pos.X, pos.Y, z);
                var final = RendererManager.WorldToScreen(origin, true);

                final *= MainSettings.Scaling;

                RelocateText = new RectangleF(
                final.X + 55 * MainSettings.Scaling,
                final.Y,
                135 * MainSettings.Scaling, 30 * MainSettings.Scaling);
            }

            var mousePos = InputManager.MousePosition;

            mousePos *= MainSettings.Scaling;

            RelocateTextMouse = new RectangleF(
                mousePos.X + 55 * MainSettings.Scaling,
                mousePos.Y,
                135 * MainSettings.Scaling, 30 * MainSettings.Scaling);
        }
        
        public void DrawReloInfo()
        {
            if (Init.MyEntity.HasModifier("modifier_teleporting"))
            {
                ReloRects();
                if (MainMenu.WispShowRelocateStart.Value == "Text")
                {
                    RendererManager.DrawText($"Relocate: {MathF.Round(RelocateTime - GameManager.GameTime, 2)}",
                    RelocateText,
                    MainSettings.WhiteColure,
                    "Neometric",
                    FontWeight.ExtraBold,
                    FontFlags.Left,
                    18 * MainSettings.Scaling);
                }
                else if (MainMenu.WispShowRelocateStart.Value == "Text and Cursor")
                {
                    RendererManager.DrawText($"Relocate: {MathF.Round(RelocateTime - GameManager.GameTime, 2)}",
                    RelocateTextMouse,
                    MainSettings.WhiteColure,
                    "Neometric",
                    FontWeight.ExtraBold,
                    FontFlags.Left,
                    18 * MainSettings.Scaling);

                    RendererManager.DrawText($"Relocate: {MathF.Round(RelocateTime - GameManager.GameTime, 2)}",
                    RelocateText,
                    MainSettings.WhiteColure,
                    "Neometric",
                    FontWeight.ExtraBold,
                    FontFlags.Left,
                    18 * MainSettings.Scaling);
                }
            }
        }

        private void ParticleManager_ParticleAdded(Divine.Particle.EventArgs.ParticleAddedEventArgs e)
        {
            if (e.Particle.Name == "particles/units/heroes/hero_wisp/wisp_relocate_marker_endpoint.vpcf")
            {
                RelocateTime = GameManager.GameTime + ReloAbil.GetAbilitySpecialData("cast_delay");
            }
        }

        internal void Dispose()
        {
            ParticleManager.ParticleAdded -= ParticleManager_ParticleAdded;
        }
    }
}
