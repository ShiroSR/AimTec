using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK;
using Aimtec;
using Activator;
using Activator.GeneralMenu;
using static Activator.GeneralMenu.General;

namespace Activator.GeneralMenu
{
    class SpellMenus
    {
        public SpellMenus()
        {
            var SummonerMenu = new Menu("summoner", "Summoner Spells");
            {
                var HealMenu = new Menu("healmenu", "Heal");
                {
                    HealMenu.Add(new MenuBool("useheal", "Use Heal", true));
                    HealMenu.Add(new MenuBool("useallies", "Use Heal on Allies", false));

                    if (GameObjects.AllyHeroes.Any(t => !t.IsMe))
                    {
                        ActivatorClass.Whitelist = new Menu("healwhitelist", "Heal Whitelist:");
                        {
                            foreach (var ally in GameObjects.AllyHeroes.Where(t => !t.IsMe))
                            {
                                ActivatorClass.Whitelist.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName));
                            }
                        }
                        HealMenu.Add(ActivatorClass.Whitelist);
                    }
                    else
                    {
                        HealMenu.Add(new MenuSeperator("separator", "No allies found."));
                    }

                    HealMenu.Add(new MenuSeperator("healcustom", "Heal Settings"));
                    HealMenu.Add(new MenuSeperator("sepaarator", "Use Heal when HP% is less than:"));
                    HealMenu.Add(new MenuSlider("healpercent", ">= 1 Enemy in range", 10, 0, 100));
                    HealMenu.Add(new MenuSlider("healpercent2", "No enemies in range", 5, 0, 100));
                }
                SummonerMenu.Add(HealMenu);

                var BarrierMenu = new Menu("barriermenu", "Barrier");
                {
                    BarrierMenu.Add(new MenuBool("usebarrier", "Use Barrier", true));
                    BarrierMenu.Add(new MenuSeperator("separator", "Barrier Settings"));
                    BarrierMenu.Add(new MenuSlider("barrierslider", "Use Barrier when HP% is less than", 20, 0, 100));
                }
                SummonerMenu.Add(BarrierMenu);

                var SmiteMenu = new Menu("smitemenu", "Smite");
                {
                    SmiteMenu.Add(new MenuBool("usesmite", "Use Smite", true));
                    SmiteMenu.Add(new MenuKeyBind("smiteactive", "AutoSmite", Aimtec.SDK.Util.KeyCode.M, KeybindType.Toggle, true));
                    SmiteMenu.Add(new MenuSeperator("separator", "Smite Settings"));
                    SmiteMenu.Add(new MenuBool("rangedrawing", "Draw Smite range", false));
                    SmiteMenu.Add(new MenuBool("statusdrawing", "Draw Smite status", true));
                    var Dragons = new Menu("dragons", "Use Smite on these Drakes:");
                    {
                        Dragons.Add(new MenuBool("SRU_Dragon_Air", "Cloud Drake"));
                        Dragons.Add(new MenuBool("SRU_Dragon_Fire", "Fire Drake"));
                        Dragons.Add(new MenuBool("SRU_Dragon_Earth", "Earth Drake"));
                        Dragons.Add(new MenuBool("SRU_Dragon_Water", "Water Drake"));
                        Dragons.Add(new MenuBool("SRU_Dragon_Elder", "Elder Drake"));
                    }
                    var EpicMonsters = new Menu("epicmonsters", "Use Smite on these Epic Camps:");
                    {
                        EpicMonsters.Add(new MenuBool("SRU_Baron", "Baron"));
                        EpicMonsters.Add(new MenuBool("SRU_RiftHerald", "Rift Herald", false));
                        EpicMonsters.Add(new MenuBool("SRU_Vilemaw", "Vilemaw"));
                    }
                    var Monsters = new Menu("normalmonsters", "Use Smite on these Camps:");
                    {
                        Monsters.Add(new MenuBool("SRU_Blue", "Blue Sentinel"));
                        Monsters.Add(new MenuBool("SRU_Red", "Red Brambleback"));
                        Monsters.Add(new MenuBool("SRU_Gromp", "Gromp", false));
                        Monsters.Add(new MenuBool("SRU_Murkwolf", "Wolves", false));
                        Monsters.Add(new MenuBool("SRU_Krug", "Krug", false));
                        Monsters.Add(new MenuBool("SRU_Razorbeak", "Razor", false));
                        Monsters.Add(new MenuBool("Sru_Crab", "Crab", false));
                    }
                    SmiteMenu.Add(Dragons);
                    SmiteMenu.Add(EpicMonsters);
                    SmiteMenu.Add(Monsters);
                }
                SummonerMenu.Add(SmiteMenu);

                var CleanseMenu = new Menu("cleansemenu", "Cleanse");
                {
                    CleanseMenu.Add(new MenuBool("usecleanse", "Use Cleanse", true));
                    // TODO: ADD WHITELIST
                    /*if (GameObjects.EnemyHeroes.Any())
                    {
                        Whitelist = new Menu("cleansewhitelist", "Cleanse Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes.Where(e => e.SpellBook.Spells()))
                        }
                    }*/
                }

                var IgniteMenu = new Menu("ignitemenu", "Ignite");
                {
                    IgniteMenu.Add(new MenuBool("useignite", "Use Ignite", true));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        ActivatorClass.Whitelist = new Menu("ignitewhitelist", "Ignite Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                ActivatorClass.Whitelist.Add(new MenuBool(enemy.ChampionName.ToLower(), "Use for: " + enemy.ChampionName));
                            }
                        }
                        IgniteMenu.Add(ActivatorClass.Whitelist);
                    }
                    else
                    {
                        IgniteMenu.Add(new MenuSeperator("separator", "No enemies found."));
                    }
                }
                SummonerMenu.Add(IgniteMenu);

                var ExhaustMenu = new Menu("exhaustmenu", "Exhaust");
                {
                    ExhaustMenu.Add(new MenuBool("useexhaust", "Use Exhaust", true));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        ActivatorClass.Whitelist = new Menu("exhaustwhitelist", "Exhaust Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                ActivatorClass.Whitelist.Add(new MenuBool(enemy.ChampionName.ToLower(), "Use for: " + enemy.ChampionName));
                            }
                        }
                        ExhaustMenu.Add(ActivatorClass.Whitelist);
                    }
                    else
                    {
                        ExhaustMenu.Add(new MenuSeperator("separator", "No enemies found."));
                    }
                }
                SummonerMenu.Add(ExhaustMenu);
            }
            Menus.Menu.Add(SummonerMenu);
        }
    }
}
