using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterClass {
        public static readonly CharacterClass WAR = new Warrior();
        public static readonly CharacterClass RAN = new Ranger();
        public static readonly CharacterClass PAL = new Paladin();
        public static readonly CharacterClass SK = new Shadowknight();
        public static readonly CharacterClass MNK = new Monk();
        public static readonly CharacterClass BRD = new Bard();
        public static readonly CharacterClass RGE = new Rogue();
        public static readonly CharacterClass DRD = new Druid();
        public static readonly CharacterClass SHA = new Shaman();
        public static readonly CharacterClass CL = new Cleric();
        public static readonly CharacterClass MAG = new Magician();
        public static readonly CharacterClass NEC = new Necromancer();
        public static readonly CharacterClass ENC = new Enchanter();
        public static readonly CharacterClass WIZ = new Wizard();
        public static readonly CharacterClass ALC = new Alchemist();
        
        private static readonly Dictionary<byte, CharacterClass> classFor = new Dictionary<byte, CharacterClass>{
            {0, WAR},
            {1, RAN},
            {2, PAL},
            {3, SK},
            {4, MNK},
            {5, BRD},
            {6, RGE},
            {7, DRD},
            {8, SHA},
            {9, CL},
            {10, MAG},
            {11, NEC},
            {12, ENC},
            {13, WIZ},
            {14, ALC},
        };

        public static CharacterClass Of(byte val) => classFor[val];

        byte ToByte();

        private class Warrior : CharacterClass {
            public byte ToByte() => 0;
            public override string ToString() => "WAR";
        }
        private class Ranger : CharacterClass {
            public byte ToByte() => 1;
            public override string ToString() => "RAN";
        }

        private class Paladin : CharacterClass {
            public byte ToByte() => 2;
            public override string ToString() => "PAL";
        }

        private class Shadowknight : CharacterClass {
            public byte ToByte() => 3;
            public override string ToString() => "SK";
        }

        private class Monk : CharacterClass {
            public byte ToByte() => 4;
            public override string ToString() => "MNK";
        }

        private class Bard : CharacterClass {
            public byte ToByte() => 5;
            public override string ToString() => "BRD";
        }

        private class Rogue : CharacterClass {
            public byte ToByte() => 6;
            public override string ToString() => "RGE";
        }

        private class Druid : CharacterClass {
            public byte ToByte() => 7;
            public override string ToString() => "DRD";
        }

        private class Shaman : CharacterClass {
            public byte ToByte() => 8;
            public override string ToString() => "SHA";
        }

        private class Cleric : CharacterClass {
            public byte ToByte() => 9;
            public override string ToString() => "CL";
        }

        private class Magician : CharacterClass {
            public byte ToByte() => 10;
            public override string ToString() => "MAG";
        }

        private class Necromancer : CharacterClass {
            public byte ToByte() => 11;
            public override string ToString() => "NEC";
        }

        private class Enchanter : CharacterClass {
            public byte ToByte() => 12;
            public override string ToString() => "ENC";
        }

        private class Wizard : CharacterClass {
            public byte ToByte() => 13;
            public override string ToString() => "WIZ";
        }

        private class Alchemist : CharacterClass {
            public byte ToByte() => 14;
            public override string ToString() => "ALC";
        }
    }
}
