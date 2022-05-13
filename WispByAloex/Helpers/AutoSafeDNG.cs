
using Wisp.Info;
using Wisp;
using WispByAloex.Items;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Extensions;
using Wisp.Spells;
using Divine.Helpers;
using Divine.Entity.Entities;
using Divine.Entity.Entities.EventArgs;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Entity.Entities.Components;
using Divine.Game;
using WispByAloex.Extensions;
using Divine.Update;
using Divine.Entity.Entities.Abilities;
using Divine.Numerics;
using Divine.Renderer;
using Divine.Particle;
using WispByAloex.Draw.Panels;
using WispByAloex.Draw;
using Divine.Input;

namespace WispByAloex.Helpers
{
    internal class AutoSafeDNG
    {
        private Init Init;
        private MainMenu MainMenu;
        private AbilityItemManager AbilityItemManager;
        private MainSettings MainSettings;
        public Sleeper TimerToButton = new Sleeper();
        private Tether Tether;
        private TetherBrake TetherBrake;
        private Overcharge Overcharge;

        public UpPanel UpPanel { get; private set; }

        private Hero HeroToSafe;
        public Hero SourceAnim;
        public float TimerStartAutoSafeDNG { get; private set; }
        public float AbilityDmg { get; private set; }
        public float TimerToCheck { get; private set; }
        public float LastAbilCheck { get; private set; }

        public AutoSafeDNG(Init init, MainMenu mainMenu, AbilityItemManager abilityItemManager, MainSettings mainSettings)
        {
            Init = init;
            MainMenu = mainMenu;
            AbilityItemManager = abilityItemManager;
            MainSettings = mainSettings;

            Tether = new Tether(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether), MainMenu);
            TetherBrake = new TetherBrake(Init.MyEntity.GetAbilityById(AbilityId.wisp_tether_break));
            Overcharge = new Overcharge(Init.MyEntity.GetAbilityById(AbilityId.wisp_overcharge), MainMenu, Init);

            Entity.NetworkPropertyChanged += Entity_NetworkPropertyChanged;
            UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
        }

        private void Entity_NetworkPropertyChanged(Entity sender, NetworkPropertyChangedEventArgs e)
        {
            if (!MainMenu.WispAutoSafeDngEnble.Value)
            {
                return;
            }

            if (e.PropertyName != "m_bInAbilityPhase")
            {
                return;
            }

            if (!e.NewValue.GetBoolean())
            {
                return;
            }

            UpdateManager.BeginInvoke(() =>
            {
                if (sender.Owner is not Hero hero)
                {
                    return;
                }

                if (hero.Name == "npc_dota_hero_axe"
                    && sender.Name == "axe_culling_blade"
                    && hero.Position.Distance2D(Init.MyEntity.Position) <
                    Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).CastRange + hero.GetAbilityById(AbilityId.axe_culling_blade).CastRange
                    && MainSettings.AxeUltRectEnable)
                {
                    AbilityDmg = hero.GetAbilityById(AbilityId.axe_culling_blade).GetAbilitySpecialDataWithTalent("damage", AbilityId.special_bonus_unique_axe_5);

                    TimerToCheck = GameManager.GameTime + 1 + GameManager.Ping;
                    SourceAnim = hero;
                }
                
                if (hero.Name == "npc_dota_hero_lina"
                    && sender.Name == "lina_laguna_blade"
                    && hero.Position.Distance2D(Init.MyEntity.Position) <
                    Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).CastRange + hero.GetAbilityById(AbilityId.lina_laguna_blade).CastRange
                    && MainSettings.LinaUltUltRectEnable)
                {
                    AbilityDmg = hero.GetAbilityById(AbilityId.lina_laguna_blade).GetAbilitySpecialData("damage");
                    
                    TimerToCheck = GameManager.GameTime + 0.35f + GameManager.Ping;
                    SourceAnim = hero;
                }

                if (hero.Name == "npc_dota_hero_lion"
                    && sender.Name == "lion_finger_of_death"
                    && hero.Position.Distance2D(Init.MyEntity.Position) <
                    Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).CastRange + hero.GetAbilityById(AbilityId.lion_finger_of_death).CastRange
                    && MainSettings.LionUltRectEnable)
                {
                    AbilityDmg = hero.GetAbilityById(AbilityId.lion_finger_of_death).GetAbilitySpecialData("damage");
                    
                    TimerToCheck = GameManager.GameTime + 0.35f + GameManager.Ping;
                    SourceAnim = hero;
                }

