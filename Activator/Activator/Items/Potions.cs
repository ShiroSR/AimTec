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
    class Potions
    {
        public Potions()
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

            var HealthPotion = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "RegenerationPotion" || o.SpellData.Name == "ItemMiniRegenPotion");
            if (HealthPotion != null)
            {
                Spell HealthP = new Spell(HealthPotion.Slot);
                if (MenuClass.PotionsItemsMenu["usepotions"].Enabled && HealthP.Ready)
                {
                    if (Player.Health <= Player.MaxHealth / 100 *
                        MenuClass.PotionsItemsMenu["potionslider"].Value)
                    {
                        if (Player.HasBuff("RegenerationPotion") || Player.HasBuff("ItemMiniRegenPotion"))
                        {
                            return;
                        }
                        HealthP.Cast();
                    }
                }
            }

            var RefillablePotion = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "ItemCrystalFlask");
            if (RefillablePotion != null)
            {
                Spell RefillableP = new Spell(RefillablePotion.Slot);
                if (MenuClass.PotionsItemsMenu["userefillable"].Enabled && RefillableP.Ready)
                {
                    if (Player.Health <= Player.MaxHealth / 100 *
                        MenuClass.PotionsItemsMenu["refillableslider"].Value)
                    {
                        if (Player.HasBuff("ItemCrystalFlask"))
                        {
                            return;
                        }
                        RefillableP.Cast();
                    }
                }
            }
        }
    }
}
