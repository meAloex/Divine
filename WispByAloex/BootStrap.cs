using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Menu.EventArgs;
using Divine.Menu.Items;
using Divine.Service;

using Wisp.Combo;
using Wisp.Info;

using WispByAloex.Draw;
using WispByAloex.Draw.Panels;
using WispByAloex.Helpers;
using WispByAloex.Items;

namespace Wisp
{
    public class BootStrap : Bootstrapper
    {
        private MainMenu MainMenu;
        private Init Init;
        private TargetSelector TargetSelector;
        private AbilityItemManager AbilityItemManager;
        private KeyCombo KeyCombo;
        private MainSettings MainSettings;
        private DrawInfo DrawPanels;
        private AutoSafe AutoSafe;
        private AutoSafeDNG AutoSafeDNG;

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
                AbilityItemManager = new AbilityItemManager(Init, MainMenu);
                KeyCombo = new KeyCombo(Init, MainMenu, AbilityItemManager);
                MainSettings = new MainSettings();
                DrawPanels = new DrawInfo(Init, MainMenu, MainSettings);
                AutoSafe = new AutoSafe(Init, MainMenu, AbilityItemManager);
                AutoSafeDNG = new AutoSafeDNG(Init, MainMenu, AbilityItemManager, MainSettings);
            }
            else
            {
                Init?.Dispose();
                TargetSelector?.Dispose();
                AbilityItemManager?.Dispose();
                KeyCombo?.Dispose();
                DrawPanels?.Dispose();
                AutoSafe?.Dispose();
                AutoSafeDNG?.Dispose();
            }
        }
    }
}