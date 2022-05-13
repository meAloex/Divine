
using Wisp.Info;
using Wisp;
using WispByAloex.Draw;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Extensions;
using Wisp.Spells;
using WispByAloex.Items;
using Divine.Projectile;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Entity;
using Divine.Game;
using Divine.Update;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Helpers;
using Divine.Entity.Entities;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Components;
using WispByAloex.Extensions;
using static Wisp.Info.Init;

namespace WispByAloex.Helpers
{
    internal class AutoSafe
    {
        private Init Init;
        private MainMenu MainMenu;
        private AbilityItemManager AbilityItemManager;
        private float TimerStartAutoSafe;
        private Hero HeroToSafe;
        public Sleeper TimerToButton = new Sleeper();
        public Tether Tether { get; }
        public TetherBrake TetherBrake { get; }
        public Overcharge Overcharge { get; }

        public AutoSafe(Init init, MainMenu mainMenu, AbilityItemManager abilityItemManager)
        {
            Init = init;
            MainMenu = mainMenu;
            AbilityItemManager = abilityItemManager;

            Tether = new Tether(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether), MainMenu);
            TetherBrake = new TetherBrake(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether_break));
            Overcharge = new Overcharge(Init.MyEntity.GetAbilityById(AbilityId.wisp_overcharge), MainMenu, Init);

            ProjectileManager.TrackingProjectileAdded += ProjectileManager_TrackingProjectileAdded;
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            Entity.NetworkPropertyChanged += Entity_NetworkPropertyChanged;
            
        }

        private void Entity_NetworkPropertyChanged(Entity sender, Divine.Entity.Entities.EventArgs.NetworkPropertyChangedEventArgs e)
        {
            if (!MainMenu.WispAutoSafeEnble.Value)
            {
                return;
            }

            if (e.PropertyName != "m_NetworkActivity")
            {
                return;
            }

            if (!(((NetworkActivity)e.NewValue.GetInt32()) is NetworkActivity.Attack)
                && !(((NetworkActivity)e.NewValue.GetInt32()) is NetworkActivity.Attack2))
            {
                return;
            }
            
            UpdateManager.BeginInvoke(() =>
            {
                if (sender is not Hero hero)
                {
                    return;
                }
                if (!Init.MyEntity.IsEnemy(hero))
                {
                    return;
                }

                Console.WriteLine(hero.Target);
     
                var target = Init.DicUIInfo.Values
                    .Where(x => x.Enable && x.EntInfo.Distance2D(hero.Position) < hero.AttackRange + 75
                        && hero.IsNetworkDirectlyFacing(x.EntInfo) < 45)
                    .OrderBy(x => x.EntInfo.Distance2D(hero.Position) < hero.AttackRange + 75)
                    .OrderBy(x => hero.IsNetworkDirectlyFacing(x.EntInfo) < 45)
                    .FirstOrDefault();

                if (target == null)
                {
                    return;
                }
                Console.WriteLine(target.EntInfo.Health);
                if (target.Safe_Hp + hero.DamageAverage > target.EntInfo.Health)
                {
                    TimerStartAutoSafe = GameManager.RawGameTime + 3 + GameManager.Ping;
                    HeroToSafe = target.EntInfo;
                }
            });
        }

        private void Entity_AnimationChanged(Entity sender, Divine.Entity.Entities.EventArgs.AnimationChangedEventArgs e)
        {
            if (!MainMenu.WispAutoSafeEnble.Value)
            {
                return;
            }

            if (sender is not Hero hero)
            {
                return;
            }

            var target = Init.DicUIInfo.Values.FirstOrDefault(x => x.Enable && hero.IsDirectlyFacing(x.EntInfo));

            if (target == null)
            {
                return;
            }

            //Console.WriteLine(target.EntInfo);
        }

        private void ProjectileManager_TrackingProjectileAdded(Divine.Projectile.EventArgs.TrackingProjectileAddedEventArgs e)
        {
            if (!MainMenu.WispAutoSafeEnble.Value)
            {
                return;
            }

            if (e.Projectile.Source is Hero source
                && e.Projectile.Target is Hero target
                && Init.DicUIInfo.TryGetValue(target.HeroId, out var userInfo)
                && userInfo.Enable
                && userInfo.Safe_Hp + source.DamageAverage > userInfo.EntInfo.Health)
            {
                
                //Console.WriteLine(source.HeroId);
                //Console.WriteLine(source.DamageAverage);

                TimerStartAutoSafe = GameManager.RawGameTime + 3 + GameManager.Ping;
                HeroToSafe = userInfo.EntInfo;
            }
        }

        private void UpdateManager_IngameUpdate()
        {
            if (!MainMenu.WispAutoSafeEnble.Value)
            {
                return;
            }

            if (AbilityItemManager.TimerToButton.Sleeping)
            {
                return;
            }

            if (TimerStartAutoSafe >= GameManager.GameTime
                && HeroToSafe != null)
            {
                if (Tether.CanBeCasted(HeroToSafe)
                    && Tether.UseAbility(HeroToSafe, TetherBrake))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Greaves != null && AbilityItemManager.Greaves.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Greaves.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Mekanism != null && AbilityItemManager.Mekanism.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Locket != null && AbilityItemManager.Locket.CanBeCasted(HeroToSafe)
                    && (AbilityItemManager.Locket.UseAbilityOnOwner(HeroToSafe)
                    || AbilityItemManager.Locket.UseAbilityOnFriendTarget(HeroToSafe)))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Wand != null && AbilityItemManager.Wand.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Wand.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Stick != null && AbilityItemManager.Stick.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Stick.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Faerie != null && AbilityItemManager.Faerie.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Faerie.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (Overcharge != null && Overcharge.CanBeCasted(HeroToSafe)
                    && Overcharge.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Bottle != null && AbilityItemManager.Bottle.CanBeCasted(HeroToSafe)
                    && (AbilityItemManager.Bottle.UseAbilityOnOwner(HeroToSafe)
                    || AbilityItemManager.Bottle.UseAbilityOnFriendTarget(HeroToSafe)))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Salve != null && AbilityItemManager.Salve.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Salve.UseAbilityOnOwner(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Lotus != null && AbilityItemManager.Lotus.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Lotus.UseAbilityOnFriendTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Glimmer != null && AbilityItemManager.Glimmer.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Glimmer.UseAbilityOnFriendTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Pipe != null && AbilityItemManager.Pipe.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Pipe.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }

                if (AbilityItemManager.Crimson != null && AbilityItemManager.Crimson.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Crimson.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(100);
                    return;
                }
            }
            HeroToSafe = null;
        }

        public void Dispose()
        {
            ProjectileManager.TrackingProjectileAdded -= ProjectileManager_TrackingProjectileAdded;
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
            Entity.NetworkPropertyChanged -= Entity_NetworkPropertyChanged;
        }
    }
}
