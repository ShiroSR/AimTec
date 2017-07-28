using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Prediction.Health;
using Spell = Aimtec.SDK.Spell;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Config;

namespace Activator.Items
{
    class SupportItems
    {
        public SupportItems()
        {
            Game.OnUpdate += OnUpdate;
        }

        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();

        public static void OnUpdate()
        {
            if (Player.IsDead)
            {
                return;
            }

            var ItemRanduins = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "RanduinsOmen");
            if (ItemRanduins != null)
            {
                Spell Randuins = new Spell(ItemRanduins.Slot, 500);
                if (MenuClass.SupportItemsMenu["useranduins"].Enabled && Randuins.Ready)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Randuins.Range, true));
                    foreach (var enemy in Enemies.Where(
                        e => Player.CountEnemyHeroesInRange(Randuins.Range) >=
                             MenuClass.SupportItemsMenu["randuinsslider"].Value))
                    {
                        Randuins.Cast();
                    }
                    if (HealthPrediction.Implementation.GetPrediction(Player, 250 + Game.Ping) <= Player.MaxHealth * 0)
                    {
                        Randuins.Cast();
                    }
                }
            }

            var ItemSolari = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "IronStylus");
            if (ItemSolari != null)
            {
                Spell Solari = new Spell(ItemSolari.Slot, 600);
                if (MenuClass.SupportItemsMenu["usesolari"].Enabled && Solari.Ready)
                {
                    var Allies = GameObjects.AllyHeroes.Where(t => t.IsValidTarget(Solari.Range, true));
                    foreach (var ally in Allies.Where(
                        a => Player.CountAllyHeroesInRange(Solari.Range) >=
                             MenuClass.SupportItemsMenu["solarislider"].Value &&
                             a.Health == a.MaxHealth / 100 *
                             MenuClass.SupportItemsMenu["solarislider2"].Value))
                    {
                        Solari.Cast();
                    }
                    if (HealthPrediction.Implementation.GetPrediction(Player, 250 + Game.Ping) <= Player.MaxHealth * 0)
                    {
                        Solari.Cast();
                    }
                }
            }

            var ItemFaceOfTheMountain = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "HealthBomb");
            if (ItemFaceOfTheMountain != null)
            {
                Spell FOTM = new Spell(ItemFaceOfTheMountain.Slot, 700);
                if (MenuClass.SupportItemsMenu["usefotm"].Enabled && FOTM.Ready)
                {
                    var Allies = GameObjects.AllyHeroes.Where(t => t.IsValidTarget(FOTM.Range, true) && !t.IsMe);
                    foreach (var ally in Allies.Where(
                        a => MenuClass.SupportItemsMenu["fotmwhitelist"][a.ChampionName.ToLower()]
                                 .As<MenuBool>().Enabled &&
                             a.Health <= a.MaxHealth / 100 * MenuClass.SupportItemsMenu["fotmslider"].Value))
                    {
                        FOTM.Cast(ally);
                    }
                    if (HealthPrediction.Implementation.GetPrediction(Player, 250 + Game.Ping) <= Player.MaxHealth * 0)
                    {
                        FOTM.Cast(Player);
                    }
                }
            }
        }
    }
}
