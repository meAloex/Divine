using Divine.Entity;
using Divine.Entity.Entities.Components;
using Divine.Entity.Entities.Players;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Extensions;

namespace Wisp.Info
{
    internal sealed class Init
    {
        public Player GlobalPlayer { get; set; }
        public Hero MyEntity { get; set; }
        public Team MyTeam { get; set; }

        public Dictionary<HeroId, UserInfo> DicUIInfo = new Dictionary<HeroId, UserInfo>();
        public int HeroesNumberTb { get; private set; }

        public Init()
        {
            GlobalPlayer = EntityManager.LocalPlayer;
            MyEntity = EntityManager.LocalHero;
            MyTeam = EntityManager.LocalHero.Team;

            GetFriendsTable();
        }

        public class UserInfo
        {
            public float LimitHp { get; set; }
            public float Safe_Hp { get; set; }
            public bool Enable { get; set; }
            public Hero EntInfo { get; internal set; }
        }

        private void GetFriendsTable()
        {
            var friendAll = EntityManager.GetEntities<Hero>()
                .Where(x => x.Name != "npc_dota_hero_wisp"
                && !x.IsEnemy(MyEntity));

            HeroesNumberTb = 0;
            foreach (var friend in friendAll)
            {
                DicUIInfo.Add(friend.HeroId, new UserInfo { Enable = true, EntInfo = friend, Safe_Hp = MathF.Ceiling(friend.MaximumHealth * 0.28f), LimitHp = MathF.Ceiling(friend.MaximumHealth * 0.75f)});
            }
        }

        public void Dispose()
        {
            // если небходимо очистит
        }
    }
}