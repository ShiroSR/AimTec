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

            var HealthPotion = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "RegenerationPotion");
            if (HealthPotion != null)
            {
                Spell HealthP = new Spell(HealthPotion.Slot);
                if (Menus.Menu["items"]["potionsitems"]["usepotions"].Enabled)
                {
                    if (Player.Health <= Player.MaxHealth / 100 * Menus.Menu["items"]["potionsitems"]["potionslider"].Value)
                    {
                        HealthP.Cast();
                    }
                }
            }

            var RefillablePotion = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "ItemCrystalFlask");
            if (RefillablePotion != null)
            {
                Spell RefillableP = new Spell(RefillablePotion.Slot);
                if (Menus.Menu["items"]["potionsitems"]["userefillable"].Enabled)
                {
                    if (Player.Health <= Player.MaxHealth / 100 * Menus.Menu["items"]["potionsitems"]["refillableslider"].Value)
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
