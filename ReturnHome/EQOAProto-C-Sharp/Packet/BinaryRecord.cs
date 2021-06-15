namespace ReturnHome.Packet {
	/// <summary> This is the top level interface for objects that can be serialized into EQOA packet format </summary>
	/// <exception cref="SerializationException">If serialization was unsuccessful.</exception>
	public interface BinaryRecord {
	    PacketBytes Serialize();
	}
}
