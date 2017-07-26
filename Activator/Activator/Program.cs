using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Configuration;
using System.Resources;
using System.Security.Authentication.ExtendedProtection;
using System.Drawing;

using Aimtec.SDK.Events;
using Activator.GeneralMenu;
using Activator.Spells;
using Activator.Items;

namespace Activator
{
    class Program
    {
        static void Main()
        {
            GameEvents.GameStart += OnLoad;
        }

        private static void OnLoad()
        {
            var ItemsDamageItems = new DamageItems();
            var ItemPotions = new Potions();
            var ItemsSupportItems = new SupportItems();

            /*var MenuGeneralMenu = new General();
            var MenuItemMenus = new ItemMenus();
            var MenuSpellMenus = new SpellMenus();*/
            var LoadMenu = new Menus();

            var SpellBarrier = new Barrier();
            var SpellHeal = new Heal();
            var SpeallIgnite = new Ignite();
            var SpellSmite = new Smite();
            Console.WriteLine("Activator loaded!");
        }
    }
}
