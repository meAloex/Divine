
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Input;
using Divine.Input.EventArgs;
using Divine.Numerics;
using Divine.Renderer;

using Wisp;
using Wisp.Info;

namespace WispByAloex.Draw
{
    internal class MainPanel
    {
        private MainMenu MainMenu;
        private MainSettings MainSettings;
        private Init Init;

        public Vector2 ScreenSize { get; private set; }
        public RectangleF MainPanelRect { get; private set; }
        public RectangleF ButtonAutoSafe { get; private set; }
        public RectangleF TextAutoSafe { get; private set; }
        public RectangleF CircleAutoSafe { get; private set; }
        public RectangleF ButtonAutoSafeDNG { get; private set; }
        public RectangleF TextAutoSafeDNG { get; private set; }
        public RectangleF CircleAutoSafeDNG { get; private set; }
        public RectangleF ButtonRightExp { get; private set; }
        public RectangleF ArrowToOpen { get; private set; }
        public RectangleF RightlineToOpen { get; private set; }
        public RectangleF OpenerUp { get; private set; }
        public RectangleF OpenerUpText { get; private set; }
        public RectangleF PanelHeroesImages { get; private set; }
        public RectangleF PanelHeroesRectOnOff { get; private set; }
        public RectangleF PanelTextAutoSafeOff { get; private set; }
        public RectangleF PanelSliderAutoSafe { get; private set; }
        public RectangleF TextAutoSafeMinHP { get; private set; }
        public RectangleF TextAutoSafeMaxHP3Num { get; private set; }
        public RectangleF TextAutoSafeMaxHP4Num { get; private set; }
        public RectangleF SliderAutoSafeShowHPPos { get; private set; }
        public RectangleF TextAutoSafeShowHPPos { get; private set; }
        public int OffForAllRender { get; private set; }
        public int OffForMouse { get; private set; }
        public bool MousePressedOnSlider { get; private set; }

        public MainPanel(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;

            RectsMainPanel();
            SliderRectsMainPanel();
            InputManager.MouseKeyDown += InputManager_MouseKeyDown;
            InputManager.MouseMove += InputManager_MouseMove;
            InputManager.MouseKeyUp += InputManager_MouseKeyUp;
        }

        public void RectsMainPanel()
        {
            MainPanelRect = new RectangleF(
                MainSettings.UIXPos, 
                MainSettings.UIYPos, 
                MainSettings.MainPanelWidth, MainSettings.MainPanelHeight);

            //Автоспасение кнопка
            ButtonAutoSafe = new RectangleF(
                MainSettings.UIXPos + 14 * MainSettings.Scaling, 
                MainSettings.UIYPos + 19 * MainSettings.Scaling, 
                132 * MainSettings.Scaling, 34 * MainSettings.Scaling);
            TextAutoSafe = new RectangleF(
                MainSettings.UIXPos + 24 * MainSettings.Scaling,
                MainSettings.UIYPos + 21 * MainSettings.Scaling, 
                94 * MainSettings.Scaling, 24 * MainSettings.Scaling);
            CircleAutoSafe = new RectangleF(
                MainSettings.UIXPos + 127 * MainSettings.Scaling,
                MainSettings.UIYPos + 31 * MainSettings.Scaling, 
                10 * MainSettings.Scaling, 11 * MainSettings.Scaling);

            //Автоспасение кнопка при опасных способностях 
            ButtonAutoSafeDNG = new RectangleF(
                MainSettings.UIXPos + 160 * MainSettings.Scaling,
                MainSettings.UIYPos + 19 * MainSettings.Scaling, 
                85 * MainSettings.Scaling, 34 * MainSettings.Scaling);
            TextAutoSafeDNG = new RectangleF(
                MainSettings.UIXPos + 170 * MainSettings.Scaling,
                MainSettings.UIYPos + 21 * MainSettings.Scaling, 
                47 * MainSettings.Scaling, 24 * MainSettings.Scaling);
            CircleAutoSafeDNG = new RectangleF(
                MainSettings.UIXPos + 226 * MainSettings.Scaling, 
                MainSettings.UIYPos + 31 * MainSettings.Scaling, 
                10 * MainSettings.Scaling, 11 * MainSettings.Scaling);

            //Кнопка открытия и закрытия справа
            ButtonRightExp = new RectangleF(
                MainSettings.UIXPos + 351 * MainSettings.Scaling,
                MainSettings.UIYPos + 19 * MainSettings.Scaling, 
                52 * MainSettings.Scaling, 34 * MainSettings.Scaling);
            ArrowToOpen = new RectangleF(
                MainSettings.UIXPos + 367 * MainSettings.Scaling,
                MainSettings.UIYPos + 29 * MainSettings.Scaling, 
                26 * MainSettings.Scaling, 15 * MainSettings.Scaling);
            RightlineToOpen = new RectangleF(
                MainSettings.UIXPos + 361 * MainSettings.Scaling,
                MainSettings.UIYPos + 29 * MainSettings.Scaling, 
                3 * MainSettings.Scaling, 16 * MainSettings.Scaling);

            //Кнопка для открытия доп инфо меню
            OpenerUp = new RectangleF(
                MainSettings.UIXPos + 255 * MainSettings.Scaling,
                MainSettings.UIYPos + 10 * MainSettings.Scaling,
                22 * MainSettings.Scaling, 22 * MainSettings.Scaling);

            OpenerUpText = new RectangleF(
                MainSettings.UIXPos + 264.5f * MainSettings.Scaling,
                MainSettings.UIYPos + 9.5f * MainSettings.Scaling,
                20 * MainSettings.Scaling, 20 * MainSettings.Scaling);
        }

