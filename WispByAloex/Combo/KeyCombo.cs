using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Game;
using Divine.Helpers;
using Divine.Menu.EventArgs;
using Divine.Menu.Items;
using Divine.Update;

using Wisp.Info;
using Wisp.Items;
using Wisp.Spells;

namespace Wisp.Combo
{
    internal class KeyCombo
    {
        public Init Init { get; }
        public MainMenu MainMenu { get; }
        public Tether Tether { get; private set; }
        public TetherBrake TetherBrake { get; private set; }
        public Overcharge Overcharge { get; private set; }
        public float TimerStartCombo { get; private set; }

        private Sleeper TimerToButton = new Sleeper();
        public Hero FriendTarget { get; private set; }
        public Greaves Greaves { get; private set; }
        public Mekanism Mekanism { get; private set; }
        public Locket Locket { get; private set; }
        public Wand Wand { get; private set; }
        public Stick Stick { get; private set; }
        public Faerie Faerie { get; private set; }
        public Bottle Bottle { get; private set; }
        public Salve Salve { get; private set; }
        public Lotus Lotus { get; private set; }
        public Glimmer Glimmer { get; private set; }
        public Pipe Pipe { get; private set; }
        public Crimson Crimson { get; private set; }

        public KeyCombo(Init init, MainMenu mainMenu)
        {
            Init = init;
            MainMenu = mainMenu;

            Tether = new Tether(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether), MainMenu);
            TetherBrake = new TetherBrake(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether_break));
            Overcharge = new Overcharge(Init.MyEntity.GetAbilityById(AbilityId.wisp_overcharge), MainMenu, Init);
            MainMenu.WispButtonHeal.ValueChanged += WispButtonHeal_ValueChanged;
            UpdateManager.CreateIngameUpdate(1000, ItemUpdater);
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
        }

        private void WispButtonHeal_ValueChanged(MenuHoldKey holdKey, HoldKeyEventArgs e)
        {
            if (!e.Value || TimerStartCombo >= GameManager.GameTime)
            {
                return;
            }

            FriendTarget = TargetSelector.CurrentTarget;
            if (FriendTarget == null || !FriendTarget.IsAlive)
            {
                return;
            }
            else
            {
                TimerStartCombo = GameManager.GameTime + 5 + GameManager.Ping;
            }
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

        private void UpdateManager_IngameUpdate()
        {
            if (TimerStartCombo >= GameManager.GameTime)
            {
                if (Tether.CanBeCasted(FriendTarget)
                && Tether.UseAbility(FriendTarget, TetherBrake))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Greaves != null && Greaves.CanBeCasted(FriendTarget)
                    && Greaves.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Mekanism != null && Mekanism.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Locket != null && Locket.CanBeCasted(FriendTarget)
                    && (Locket.UseAbilityOnOwner(FriendTarget)
                    || Locket.UseAbilityOnFriendTarget(FriendTarget)))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Wand != null && Wand.CanBeCasted(FriendTarget)
                    && Wand.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Stick != null && Stick.CanBeCasted(FriendTarget)
                    && Stick.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Faerie != null && Faerie.CanBeCasted(FriendTarget)
                    && Faerie.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Overcharge != null && Overcharge.CanBeCasted(FriendTarget)
                    && Overcharge.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Bottle != null && Bottle.CanBeCasted(FriendTarget)
                    && (Bottle.UseAbilityOnOwner(FriendTarget)
                    || Bottle.UseAbilityOnFriendTarget(FriendTarget)))
                {
                    TimerToButton.Sleep(150);
                    return;
                }

                if (Salve != null && Salve.CanBeCasted(FriendTarget)
                    && Salve.UseAbilityOnOwner(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Lotus != null && Lotus.CanBeCasted(FriendTarget)
                    && Lotus.UseAbilityOnFriendTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Glimmer != null && Glimmer.CanBeCasted(FriendTarget)
                    && Glimmer.UseAbilityOnFriendTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Pipe != null && Pipe.CanBeCasted(FriendTarget)
                    && Pipe.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Crimson != null && Crimson.CanBeCasted(FriendTarget)
                    && Crimson.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }
            }
            else
            {
                MainMenu.WispButtonHeal.Value = false;
                return;
            }

            if (TimerToButton.Sleeping)
            {
                return;
            }
            //Console.WriteLine(TimerToButton);
        }

        public void Dispose()
        {
            MainMenu.WispButtonHeal.ValueChanged -= WispButtonHeal_ValueChanged;
            UpdateManager.DestroyIngameUpdate(ItemUpdater);
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }
    }
}
