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
using Aimtec.SDK.Extensions;
using System.Drawing;

namespace Activator.Spells
{
    class Smite
    {
        private static int SmiteDamages
        {
            get
            {
                int[] Dmg = new int[] { 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 };

                return Dmg[Player.Level - 1];
            }
        }

        private static int SmiteDamagesChamp
        {
            get
            {
                int[] Dmg = new int[] { 28, 36, 44, 52, 60, 68, 76, 84, 92, 100, 108, 116, 124, 132, 140, 148, 156, 166 };

                return Dmg[Player.Level - 1];
            }
        }

        private static string[] pMobs = new string[] { "SRU_Baron", "SRU_Blue", "SRU_Red", "SRU_RiftHerald" };
        private static string[] small = new string[] { "SRU_Murkwolf", "SRU_Razorbeak", "SRU_Gromp", "SRU_Krug", "Sru_Crab" };


        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();

        public Smite()
        {
            Game.OnUpdate += OnUpdate;
        }

        public static void OnUpdate()
        {
            var SummonerSmite = Player.SpellBook.Spells.Where(o => o != null && o.SpellData != null).FirstOrDefault(o => o.SpellData.Name == "SummonerSmite");
            if (SummonerSmite != null)
            {
                Spell Smite = new Spell(SummonerSmite.Slot, 500);
                if (Smite.Slot != SpellSlot.Unknown && Menus.Menu["summoner"]["smitemenu"]["usesmite"].Enabled)
                {
                    if (!Menus.Menu["summoner"]["smitemenu"]["smiteactive"].Enabled)
                    {
                        if (Render.WorldToScreen(Player.Position, out Vector2 coord) && Menus.Menu["summoner"]["smitemenu"]["statusdrawing"].Enabled)
                        {
                            coord.Y -= -30;
                            Render.Text(coord.X, coord.Y, Color.Red, "SMITE DISABLED.");
                        }
                        return;
                    }
                    else
                    {
                        if (Render.WorldToScreen(Player.Position, out Vector2 coord) && Menus.Menu["summoner"]["smitemenu"]["statusdrawing"].Enabled)
                        {
                            coord.Y -= -30;
                            Render.Text(coord.X, coord.Y, Color.Green, "SMITE READY.");
                        }
                    }
                    foreach (var Obj in ObjectManager.Get<Obj_AI_Minion>().Where(t => t.IsValidTarget(Smite.Range) && SmiteDamages >= t.Health))
                    {
                        if (Obj.UnitSkinName.Contains("Dragon"))
                        {
                            if (Menus.Menu["summoner"]["smitemenu"]["dragons"][Obj.UnitSkinName].Enabled)
                            {
                                Smite.Cast(Obj);
                            }
                        }
                        if (pMobs.Contains(Obj.UnitSkinName))
                        {
                            if (Menus.Menu["summoner"]["smitemenu"]["epicmonsters"][Obj.UnitSkinName].Enabled)
                            {
                                Smite.Cast(Obj);
                            }
                        }
                        if (small.Contains(Obj.UnitSkinName))
                        {
                            if (Menus.Menu["summoner"]["smitemenu"]["normalmonsters"][Obj.UnitSkinName].Enabled)
                            {
                                Smite.Cast(Obj);
                            }
                        }
                    }
                }
            }
        }
    }
}