        public void SliderRectsMainPanel()
        {
            //Пикчи героев
            PanelHeroesImages = new RectangleF(
                MainSettings.UIXPos + 17,
                MainSettings.UIYPos + (92 + OffForAllRender) * MainSettings.Scaling,
                78 * MainSettings.Scaling, 43 * MainSettings.Scaling);
            //Прямоугольник для героев on/off
            PanelHeroesRectOnOff = new RectangleF(
                MainSettings.UIXPos + 14,
                MainSettings.UIYPos + (92 + OffForAllRender) * MainSettings.Scaling,
                3 * MainSettings.Scaling, 43 * MainSettings.Scaling);
            //Отображение текстом состояния off
            PanelTextAutoSafeOff = new RectangleF(
                MainSettings.UIXPos + 210 * MainSettings.Scaling,
                MainSettings.UIYPos + (105 + OffForAllRender) * MainSettings.Scaling,
                62 * MainSettings.Scaling, 16 * MainSettings.Scaling);
            //Слайдер
            PanelSliderAutoSafe = new RectangleF(
                MainSettings.UIXPos + 111 * MainSettings.Scaling,
                MainSettings.UIYPos + (112 + OffForAllRender) * MainSettings.Scaling,
                292 * MainSettings.Scaling, 23 * MainSettings.Scaling);

            //Минимальное значение хп
            TextAutoSafeMinHP = new RectangleF(
                MainSettings.UIXPos + 112 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                8 * MainSettings.Scaling, 20 * MainSettings.Scaling);
            //Максимальное значение хп
            TextAutoSafeMaxHP3Num = new RectangleF(
                MainSettings.UIXPos + 379 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                35 * MainSettings.Scaling, 20 * MainSettings.Scaling);
            TextAutoSafeMaxHP4Num = new RectangleF(
                MainSettings.UIXPos + 370 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                35 * MainSettings.Scaling, 20 * MainSettings.Scaling);
            //Текущее хп
            TextAutoSafeShowHPPos = new RectangleF(
                MainSettings.UIXPos + 243 * MainSettings.Scaling,
                MainSettings.UIYPos + (90 + OffForAllRender) * MainSettings.Scaling,
                34 * MainSettings.Scaling, 19 * MainSettings.Scaling);
        }