                if (hero.Name == "npc_dota_hero_queenofpain"
                    && sender.Name == "queenofpain_sonic_wave"
                    && hero.Position.Distance2D(Init.MyEntity.Position) <
                    Init.MyEntity.GetAbilityById(AbilityId.wisp_tether).CastRange + hero.GetAbilityById(AbilityId.queenofpain_sonic_wave).CastRange
                    && MainSettings.QueenUltRectEnable)
                {
                    if (hero.HasModifier("modifier_item_ultimate_scepter"))
                    {
                        AbilityDmg = hero.GetAbilityById(AbilityId.queenofpain_sonic_wave).GetAbilitySpecialData("damage_scepter");
                    }
                    else
                    {
                        AbilityDmg = hero.GetAbilityById(AbilityId.queenofpain_sonic_wave).GetAbilitySpecialData("damage");

                    }
                    
                    TimerToCheck = GameManager.GameTime + 0.35f + GameManager.Ping;
                    SourceAnim = hero;
                }
            });
        }

        private void UpdateManager_IngameUpdate()
        {
            if (!MainMenu.WispAutoSafeDngEnble.Value)
            {
                return;
            }
            if (TimerToCheck >= GameManager.GameTime
                && SourceAnim != null
                && !SourceAnim.IsRotating())
            {
                if (SourceAnim.Name == "npc_dota_hero_axe")
                {
                    var target = Init.DicUIInfo.Values
                       .Where(x => x.Enable && x.EntInfo.Distance2D(SourceAnim.Position) < (SourceAnim.GetAbilityById(AbilityId.axe_culling_blade).CastRange + 150)
                            && SourceAnim.IsNetworkDirectlyFacing(x.EntInfo) < 30)
                       .OrderBy(x => x.EntInfo.Distance2D(SourceAnim.Position))
                       .OrderBy(x => SourceAnim.IsNetworkDirectlyFacing(x.EntInfo) < 30)
                       .Select(x => x.EntInfo)
                       .FirstOrDefault();

                    if (target == null)
                    {
                        return;
                    }

                    if (AbilityDmg + 50 > target.Health
                        && Init.MyEntity.TurnTime(target.Position) < SourceAnim.GetAbilityById(AbilityId.axe_culling_blade).CastPoint)
                    {
                        HeroToSafe = target;
                        TimerStartAutoSafeDNG = GameManager.GameTime + 3 + GameManager.Ping;
                    }
                }
                else if (SourceAnim.Name == "npc_dota_hero_lina")
                {
                    var target = Init.DicUIInfo.Values
                    .Where(x => x.Enable && x.EntInfo.Distance2D(SourceAnim.Position) < (SourceAnim.GetAbilityById(AbilityId.lina_laguna_blade).CastRange + 50)
                        && x.EntInfo.IsInRange(SourceAnim.InFront(SourceAnim.Distance2D(x.EntInfo)), x.EntInfo.HullRadius + 30))
                    .OrderBy(x => x.EntInfo.Distance2D(SourceAnim.Position) < (SourceAnim.GetAbilityById(AbilityId.lina_laguna_blade).CastRange + 50))
                    .OrderBy(x => x.EntInfo.IsInRange(SourceAnim.InFront(SourceAnim.Distance2D(x.EntInfo)), x.EntInfo.HullRadius + 50))
                    .Select(x => x.EntInfo)
                    .FirstOrDefault();

                    if (target == null)
                    {
                        return;
                    }

                    if (SourceAnim.HasModifier("modifier_item_ultimate_scepter"))
                    {
                        LastAbilCheck = AbilityDmg * (1f + SourceAnim.GetSpellAmplification());
                    }
                    else
                    {
                        LastAbilCheck = SourceAnim.CalculateSpellDamage(target, DamageType.Magical, AbilityDmg);
                    }

                    if (LastAbilCheck + 75 > target.Health
                        && Init.MyEntity.TurnTime(target.Position) < SourceAnim.GetAbilityById(AbilityId.lina_laguna_blade).CastPoint
                        && TimerStartAutoSafeDNG <= GameManager.GameTime)
                    {
                        HeroToSafe = target;
                        TimerStartAutoSafeDNG = GameManager.GameTime + 3 + GameManager.Ping;
                    }   
                }
                else if (SourceAnim.Name == "npc_dota_hero_lion")
                {
                    var target = Init.DicUIInfo.Values
                    .Where(x => x.Enable && x.EntInfo.Distance2D(SourceAnim.Position) < (SourceAnim.GetAbilityById(AbilityId.lion_finger_of_death).CastRange + 50)
                        && x.EntInfo.IsInRange(SourceAnim.InFront(SourceAnim.Distance2D(x.EntInfo)), x.EntInfo.HullRadius + 30))
                    .OrderBy(x => x.EntInfo.Distance2D(SourceAnim.Position) < (SourceAnim.GetAbilityById(AbilityId.lion_finger_of_death).CastRange + 50))
                    .OrderBy(x => x.EntInfo.IsInRange(SourceAnim.InFront(SourceAnim.Distance2D(x.EntInfo)), x.EntInfo.HullRadius + 50))
                    .Select(x => x.EntInfo)
                    .FirstOrDefault();

                    if (target == null)
                    {
                        return;
                    }

                    if (SourceAnim.HasModifier("modifier_item_ultimate_scepter"))
                    {
                        LastAbilCheck = (AbilityDmg + 100) * (1f + SourceAnim.GetSpellAmplification());
                    }
                    else
                    {
                        LastAbilCheck = SourceAnim.CalculateSpellDamage(target, DamageType.Magical, AbilityDmg);
                    }
                   
                    if (LastAbilCheck + 75 > target.Health
                        && Init.MyEntity.TurnTime(target.Position) < SourceAnim.GetAbilityById(AbilityId.lion_finger_of_death).CastPoint
                        && TimerStartAutoSafeDNG <= GameManager.GameTime)
                    {
                        HeroToSafe = target;
                        TimerStartAutoSafeDNG = GameManager.GameTime + 3 + GameManager.Ping;
                    }
                }
                else if (SourceAnim.Name == "npc_dota_hero_queenofpain")
                {
                    var target = Init.DicUIInfo.Values
                    .Where(x => x.Enable && x.EntInfo.Distance2D(SourceAnim.Position) < (SourceAnim.GetAbilityById(AbilityId.queenofpain_sonic_wave).CastRange + 50)
                        && SourceAnim.IsNetworkDirectlyFacing(x.EntInfo) < 25)
                    .OrderBy(x => x.EntInfo.Distance2D(SourceAnim.Position) < (SourceAnim.GetAbilityById(AbilityId.queenofpain_sonic_wave).CastRange + 50))
                    .OrderBy(x => SourceAnim.IsNetworkDirectlyFacing(x.EntInfo) < 25)
                    .Select(x => x.EntInfo)
                    .FirstOrDefault();

                    if (target == null)
                    {
                        return;
                    }

                    LastAbilCheck = SourceAnim.CalculateSpellDamage(target, DamageType.Pure, AbilityDmg);
                    Console.WriteLine(LastAbilCheck);
                    if (LastAbilCheck + 75 > target.Health
                        && Init.MyEntity.TurnTime(target.Position) < SourceAnim.GetAbilityById(AbilityId.queenofpain_sonic_wave).CastPoint
                        && TimerStartAutoSafeDNG <= GameManager.GameTime)
                    {
                        HeroToSafe = target;
                        TimerStartAutoSafeDNG = GameManager.GameTime + 3 + GameManager.Ping;
                    }
                }
            }

            if (AbilityItemManager.TimerToButton.Sleeping)
            {
                return;
            }

            if (TimerStartAutoSafeDNG >= GameManager.GameTime
                && HeroToSafe != null)
            {
                if (Tether.CanBeCasted(HeroToSafe)
                    && Tether.UseAbility(HeroToSafe, TetherBrake))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Greaves != null && AbilityItemManager.Greaves.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Greaves.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Mekanism != null && AbilityItemManager.Mekanism.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Locket != null && AbilityItemManager.Locket.CanBeCasted(HeroToSafe)
                    && (AbilityItemManager.Locket.UseAbilityOnOwner(HeroToSafe)
                    || AbilityItemManager.Locket.UseAbilityOnFriendTarget(HeroToSafe)))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Wand != null && AbilityItemManager.Wand.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Wand.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Stick != null && AbilityItemManager.Stick.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Stick.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Faerie != null && AbilityItemManager.Faerie.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Faerie.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (Overcharge != null && Overcharge.CanBeCasted(HeroToSafe)
                    && Overcharge.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Bottle != null && AbilityItemManager.Bottle.CanBeCasted(HeroToSafe)
                    && (AbilityItemManager.Bottle.UseAbilityOnOwner(HeroToSafe)
                    || AbilityItemManager.Bottle.UseAbilityOnFriendTarget(HeroToSafe)))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Salve != null && AbilityItemManager.Salve.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Salve.UseAbilityOnOwner(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Lotus != null && AbilityItemManager.Lotus.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Lotus.UseAbilityOnFriendTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Glimmer != null && AbilityItemManager.Glimmer.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Glimmer.UseAbilityOnFriendTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Pipe != null && AbilityItemManager.Pipe.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Pipe.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }

                if (AbilityItemManager.Crimson != null && AbilityItemManager.Crimson.CanBeCasted(HeroToSafe)
                    && AbilityItemManager.Crimson.UseAbilityNoTarget(HeroToSafe))
                {
                    TimerToButton.Sleep(75);
                    return;
                }
            }
            HeroToSafe = null;
        }

        public void Dispose()
        {
            Entity.NetworkPropertyChanged -= Entity_NetworkPropertyChanged;
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }
    }
}
