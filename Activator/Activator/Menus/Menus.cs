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

namespace Activator
{
    class Menus
    {
        public static Menu Whitelist { get; set; }

        public static Menu Menu = new Menu("activator", "Activator", true);

        public Menus()
        {
            var SummonerMenu = new Menu("summoner", "Summoner Spells");
            {
                var HealMenu = new Menu("healmenu", "Heal");
                {
                    HealMenu.Add(new MenuBool("useheal", "Use Heal", true));
                    HealMenu.Add(new MenuBool("useallies", "Use Heal on Allies", false));

                    if (GameObjects.AllyHeroes.Any(t => !t.IsMe))
                    {
                        Whitelist = new Menu("healwhitelist", "Heal Whitelist:");
                        {
                            foreach (var ally in GameObjects.AllyHeroes.Where(t => !t.IsMe))
                            {
                                Whitelist.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName));
                            }
                        }
                        HealMenu.Add(Whitelist);
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

                /*var CleanseMenu = new Menu("cleansemenu", "Cleanse");
                {
                    CleanseMenu.Add(new MenuBool("usecleanse", "Use Cleanse", true));
                    if (GameObjects.EnemyHeroes.Any())
                    {
                        Whitelist = new Menu("cleansewhitelist", "Cleanse Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes.Where(e => e.SpellBook.Spells()))
                        }
                    }
                }*/

                var IgniteMenu = new Menu("ignitemenu", "Ignite");
                {
                    IgniteMenu.Add(new MenuBool("useignite", "Use Ignite", true));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        Whitelist = new Menu("ignitewhitelist", "Ignite Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                Whitelist.Add(new MenuBool(enemy.ChampionName.ToLower(), "Use for: " + enemy.ChampionName));
                            }
                        }
                        IgniteMenu.Add(Whitelist);
                    }
                    else
                    {
                        IgniteMenu.Add(new MenuSeperator("separator", "No enemies found."));
                    }
                }
                SummonerMenu.Add(IgniteMenu);

                /*var ExhaustMenu = new Menu("exhaustmenu", "Exhaust");
                {
                    ExhaustMenu.Add(new MenuBool("useexhaust", "Use Exhaust", true));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        Whitelist = new Menu("exhaustwhitelist", "Exhaust Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                Whitelist.Add(new MenuBool(enemy.ChampionName.ToLower(), "Use for: " + enemy.ChampionName));
                            }
                        }
                        ExhaustMenu.Add(Whitelist);
                    }
                    else
                    {
                        ExhaustMenu.Add(new MenuSeperator("separator", "No enemies found."));
                    }
                }
                SummonerMenu.Add(ExhaustMenu);*/
            }
            Menu.Add(SummonerMenu);

            var ItemMenu = new Menu("items", "Items");
            {
                var DamageItems = new Menu("damageitems", "Damage Items");
                {
                    DamageItems.Add(new MenuBool("usecutlass", "Use Bilgewater Cutlass", true));
                    DamageItems.Add(new MenuBool("usebotrk", "Use Blade of the Ruined King", true));
                    DamageItems.Add(new MenuBool("useglp", "Use Hextech GLP-800", true));
                    DamageItems.Add(new MenuBool("usegunblade", "Use Hextech Gunblade", true));
                    DamageItems.Add(new MenuSeperator("separator", "Item Settings"));
                    DamageItems.Add(new MenuBool("onlycombo", "Only use Damage Items if Combo key is pressed", true));
                    DamageItems.Add(new MenuSlider("glpslider", "Use GLP-800 when enemies in range >=", 2, 1, 5));
                    if (GameObjects.EnemyHeroes.Any())
                    {
                        Whitelist = new Menu("gunbladewhitelist", "Gunblade Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                Whitelist.Add(new MenuBool(enemy.ChampionName.ToLower(), "Use for: " + enemy.ChampionName));
                            }
                        }
                        DamageItems.Add(Whitelist);
                    }
                    else
                    {
                        DamageItems.Add(new MenuSeperator("separaator", "No enemies found."));
                    }
                }
                ItemMenu.Add(DamageItems);
                var SupportItems = new Menu("supportitems", "Support Items");
                {
                    SupportItems.Add(new MenuBool("useranduins", "Use Randuin's Omen", true));
                    SupportItems.Add(new MenuBool("usesolari", "Use Solari", true));
                    SupportItems.Add(new MenuBool("usefotm", "Use Face of the Mountain", true));
                    SupportItems.Add(new MenuSeperator("separator", "Item Settings"));
                    SupportItems.Add(new MenuSlider("randuinsslider", "Use Randuin's when enemies in range >=", 3, 1, 5));
                    SupportItems.Add(new MenuSlider("solarislider", "Use Solari when allies in range >=", 3, 1, 5));
                    SupportItems.Add(new MenuSlider("solarislider2", "and HP% is less than:", 40, 0, 100));
                    if (GameObjects.AllyHeroes.Any(t => !t.IsMe))
                    {
                        Whitelist = new Menu("fotmwhitelist", "Face of the Mountain Whitelist:");
                        {
                            foreach (var ally in GameObjects.AllyHeroes.Where(t => !t.IsMe))
                            {
                                Whitelist.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName));
                            }
                        }
                        SupportItems.Add(Whitelist);
                    }
                    else
                    {
                        SupportItems.Add(new MenuSeperator("separaator", "No allies found."));
                    }
                    SupportItems.Add(new MenuSlider("fotmslider", "Use FotM when ally HP% is less than:", 30, 0, 100));
                }
                ItemMenu.Add(SupportItems);
                var PotionsItems = new Menu("potionsitems", "Potions");
                {
                    PotionsItems.Add(new MenuBool("usepotions", "Use Health Potions", true));
                    PotionsItems.Add(new MenuBool("userefillable", "Use Refillable Potions", false));
                    PotionsItems.Add(new MenuSeperator("potionsettings", "Potion Settings"));
                    PotionsItems.Add(new MenuSlider("potionslider", "Use Health Potions when HP is less than", 40, 0, 100));
                    PotionsItems.Add(new MenuSlider("refillableslider", "Use Refillable Potions when HP is less than", 40, 0, 100));
                }
                ItemMenu.Add(PotionsItems);
            }
            Menu.Add(ItemMenu);

            var Dev = new Menu("dev", "Dev");
            {
                Dev.Add(new MenuKeyBind("getname", "Get Item's name", Aimtec.SDK.Util.KeyCode.Z, KeybindType.Press));
            }
            Menu.Add(Dev);

            Menu.Attach();
        }
    }
}
