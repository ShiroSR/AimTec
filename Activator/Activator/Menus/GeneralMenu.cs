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
    class GeneralMenu
    {
        public GeneralMenu()
        {
            MenuClass.Root = new Menu("activator", "Activator", true);

            MenuClass.SpellMenu = new Menu("summoner", "Summoner Spells");

            MenuClass.ItemMenu = new Menu("items", "Items");

            /*MenuClass.Dev = new Menu("dev", "Dev");
            {
                MenuClass.Dev.Add(new MenuKeyBind("getname", "Get Item's name", Aimtec.SDK.Util.KeyCode.Z, KeybindType.Press));
            }*/

            MenuClass.Root.Add(MenuClass.SpellMenu);
            MenuClass.Root.Add(MenuClass.ItemMenu);
            //MenuClass.Root.Add(MenuClass.Dev);
            MenuClass.Root.Attach();
        }
    }
}
