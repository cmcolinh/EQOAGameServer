using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterHairColor {
        public static readonly CharacterHairColor BLACK = new BlackHair();
        public static readonly CharacterHairColor BROWN = new BrownHair();
        public static readonly CharacterHairColor BLOND = new BlondHair();
        public static readonly CharacterHairColor GRAY = new GrayHair();
        public static readonly CharacterHairColor ORANGE = new OrangeHair();
        public static readonly CharacterHairColor GREEN = new GreenHair();
        public static readonly CharacterHairColor TEAL = new TealHair();
        public static readonly CharacterHairColor PINK = new PinkHair();
        
        private static readonly Dictionary<byte, CharacterHairColor> hairColorFor = new Dictionary<byte, CharacterHairColor>{
            {0, BLACK},
            {1, BROWN},
            {2, BLOND},
            {3, GRAY},
            {4, ORANGE},
            {5, GREEN},
            {6, TEAL},
            {7, PINK}
        };

        public static CharacterHairColor Of(byte val) => hairColorFor[val];

        byte ToByte();

        private class BlackHair : CharacterHairColor {
            public byte ToByte() => 0;
            public override string ToString() => "black hair";
        }

        private class BrownHair : CharacterHairColor {
            public byte ToByte() => 1;
            public override string ToString() => "brown hair";
        }

        private class BlondHair : CharacterHairColor {
            public byte ToByte() => 2;
            public override string ToString() => "blond hair";
        }

        private class GrayHair : CharacterHairColor {
            public byte ToByte() => 3;
            public override string ToString() => "gray hair";
        }

        private class OrangeHair : CharacterHairColor {
            public byte ToByte() => 4;
            public override string ToString() => "orange hair";
        }

        private class GreenHair : CharacterHairColor {
            public byte ToByte() => 5;
            public override string ToString() => "green hair";
        }

        private class TealHair : CharacterHairColor {
            public byte ToByte() => 6;
            public override string ToString() => "teal hair";
        }

        private class PinkHair : CharacterHairColor {
            public byte ToByte() => 7;
            public override string ToString() => "pink hair";
        }
    }
}
