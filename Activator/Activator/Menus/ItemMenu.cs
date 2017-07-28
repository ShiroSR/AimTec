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
    class ItemMenu
    {
        public ItemMenu()
        {
            MenuClass.DamageItemsMenu = new Menu("damageitems", "Damage Items");
            {
                MenuClass.DamageItemsMenu.Add(new MenuBool("usecutlass", "Use Bilgewater Cutlass"));
                MenuClass.DamageItemsMenu.Add(new MenuBool("usebotrk", "Use Blade of the Ruined King"));
                MenuClass.DamageItemsMenu.Add(new MenuBool("useglp", "Use Hextech GLP-800"));
                MenuClass.DamageItemsMenu.Add(new MenuBool("usegunblade", "Use Hextech Gunblade"));
                MenuClass.DamageItemsMenu.Add(new MenuBool("usetiamat", "Use Tiamat and Hydra"));
                MenuClass.DamageItemsMenu.Add(new MenuBool("tiamatlaneclear", "Use Hydra for Laneclear"));
                MenuClass.DamageItemsMenu.Add(new MenuBool("useqss", "Use QSS (And Scimitar)"));
                MenuClass.DamageItemsMenu.Add(new MenuSeperator("separator", "Item Settings"));
                MenuClass.DamageItemsMenu.Add(new MenuBool("onlycombo", "Only use Damage Items if Combo key is pressed"));
                MenuClass.DamageItemsMenu.Add(new MenuSlider("glpslider", "Use GLP-800 when enemies in range >=", 2, 1, 5));
                MenuClass.DamageItemsMenu.Add(new MenuSlider("tiamatslider", "Use Hydra when enemy HP% is less than:", 70, 0, 100));
                MenuClass.DamageItemsMenu.Add(new MenuBool("gunbladewhenXhp", "Use Gunblade with HP%", false));
                MenuClass.DamageItemsMenu.Add(new MenuSlider("gunbladeslider","Use Gunblade when enemy HP% is less than:", 70, 0, 100));
                MenuClass.WhitelistEnemies = new Menu("gunbladewhitelist", "Gunblade Whitelist:");
                MenuClass.DamageItemsMenu.Add(MenuClass.WhitelistEnemies);
                if (GameObjects.EnemyHeroes.Any())
                {
                    foreach (var enemy in GameObjects.EnemyHeroes)
                    {
                        MenuClass.WhitelistEnemies.Add(new MenuBool(enemy.ChampionName.ToLower(),
                            "Use for: " + enemy.ChampionName));
                    }
                }
                else
                {
                    MenuClass.WhitelistEnemies.Add(new MenuSeperator("separaator", "No enemies found."));
                }
                MenuClass.CCMenu = new Menu("ccmenu", "Use QSS (and Scimitar) on CC Type:");
                {
                    MenuClass.CCMenu.Add(new MenuBool("BuffType.Stun", "Stun"));
                    MenuClass.CCMenu.Add(new MenuBool("BuffType.Fear", "Fear"));
                    MenuClass.CCMenu.Add(new MenuBool("BuffType.Flee", "Flee"));
                    MenuClass.CCMenu.Add(new MenuBool("BuffType.Snare", "Snare"));
                    MenuClass.CCMenu.Add(new MenuBool("BuffType.Taunt", "Taunt"));
                    MenuClass.CCMenu.Add(new MenuBool("BuffType.Charm", "Charm"));
                }
                MenuClass.DamageItemsMenu.Add(MenuClass.CCMenu);
            }
            MenuClass.ItemMenu.Add(MenuClass.DamageItemsMenu);

            MenuClass.SupportItemsMenu = new Menu("supportitems", "Support Items");
            {
                MenuClass.SupportItemsMenu.Add(new MenuBool("useranduins", "Use Randuin's Omen", true));
                MenuClass.SupportItemsMenu.Add(new MenuBool("usesolari", "Use Solari", true));
                MenuClass.SupportItemsMenu.Add(new MenuBool("usefotm", "Use Face of the Mountain", true));
                MenuClass.SupportItemsMenu.Add(new MenuSeperator("separator", "Item Settings"));
                MenuClass.SupportItemsMenu.Add(new MenuSlider("randuinsslider", "Use Randuin's when enemies in range >=", 3, 1, 5));
                MenuClass.SupportItemsMenu.Add(new MenuSlider("solarislider", "Use Solari when allies in range >=", 3, 1, 5));
                MenuClass.SupportItemsMenu.Add(new MenuSlider("solarislider2", "and HP% is less than:", 40, 0, 100));
                MenuClass.WhitelistAllies = new Menu("fotmwhitelist", "Face of the Mountain Whitelist:");
                MenuClass.SupportItemsMenu.Add(MenuClass.WhitelistAllies);
                if (GameObjects.AllyHeroes.Any(t => !t.IsMe))
                {
                    foreach (var ally in GameObjects.AllyHeroes.Where(t => !t.IsMe))
                    {
                        MenuClass.WhitelistAllies.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName));
                    }
                }
                else
                {
                    MenuClass.WhitelistAllies.Add(new MenuSeperator("separaator", "No allies found."));
                }
                MenuClass.SupportItemsMenu.Add(new MenuSlider("fotmslider", "Use FotM when ally HP% is less than:", 30, 0, 100));
            }
            MenuClass.ItemMenu.Add(MenuClass.SupportItemsMenu);

            MenuClass.PotionsItemsMenu = new Menu("potionsitems", "Potions");
            {
                MenuClass.PotionsItemsMenu.Add(new MenuBool("usepotions", "Use Health Potions"));
                MenuClass.PotionsItemsMenu.Add(new MenuBool("userefillable", "Use Refillable Potions"));
                MenuClass.PotionsItemsMenu.Add(new MenuSeperator("potionsettings", "Potion Settings"));
                MenuClass.PotionsItemsMenu.Add(new MenuSlider("potionslider", "Use Health Potions when HP is less than", 40, 0, 100));
                MenuClass.PotionsItemsMenu.Add(new MenuSlider("refillableslider", "Use Refillable Potions when HP is less than", 40, 0, 100));
            }
            MenuClass.ItemMenu.Add(MenuClass.PotionsItemsMenu);
        }
    }
}
