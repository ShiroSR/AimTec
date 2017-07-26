using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK;
using Aimtec;
using Activator;
using Activator.GeneralMenu;
using static Activator.GeneralMenu.General;

namespace Activator.GeneralMenu
{
    class ItemMenus
    {
        public ItemMenus()
        {
         
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
                        ActivatorClass.Whitelist = new Menu("gunbladewhitelist", "Gunblade Whitelist:");
                        {
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                ActivatorClass.Whitelist.Add(new MenuBool(enemy.ChampionName.ToLower(), "Use for: " + enemy.ChampionName));
                            }
                        }
                        DamageItems.Add(ActivatorClass.Whitelist);
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
                        ActivatorClass.Whitelist = new Menu("fotmwhitelist", "Face of the Mountain Whitelist:");
                        {
                            foreach (var ally in GameObjects.AllyHeroes.Where(t => !t.IsMe))
                            {
                                ActivatorClass.Whitelist.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName));
                            }
                        }
                        SupportItems.Add(ActivatorClass.Whitelist);
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
            
            Menus.Menu.Add(ItemMenu);

            var Dev = new Menu("dev", "Dev");
            {
                Dev.Add(new MenuKeyBind("getname", "Get Item's name", Aimtec.SDK.Util.KeyCode.Z, KeybindType.Press));
            }
            Menus.Menu.Add(Dev);
        }
    }
}
