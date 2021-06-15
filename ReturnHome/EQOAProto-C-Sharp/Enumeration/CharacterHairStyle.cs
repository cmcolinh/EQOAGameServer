using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterHairStyle {
        public static readonly CharacterHairStyle STYLE_0 = new HairStyle0();
        public static readonly CharacterHairStyle STYLE_1 = new HairStyle1();
        public static readonly CharacterHairStyle STYLE_2 = new HairStyle2();
        public static readonly CharacterHairStyle STYLE_3 = new HairStyle3();
        
        private static readonly Dictionary<byte, CharacterHairStyle> hairStyleFor = new Dictionary<byte, CharacterHairStyle>{
            {0, STYLE_0},
            {1, STYLE_1},
            {2, STYLE_2},
            {3, STYLE_3}
        };

        public static CharacterHairStyle Of(byte val) => hairStyleFor[val];

        byte ToByte();

        private class HairStyle0 : CharacterHairStyle {
            public byte ToByte() => 0;
            public override string ToString() => "hair style 0";
        }

        private class HairStyle1 : CharacterHairStyle {
            public byte ToByte() => 1;
            public override string ToString() => "hair style 1";
        }

        private class HairStyle2 : CharacterHairStyle {
            public byte ToByte() => 2;
            public override string ToString() => "hair style 2";
        }

        private class HairStyle3 : CharacterHairStyle {
            public byte ToByte() => 3;
            public override string ToString() => "hair style 3";
        }
    }
}
