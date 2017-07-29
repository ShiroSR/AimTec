using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Activator.Spells;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Prediction.Health;
using Spell = Aimtec.SDK.Spell;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Config;
using Aimtec.SDK.Orbwalking;

namespace Activator.Items
{
    class DamageItems
    {
        public DamageItems()
        {
            Game.OnUpdate += OnUpdate;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
        }

        public static void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            var ItemTiamatHydra = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "ItemTiamatCleave" || o.SpellData.Name == "ItemTitanicHydraCleave");
            if (ItemTiamatHydra != null)
            {
                if (Player.ChampionName == "Riven")
                {
                    return;
                }
                Spell Tiamat = new Spell(ItemTiamatHydra.Slot, 400);
                if (MenuClass.DamageItemsMenu["usetiamat"].Enabled && Tiamat.Ready)
                {
                    if (MenuClass.DamageItemsMenu["tiamatlaneclear"].Enabled && GlobalKeys.WaveClearKey.Active)
                    {
                        Tiamat.Cast();
                    }
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Tiamat.Range, true) && !t.IsInvulnerable && GlobalKeys.ComboKey.Active);
                    foreach (var enemy in Enemies.Where(
                        e => e.Health <= e.MaxHealth / 100 *
                             MenuClass.DamageItemsMenu["tiamatslider"].Value))
                    {
                        Tiamat.Cast();
                    }
                }
            }
        }

        public static int CountEnemyMinionsInRange(Vector3 vector3, float range)
        {
            return GameObjects.EnemyMinions.Count(h => h.IsValidTarget(range, false, false, vector3));
        }

        public static int CountEnemyMinionsInRange(GameObject unit, float range)
        {
            return unit.Position.CountAllyHeroesInRange(range);
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
                if (MenuClass.DamageItemsMenu["usecutlass"].Enabled && Cutlass.Ready)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Cutlass.Range, true) && !t.IsInvulnerable);
                    foreach (var enemy in Enemies.Where(e =>
                        // TODO: CHANGE LOGICS
                            e.Health <= Player.Health && Player.CountEnemyHeroesInRange(1000) <= 1 ||
                            e.IsFacing(Player) && e.Health >= Player.Health &&
                            Player.CountEnemyHeroesInRange(1000) <= 1 ||
                            e.TotalAttackDamage >= 100 &&
                            Player.CountEnemyHeroesInRange(1000) <= 2 ||
                            e.IsFacing(Player) && e.Health >= Player.Health &&
                            Player.CountEnemyHeroesInRange(1000) >= 3 ||
                            e.TotalAttackDamage >= Player.TotalAttackDamage &&
                            Player.CountEnemyHeroesInRange(1000) <= 3))
                    {
                        if (MenuClass.DamageItemsMenu["onlycombo"].Enabled && !GlobalKeys.ComboKey.Active)
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
                if (MenuClass.DamageItemsMenu["usebotrk"].Enabled && BOTRK.Ready)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(BOTRK.Range, true) && !t.IsInvulnerable);
                    foreach (var enemy in Enemies.Where(e =>
                        // TODO: IMPROVE LOGICS
                            e.Health <= Player.Health && Player.CountEnemyHeroesInRange(1000) <= 1 ||
                            e.IsFacing(Player) && e.Health >= Player.Health &&
                            Player.CountEnemyHeroesInRange(1000) <= 1 ||
                            e.TotalAttackDamage >= 100 &&
                            Player.CountEnemyHeroesInRange(1000) <= 2 ||
                            e.IsFacing(Player) && e.Health >= Player.Health &&
                            Player.CountEnemyHeroesInRange(1000) >= 3 ||
                            e.TotalAttackDamage >= Player.TotalAttackDamage &&
                            Player.CountEnemyHeroesInRange(1000) <= 3))
                    {
                        if (MenuClass.DamageItemsMenu["onlycombo"].Enabled && !GlobalKeys.ComboKey.Active)
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
                if (MenuClass.DamageItemsMenu["useglp"].Enabled && GLP.Ready)
                {
                    var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(GLP.Range, true) && !t.IsInvulnerable);
                    foreach (var enemy in Enemies.Where(e => Player.CountEnemyHeroesInRange(GLP.Range) >= MenuClass.DamageItemsMenu["glpslider"].Value))
                    {
                        if (MenuClass.DamageItemsMenu["onlycombo"].Enabled && !GlobalKeys.ComboKey.Active)
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
                if (MenuClass.DamageItemsMenu["usegunblade"].Enabled && Gunblade.Ready)
                {
                    if (MenuClass.DamageItemsMenu["onlycombo"].Enabled && !GlobalKeys.ComboKey.Active)
                    {
                        return;
                    }
                    if (MenuClass.DamageItemsMenu["gunbladewhenXhp"].Enabled)
                    {
                        var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Gunblade.Range, true) && !t.IsInvulnerable);
                        foreach (var enemy in Enemies.Where(
                            e => e.Health <= e.MaxHealth / 100 * MenuClass.DamageItemsMenu["gunbladeslider"].Value &&
                                 MenuClass.DamageItemsMenu["gunbladewhitelist"][
                                     e.ChampionName.ToLower()].As<MenuBool>().Enabled))
                        {
                            Gunblade.Cast(enemy);
                        }
                    }
                    else
                    {
                        var Enemies = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(Gunblade.Range, true) && !t.IsInvulnerable);
                        foreach (var enemy in Enemies.Where(e => e.Health <= 200 &&
                                                                 MenuClass.DamageItemsMenu["gunbladewhitelist"][
                                                                     e.ChampionName.ToLower()].As<MenuBool>().Enabled))
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
            var ItemQSS = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "ItemMercurial" || o.SpellData.Name == "QuicksilverSash");
            if (ItemQSS != null)
            {
                Spell QSS = new Spell(ItemQSS.Slot);
                if (MenuClass.DamageItemsMenu["useqss"].Enabled && QSS.Ready)
                {
                    if (Player.HasBuffOfType(BuffType.Stun) && MenuClass.CCMenu["BuffType.Stun"].Enabled ||
                        Player.HasBuffOfType(BuffType.Fear) && MenuClass.CCMenu["BuffType.Fear"].Enabled ||
                        Player.HasBuffOfType(BuffType.Flee) && MenuClass.CCMenu["BuffType.Flee"].Enabled ||
                        Player.HasBuffOfType(BuffType.Snare) && MenuClass.CCMenu["BuffType.Snare"].Enabled ||
                        Player.HasBuffOfType(BuffType.Taunt) && MenuClass.CCMenu["BuffType.Taunt"].Enabled ||
                        Player.HasBuffOfType(BuffType.Charm) && MenuClass.CCMenu["BuffType.Charm"].Enabled)
                    {
                        QSS.Cast();
                    }
                }
            }
        }
    }
}