        internal void DrawMainPanel()
        {
            RectsMainPanel();
            //Отрисовка основного прямоугольника
            RendererManager.DrawFilledRoundedRectangle(
            MainPanelRect,
            MainSettings.MainPanelColure,
            MainSettings.Round20);

            //Отрисовка кнопок на основной панели
            RendererManager.DrawFilledRoundedRectangle(
            ButtonAutoSafe,
            MainSettings.ButtonCloure,
            MainSettings.ButtonRound5);

            RendererManager.DrawFilledRoundedRectangle(
            ButtonAutoSafeDNG,
            MainSettings.ButtonCloure,
            MainSettings.ButtonRound5);

            RendererManager.DrawFilledRoundedRectangle(
            ButtonRightExp,
            MainSettings.ButtonCloure,
            MainSettings.ButtonRound5);

            RendererManager.DrawImage("Wisp.opener.RightArrow.png",
            ArrowToOpen);
            RendererManager.DrawImage("Wisp.opener.Rightline.png",
            RightlineToOpen);
            //

            //Отрисовка открытие верхнего слайдера
            
            RendererManager.DrawFilledRoundedRectangle(
                OpenerUp,
                MainSettings.ButtonCloure,
                MainSettings.Round10);

            RendererManager.DrawText("i",
                OpenerUpText,
                MainSettings.WhiteColure,
                "Neometric",
                17 * MainSettings.Scaling);

            //Отрисовка кругов на кнопках вместе с текстом подстраивая яркость
            if (MainMenu.WispAutoSafeEnble.Value)
            {
                RendererManager.DrawFilledRoundedRectangle(
                CircleAutoSafe,
                MainSettings.ColureOn,
                MainSettings.Round10);

                RendererManager.DrawText("Auto save ",
                TextAutoSafe,
                MainSettings.WhiteColure,
                "Neometric",
                MainSettings.TextFontSize21);
            }
            else
            {
                RendererManager.DrawFilledRoundedRectangle(
                CircleAutoSafe,
                MainSettings.ColureOff,
                MainSettings.Round10);

                RendererManager.DrawText("Auto save ",
                TextAutoSafe,
                MainSettings.WhiteColureLowOpacity,
                "Neometric",
                MainSettings.TextFontSize21);
            }

            if (MainMenu.WispAutoSafeDngEnble.Value)
            {
                RendererManager.DrawFilledRoundedRectangle(
                CircleAutoSafeDNG,
                MainSettings.ColureOn,
                MainSettings.Round10);

                RendererManager.DrawText("DNG ",
                TextAutoSafeDNG,
                MainSettings.WhiteColure,
                "Neometric",
                MainSettings.TextFontSize21);
            }
            else
            {
                RendererManager.DrawFilledRoundedRectangle(
                CircleAutoSafeDNG,
                MainSettings.ColureOff,
                MainSettings.Round10);

                RendererManager.DrawText("DNG ",
                TextAutoSafeDNG,
                MainSettings.WhiteColureLowOpacity,
                "Neometric",
                MainSettings.TextFontSize21);
            }
            //
        }

