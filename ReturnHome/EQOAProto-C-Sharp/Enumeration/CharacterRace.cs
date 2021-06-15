using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterRace {
        public static readonly CharacterRace HUM = new Human();
        public static readonly CharacterRace ELF = new Elf();
        public static readonly CharacterRace DELF = new DarkElf();
        public static readonly CharacterRace GNO = new Gnome();
        public static readonly CharacterRace DWF = new Dwarf();
        public static readonly CharacterRace TRL = new Troll();
        public static readonly CharacterRace BAR = new Barbarian();
        public static readonly CharacterRace HLF = new Halfling();
        public static readonly CharacterRace ERU = new Erudite();
        public static readonly CharacterRace OGR = new Ogre();
        
        private static readonly Dictionary<byte, CharacterRace> raceFor = new Dictionary<byte, CharacterRace>{
            {0, HUM},
            {1, ELF},
            {2, DELF},
            {3, GNO},
            {4, DWF},
            {5, TRL},
            {6, BAR},
            {7, HLF},
            {8, ERU},
            {9, OGR}
        };

        public static CharacterRace Of(byte val) => raceFor[val];

        byte ToByte();

        private class Human : CharacterRace {
            public byte ToByte() => 0;
            public override string ToString() => "HUM";
        }

        private class Elf : CharacterRace {
            public byte ToByte() => 1;
            public override string ToString() => "ELF";
        }

        private class DarkElf : CharacterRace {
            public byte ToByte() => 2;
            public override string ToString() => "DELF";
        }

        private class Gnome : CharacterRace {
            public byte ToByte() => 3;
            public override string ToString() => "GNO";
        }

        private class Dwarf : CharacterRace {
            public byte ToByte() => 4;
            public override string ToString() => "DWF";
        }

        private class Troll : CharacterRace {
            public byte ToByte() => 5;
            public override string ToString() => "TRL";
        }

        private class Barbarian : CharacterRace {
            public byte ToByte() => 6;
            public override string ToString() => "BAR";
        }

        private class Halfling : CharacterRace {
            public byte ToByte() => 7;
            public override string ToString() => "HLF";
        }

        private class Erudite : CharacterRace {
            public byte ToByte() => 8;
            public override string ToString() => "ERU";
        }

        private class Ogre : CharacterRace {
            public byte ToByte() => 9;
            public override string ToString() => "OGR";
        }
    }
}
