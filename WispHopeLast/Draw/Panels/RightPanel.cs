using Divine.Input;
using Wisp.Info;
using Wisp;
using Divine.Input.EventArgs;
using Divine.Numerics;
using Divine.Renderer;

namespace WispHopeLast.Draw.Panels
{
    internal class RightPanel
    {
        private Init Init;
        private MainMenu MainMenu;
        private MainSettings MainSettings;

        public RectangleF MainPanelRightRect { get; private set; }
        public RectangleF ButtonOnRightBind { get; private set; }
        public RectangleF OnRightBindText { get; private set; }
        public RectangleF OnRightHealText { get; private set; }
        public RectangleF PanelSliderStopHeal { get; private set; }
        public RectangleF SliderLimitHPShowHPPos { get; private set; }
        public RectangleF TextLimitHPShowHPPos { get; private set; }
        public RectangleF TextAutoSafeMinHP { get; private set; }
        public RectangleF TextAutoSafeMaxHP3Num { get; private set; }
        public RectangleF TextAutoSafeMaxHP4Num { get; private set; }
        public int OffForAllRender { get; private set; }
        public bool MousePressedOnSlider { get; private set; }

        public RightPanel(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;

            RectsRightPanel();
            SliderRectsRightPanel();
            InputManager.MouseKeyDown += InputManager_MouseKeyDown;
            InputManager.MouseMove += InputManager_MouseMove;
            InputManager.MouseKeyUp += InputManager_MouseKeyUp;
        }

        public void RectsRightPanel()
        {
            MainPanelRightRect = new RectangleF(
                MainSettings.UIXPos + 381 * MainSettings.Scaling,
                MainSettings.UIYPos,
                MainSettings.MainPanelWidth, MainSettings.MainPanelHeight);

            //Отрисовка кнопок на правой панели
            ButtonOnRightBind = new RectangleF(
                MainSettings.UIXPos + 431 * MainSettings.Scaling,
                MainSettings.UIYPos + 19 * MainSettings.Scaling, 
                157 * MainSettings.Scaling, 34 * MainSettings.Scaling);
            OnRightBindText = new RectangleF(
                MainSettings.UIXPos + 452 * MainSettings.Scaling,
                MainSettings.UIYPos + 21 * MainSettings.Scaling, 
                115 * MainSettings.Scaling, 24 * MainSettings.Scaling);
            OnRightHealText = new RectangleF(
                MainSettings.UIXPos + 595 * MainSettings.Scaling,
                MainSettings.UIYPos + 21 * MainSettings.Scaling, 
                175 * MainSettings.Scaling, 24 * MainSettings.Scaling);
        }

        public void SliderRectsRightPanel()
        {
            //Слайдер
            PanelSliderStopHeal = new RectangleF(
                MainSettings.UIXPos + 431 * MainSettings.Scaling,
                MainSettings.UIYPos + (112 + OffForAllRender) * MainSettings.Scaling, 
                329 * MainSettings.Scaling, 23 * MainSettings.Scaling);

            //Минимальное значение хп
            TextAutoSafeMinHP = new RectangleF(
                MainSettings.UIXPos + 432 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                8 * MainSettings.Scaling, 20 * MainSettings.Scaling);
            //Максимальное значение хп
            TextAutoSafeMaxHP3Num = new RectangleF(
                MainSettings.UIXPos + 735 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                35 * MainSettings.Scaling, 20 * MainSettings.Scaling);
            TextAutoSafeMaxHP4Num = new RectangleF(
                MainSettings.UIXPos + 727 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                35 * MainSettings.Scaling, 20 * MainSettings.Scaling);
            //Текущее хп
            TextLimitHPShowHPPos = new RectangleF(
                MainSettings.UIXPos + 583 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                34 * MainSettings.Scaling, 19 * MainSettings.Scaling);
        }

        internal void DrawRightPanel()
        {
            RectsRightPanel();
            //Отрисовка правого прямоугольника
            RendererManager.DrawFilledRoundedRectangle(
            MainPanelRightRect,
            MainSettings.RightPanelColure,
            MainSettings.Round20);

            //Отрисовка кнопок и текста
            RendererManager.DrawFilledRoundedRectangle(
            ButtonOnRightBind,
            MainSettings.ButtonCloure,
            MainSettings.Round20);

            RendererManager.DrawText("Press to bind ",
            OnRightBindText,
            MainSettings.OrangeColureText,
            "Neometric",
            MainSettings.TextFontSize19);

            RendererManager.DrawText("Heal friend HP until ",
            OnRightHealText,
            MainSettings.WhiteColure,
            "Neometric",
            MainSettings.TextFontSize19);
        }