        internal void DrawHeroesInfOnMainPanel()
        {
            OffForAllRender = 0;
            foreach (var HeroesID in Init.DicUIInfo)
            {
                SliderRectsMainPanel();
                SliderAutoSafeShowHPPos = new RectangleF(
                    MainSettings.UIXPos + ((114 * MainSettings.Scaling) + MathF.Max(MathF.Min(HeroesID.Value.Safe_Hp / HeroesID.Value.EntInfo.MaximumHealth * (292 * MainSettings.Scaling), 287 * MainSettings.Scaling), 1) - (4 * MainSettings.Scaling)),
                    MainSettings.UIYPos + (107 + OffForAllRender) * MainSettings.Scaling,
                    8 * MainSettings.Scaling, 32 * MainSettings.Scaling);
                if (HeroesID.Value.Enable)
                {
                    //Отображение прямоугольника состояния
                    RendererManager.DrawFilledRectangle(
                    PanelHeroesRectOnOff,
                    MainSettings.ColureOn);

                    //Отображение иконок героев
                    RendererManager.DrawImage(HeroesID.Key,
                    PanelHeroesImages,
                    UnitImageType.Default,
                    true);

                    RendererManager.DrawImage("Wisp.sliderforAutoSafe.png",
                    PanelSliderAutoSafe);

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

                    //Отображение ползунка на слайдере
                    RendererManager.DrawFilledRoundedRectangle(
                    SliderAutoSafeShowHPPos,
                    MainSettings.HPColureSlider,
                    MainSettings.ButtonRound5);

                    RendererManager.DrawText($"{HeroesID.Value.Safe_Hp}",
                    TextAutoSafeShowHPPos,
                    MainSettings.WhiteColure,
                    "Neometric",
                    FontWeight.Bold,
                    FontFlags.Left,
                    15 * MainSettings.Scaling);
                }
                else
                {
                    RendererManager.DrawFilledRectangle(
                    PanelHeroesRectOnOff,
                    MainSettings.ColureOff);

                    RendererManager.DrawImage(HeroesID.Key,
                    PanelHeroesImages,
                    UnitImageType.Default,
                    true,
                    0.6f);

                    RendererManager.DrawText("Turned off ",
                    PanelTextAutoSafeOff,
                    MainSettings.WhiteColureLowOpacity,
                    "Neometric",
                    MainSettings.TextFontSize13);
                }
                OffForAllRender += 62;
            }
        }

        private void InputManager_MouseKeyDown(MouseEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value)
            {
                return;
            }

            if (e.MouseKey == MouseKey.Left
                && ButtonRightExp.Contains(e.Position))
            {
                MainSettings.IsExpRight = !MainSettings.IsExpRight;
            }

            if (e.MouseKey == MouseKey.Left
                && OpenerUp.Contains(e.Position))
            {
                MainSettings.IsExpUpDng = !MainSettings.IsExpUpDng;
            }

            if (e.MouseKey == MouseKey.Left
                && ButtonAutoSafe.Contains(e.Position))
            {
                MainMenu.WispAutoSafeEnble.Value = !MainMenu.WispAutoSafeEnble.Value;
            }

            if (e.MouseKey == MouseKey.Left
                && ButtonAutoSafeDNG.Contains(e.Position))
            {
                MainMenu.WispAutoSafeDngEnble.Value = !MainMenu.WispAutoSafeDngEnble.Value;
            }

            //Передвижение ползунка слайдера и отключение иконок героев
            OffForAllRender = 0;
            foreach (var HeroesClick in Init.DicUIInfo)
            {
                SliderRectsMainPanel();

                if (e.MouseKey == MouseKey.Left
                && PanelHeroesImages.Contains(e.Position))
                {
                    HeroesClick.Value.Enable = !HeroesClick.Value.Enable;
                }

                if (e.MouseKey == MouseKey.Left
                && PanelSliderAutoSafe.Contains(e.Position))
                {
                    var mousePos = e.Position;
                    mousePos.X -= (MainSettings.UIXPos + (111 * MainSettings.Scaling));
                    mousePos.Y -= (MainSettings.UIYPos + (107 * MainSettings.Scaling));
                    HeroesClick.Value.Safe_Hp = MathF.Ceiling(mousePos.X / (292 * MainSettings.Scaling) * HeroesClick.Value.EntInfo.MaximumHealth);

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

            //Изменеие позиции слайдера и хп при котором нужно спасать союзника
            OffForAllRender = 0;
            foreach (var HeroesClick in Init.DicUIInfo)
            {
                SliderRectsMainPanel();
                if (MousePressedOnSlider
                && PanelSliderAutoSafe.Contains(e.Position))
                {
                    var mousePos = e.Position;
                    mousePos.X -= (MainSettings.UIXPos + (111 * MainSettings.Scaling));
                    mousePos.Y -= (MainSettings.UIYPos + (107 * MainSettings.Scaling));
                    HeroesClick.Value.Safe_Hp = MathF.Ceiling(mousePos.X / (292 * MainSettings.Scaling) * HeroesClick.Value.EntInfo.MaximumHealth);
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
