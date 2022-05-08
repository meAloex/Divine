using Divine.Renderer;
using Wisp.Info;
using Wisp;
using Divine.Numerics;
using System.Net;
using WispHopeLast.Draw.Panels;
using WispHopeLast.Draw.Ability;

namespace WispHopeLast.Draw
{
    internal class DrawInfo
    {
        private Init Init;
        private MainMenu MainMenu;
        private MainSettings MainSettings;

        public Vector2 ScreenSize { get; private set; }

        private MainPanel MainPanel;
        private RightPanel RightPanel;
        private MovePanel MovePanel;
        private RelocateStart RelocateStart;

        public TetherKD TetherKD { get; }
        public TetherRange TetherRange { get; }
        public TetherRangeBrake TetherRangeBrake { get; }

        public float accel = 0;
        public float decel = 0;

        public DrawInfo(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;

            ScreenSize = RendererManager.ScreenSize;
            MainPanel = new MainPanel(Init, MainMenu, MainSettings);
            RightPanel = new RightPanel(Init, MainMenu, MainSettings);
            MovePanel = new MovePanel(MainMenu, MainSettings, MainPanel, RightPanel);
            RelocateStart = new RelocateStart(Init, MainMenu, MainSettings);
            TetherKD = new TetherKD(Init, MainMenu, MainSettings);
            TetherRange = new TetherRange(Init, MainMenu, MainSettings);
            TetherRangeBrake = new TetherRangeBrake(Init, MainMenu, MainSettings);

            RendererManager.Draw += RendererManager_Draw;
        }

        private void RendererManager_Draw()
        {
            //Отрисовка основной панели
            DrawPanels();

            //Отрисовка старта до тп релокейтом
            if (MainMenu.WispShowRelocateStart.Value != "Non")
            {
                RelocateStart.DrawReloInfo();
            }
            else
            {
                RelocateStart.Dispose();
            }

            //Отрисовка кд тизера
            if (MainMenu.WispShowTetherKD.Value != "Non")
            {
                TetherKD.DrawTether();
            }

            //Отображение радуса тизера
            if (MainMenu.WispShowTetherRange.Value == true)
            {
                TetherRange.DrawTetherRange();
            }
            else
            {
                TetherRange.Dispose();
            }

            //Отображение радуса разрыва тизера
            if (MainMenu.WispShowTetherRangeBrake.Value == true)
            {
                TetherRangeBrake.DrawTetherRangeBrake();
            }
            else
            {
                TetherRangeBrake.Dispose();
            }
        }

        private void DrawPanels()
        {
            if (!MainMenu.WispMainUIKey.Value)
            {
                return;
            }

            if (MainSettings.IsExpRight)
            {
                //Отрисовка правого прямоугольника с слайдером
                RightPanel.DrawRightPanel();
                RightPanel.DrawHeroesInfOnRightPanel();
                //Отрисовка основного прямоугольника с слайдером
                MainPanel.DrawMainPanel();
                MainPanel.DrawHeroesInfOnMainPanel();

                //Отмотка меню обратно, если выходит за рамки
                if (MainSettings.UIXPos < 0)
                {
                    accel = (MainSettings.UIXPos / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIXPos += Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    RightPanel.RectsRightPanel();

                    MainPanel.SliderRectsMainPanel();
                    RightPanel.SliderRectsRightPanel();
                }
                else if (MainSettings.UIXPos + 798 * MainSettings.Scaling > ScreenSize.X)
                {
                    accel = ((ScreenSize.X - MainSettings.UIXPos - 798 * MainSettings.Scaling) / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIXPos -= Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    RightPanel.RectsRightPanel();

                    MainPanel.SliderRectsMainPanel();
                    RightPanel.SliderRectsRightPanel();
                }

                if (MainSettings.UIYPos < 0)
                {
                    accel = (MainSettings.UIYPos / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIYPos += Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    RightPanel.RectsRightPanel();

                    MainPanel.SliderRectsMainPanel();
                    RightPanel.SliderRectsRightPanel();
                }
                else if (MainSettings.UIYPos + MainSettings.MainPanelHeight > ScreenSize.Y)
                {
                    accel = ((ScreenSize.Y - MainSettings.UIYPos - MainSettings.MainPanelHeight) / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIYPos -= Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    RightPanel.RectsRightPanel();

                    MainPanel.SliderRectsMainPanel();
                    RightPanel.SliderRectsRightPanel();
                }
            }
            else
            {
                //Отрисовка основного прямоугольника с слайдером
                MainPanel.DrawMainPanel();
                MainPanel.DrawHeroesInfOnMainPanel();

                //Отмотка меню обратно, если выходит за рамки
                if (MainSettings.UIXPos < 0)
                {
                    accel = (MainSettings.UIXPos / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIXPos += Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    MainPanel.SliderRectsMainPanel();
                }
                else if (MainSettings.UIXPos + MainSettings.MainPanelWidth > ScreenSize.X)
                {
                    accel = ((ScreenSize.X - MainSettings.UIXPos - MainSettings.MainPanelWidth) / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIXPos -= Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    MainPanel.SliderRectsMainPanel();
                }

                if (MainSettings.UIYPos < 0)
                {
                    accel = (MainSettings.UIYPos / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIYPos += Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    MainPanel.SliderRectsMainPanel();
                }
                else if (MainSettings.UIYPos + MainSettings.MainPanelHeight > ScreenSize.Y)
                {
                    accel = ((ScreenSize.Y - MainSettings.UIYPos - MainSettings.MainPanelHeight) / (3.6f * 15));

                    if (Math.Abs(accel) <= 0.1) accel = 1;

                    MainSettings.UIYPos -= Math.Abs(accel);
                    MainPanel.RectsMainPanel();
                    MainPanel.SliderRectsMainPanel();
                }
            }
        }

        internal void Dispose()
        {
            RendererManager.Draw -= RendererManager_Draw;
            MainPanel.Dispose();
            RightPanel.Dispose();
            RelocateStart.Dispose();
            TetherRange.Dispose();
            TetherRangeBrake.Dispose();
        }
    }
}
