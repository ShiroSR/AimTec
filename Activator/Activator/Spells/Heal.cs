using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using static Activator.GeneralMenu.General;
using Aimtec.SDK.Prediction.Health;
using Spell = Aimtec.SDK.Spell;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK.Extensions;

namespace Activator.Spells
{
    class Heal
    {
        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();

        public Heal()
        {
            Game.OnUpdate += OnUpdate;
        }

        public static void OnUpdate()
        {
            if (Player.IsDead)
            {
                return;
            }

            var SummonerHeal = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "SummonerHeal");
            if (SummonerHeal != null)
            {
                Spell Heal = new Spell(SummonerHeal.Slot, 850);
                if (Heal.Slot != SpellSlot.Unknown && Menus.Menu["summoner"]["healmenu"]["useheal"].Enabled)
                {
                    var Allies = GameObjects.AllyHeroes.Where(t => !t.IsMe && t.IsValidTarget(Heal.Range, true));
                    foreach (var ally in Allies.Where(a => Menus.Menu["summoner"]["healmenu"]["healwhitelist"][a.ChampionName.ToLower()].As<MenuBool>().Enabled &&
                        Menus.Menu["summoner"]["healmenu"]["useallies"].Enabled &&
                        a.CountEnemyHeroesInRange(Player.AttackRange) >= 1 &&
                        a.Health <= a.MaxHealth / 100 * Menus.Menu["summoner"]["healmenu"]["healcustom"]["healpercent"].Value))
                    {
                        Heal.Cast();
                    }
                    if (HealthPrediction.Implementation.GetPrediction(Player, 250 + Game.Ping) <= Player.MaxHealth / 100 * Menus.Menu["summoner"]["healmenu"]["healpercent"].Value &&
                        Player.CountEnemyHeroesInRange(1000) >= 1
                        || HealthPrediction.Implementation.GetPrediction(Player, 250 + Game.Ping) <= Player.MaxHealth / 0)
                    {
                        Heal.Cast();
                    }
                }
            }
        }
    }
}
