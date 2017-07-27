using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;

namespace Activator.Spells
{
    class DrawingClass
    {
        internal static readonly string[] JungleList =
        {
            "SRU_Dragon_Air", "SRU_Dragon_Fire", "SRU_Dragon_Water",
            "SRU_Dragon_Earth", "SRU_Dragon_Elder", "SRU_Baron",
            "SRU_RiftHerald", "SRU_Red", "SRU_Blue", "SRU_Gromp",
            "Sru_Crab", "SRU_Krug", "SRU_Razorbeak", "SRU_Murkwolf"
        };

        public static readonly List<string> SpecialChampions = new List<string> { "Annie", "Jhin" };

        public static int SHeight = 8;

        public static int SWidth = 103;

        internal static readonly List<JungleHpBarOffset> JungleHpBarOffsetList1080p = new List<JungleHpBarOffset>
        {
            new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Air", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Fire", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Water", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Earth", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Elder", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Baron", Width = 190, Height = 10, XOffset = 16, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_RiftHerald", Width = 139, Height = 6, XOffset = 12, YOffset = 22 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Red", Width = 139, Height = 4, XOffset = 12, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Blue", Width = 139, Height = 4, XOffset = 12, YOffset = 24 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Gromp", Width = 86, Height = 2, XOffset = 1, YOffset = 7 },
            new JungleHpBarOffset { UnitSkinName = "Sru_Crab", Width = 61, Height = 2, XOffset = 1, YOffset = 5 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Krug", Width = 79, Height = 2, XOffset = 1, YOffset = 7 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Razorbeak", Width = 74, Height = 2, XOffset = 1, YOffset = 7 },
            new JungleHpBarOffset { UnitSkinName = "SRU_Murkwolf", Width = 74, Height = 2, XOffset = 1, YOffset = 7 }
        };

        public static int SxOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 1 : 10;
        }

        public static int SyOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 3 : 20;
        }

        internal class JungleHpBarOffset
        {
            internal int Height;

            internal string UnitSkinName;

            internal int Width;

            internal int XOffset;

            internal int YOffset;
        }
    }
}
