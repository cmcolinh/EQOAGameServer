using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterGender {
        public static readonly CharacterGender MALE = new Male();
        public static readonly CharacterGender FEMALE = new Female();
        
        private static readonly Dictionary<byte, CharacterGender> genderFor = new Dictionary<byte, CharacterGender>{
            {0, MALE},
            {1, FEMALE}
        };

        public static CharacterGender Of(byte val) => genderFor[val];

        byte ToByte();

        private class Male : CharacterGender {
            public byte ToByte() => 0;
            public override string ToString() => "Male";
        }

        private class Female : CharacterGender {
            public byte ToByte() => 1;
            public override string ToString() => "Female";
        }
    }
}
