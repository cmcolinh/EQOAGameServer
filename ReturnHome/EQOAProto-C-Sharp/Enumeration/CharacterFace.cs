using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterFace {
        public static readonly CharacterFace FACE_0 = new Face0();
        public static readonly CharacterFace FACE_1 = new Face1();
        public static readonly CharacterFace FACE_2 = new Face2();
        public static readonly CharacterFace FACE_3 = new Face3();
        
        private static readonly Dictionary<byte, CharacterFace> faceFor = new Dictionary<byte, CharacterFace>{
            {0, FACE_0},
            {1, FACE_1},
            {2, FACE_2},
            {3, FACE_3}
        };

        public static CharacterFace Of(byte val) => faceFor[val];

        byte ToByte();

        private class Face0 : CharacterFace {
            public byte ToByte() => 0;
            public override string ToString() => "face 0";
        }

        private class Face1 : CharacterFace {
            public byte ToByte() => 1;
            public override string ToString() => "face 1";
        }

        private class Face2 : CharacterFace {
            public byte ToByte() => 2;
            public override string ToString() => "face 2";
        }

        private class Face3 : CharacterFace {
            public byte ToByte() => 3;
            public override string ToString() => "face 3";
        }
    }
}
