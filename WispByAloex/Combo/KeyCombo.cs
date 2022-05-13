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

using WispByAloex.Items;

namespace Wisp.Combo
{
    internal class KeyCombo
    {
        public Init Init;
        public MainMenu MainMenu;
        private AbilityItemManager AbilityItemManager;

        public Sleeper TimerToButton = new Sleeper();
        public Tether Tether { get; private set; }
        public TetherBrake TetherBrake { get; private set; }
        public Overcharge Overcharge { get; private set; }
        public float TimerStartCombo { get; private set; }

        public Hero FriendTarget { get; private set; }

        public KeyCombo(Init init, MainMenu mainMenu, AbilityItemManager abilityItemManager)
        {
            Init = init;
            MainMenu = mainMenu;
            AbilityItemManager = abilityItemManager;

            Tether = new Tether(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether), MainMenu);
            TetherBrake = new TetherBrake(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether_break));
            Overcharge = new Overcharge(Init.MyEntity.GetAbilityById(AbilityId.wisp_overcharge), MainMenu, Init);
            MainMenu.WispButtonHeal.ValueChanged += WispButtonHeal_ValueChanged;
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

        private void UpdateManager_IngameUpdate()
        {
            if (AbilityItemManager.TimerToButton.Sleeping)
            {
                return;
            }

            if (TimerStartCombo >= GameManager.GameTime
                && FriendTarget != null)
            {
                if (Tether.CanBeCasted(FriendTarget)
                    && Tether.UseAbility(FriendTarget, TetherBrake))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Greaves != null && AbilityItemManager.Greaves.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Greaves.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Mekanism != null && AbilityItemManager.Mekanism.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Locket != null && AbilityItemManager.Locket.CanBeCasted(FriendTarget)
                    && (AbilityItemManager.Locket.UseAbilityOnOwner(FriendTarget)
                    || AbilityItemManager.Locket.UseAbilityOnFriendTarget(FriendTarget)))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Wand != null && AbilityItemManager.Wand.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Wand.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Stick != null && AbilityItemManager.Stick.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Stick.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Faerie != null && AbilityItemManager.Faerie.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Faerie.UseAbilityNoTarget(FriendTarget))
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

                if (AbilityItemManager.Bottle != null && AbilityItemManager.Bottle.CanBeCasted(FriendTarget)
                    && (AbilityItemManager.Bottle.UseAbilityOnOwner(FriendTarget)
                    || AbilityItemManager.Bottle.UseAbilityOnFriendTarget(FriendTarget)))
                {
                    TimerToButton.Sleep(150);
                    return;
                }

                if (AbilityItemManager.Salve != null && AbilityItemManager.Salve.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Salve.UseAbilityOnOwner(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Lotus != null && AbilityItemManager.Lotus.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Lotus.UseAbilityOnFriendTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Glimmer != null && AbilityItemManager.Glimmer.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Glimmer.UseAbilityOnFriendTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Pipe != null && AbilityItemManager.Pipe.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Pipe.UseAbilityNoTarget(FriendTarget))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Crimson != null && AbilityItemManager.Crimson.CanBeCasted(FriendTarget)
                    && AbilityItemManager.Crimson.UseAbilityNoTarget(FriendTarget))
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
            //Console.WriteLine(TimerToButton);
        }

        public void Dispose()
        {
            MainMenu.WispButtonHeal.ValueChanged -= WispButtonHeal_ValueChanged;
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }
    }
}
