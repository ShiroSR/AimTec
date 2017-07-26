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

namespace Activator.GeneralMenu
{
    class General
    {
        internal static class ActivatorClass
        {
            public static Menu Root { get; set; }
            public static Menu Whitelist { get; set; }
        }

        public static void GeneralMenu()
        {
            Menus.Menu = new Menu("activator", "Activator", true);
            Menus.Menu.Attach();
        }
    }
}
