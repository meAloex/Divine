using Divine.Numerics;
using Divine.Renderer;

namespace WispByAloex.Draw
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
        public bool IsExpUpDng = false;

        public float MainPanelWidth = 417;
        public float MainPanelHeight = 349;

        public float MainPanelWidthRight = 392;
        public float MainPanelHeightRight = 349;

        public float MainPanelWidthUP = 417;
        public float MainPanelHeightUP = 253;

        public bool QueenUltRectEnable = true;
        public bool AxeUltRectEnable = true;
        public bool LionUltRectEnable = true;
        public bool LinaUltUltRectEnable = true;

        public MainSettings()
        {
            Scaling = RendererManager.ScalingNew;
            RendererManager.LoadImageFromAssembly("Wisp.opener.RightArrow.png", "WispByAloex.Images.openerRightArrow.png");
            RendererManager.LoadImageFromAssembly("Wisp.opener.Rightline.png", "WispByAloex.Images.openerRightline.png");
            RendererManager.LoadImageFromAssembly("Wisp.sliderforAutoSafe.png", "WispByAloex.Images.healSliderAutoSafe.png");
            RendererManager.LoadImageFromAssembly("Wisp.healSliderStopHeal.png", "WispByAloex.Images.healSliderStopHeal.png");
            RendererManager.LoadImageFromAssembly("Wisp.Lock.png", "WispByAloex.Images.Lock.png");
            RendererManager.LoadImageFromAssembly("Wisp.BlackRectBlure.png", "WispByAloex.Images.BlackRectBlure.png");

            RendererManager.LoadImageFromAssembly("Wisp.antimageUlt.png", "WispByAloex.Images.antimageUlt.png");

            RendererManager.LoadImageFromAssembly("Wisp.AxeUltAloex.png", "WispByAloex.Images.AxeUlt.png");
            RendererManager.LoadImageFromAssembly("Wisp.AxeUltBluredAloex.png", "WispByAloex.Images.AxeUltBlure.png");

            RendererManager.LoadImageFromAssembly("Wisp.BatellRorrr.png", "WispByAloex.Images.BatellRorrr.png");

            RendererManager.LoadImageFromAssembly("Wisp.LinaUltAloex.png", "WispByAloex.Images.LinaUlt.png");
            RendererManager.LoadImageFromAssembly("Wisp.LinaUltBluredAloex.png", "WispByAloex.Images.LinaUltBlured.png");

            RendererManager.LoadImageFromAssembly("Wisp.LionUltAloex.png", "WispByAloex.Images.LionUlt.png");
            RendererManager.LoadImageFromAssembly("Wisp.LionUltBluredAloex.png", "WispByAloex.Images.LionUltBlured.png");

            RendererManager.LoadImageFromAssembly("Wisp.QueenUltAloex.png", "WispByAloex.Images.QueenUlt.png");
            RendererManager.LoadImageFromAssembly("Wisp.QueenUltBluredAloex.png", "WispByAloex.Images.QueenUltBlured.png");

            RendererManager.LoadImageFromAssembly("Wisp.SFUlt.png", "WispByAloex.Images.SFUlt.png");
            RendererManager.LoadImageFromAssembly("Wisp.ZeusUlt.png", "WispByAloex.Images.ZeusUlt.png");

            //Скейлинг панелей
            MainPanelWidth *= Scaling;
            MainPanelHeight *= Scaling;
            MainPanelWidthRight *= Scaling;
            MainPanelHeightRight *= Scaling;
            MainPanelWidthUP *= Scaling;
            MainPanelHeightUP *= Scaling;

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
