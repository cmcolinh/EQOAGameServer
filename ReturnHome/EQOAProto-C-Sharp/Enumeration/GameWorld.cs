using System.Collections.Generic;

namespace ReturnHome.Enumeration {
    public interface GameWorld {
        public static readonly GameWorld TUNARIA = new Tunaria();
        public static readonly GameWorld RATHE = new Rathe();
        public static readonly GameWorld ODUS = new Odus();
        public static readonly GameWorld LAVASTORM = new Lavastorm();
        public static readonly GameWorld PLANE_OF_SKY = new PlaneOfSky();
        public static readonly GameWorld SECRETS = new SecretZones();
        
        private static readonly Dictionary<byte, GameWorld> genderFor = new Dictionary<byte, GameWorld>{
            {0, TUNARIA},
            {1, RATHE}
        };

        public static GameWorld Of(byte val) => genderFor[val];

        byte ToByte();

        private class Tunaria : GameWorld {
            public byte ToByte() => 0;
            public override string ToString() => "Tunaria";
        }

        private class Rathe : GameWorld {
            public byte ToByte() => 1;
            public override string ToString() => "Rathe Mountains";
        }

        private class Odus : GameWorld {
            public byte ToByte() => 2;
            public override string ToString() => "Odus";
        }

        private class Lavastorm : GameWorld {
            public byte ToByte() => 3;
            public override string ToString() => "Lavastorm";
        }

        private class PlaneOfSky : GameWorld {
            public byte ToByte() => 4;
            public override string ToString() => "Plane of Sky";
        }

        private class SecretZones : GameWorld {
            public byte ToByte() => 5 ;
            public override string ToString() => "Plane of Sky";
        }
    }
}
