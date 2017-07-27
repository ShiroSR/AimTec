using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Prediction.Health;
using Spell = Aimtec.SDK.Spell;
using Aimtec.SDK.Extensions;
using System.Drawing;
using System.Net;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Events;

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

        private static string[] pMobs = new string[] { "SRU_Baron", "SRU_RiftHerald" };
        private static string[] small = new string[] { "SRU_Blue", "SRU_Red", "SRU_Murkwolf", "SRU_Razorbeak", "SRU_Gromp", "SRU_Krug", "Sru_Crab" };

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
                Spell Smite = new Spell(SummonerSmite.Slot, 560);
                if (Smite.Slot != SpellSlot.Unknown && MenuClass.SmiteMenu["usesmite"].Enabled)
                {
                    if (!MenuClass.SmiteMenu["smiteactive"].Enabled)
                    {
                        if (Render.WorldToScreen(Player.Position, out Vector2 coord) && MenuClass.SmiteMenu["statusdrawing"].Enabled)
                        {
                            coord.Y -= -30;
                            coord.X -= +35;
                            Render.Text(coord.X, coord.Y, Color.Red, "SMITE DISABLED.");
                        }
                        if (MenuClass.SmiteMenu["rangedrawing"].Enabled)
                        {
                            Render.Circle(Player.Position, Smite.Range, 30, Color.Red);
                        }
                    }
                    else
                    {
                        if (Smite.Ready)
                        {
                            if (Render.WorldToScreen(Player.Position, out Vector2 coord) && MenuClass.SmiteMenu["statusdrawing"].Enabled)
                            {
                                coord.Y -= -30;
                                coord.X -= +35;
                                Render.Text(coord.X, coord.Y, Color.Lime, "SMITE READY.");
                            }
                            if (MenuClass.SmiteMenu["rangedrawing"].Enabled)
                            {
                                Render.Circle(Player.Position, Smite.Range, 30, Color.Lime);
                            }
                            foreach (var Obj in ObjectManager.Get<Obj_AI_Minion>().Where(t => t.IsValidTarget(Smite.Range) && SmiteDamages >= t.Health))
                            {
                                if (Obj.UnitSkinName.Contains("Dragon"))
                                {
                                    if (MenuClass.Dragons[Obj.UnitSkinName].Enabled)
                                    {
                                        Smite.Cast(Obj);
                                    }
                                }
                                if (pMobs.Contains(Obj.UnitSkinName))
                                {
                                    if (MenuClass.EpicMonsters[Obj.UnitSkinName].Enabled)
                                    {
                                        Smite.Cast(Obj);
                                    }
                                }
                                if (small.Contains(Obj.UnitSkinName))
                                {
                                    if (MenuClass.Monsters[Obj.UnitSkinName].Enabled)
                                    {
                                        Smite.Cast(Obj);
                                    }
                                }
                            }
                            if (MenuClass.SmiteMenu["damagedrawing"].Enabled)
                            {
                                ObjectManager.Get<Obj_AI_Minion>()
                                    .Where(h => DrawingClass.JungleList.Contains(h.UnitSkinName) && h.IsValidTarget(Smite.Range, true))
                                    .ToList()
                                    .ForEach(
                                        unit =>
                                        {
                                            if (pMobs.Contains(unit.UnitSkinName) && !MenuClass.EpicMonsters[unit.UnitSkinName].Enabled)
                                            {
                                                return;
                                            }
                                            if (unit.UnitSkinName.Contains("Dragon") && !MenuClass.Dragons[unit.UnitSkinName].Enabled)
                                            {
                                                return;
                                            }
                                            if (small.Contains(unit.UnitSkinName) && !MenuClass.Monsters[unit.UnitSkinName].Enabled)
                                            {
                                                return;
                                            }
                                            var jungleList = DrawingClass.JungleList;
                                            var mobOffset = DrawingClass.JungleHpBarOffsetList1080p.FirstOrDefault(x => x.UnitSkinName.Equals(unit.UnitSkinName));
                                            int width;
                                            if (jungleList.Contains(unit.UnitSkinName))
                                            {
                                                width = mobOffset != null ? mobOffset.Width : DrawingClass.SWidth;
                                            }
                                            else
                                            {
                                                width = DrawingClass.SWidth;
                                            }

                                            int height;
                                            if (jungleList.Contains(unit.UnitSkinName))
                                            {
                                                height = mobOffset != null ? mobOffset.Height : DrawingClass.SHeight;
                                            }
                                            else
                                            {
                                                height = DrawingClass.SHeight;
                                            }

                                            var barPos = unit.FloatingHealthBarPosition;

                                            var drawEndXPos = (float)(barPos.X + (unit.Health > SmiteDamages
                                                                          ? width * ((0 + SmiteDamages) / unit.MaxHealth * 100 / 100)
                                                                          : 0));
                                            var drawStartXPos = barPos.X + width * (unit.MaxHealth * 0);

                                            Render.Line(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, height, true, unit.Health < SmiteDamages ? Color.Blue : Color.Orange);
                                            Render.Line(drawEndXPos, barPos.Y, drawEndXPos, barPos.Y + height + 1, 1, true, Color.Lime);
                                        });
                            }
                        }
                        else
                        {
                            if (Render.WorldToScreen(Player.Position, out Vector2 coord) && MenuClass.SmiteMenu["statusdrawing"].Enabled)
                            {
                                coord.Y -= -30;
                                coord.X -= +55;
                                Render.Text(coord.X, coord.Y, Color.DarkViolet, "SMITE ON COOLDOWN.");
                            }
                            if (MenuClass.SmiteMenu["rangedrawing"].Enabled)
                            {
                                Render.Circle(Player.Position, Smite.Range, 30, Color.DarkViolet);
                            }
                        }
                    }
                }
            }
    }
    }
}
