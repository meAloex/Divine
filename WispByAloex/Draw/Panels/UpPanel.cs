
using Wisp.Info;
using Wisp;
using WispByAloex.Draw;
using Divine.Numerics;
using Divine.Renderer;
using Divine.Input;
using Divine.Input.EventArgs;
using Divine.Helpers;
using Divine.Game;
using Divine.Projectile;
using Divine.Update;

namespace WispByAloex.Draw.Panels
{
    internal class UpPanel
    {
        private Init Init;
        private MainMenu MainMenu;
        private MainSettings MainSettings;

        public RectangleF MainPanelUpRect { get; private set; }
        public RectangleF BlackBlure { get; private set; }
        public RectangleF LockBlure { get; private set; }
        public RectangleF QueenUltRect;

        public RectangleF QueenUltRectBlure { get; private set; }

        public RectangleF AntimageUltRect;

        public RectangleF AxeUltRect;
        public RectangleF AxeUltRectBlure { get; private set; }

        public RectangleF LionUltRect;
        public RectangleF LionUltRectBlure { get; private set; }

        public RectangleF BatellRorrrUltRect;
        public RectangleF SFUltRect;
        public RectangleF ZeusUltUltRect;
        public RectangleF LinaUltUltRect;

        public RectangleF LinaUltUltRectBlure { get; private set; }

        public Vector2 StartCursorPos { get; private set; }
        public bool MousePressed { get; private set; }
        public Vector2 CurrentMousePos { get; private set; }
        public bool MousePressedQueen { get; private set; }
        public float AvrSpeed { get; private set; }
        public float TimeStart { get; private set; }
        public bool MousePressedAxe { get; private set; }
        public bool MousePressedLion { get; private set; }
        public bool MousePressedLina { get; private set; }
        public bool MousePressedAntimage { get; private set; }
        public bool MousePressedBatellRorrr { get; private set; }
        public bool MousePressedSF { get; private set; }
        public bool MousePressedAntimageZeus { get; private set; }
        public float SleeperYPos { get; private set; }

        public UpPanel(Init init, MainMenu mainMenu, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            MainSettings = mainSettings;
            RectsUpPanel();

            InputManager.MouseKeyDown += InputManager_MouseKeyDown;
        }

