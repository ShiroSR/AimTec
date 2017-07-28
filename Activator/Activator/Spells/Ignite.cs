using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Prediction.Health;
using Spell = Aimtec.SDK.Spell;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Menu.Config;

namespace Activator.Spells
{
    class Ignite
    {
        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();

        public Ignite()
        {
            Game.OnUpdate += OnUpdate;
        }

        public static void OnUpdate()
        {
            // TODO: IMPROVE LOGICS 
            var SummonerIgnite = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "SummonerDot");
            if (SummonerIgnite != null)
            {
                Spell Ignite = new Spell(SummonerIgnite.Slot, 600);
                if (Ignite.Slot != SpellSlot.Unknown && MenuClass.IgniteMenu["useignite"].Enabled)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Ignite.Range, true));
                    foreach (var enemy in Enemies.Where(e => e.Health <= 300
                    && MenuClass.IgniteMenu["ignitewhitelist"][e.ChampionName.ToLower()].As<MenuBool>().Enabled))
                    {
                        if (!GlobalKeys.ComboKey.Active)
                        {
                            return;
                        }
                        if (HealthPrediction.Implementation.GetPrediction(enemy, 100 + Game.Ping) <= enemy.MaxHealth / 0)
                        {
                            Ignite.Cast(enemy);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
