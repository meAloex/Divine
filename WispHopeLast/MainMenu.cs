using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Menu;
using Divine.Menu.Items;
using Divine.Renderer;

namespace Wisp
{
    internal sealed class MainMenu
    {
        public MenuSwitcher WispEnabler { get; }
        public MenuSelector TargetSelector { get; }
        public MenuSelector TargetIndicator { get; }
        public MenuSlider TargetSlider { get; }
        public MenuHoldKey WispButtonHeal { get; }
        public MenuAbilityToggler WispAbilityToggler { get; }
        public MenuItemToggler WispItemsToggler { get; }
        public MenuSwitcher WispUseItemInRange { get; }
        public MenuSwitcher WispUseItemNoTether { get; }
        public MenuSwitcher WispAutoSafeEnble { get; }
        public MenuHoldKey WispAutoSafeKeyOnOff { get; }
        public MenuSwitcher WispAutoSafeOnlyTether { get; }
        public MenuSwitcher WispAutoSafeDngEnble { get; }
        public MenuHoldKey WispAutoSafeDngKeyOnOff { get; }
        public MenuSwitcher WispAutoSafeDngOnlyTether { get; }
        public MenuToggleKey WispMainUIKey { get; }
        public MenuSwitcher WispPotentialHealUI { get; }
        public MenuSwitcher WispMoveEnable { get; }
        public MenuHoldKey WispMoveKey { get; }
        public MenuHoldKey WispOpenKey { get; }
        public MenuSelector WispShowTetherKD { get; }
        public MenuSelector WispShowRelocateStart { get; }
        public MenuSwitcher WispShowTetherRange { get; }
        public MenuSwitcher WispShowTetherRangeBrake { get; }

        public MainMenu()
        {
            RendererManager.LoadImageFromAssembly("IconMain", "WispHopeLast.Images.utils_wheel.png"); // Подгрузить картинку

            var rootMenu = MenuManager.CreateRootMenu("Wisp test").SetHeroImage(HeroId.npc_dota_hero_wisp); // Создаем виспа в меню
            WispEnabler = rootMenu.CreateSwitcher("Enabler"); //Включатель

            var comboMenu = rootMenu.CreateMenu("Combo safe").SetImage("IconMain"); // Подгрузка в меню
            TargetSelector = comboMenu.CreateSelector("Style", new[] { "Free", "Locked" });
            TargetIndicator = comboMenu.CreateSelector("Target Indicator", new[] { "Friend Team", "None" });
            TargetSlider = comboMenu.CreateSlider("Closest to mouse range", 450, 100, 800);

            var itemsSetting = rootMenu.CreateMenu("Items Settings").SetImage("IconMain");

            var autoSafeMainSetting = rootMenu.CreateMenu("Auto Safe Setting").SetImage("IconMain");
            var autoSafe = autoSafeMainSetting.CreateMenu("Auto Safe").SetImage("IconMain");
            var autoSafeDng = autoSafeMainSetting.CreateMenu("Auto Safe Dangerous abilities").SetImage("IconMain");

            var visualMenu = rootMenu.CreateMenu("Visual").SetImage("IconMain");
            var visualAbility = visualMenu.CreateMenu("Ability").SetImage("IconMain");
          
            WispButtonHeal = comboMenu.CreateHoldKey("Combo key", Divine.Input.Key.None);
            WispAbilityToggler = comboMenu.CreateAbilityToggler("Ability", new() // Создание меню спеллов
            { 
                { AbilityId.wisp_tether, false },
                { AbilityId.wisp_spirits, false },
                { AbilityId.wisp_overcharge, false },
            });
            WispItemsToggler = comboMenu.CreateItemToggler("Items", new() // Создание меню айтемов
            {
                { AbilityId.item_guardian_greaves, true },
                { AbilityId.item_mekansm, true },
                { AbilityId.item_holy_locket, true },
                { AbilityId.item_magic_wand, true },
                { AbilityId.item_magic_stick, true },
                { AbilityId.item_faerie_fire, true },
                { AbilityId.item_bottle, true },
                { AbilityId.item_flask, true },
                { AbilityId.item_urn_of_shadows, false },
                { AbilityId.item_spirit_vessel, false },
                { AbilityId.item_lotus_orb, true },
                { AbilityId.item_glimmer_cape, false },
                { AbilityId.item_pipe, true },
                { AbilityId.item_crimson_guard, true },
            });

            WispUseItemInRange = itemsSetting.CreateSwitcher("Use Greaves/Mekanism if tether and only in range", true);
            WispUseItemNoTether = itemsSetting.CreateSwitcher("Use Greaves/Mekanism if in range, but no tether and need heal", true);

            WispAutoSafeEnble = autoSafe.CreateSwitcher("Auto Safe Enable", false);
            WispAutoSafeKeyOnOff = autoSafe.CreateHoldKey("Auto Safe Key On/Off", Divine.Input.Key.None);
            WispAutoSafeOnlyTether = autoSafe.CreateSwitcher("Auto Safe only if tethered", false);

            WispAutoSafeDngEnble = autoSafeDng.CreateSwitcher("Auto Safe Dng Enable", true);
            WispAutoSafeDngKeyOnOff = autoSafeDng.CreateHoldKey("Auto Safe Dng Key On/Off", Divine.Input.Key.None);
            WispAutoSafeDngOnlyTether = autoSafeDng.CreateSwitcher("Auto Safe Dng only if tethered", false);

            WispMainUIKey = visualMenu.CreateToggleKey("Show On/Off heal panel", Divine.Input.Key.None, true);
            WispPotentialHealUI = visualMenu.CreateSwitcher("Show potential heal panel", true);
            WispMoveEnable = visualMenu.CreateSwitcher("Move panels", true);
            WispMoveKey = visualMenu.CreateHoldKey("Move panels key", Divine.Input.Key.None);
            WispOpenKey = visualMenu.CreateHoldKey("Open key panels", Divine.Input.Key.None);

            WispShowTetherKD = visualAbility.CreateSelector("Show tether kd", new[] { "Non", "Text", "Image" });
            WispShowRelocateStart = visualAbility.CreateSelector("Show relocate on start", new[] { "Non", "Text", "Text and Cursor" });
            WispShowTetherRange = visualAbility.CreateSwitcher("Show tether range", true);
            WispShowTetherRangeBrake = visualAbility.CreateSwitcher("Show tether range brake", true);
        }
    }
}