using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec.SDK.Menu;

namespace Activator
{
    class MenuClass
    {
        public static Menu Root { get; set; }

        public static Menu Dev { get; set; }
        public static Menu SpellMenu { get; set; }
        public static Menu ItemMenu { get; set; }

        public static Menu WhitelistAllies { get; set; }
        public static Menu WhitelistEnemies { get; set; }

        public static Menu HealMenu { get; set; }
        public static Menu BarrierMenu { get; set; }
        public static Menu SmiteMenu { get; set; }
        public static Menu Dragons { get; set; }
        public static Menu EpicMonsters { get; set; }
        public static Menu Monsters { get; set; }
        public static Menu CCMenu { get; set; }
        public static Menu CCMenu2 { get; set; }
        public static Menu CleanseMenu { get; set; }
        public static Menu IgniteMenu { get; set; }

        public static Menu DamageItemsMenu { get; set; }
        public static Menu SupportItemsMenu { get; set; }
        public static Menu PotionsItemsMenu { get; set; }
    }
}
