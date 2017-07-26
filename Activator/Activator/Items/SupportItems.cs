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
                if (Menus.Menu["items"]["tankitems"]["useranduins"].Enabled)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Randuins.Range, true));
                    foreach (var enemy in Enemies.Where(e => Player.CountEnemyHeroesInRange(Randuins.Range) >= Menus.Menu["items"]["supportitems"]["randuinsslider"].Value))
                    {
                        Randuins.Cast();
                    }
                }
            }

            var ItemSolari = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "IronStylus");
            if (ItemSolari != null)
            {
                Spell Solari = new Spell(ItemSolari.Slot, 600);
                if (Menus.Menu["items"]["supportitems"]["usesolari"].Enabled)
                {
                    var Allies = GameObjects.AllyHeroes.Where(t => t.IsValidTarget(Solari.Range, true));
                    foreach (var ally in Allies.Where(a => Player.CountAllyHeroesInRange(Solari.Range) >= Menus.Menu["items"]["supportitems"]["solarislider"].Value &&
                    a.Health == a.MaxHealth / 100 * Menus.Menu["items"]["supportitems"]["solarislider2"].Value))
                    {
                        Solari.Cast();
                    }
                }
            }

            var ItemFaceOfTheMountain = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "HealthBomb");
            if (ItemFaceOfTheMountain != null)
            {
                Spell FOTM = new Spell(ItemFaceOfTheMountain.Slot, 700);
                if (Menus.Menu["items"]["supportitems"]["usefotm"].Enabled)
                {
                    var Allies = GameObjects.AllyHeroes.Where(t => t.IsValidTarget(FOTM.Range, true) && !t.IsMe);
                    foreach (var ally in Allies.Where(a => Menus.Menu["items"]["supportitems"]["fotmwhitelist"][a.ChampionName.ToLower()].As<MenuBool>().Enabled &&
                    a.Health <= a.MaxHealth / 100 * Menus.Menu["items"]["supportitems"]["fotmslider"].Value))
                    {
                        FOTM.Cast(ally);
                    }
                    if (Player.Health <= 300 && Player.CountAllyHeroesInRange(FOTM.Range) == 0 && Player.CountEnemyHeroesInRange(1000) >= 1)
                    {
                        FOTM.Cast(Player);
                    }
                }
            }
        }
    }
}
