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
using Activator.Spells;
using Activator.Items;
using Aimtec;
using Aimtec.SDK.Menu.Components;

namespace Activator
{
    class Program
    {
        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();

        static void Main()
        {
            GameEvents.GameStart += OnLoad;
            Game.OnUpdate += OnUpdate;
        }

        private static void OnLoad()
        {
            var ItemsDamageItems = new DamageItems();
            var ItemPotions = new Potions();
            var ItemsSupportItems = new SupportItems();

            var MenuGeneralMenu = new GeneralMenu();
            var MenuItemMenus = new ItemMenu();
            var MenuSpellMenus = new SpellMenu();
            var LoadMenuClass = new MenuClass();

            var SpellBarrier = new Barrier();
            var SpellHeal = new Heal();
            var SpeallIgnite = new Ignite();
            var SpellSmite = new Smite();
            var SpellCleanse = new Cleanse();
            Console.WriteLine("Activator loaded!");
        }

        public static void OnUpdate()
        {
            if (Player.IsDead)
            {
                return;
            }

            if (MenuClass.Dev["getname"].As<MenuKeyBind>().Enabled)
            {
                foreach (var inventorySlot in ObjectManager.GetLocalPlayer().Inventory.Slots)
                {
                    //Console.WriteLine(inventorySlot.SpellName);
                }
            }
        }
    }
}
