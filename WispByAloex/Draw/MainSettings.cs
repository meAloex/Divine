using Divine.Numerics;
using Divine.Renderer;

namespace WispHopeLast.Draw
{
    internal class MainSettings
    {
        public float Scaling { get; private set; }
        public Color WhiteColure { get; private set; }
        public Color WhiteColureLowOpacity { get; private set; }
        public Color MainPanelColure { get; private set; }
        public Color RightPanelColure { get; private set; }
        public Color ButtonCloure { get; private set; }
        public Color ColureOn { get; private set; }
        public Color ColureOff { get; private set; }
        public Color OrangeColureText { get; private set; }
        public Color HPColureSlider { get; private set; }
        public Vector2 Round20 { get; private set; }
        public Vector2 Round10 { get; private set; }
        public Vector2 ButtonRound5 { get; private set; }
        public float TextFontSize21 { get; private set; }
        public float TextFontSize20 { get; private set; }
        public float TextFontSize19 { get; private set; }
        public float TextFontSize13 { get; private set; }

        public float UIXPos = 200;
        public float UIYPos = 0;

        public bool IsExpRight = false;
        public bool IsExpRightItems = false;

        public float MainPanelWidth = 417;
        public float MainPanelHeight = 349;

        public float MainPanelWidthRight = 392;
        public float MainPanelHeightRight = 349;

        public MainSettings()
        {
            Scaling = RendererManager.ScalingNew;
            RendererManager.LoadImageFromAssembly("Wisp.opener.RightArrow.png", "WispHopeLast.Images.openerRightArrow.png");
            RendererManager.LoadImageFromAssembly("Wisp.opener.Rightline.png", "WispHopeLast.Images.openerRightline.png");
            RendererManager.LoadImageFromAssembly("Wisp.sliderforAutoSafe.png", "WispHopeLast.Images.healSliderAutoSafe.png");
            RendererManager.LoadImageFromAssembly("Wisp.healSliderStopHeal.png", "WispHopeLast.Images.healSliderStopHeal.png");

            //Скейлинг панелей
            MainPanelWidth *= Scaling;
            MainPanelHeight *= Scaling;
            MainPanelWidthRight *= Scaling;
            MainPanelHeightRight *= Scaling;

            //Для того, что бы подгружалась по середине экрана Y
            UIYPos = (RendererManager.ScreenSize.Y / 2) - (MainPanelHeight / 2);

            //Задаю основные X Y от которых отталкивается все значения
            UIXPos *= Scaling;
            UIYPos *= Scaling;

            //Цвета
            WhiteColure = new Color(255, 255, 255, 255);
            WhiteColureLowOpacity = new Color(255, 255, 255, 70);
            MainPanelColure = new Color(28, 33, 36, 255);
            RightPanelColure = new Color(36, 42, 46, 255);

            ButtonCloure = new Color(26, 26, 29, 255);
            ColureOn = new Color(70, 170, 35, 255);
            ColureOff = new Color(255, 0, 0, 70);
            OrangeColureText = new Color(236, 112, 23, 255);
            HPColureSlider = new Color(140, 140, 142, 255);

            //Закругления
            Round20 = new Vector2(20f * Scaling, 20f * Scaling);
            Round10 = new Vector2(10f * Scaling, 10f * Scaling);
            ButtonRound5 = new Vector2(5f * Scaling, 5f * Scaling);

            //Текст размер
            TextFontSize21 = 21 * Scaling;
            TextFontSize20 = 20 * Scaling;
            TextFontSize19 = 19 * Scaling;
            TextFontSize13 = 13 * Scaling;
        }
    }
}
