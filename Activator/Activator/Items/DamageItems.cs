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
    class DamageItems
    {
        public DamageItems()
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

            var ItemCutlass = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "BilgewaterCutlass");
            if (ItemCutlass != null)
            {
                Spell Cutlass = new Spell(ItemCutlass.Slot, 550);
                if (Menus.Menu["items"]["damageitems"]["usecutlass"].Enabled)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Cutlass.Range, true));
                    foreach (var enemy in Enemies.Where(e =>
                    // TODO: CHANGE LOGICS
                    e.Health <= Player.Health && Player.CountEnemyHeroesInRange(1000) <= 1 ||
                    e.IsFacing(Player) && e.Health >= Player.Health && Player.CountEnemyHeroesInRange(1000) <= 1 ||
                    e.TotalAttackDamage >= Player.TotalAttackDamage && Player.CountEnemyHeroesInRange(1000) == 2 ||
                    e.IsFacing(Player) && e.Health >= Player.Health && Player.CountEnemyHeroesInRange(1000) >= 3 ||
                    e.IsFacing(Player) && e.TotalAttackDamage >= Player.TotalAttackDamage && Player.CountEnemyHeroesInRange(1000) <= 3))
                    {
                        if (Menus.Menu["items"]["damageitems"]["onlycombo"].Enabled && !GlobalKeys.ComboKey.Active)
                        {
                            return;
                        }
                        Cutlass.Cast(enemy);
                    }
                }
            }

            var ItemBOTRK = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "ItemSwordOfFeastAndFamine");
            if (ItemBOTRK != null)
            {
                Spell BOTRK = new Spell(ItemBOTRK.Slot, 550);
                if (Menus.Menu["items"]["damageitems"]["usebotrk"].Enabled)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(BOTRK.Range, true));
                    foreach (var enemy in Enemies.Where(e =>
                    e.Health <= Player.Health && Player.CountEnemyHeroesInRange(1000) <= 1 ||
                    e.IsFacing(Player) && e.Health >= Player.Health && Player.CountEnemyHeroesInRange(1000) <= 1 ||
                    e.TotalAttackDamage >= Player.TotalAttackDamage && Player.CountEnemyHeroesInRange(1000) == 2 ||
                    e.IsFacing(Player) && e.Health >= Player.Health && Player.CountEnemyHeroesInRange(1000) >= 3 ||
                    e.IsFacing(Player) && e.TotalAttackDamage >= Player.TotalAttackDamage && Player.CountEnemyHeroesInRange(1000) <= 3))
                    {
                        if (Menus.Menu["items"]["damageitems"]["onlycombo"].Enabled && !GlobalKeys.ComboKey.Active)
                        {
                            return;
                        }
                        BOTRK.Cast(enemy);
                    }
                }
            }

            var ItemHextechGLP = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "ItemWillBoltSpellBase");
            if (ItemHextechGLP != null)
            {
                Spell GLP = new Spell(ItemHextechGLP.Slot, 850);
                if (Menus.Menu["items"]["damageitems"]["useglp"].Enabled)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(GLP.Range, true));
                    foreach (var enemy in Enemies.Where(e => Player.CountEnemyHeroesInRange(GLP.Range) >= Menus.Menu["items"]["damageitems"]["glpslider"].Value))
                    {
                        if (Menus.Menu["items"]["damageitems"]["onlycombo"].Enabled && !GlobalKeys.ComboKey.Active)
                        {
                            return;
                        }
                        GLP.Cast(enemy);
                    }
                }
            }

            var ItemGunblade = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "HextechGunblade");
            if (ItemGunblade != null)
            {
                Spell Gunblade = new Spell(ItemGunblade.Slot, 700);
                if (Menus.Menu["items"]["damageitems"]["usegunblade"].Enabled)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Gunblade.Range, true));
                    foreach (var enemy in Enemies.Where(e => e.Health <= 200 &&
                    Menus.Menu["items"]["damageitems"]["gunbladewhitelist"][e.ChampionName.ToLower()].As<MenuBool>().Enabled))
                    {
                        if (HealthPrediction.Implementation.GetPrediction(enemy, 100 + Game.Ping) <= enemy.MaxHealth / 0)
                        {
                            Gunblade.Cast(enemy);
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