        public void RectsUpPanel()
        {
            MainPanelUpRect = new RectangleF(
                MainSettings.UIXPos,
                MainSettings.UIYPos - 212 * MainSettings.Scaling,
                MainSettings.MainPanelWidthUP, MainSettings.MainPanelHeightUP);

            BlackBlure = new RectangleF(
                MainSettings.UIXPos,
                MainSettings.UIYPos - 75 * MainSettings.Scaling,
                432 * MainSettings.Scaling, 100 * MainSettings.Scaling);

            LockBlure = new RectangleF(
                MainSettings.UIXPos + 195 * MainSettings.Scaling,
                MainSettings.UIYPos - 45 * MainSettings.Scaling,
                28 * MainSettings.Scaling, 28 * MainSettings.Scaling);

            //Отрисовка верхнего ряда картинок
            QueenUltRect = new RectangleF(
                MainSettings.UIXPos + 42 * MainSettings.Scaling,
                MainSettings.UIYPos - 190 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            QueenUltRectBlure = new RectangleF(
                MainSettings.UIXPos + 42 * MainSettings.Scaling,
                MainSettings.UIYPos - 189 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            AxeUltRect = new RectangleF(
                MainSettings.UIXPos + 135 * MainSettings.Scaling,
                MainSettings.UIYPos - 190 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            AxeUltRectBlure = new RectangleF(
                MainSettings.UIXPos + 135 * MainSettings.Scaling,
                MainSettings.UIYPos - 188 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            LionUltRect = new RectangleF(
                MainSettings.UIXPos + 228 * MainSettings.Scaling,
                MainSettings.UIYPos - 190 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            LionUltRectBlure = new RectangleF(
                MainSettings.UIXPos + 228 * MainSettings.Scaling,
                MainSettings.UIYPos - 188 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            LinaUltUltRect = new RectangleF(
                MainSettings.UIXPos + 321 * MainSettings.Scaling,
                MainSettings.UIYPos - 190 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            LinaUltUltRectBlure = new RectangleF(
                MainSettings.UIXPos + 321 * MainSettings.Scaling,
                MainSettings.UIYPos - 188 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            //Отрисовка нижней ряда картинок
            AntimageUltRect = new RectangleF(
                MainSettings.UIXPos + 42 * MainSettings.Scaling,
                MainSettings.UIYPos - 115 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            BatellRorrrUltRect = new RectangleF(
                MainSettings.UIXPos + 135 * MainSettings.Scaling,
                MainSettings.UIYPos - 115 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            SFUltRect = new RectangleF(
                MainSettings.UIXPos + 228 * MainSettings.Scaling,
                MainSettings.UIYPos - 115 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);

            ZeusUltUltRect = new RectangleF(
                MainSettings.UIXPos + 321 * MainSettings.Scaling,
                MainSettings.UIYPos - 115 * MainSettings.Scaling,
                55 * MainSettings.Scaling, 55 * MainSettings.Scaling);
        }

        internal void DrawUpPanel()
        {
            if (MainSettings.IsExpUpDng)
            {
                //Отрисовка верхнего прямоугольника
                RendererManager.DrawFilledRoundedRectangle(
                MainPanelUpRect,
                MainSettings.RightPanelColure,
                MainSettings.Round20);

                //Отрисовка нижней ряда картинок
                RendererManager.DrawImage("Wisp.antimageUlt.png",
                AntimageUltRect);

                RendererManager.DrawImage("Wisp.BatellRorrr.png",
                BatellRorrrUltRect);

                RendererManager.DrawImage("Wisp.SFUlt.png",
                SFUltRect);

                RendererManager.DrawImage("Wisp.ZeusUlt.png",
                ZeusUltUltRect);

                //Отрисовка верхнего ряда картинок
                if (MainSettings.QueenUltRectEnable)
                {
                    RendererManager.DrawImage("Wisp.QueenUltAloex.png",
                    QueenUltRect);
                }
                else
                {
                    RendererManager.DrawImage("Wisp.QueenUltBluredAloex.png",
                    QueenUltRectBlure);
                }

                if (MainSettings.AxeUltRectEnable)
                {
                    RendererManager.DrawImage("Wisp.AxeUltAloex.png",
                    AxeUltRect);
                }
                else
                {
                    RendererManager.DrawImage("Wisp.AxeUltBluredAloex.png",
                    AxeUltRectBlure);
                }

                if (MainSettings.LionUltRectEnable)
                {
                    RendererManager.DrawImage("Wisp.LinaUltAloex.png",
                    LionUltRect);
                }
                else
                {
                    RendererManager.DrawImage("Wisp.LinaUltBluredAloex.png",
                    LionUltRectBlure);
                }

                if (MainSettings.LinaUltUltRectEnable)
                {
                    RendererManager.DrawImage("Wisp.LionUltAloex.png",
                    LinaUltUltRect);
                }
                else
                {
                    RendererManager.DrawImage("Wisp.LionUltBluredAloex.png",
                    LinaUltUltRectBlure);
                }
               
                //Отрисовка блюра с замком
                RendererManager.DrawImage("Wisp.BlackRectBlure.png",
                BlackBlure);

                RendererManager.DrawImage("Wisp.Lock.png",
                LockBlure);
            }
        }

        private void InputManager_MouseKeyDown(MouseEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value)
            {
                return;
            }

            if (e.MouseKey == MouseKey.Left
                && QueenUltRect.Contains(e.Position))
            {
                MainSettings.QueenUltRectEnable = !MainSettings.QueenUltRectEnable;
            }

            if (e.MouseKey == MouseKey.Left
                && AxeUltRect.Contains(e.Position))
            {
                MainSettings.AxeUltRectEnable = !MainSettings.AxeUltRectEnable;
            }

            if (e.MouseKey == MouseKey.Left
                && LionUltRect.Contains(e.Position))
            {
                MainSettings.LionUltRectEnable = !MainSettings.LionUltRectEnable;
            }

            if (e.MouseKey == MouseKey.Left
                && LinaUltUltRect.Contains(e.Position))
            {
                MainSettings.LinaUltUltRectEnable = !MainSettings.LinaUltUltRectEnable;
            }
        }

        public void Dispose()
        {
            InputManager.MouseKeyDown -= InputManager_MouseKeyDown;
        }
    }
}
