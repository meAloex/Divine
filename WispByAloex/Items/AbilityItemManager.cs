
using Divine.Update;

using Wisp.Info;
using Wisp;
using Wisp.Items;

using Divine.Entity.Entities.Abilities.Components;
using Divine.Helpers;

namespace WispByAloex.Items
{
    internal class AbilityItemManager
    {
        private Init Init;
        private MainMenu MainMenu;
        public Sleeper TimerToButton = new Sleeper();
        public Greaves Greaves;
        public Mekanism Mekanism;
        public Locket Locket;
        public Wand Wand;
        public Stick Stick;
        public Faerie Faerie;
        public Bottle Bottle;
        public Salve Salve;
        public Lotus Lotus;
        public Glimmer Glimmer;
        public Pipe Pipe;
        public Crimson Crimson;

        public AbilityItemManager(Init init, MainMenu mainMenu)
        {
            Init = init;
            MainMenu = mainMenu;

            UpdateManager.CreateIngameUpdate(1000, ItemUpdater);
        }

        private void ItemUpdater()
        {
            foreach (var item in Init.MyEntity.Inventory!.MainItems)
            {
                switch (item.Id)
                {
                    case AbilityId.item_guardian_greaves:
                        {
                            Greaves = new Greaves(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_mekansm:
                        {
                            Mekanism = new Mekanism(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_holy_locket:
                        {
                            Locket = new Locket(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_magic_wand:
                        {
                            Wand = new Wand(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_magic_stick:
                        {
                            Stick = new Stick(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_faerie_fire:
                        {
                            Faerie = new Faerie(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_bottle:
                        {
                            Bottle = new Bottle(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_flask:
                        {
                            Salve = new Salve(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_lotus_orb:
                        {
                            Lotus = new Lotus(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_glimmer_cape:
                        {
                            Glimmer = new Glimmer(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_pipe:
                        {
                            Pipe = new Pipe(item, MainMenu, Init);
                            break;
                        }
                    case AbilityId.item_crimson_guard:
                        {
                            Crimson = new Crimson(item, MainMenu, Init);
                            break;
                        }
                }
            }
        }

        public void Dispose()
        {
            UpdateManager.DestroyIngameUpdate(ItemUpdater);
        }
    }
}
