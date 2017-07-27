using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Prediction.Health;
using Spell = Aimtec.SDK.Spell;

namespace Activator.Spells
{
    class Cleanse
    {
        public Cleanse()
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

            var SummonerCleanse = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "SummonerBoost");
            if (SummonerCleanse != null)
            {
                Spell Cleanse = new Spell(SummonerCleanse.Slot);
                if (Cleanse.Slot != SpellSlot.Unknown && MenuClass.CleanseMenu["usecleanse"].Enabled)
                {
                    if (Player.HasBuffOfType(BuffType.Stun) && MenuClass.CCMenu2["BuffType.Stun"].Enabled ||
                        Player.HasBuffOfType(BuffType.Fear) && MenuClass.CCMenu2["BuffType.Fear"].Enabled ||
                        Player.HasBuffOfType(BuffType.Flee) && MenuClass.CCMenu2["BuffType.Flee"].Enabled ||
                        Player.HasBuffOfType(BuffType.Snare) && MenuClass.CCMenu2["BuffType.Snare"].Enabled ||
                        Player.HasBuffOfType(BuffType.Taunt) && MenuClass.CCMenu2["BuffType.Taunt"].Enabled ||
                        Player.HasBuffOfType(BuffType.Charm) && MenuClass.CCMenu2["BuffType.Charm"].Enabled)
                    {
                        Cleanse.Cast();
                    }
                }
            }
        }
    }
}
