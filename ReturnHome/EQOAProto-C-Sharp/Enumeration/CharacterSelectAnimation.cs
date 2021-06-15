using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface CharacterSelectAnimation {
        public static readonly CharacterSelectAnimation STANDING = new Standing();
        public static readonly CharacterSelectAnimation ONE_HAND_SLASH = new OneHandedSlash();
        public static readonly CharacterSelectAnimation TWO_HAND_SLASH = new TwoHandedSlash();
        public static readonly CharacterSelectAnimation ONE_HAND_BLUNT = new OneHandedBlunt();
        public static readonly CharacterSelectAnimation TWO_HAND_BLUNT = new TwoHandedBlunt();
        public static readonly CharacterSelectAnimation ONE_HAND_PIERCE = new OneHandedPierce();
        public static readonly CharacterSelectAnimation TWO_HAND_PIERCE = new TwoHandedPierce();
        public static readonly CharacterSelectAnimation BOW = new Bow();
        public static readonly CharacterSelectAnimation FIST = new Fist();
        public static readonly CharacterSelectAnimation CROSSBOW = new Crossbow();
        public static readonly CharacterSelectAnimation THROWING = new Throwing();
        public static readonly CharacterSelectAnimation FIST_2 = new Fist2();
        public static readonly CharacterSelectAnimation OFF_HAND_SLASH = new OffHandSlash();
        public static readonly CharacterSelectAnimation OFF_HAND_BLUNT = new OffHandBlunt();
        public static readonly CharacterSelectAnimation OFF_HAND_PIERCE = new OffHandPierce();

        private static readonly Dictionary<ushort, CharacterSelectAnimation> animationFor = new Dictionary<ushort, CharacterSelectAnimation>{
            {STANDING.ToUshort(), STANDING},
            {ONE_HAND_SLASH.ToUshort(), ONE_HAND_SLASH},
            {TWO_HAND_SLASH.ToUshort(), TWO_HAND_SLASH},
            {ONE_HAND_BLUNT.ToUshort(), ONE_HAND_BLUNT},
            {TWO_HAND_BLUNT.ToUshort(), TWO_HAND_BLUNT},
            {ONE_HAND_PIERCE.ToUshort(), ONE_HAND_PIERCE},
            {TWO_HAND_PIERCE.ToUshort(), TWO_HAND_PIERCE},
            {BOW.ToUshort(), BOW},
            {FIST.ToUshort(), FIST},
            {CROSSBOW.ToUshort(), CROSSBOW},
            {THROWING.ToUshort(), THROWING},
            {FIST_2.ToUshort(), FIST_2},
            {OFF_HAND_SLASH.ToUshort(), OFF_HAND_SLASH},
            {OFF_HAND_BLUNT.ToUshort(), OFF_HAND_BLUNT},
            {OFF_HAND_PIERCE.ToUshort(), OFF_HAND_PIERCE}
        };

        public static CharacterSelectAnimation Of(ushort val) => animationFor[val];

        ushort ToUshort();

        private class Standing : CharacterSelectAnimation {
            public ushort ToUshort() => 0;
            public override string ToString() => "standing";
        }

        private class OneHandedSlash : CharacterSelectAnimation {
            public ushort ToUshort() => 1;
            public override string ToString() => "1h slash";
        }

        private class TwoHandedSlash : CharacterSelectAnimation {
            public ushort ToUshort() => 2;
            public override string ToString() => "2h slash";
        }

        private class OneHandedBlunt : CharacterSelectAnimation {
            public ushort ToUshort() => 3;
            public override string ToString() => "1h blunt";
        }

        private class TwoHandedBlunt : CharacterSelectAnimation {
            public ushort ToUshort() => 4;
            public override string ToString() => "2h blunt";
        }

        private class OneHandedPierce : CharacterSelectAnimation {
            public ushort ToUshort() => 5;
            public override string ToString() => "1h pierce";
        }

        private class TwoHandedPierce : CharacterSelectAnimation {
            public ushort ToUshort() => 6;
            public override string ToString() => "2h pierce";
        }

        private class Bow : CharacterSelectAnimation {
            public ushort ToUshort() => 7;
            public override string ToString() => "bow";
        }

        private class Fist : CharacterSelectAnimation {
            public ushort ToUshort() => 8;
            public override string ToString() => "fist";
        }

        private class Crossbow : CharacterSelectAnimation {
            public ushort ToUshort() => 9;
            public override string ToString() => "crossbow";
        }

        private class Throwing : CharacterSelectAnimation {
            public ushort ToUshort() => 10;
            public override string ToString() => "throwing";
        }

        private class Fist2 : CharacterSelectAnimation {
            public ushort ToUshort() => 11;
            public override string ToString() => "fist (0x000b)";
        }

        private class OffHandSlash : CharacterSelectAnimation {
            public ushort ToUshort() => 257;
            public override string ToString() => "off hand slash";
        }

        private class OffHandBlunt : CharacterSelectAnimation {
            public ushort ToUshort() => 771;
            public override string ToString() => "off hand blunt";
        }

        private class OffHandPierce : CharacterSelectAnimation {
            public ushort ToUshort() => 1281;
            public override string ToString() => "off hand pierce";
        }
    }
}
