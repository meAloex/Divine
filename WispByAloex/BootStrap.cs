using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Menu.EventArgs;
using Divine.Menu.Items;
using Divine.Service;

using Wisp.Combo;
using Wisp.Info;

using WispHopeLast.Draw;
using WispHopeLast.Items;

namespace Wisp
{
    public class BootStrap : Bootstrapper
    {
        private MainMenu MainMenu;
        private Init Init;
        private TargetSelector TargetSelector;
        private KeyCombo KeyCombo;
        private MainSettings MainSettings;
        private DrawInfo DrawPanels;

        protected override void OnMainActivate() //Загружается единожды при запуске доты или f5, подгрузка меню
        {
            MainMenu = new MainMenu();
        }

        protected override void OnActivate() //Если в игре, то проверить вкл скрипта героя, затем подгрузить инфу
        {
            if (EntityManager.LocalHero?.HeroId != HeroId.npc_dota_hero_wisp)
            {
                return;
            }

            MainMenu.WispEnabler.ValueChanged += OnWispEnabler;
        }

        private void OnWispEnabler(MenuSwitcher switcher, SwitcherEventArgs e)
        {
            if (e.Value)
            {
                Init = new Init();
                TargetSelector = new TargetSelector(Init, MainMenu);
                KeyCombo = new KeyCombo(Init, MainMenu);
                MainSettings = new MainSettings();
                DrawPanels = new DrawInfo(Init, MainMenu, MainSettings);
            }
            else
            {
                Init?.Dispose();
                TargetSelector?.Dispose();
                KeyCombo?.Dispose();
                DrawPanels?.Dispose();
            }
        }
    }
}