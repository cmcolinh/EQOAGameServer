using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterHairLength {
        public static readonly CharacterHairLength LENGTH_0 = new HairLength0();
        public static readonly CharacterHairLength LENGTH_1 = new HairLength1();
        public static readonly CharacterHairLength LENGTH_2 = new HairLength2();
        public static readonly CharacterHairLength LENGTH_3 = new HairLength3();
        
        private static readonly Dictionary<byte, CharacterHairLength> hairLengthFor = new Dictionary<byte, CharacterHairLength>{
            {0, LENGTH_0},
            {1, LENGTH_1},
            {2, LENGTH_2},
            {3, LENGTH_3}
        };

        public static CharacterHairLength Of(byte val) => hairLengthFor[val];

        byte ToByte();

        private class HairLength0 : CharacterHairLength {
            public byte ToByte() => 0;
            public override string ToString() => "hair length 0";
        }

        private class HairLength1 : CharacterHairLength {
            public byte ToByte() => 1;
            public override string ToString() => "hair length 1";
        }

        private class HairLength2 : CharacterHairLength {
            public byte ToByte() => 2;
            public override string ToString() => "hair length 2";
        }

        private class HairLength3 : CharacterHairLength {
            public byte ToByte() => 3;
            public override string ToString() => "hair length 3";
        }
    }
}
