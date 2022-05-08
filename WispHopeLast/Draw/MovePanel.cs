using Wisp;
using Divine.Input;
using Divine.Input.EventArgs;
using Divine.Numerics;
using WispHopeLast.Draw.Panels;

namespace WispHopeLast.Draw
{
    internal class MovePanel
    {
        private MainMenu MainMenu;
        private MainSettings MainSettings;
        private MainPanel MainPanel;
        private RightPanel RightPanel;

        public Vector2 StartCursorPos { get; private set; }
        public bool MouseAndButtonPressed { get; private set; }
        public RectangleF MainPanelRectAndRight { get; private set; }
        public Vector2 CurrentMousePos { get; private set; }
        public bool MenuButtonPressed { get; private set; }

        public MovePanel(MainMenu mainMenu, MainSettings mainSettings, MainPanel mainPanel, RightPanel rightPanel)
        {
            MainMenu = mainMenu;
            MainSettings = mainSettings;
            MainPanel = mainPanel;
            RightPanel = rightPanel;

            InputManager.MouseKeyDown += InputManager_MouseKeyDown;
            InputManager.MouseMove += InputManager_MouseMove;
            InputManager.MouseKeyUp += InputManager_MouseKeyUp;
            MainMenu.WispMoveKey.ValueChanged += WispMoveKey_ValueChanged;
        }

        private void WispMoveKey_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value
                || !MainMenu.WispMoveEnable.Value)
            {
                return;
            }

            //Проверка, что зажата кнопка из меню для перемещения панели
            if (e.Value)
            {
                MenuButtonPressed = true;
            }
            else 
            {
                MenuButtonPressed = false;
                MouseAndButtonPressed = false;
            }
        }

        private void InputManager_MouseKeyDown(MouseEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value
                || !MainMenu.WispMoveEnable.Value)
            {
                return;
            }

            if (MainSettings.IsExpRight)
            {
                MainPanelRectAndRight = new RectangleF(MainSettings.UIXPos, MainSettings.UIYPos, 771 * MainSettings.Scaling, 349 * MainSettings.Scaling);
                //Передвижение панели
                if (e.MouseKey == MouseKey.Left
                && MenuButtonPressed
                && MainPanelRectAndRight.Contains(e.Position)
                && !MainPanel.ButtonRightExp.Contains(e.Position)
                && !MainPanel.ButtonAutoSafe.Contains(e.Position)
                && !MainPanel.ButtonAutoSafeDNG.Contains(e.Position))
                {
                    StartCursorPos = new Vector2(e.Position.X - MainPanelRectAndRight.X, e.Position.Y - MainPanelRectAndRight.Y);
                    MouseAndButtonPressed = true;
                }
            }
            else
            {
                if (e.MouseKey == MouseKey.Left
                && MenuButtonPressed
                && MainPanel.MainPanelRect.Contains(e.Position)
                && !MainPanel.ButtonRightExp.Contains(e.Position)
                && !MainPanel.ButtonAutoSafe.Contains(e.Position)
                && !MainPanel.ButtonAutoSafeDNG.Contains(e.Position))
                {
                    StartCursorPos = new Vector2(e.Position.X - MainPanel.MainPanelRect.X, e.Position.Y - MainPanel.MainPanelRect.Y);
                    MouseAndButtonPressed = true;
                }
            }
        }

        private void InputManager_MouseMove(MouseMoveEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value
                || !MainMenu.WispMoveEnable.Value)
            {
                return;
            }

            //Изменение основных координат, перемещение панели
            if (MouseAndButtonPressed)
            {
                CurrentMousePos = e.Position;
                if (MainSettings.IsExpRight)
                {
                    MainPanel.RectsMainPanel();
                    MainPanel.SliderRectsMainPanel();

                    RightPanel.RectsRightPanel();
                    RightPanel.SliderRectsRightPanel();

                    MainSettings.UIXPos = CurrentMousePos.X - StartCursorPos.X;
                    MainSettings.UIYPos = CurrentMousePos.Y - StartCursorPos.Y;
                }
                else
                {
                    MainPanel.RectsMainPanel();
                    MainPanel.SliderRectsMainPanel();

                    MainSettings.UIXPos = CurrentMousePos.X - StartCursorPos.X;
                    MainSettings.UIYPos = CurrentMousePos.Y - StartCursorPos.Y;
                }
            }
        }

        private void InputManager_MouseKeyUp(MouseEventArgs e)
        {
            if (!MainMenu.WispMainUIKey.Value
                || !MainMenu.WispMoveEnable.Value)
            {
                return;
            }

            //Отключение возможности передвигать панель если отжата левая кнопка
            if (MainSettings.IsExpRight)
            {
                MainPanelRectAndRight = new RectangleF(MainSettings.UIXPos, MainSettings.UIYPos, 771 * MainSettings.Scaling, 349 * MainSettings.Scaling);

                if (e.MouseKey == MouseKey.Left
                && MainPanelRectAndRight.Contains(e.Position))
                {
                    MouseAndButtonPressed = false;
                }
            }
            else
            {
                if (e.MouseKey == MouseKey.Left
                && MainPanel.MainPanelRect.Contains(e.Position))
                {
                    MouseAndButtonPressed = false;
                }
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