        internal void DrawHeroesInfOnRightPanel()
        {
            OffForAllRender = 0;
            foreach (var HeroesID in Init.DicUIInfo)
            {
                SliderRectsRightPanel();
                SliderLimitHPShowHPPos = new RectangleF(
                    MainSettings.UIXPos + ((431 * MainSettings.Scaling) + MathF.Max(MathF.Min(HeroesID.Value.LimitHp / HeroesID.Value.EntInfo.MaximumHealth * (329 * MainSettings.Scaling), 325 * MainSettings.Scaling), 1) - (4 * MainSettings.Scaling)),
                    MainSettings.UIYPos + (107 + OffForAllRender) * MainSettings.Scaling,
                    8 * MainSettings.Scaling, 32 * MainSettings.Scaling);

                var LimitHPCheck = HeroesID.Value.LimitHp / HeroesID.Value.EntInfo.MaximumHealth;
                if (LimitHPCheck < 0.37)
                {
                    HeroesID.Value.LimitHp = MathF.Ceiling(HeroesID.Value.EntInfo.MaximumHealth * 0.75f);
                }

                RendererManager.DrawImage("Wisp.healSliderStopHeal.png",
                PanelSliderStopHeal);

                //Отображение минимального хп
                RendererManager.DrawText("0",
                TextAutoSafeMinHP,
                MainSettings.WhiteColure,
                "Neometric",
                15 * MainSettings.Scaling);

                //Отображение максимального хп
                if (HeroesID.Value.EntInfo.MaximumHealth >= 1000)
                {
                    RendererManager.DrawText($"{HeroesID.Value.EntInfo.MaximumHealth}",
                    TextAutoSafeMaxHP4Num,
                    MainSettings.WhiteColure,
                    "Neometric",
                    15 * MainSettings.Scaling);
                }
                else
                {
                    RendererManager.DrawText($"{HeroesID.Value.EntInfo.MaximumHealth}",
                    TextAutoSafeMaxHP3Num,
                    MainSettings.WhiteColure,
                    "Neometric",
                    15 * MainSettings.Scaling);
                }

                //Отображение ползунка на слайдере и вывод текста значения до которого будет хилять скрипт
                RendererManager.DrawFilledRoundedRectangle(
                SliderLimitHPShowHPPos,
                MainSettings.HPColureSlider,
                MainSettings.ButtonRound5);

                RendererManager.DrawText($"{HeroesID.Value.LimitHp}",
                TextLimitHPShowHPPos,
                MainSettings.WhiteColure,
                "Neometric",
                FontWeight.Bold,
                FontFlags.Left,
                15 * MainSettings.Scaling);

                OffForAllRender += 62;
            }
        }

        private void InputManager_MouseKeyDown(MouseEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value)
            {
                return;
            }

            //Проверка на то что на слайдере была нажата левая кнопка и перемещаем значение хп
            OffForAllRender = 0;
            foreach (var HeroesClick in Init.DicUIInfo)
            {
                SliderRectsRightPanel();
                if (e.MouseKey == MouseKey.Left
                && PanelSliderStopHeal.Contains(e.Position))
                {
                    var mousePos = e.Position;
                    mousePos.X -= (MainSettings.UIXPos + (431 * MainSettings.Scaling));
                    mousePos.Y -= (MainSettings.UIYPos + (107 * MainSettings.Scaling));
                    HeroesClick.Value.LimitHp = MathF.Ceiling(mousePos.X / (329 * MainSettings.Scaling) * HeroesClick.Value.EntInfo.MaximumHealth);

                    MousePressedOnSlider = true;
                }

                OffForAllRender += 62;
            }
        }

        private void InputManager_MouseMove(MouseMoveEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value)
            {
                return;
            }

            //Изменеие позиции слайдера и хп при котором нужно спасать союзника движением мыши
            OffForAllRender = 0;
            foreach (var HeroesClick in Init.DicUIInfo)
            {
                SliderRectsRightPanel();
                if (MousePressedOnSlider
                && PanelSliderStopHeal.Contains(e.Position))
                {
                    var mousePos = e.Position;
                    mousePos.X -= (MainSettings.UIXPos + (431 * MainSettings.Scaling));
                    mousePos.Y -= (MainSettings.UIYPos + (107 * MainSettings.Scaling));
                    HeroesClick.Value.LimitHp = MathF.Ceiling(mousePos.X / (329 * MainSettings.Scaling) * HeroesClick.Value.EntInfo.MaximumHealth);
                }
                OffForAllRender += 62;
            }
        }

        private void InputManager_MouseKeyUp(MouseEventArgs e)
        {
            if (e.MouseKey == MouseKey.Left)
            {
                MousePressedOnSlider = false;
            }
        }

        public void Dispose()
        {
            InputManager.MouseKeyDown -= InputManager_MouseKeyDown;
            InputManager.MouseMove -= InputManager_MouseMove;
            InputManager.MouseKeyUp -= InputManager_MouseKeyUp;
        }
    }
}
