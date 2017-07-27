using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;

namespace Activator
{
    class SpellMenu
    {
        public SpellMenu()
        {
            MenuClass.HealMenu = new Menu("healmenu", "Heal");
            {
                MenuClass.HealMenu.Add(new MenuBool("useheal", "Use Heal", true));
                MenuClass.HealMenu.Add(new MenuBool("useallies", "Use Heal on Allies", false));
                MenuClass.WhitelistAllies = new Menu("healwhitelist", "Heal Whitelist:");
                MenuClass.HealMenu.Add(MenuClass.WhitelistAllies);
                if (GameObjects.AllyHeroes.Any(t => !t.IsMe))
                {
                    foreach (var ally in GameObjects.AllyHeroes.Where(t => !t.IsMe))
                    {
                        MenuClass.WhitelistAllies.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName));
                    }
                }
                else
                {
                    MenuClass.WhitelistAllies.Add(new MenuSeperator("separator", "No allies found."));
                }

                MenuClass.HealMenu.Add(new MenuSeperator("healcustom", "Heal Settings"));
                MenuClass.HealMenu.Add(new MenuSlider("healpercent", "Use Heal when HP % is less than:", 10, 0, 100));
            }
            MenuClass.SpellMenu.Add(MenuClass.HealMenu);

            MenuClass.BarrierMenu = new Menu("barriermenu", "Barrier");
            {
                MenuClass.BarrierMenu.Add(new MenuBool("usebarrier", "Use Barrier", true));
                MenuClass.BarrierMenu.Add(new MenuSeperator("separator", "Barrier Settings"));
                MenuClass.BarrierMenu.Add(new MenuSlider("barrierslider", "Use Barrier when HP% is less than", 20, 0, 100));
            }
            MenuClass.SpellMenu.Add(MenuClass.BarrierMenu);

            MenuClass.SmiteMenu = new Menu("smitemenu", "Smite");
            {
                MenuClass.SmiteMenu.Add(new MenuBool("usesmite", "Use Smite", true));
                MenuClass.SmiteMenu.Add(new MenuKeyBind("smiteactive", "AutoSmite", Aimtec.SDK.Util.KeyCode.M, KeybindType.Toggle, true));
                MenuClass.SmiteMenu.Add(new MenuSeperator("separator", "Smite Settings"));
                MenuClass.SmiteMenu.Add(new MenuBool("rangedrawing", "Draw Smite range", false));
                MenuClass.SmiteMenu.Add(new MenuBool("statusdrawing", "Draw Smite status", true));
                MenuClass.SmiteMenu.Add(new MenuBool("damagedrawing", "Draw Smite damage", true));
                MenuClass.Dragons = new Menu("dragons", "Use Smite on these Drakes:");
                {
                    MenuClass.Dragons.Add(new MenuBool("SRU_Dragon_Air", "Cloud Drake"));
                    MenuClass.Dragons.Add(new MenuBool("SRU_Dragon_Fire", "Fire Drake"));
                    MenuClass.Dragons.Add(new MenuBool("SRU_Dragon_Earth", "Earth Drake"));
                    MenuClass.Dragons.Add(new MenuBool("SRU_Dragon_Water", "Water Drake"));
                    MenuClass.Dragons.Add(new MenuBool("SRU_Dragon_Elder", "Elder Drake"));
                }
                MenuClass.EpicMonsters = new Menu("epicmonsters", "Use Smite on these Epic Camps:");
                {
                    MenuClass.EpicMonsters.Add(new MenuBool("SRU_Baron", "Baron"));
                    MenuClass.EpicMonsters.Add(new MenuBool("SRU_RiftHerald", "Rift Herald", false));
                }
                MenuClass.Monsters = new Menu("normalmonsters", "Use Smite on these Camps:");
                {
                    MenuClass.Monsters.Add(new MenuBool("SRU_Blue", "Blue Sentinel"));
                    MenuClass.Monsters.Add(new MenuBool("SRU_Red", "Red Brambleback"));
                    MenuClass.Monsters.Add(new MenuBool("SRU_Gromp", "Gromp", false));
                    MenuClass.Monsters.Add(new MenuBool("SRU_Murkwolf", "Wolves", false));
                    MenuClass.Monsters.Add(new MenuBool("SRU_Krug", "Krug", false));
                    MenuClass.Monsters.Add(new MenuBool("SRU_Razorbeak", "Razor", false));
                    MenuClass.Monsters.Add(new MenuBool("Sru_Crab", "Crab", false));
                }
                MenuClass.SmiteMenu.Add(MenuClass.Dragons);
                MenuClass.SmiteMenu.Add(MenuClass.EpicMonsters);
                MenuClass.SmiteMenu.Add(MenuClass.Monsters);
            }
            MenuClass.SpellMenu.Add(MenuClass.SmiteMenu);

            MenuClass.CleanseMenu = new Menu("cleansemenu", "Cleanse");
            {
                MenuClass.CleanseMenu.Add(new MenuBool("usecleanse", "Use Cleanse", true));
                MenuClass.CCMenu2 = new Menu("ccmenu", "Use Cleanse on CC Type:");
                {
                    MenuClass.CCMenu2.Add(new MenuBool("BuffType.Stun", "Stun"));
                    MenuClass.CCMenu2.Add(new MenuBool("BuffType.Fear", "Fear"));
                    MenuClass.CCMenu2.Add(new MenuBool("BuffType.Flee", "Flee"));
                    MenuClass.CCMenu2.Add(new MenuBool("BuffType.Snare", "Snare"));
                    MenuClass.CCMenu2.Add(new MenuBool("BuffType.Taunt", "Taunt"));
                    MenuClass.CCMenu2.Add(new MenuBool("BuffType.Charm", "Charm"));
                }
                MenuClass.CleanseMenu.Add(MenuClass.CCMenu2);
            }
            MenuClass.SpellMenu.Add(MenuClass.CleanseMenu);

            MenuClass.IgniteMenu = new Menu("ignitemenu", "Ignite");
            {
                MenuClass.IgniteMenu.Add(new MenuBool("useignite", "Use Ignite", true));
                MenuClass.WhitelistEnemies = new Menu("ignitewhitelist", "Ignite Whitelist:");
                MenuClass.IgniteMenu.Add(MenuClass.WhitelistEnemies);
                if (GameObjects.EnemyHeroes.Any())
                {
                    foreach (var enemy in GameObjects.EnemyHeroes)
                    {
                        MenuClass.WhitelistEnemies.Add(new MenuBool(enemy.ChampionName.ToLower(), "Use for: " + enemy.ChampionName));
                    }
                }
                else
                {
                    MenuClass.WhitelistEnemies.Add(new MenuSeperator("separator", "No enemies found."));
                }
            }
            MenuClass.SpellMenu.Add(MenuClass.IgniteMenu);
        }
    }
}
