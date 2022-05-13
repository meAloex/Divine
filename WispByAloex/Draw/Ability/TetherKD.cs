using Wisp.Info;
using Wisp;
using Divine.Renderer;
using Divine.Numerics;
using Divine.Helpers;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Extensions;

namespace WispByAloex.Draw.Ability
{
    internal class TetherKD
    {
        private Init Init;
        private MainMenu MainMenu;
        private MainSettings MainSettings;

        public Divine.Entity.Entities.Abilities.Ability SpiritsAbil { get; }
        public Vector2 ScreenSize { get; }
        public RectangleF RelocateImageDK { get; private set; }
        public RectangleF RelocateTextNumDK { get; private set; }
        public RectangleF RelocateTextDK { get; private set; }

        public TetherKD(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;
            SpiritsAbil = Init.MyEntity.GetAbilityById(AbilityId.wisp_spirits_in);

            ScreenSize = RendererManager.ScreenSize;
            
        }

        public void TetherRects()
        {
            var startXTKd = (ScreenSize.X / 2) - (!SpiritsAbil.IsHidden ? (205 * MainSettings.Scaling) : (155 * MainSettings.Scaling));
            var startYTKd = (ScreenSize.Y / 2) + 340 * MainSettings.Scaling;

            RelocateImageDK = new RectangleF(
                startXTKd,
                startYTKd,
                52 * MainSettings.Scaling, 52 * MainSettings.Scaling);

            RelocateTextNumDK = new RectangleF(
                startXTKd + 19 * MainSettings.Scaling,
                startYTKd + 13 * MainSettings.Scaling,
                25 * MainSettings.Scaling, 30 * MainSettings.Scaling);

            RelocateTextDK = new RectangleF(
                startXTKd - 25 * MainSettings.Scaling,
                startYTKd + 25 * MainSettings.Scaling,
                150 * MainSettings.Scaling, 30 * MainSettings.Scaling);
        }

        public void DrawTether()
        {
            if (Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).Cooldown == 0)
            {
                return;
            }

            if (!MultiSleeper<string>.Sleeping("TetherRectUpdate"))
            {
                TetherRects();
                MultiSleeper<string>.Sleep("TetherRectUpdate", 1000);
            }

            if (MainMenu.WispShowTetherKD.Value == "Text")
            {
                RendererManager.DrawText($"Tether KD: {MathF.Round(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).Cooldown)}",
                    RelocateTextDK,
                    MainSettings.WhiteColure,
                    "Neometric",
                    FontWeight.Bold,
                    FontFlags.Left,
                    18 * MainSettings.Scaling);
            }
            else if (MainMenu.WispShowTetherKD.Value == "Image")
            {
                //Отображение иконоки тизера
                RendererManager.DrawImage(AbilityId.wisp_tether,
                    RelocateImageDK,
                    AbilityImageType.Default,
                    true,
                    0.5f);

                RendererManager.DrawText($"{MathF.Round(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).Cooldown)}",
                    RelocateTextNumDK,
                    MainSettings.WhiteColure,
                    "Neometric",
                    FontWeight.Bold,
                    FontFlags.Left,
                    20 * MainSettings.Scaling);
            }
        }
    }
}
