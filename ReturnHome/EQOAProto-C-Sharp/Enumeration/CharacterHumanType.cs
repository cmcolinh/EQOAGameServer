using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterHumanType {
        public static readonly CharacterHumanType NON_HUMAN = new NonHuman();
        public static readonly CharacterHumanType EASTERN = new EasternHuman();
        public static readonly CharacterHumanType WESTERN = new WesternHuman();
        
        private static readonly Dictionary<byte, CharacterHumanType> humanTypeFor = new Dictionary<byte, CharacterHumanType>{
            {0, NON_HUMAN},
            {1, EASTERN},
            {2, WESTERN}
        };

        public static CharacterHumanType Of(byte val) => humanTypeFor[val];

        byte ToByte();

        private class NonHuman : CharacterHumanType {
            public byte ToByte() => 0;
            public override string ToString() => "Non Human";
        }

        private class EasternHuman : CharacterHumanType {
            public byte ToByte() => 1;
            public override string ToString() => "Eastern Human";
        }

        private class WesternHuman : CharacterHumanType {
            public byte ToByte() => 2;
            public override string ToString() => "Western Human";
        }
    }
}
