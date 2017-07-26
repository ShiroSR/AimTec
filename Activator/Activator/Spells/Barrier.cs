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

namespace Activator.Spells
{
    class Barrier
    {
        public Barrier()
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

            var SummonerBarrier = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "SummonerBarrier");
            if (SummonerBarrier != null)
            {
                Spell Barrier = new Spell(SummonerBarrier.Slot);
                if (Barrier.Slot != SpellSlot.Unknown && Menus.Menu["summoner"]["barriermenu"]["usebarrier"].Enabled)
                {
                    if (HealthPrediction.Implementation.GetPrediction(Player, 250 + Game.Ping) <= Player.MaxHealth / 100 * Menus.Menu["summoner"]["barriermenu"]["barrierslider"].Value)
                    {
                        Barrier.Cast();
                    }
                }
            }
        }
    }
}
