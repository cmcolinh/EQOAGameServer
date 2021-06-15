using ReturnHome.Packet;
using ReturnHome.Packet.Support;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReturnHome {
	/// <summary> This interface is the model for the information contained in EQOA Packets </summary>
	public interface EQOAPacket : BinaryRecord {
		ushort SourceEndpoint();
		ushort DestinationEndpoint();
		IList<PacketBundle> Bundles();
		static EQOAPacket Read(PacketBytes packetBytes) => EQOAPacket.Impl.Read(packetBytes);

		static EQOAPacket Read(PacketBytes packetBytes, Func<PacketBytes, uint> calculateCRC) => EQOAPacket.Impl.Read(packetBytes, calculateCRC);

		static EQOAPacket Of(PacketContents packetContents, Func<PacketBytes, uint> calculateCRC) => new EQOAPacket.Impl(packetContents: packetContents, crc: Uint32Le.Of(calculateCRC(packetContents.Serialize())));

		static EQOAPacket Of(PacketContents packetContents) => Of(packetContents, CalculateCRC.Instance.Value.Calculate);

		private class Impl : EQOAPacket {
			private readonly PacketContents packetContents;
			private readonly Uint32Le crc;
			private readonly Lazy<PacketBytes> bytes;

			public static EQOAPacket Read(PacketBytes packetBytes) {
				return Read(packetBytes, packetString => CalculateCRC.Instance.Value.Calculate(packetString));
			}

			public static EQOAPacket Read(PacketBytes remainingPacketBytes, Func<PacketBytes, uint> calculateCRC) {
				PacketBytes packetContentBytes = remainingPacketBytes.PopAllButLast(bytes: 4);
				Uint32Le crc = Uint32Le.Read(remainingPacketBytes);
				if (!calculateCRC(packetContentBytes).Equals(crc.ToUint())) {
					throw new SerializationException("CRC Check failed");
				}
				PacketContents packetContents = PacketContents.Read(packetContentBytes);
				return new EQOAPacket.Impl(packetContents, crc);
			}

			public Impl(PacketContents packetContents, Uint32Le crc) {
				this.packetContents = packetContents;
				this.crc = crc;
				this.bytes = new Lazy<PacketBytes>(() => this.packetContents.Serialize().Append(this.crc.Serialize()));
			}

			public ushort SourceEndpoint() => packetContents.SourceEndpoint();
			public ushort DestinationEndpoint() => packetContents.DestinationEndpoint();
			public IList<PacketBundle> Bundles() => packetContents.Bundles();
			public uint CRC() => crc.ToUint();
			public PacketBytes Serialize() => bytes.Value;
		}
	}
}
