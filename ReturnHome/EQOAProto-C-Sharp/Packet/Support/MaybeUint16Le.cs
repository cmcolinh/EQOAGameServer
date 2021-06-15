using System;

namespace ReturnHome.Packet.Support {
    public interface MaybeUint16Le : BinaryRecord {
        ushort ToUshort();
    }
}
