
using System;
using ReturnHome.Packet.Support;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface MemoryDump : OpcodeMessage {   
        public static readonly ushort OPCODE = 0x000d;

        //public static OpcodeMessage Read(PacketBytes packetBytes) {
        //    VariableLengthEncodedInt serverCount = VariableLengthEncodedInt.Read(packetBytes);
        //    List<GameServer> servers = new List<GameServer>();
        //    for (long i = 0; i < serverCount.ToLong(); i++) {
        //        servers.Add(GameServer.Read(packetBytes));
        //    }
        //   return new MemoryDump.Impl(serverCount, servers);
        //}

        //public static GameServerList Of() => new Memory.Impl(
        //    serverCount: VariableLengthEncodedInt.Of(servers.Count),
        //    servers: servers.Select(s => s.ToBinaryRecord()).ToList());

        public interface CharacterInitialLoginInformation : BinaryRecord {
            public static CharacterInitialLoginInformation Read(PacketBytes packetBytes) {
                Uint8 unknown = Uint8.Read(packetBytes.PopFirst(bytes: 1));
                int fileReferenceLength = (int)(Uint32Le.Read(packetBytes.PopFirst(bytes: 4)).ToUint());
                ASCIIString fileReference = ASCIIString.Read(packetBytes.PopFirst(bytes: fileReferenceLength));
                VariableLengthEncodedInt entityId = VariableLengthEncodedInt.Read(packetBytes);
                int nameLength = (int)(Uint32Le.Read(packetBytes.PopFirst(bytes: 4)).ToUint());
                ASCIIString name = ASCIIString.Read(packetBytes);
                VariableLengthEncodedInt characterClass = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt race = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt level = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt experiencePoints = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt experienceDebt = VariableLengthEncodedInt.Read(packetBytes);
                Uint8 breath = Uint8.Read(packetBytes.PopFirst(bytes: 1));
                VariableLengthEncodedInt tunarHeld = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt tunarInBank = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt unspentTrainingPoints = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt maximumBaseTrainingPoints = VariableLengthEncodedInt.Read(packetBytes);
                VariableLengthEncodedInt worldId = VariableLengthEncodedInt.Read(packetBytes);
                FloatLe xPosition = FloatLe.Read(packetBytes.PopFirst(bytes: 4));
                FloatLe zPosition = FloatLe.Read(packetBytes.PopFirst(bytes: 4));
                FloatLe yPosition = FloatLe.Read(packetBytes.PopFirst(bytes: 4));
                FloatLe facing = FloatLe.Read(packetBytes.PopFirst(bytes: 4));
                Uint64Le unknown2 = Uint64Le.Read(packetBytes.PopFirst(bytes: 8));
                return new CharacterInitialLoginInformation.Impl(
                    unknown: unknown,
                    fileReference: fileReference,
                    entityId: entityId,
                    name: name,
                    characterClass: characterClass,
                    race: race,
                    level: level,
                    experiencePoints: experiencePoints,
                    experienceDebt: experienceDebt,
                    breath: breath,
                    tunarHeld: tunarHeld,
                    tunarInBank: tunarInBank,
                    unspentTrainingPoints: unspentTrainingPoints,
                    maximumBaseTrainingPoints: maximumBaseTrainingPoints,
                    worldId: worldId,
                    xPosition: xPosition,
                    zPosition: zPosition,
                    yPosition: yPosition,
                    facing: facing,
                    unknown2: unknown2);
            }

            public static CharacterInitialLoginInformation Of(byte unknown, string fileReference, long entityId, string name, byte characterClass, byte race, byte level, long experiencePoints, long experienceDebt, byte breath, long tunarHeld, long tunarInBank, long unspentTrainingPoints, long maximumBaseTrainingPoints, byte worldId, float xPosition, float zPosition, float yPosition, float facing, ulong unknown2) {
                return new CharacterInitialLoginInformation.Impl(
                    unknown: Uint8.Of(unknown),
                    fileReference: ASCIIString.Of(fileReference),
                    entityId: VariableLengthEncodedInt.Of(entityId),
                    name: ASCIIString.Of(name),
                    characterClass: VariableLengthEncodedInt.Of(characterClass),
                    race: VariableLengthEncodedInt.Of(race),
                    level: VariableLengthEncodedInt.Of(level),
                    experiencePoints: VariableLengthEncodedInt.Of(experiencePoints),
                    experienceDebt: VariableLengthEncodedInt.Of(experienceDebt),
                    breath: Uint8.Of(breath),
                    tunarHeld: VariableLengthEncodedInt.Of(tunarHeld),
                    tunarInBank: VariableLengthEncodedInt.Of(tunarInBank),
                    unspentTrainingPoints: VariableLengthEncodedInt.Of(unspentTrainingPoints),
                    maximumBaseTrainingPoints: VariableLengthEncodedInt.Of(maximumBaseTrainingPoints),
                    worldId: VariableLengthEncodedInt.Of(worldId),
                    xPosition: FloatLe.Of(xPosition),
                    zPosition: FloatLe.Of(zPosition),
                    yPosition: FloatLe.Of(yPosition),
                    facing: FloatLe.Of(facing),
                    unknown2: Uint64Le.Of(unknown2));
            }

            private class Impl : CharacterInitialLoginInformation {
                private readonly Uint8 unknown;
                private readonly ASCIIString fileReference;
                private readonly VariableLengthEncodedInt entityId;
                private readonly ASCIIString name;
                private readonly VariableLengthEncodedInt characterClass;
                private readonly VariableLengthEncodedInt race;
                private readonly VariableLengthEncodedInt level;
                private readonly VariableLengthEncodedInt experiencePoints;
                private readonly VariableLengthEncodedInt experienceDebt;
                private readonly Uint8 breath;
                private readonly VariableLengthEncodedInt tunarHeld;
                private readonly VariableLengthEncodedInt tunarInBank;
                private readonly VariableLengthEncodedInt unspentTrainingPoints;
                private readonly VariableLengthEncodedInt maximumBaseTrainingPoints;
                private readonly VariableLengthEncodedInt worldId;
                private readonly FloatLe xPosition;
                private readonly FloatLe zPosition;
                private readonly FloatLe yPosition;
                private readonly FloatLe facing;
                private readonly Uint64Le unknown2;
                private readonly Lazy<PacketBytes> bytes;

                public Impl(Uint8 unknown, ASCIIString fileReference, VariableLengthEncodedInt entityId, ASCIIString name, VariableLengthEncodedInt characterClass, VariableLengthEncodedInt race, VariableLengthEncodedInt level, VariableLengthEncodedInt experiencePoints, VariableLengthEncodedInt experienceDebt, Uint8 breath, VariableLengthEncodedInt tunarHeld, VariableLengthEncodedInt tunarInBank, VariableLengthEncodedInt unspentTrainingPoints, VariableLengthEncodedInt maximumBaseTrainingPoints, VariableLengthEncodedInt worldId, FloatLe xPosition, FloatLe zPosition, FloatLe yPosition, FloatLe facing, Uint64Le unknown2) {
                    this.unknown = unknown;
                    this.fileReference = fileReference;
                    this.entityId = entityId;
                    this.name = name;
                    this.characterClass = characterClass;
                    this.race = race;
                    this.level = level;
                    this.experiencePoints = experiencePoints;
                    this.experienceDebt = experienceDebt;
                    this.breath = breath;
                    this.tunarHeld = tunarHeld;
                    this.tunarInBank = tunarInBank;
                    this.unspentTrainingPoints = unspentTrainingPoints;
                    this.maximumBaseTrainingPoints = maximumBaseTrainingPoints;
                    this.worldId = worldId;
                    this.xPosition = xPosition;
                    this.zPosition = zPosition;
                    this.yPosition = yPosition;
                    this.unknown2 = unknown2;
                    this.bytes = new Lazy<PacketBytes>(() => Bytes());
                }

                private PacketBytes Bytes() {
                    return unknown.Serialize()
                        .Append(fileReference.Serialize())
                        .Append(entityId.Serialize())
                        .Append(name.Serialize())
                        .Append(characterClass.Serialize())
                        .Append(race.Serialize())
                        .Append(level.Serialize())
                        .Append(experiencePoints.Serialize())
                        .Append(experienceDebt.Serialize())
                        .Append(breath.Serialize())
                        .Append(tunarHeld.Serialize())
                        .Append(tunarInBank.Serialize())
                        .Append(unspentTrainingPoints.Serialize())
                        .Append(maximumBaseTrainingPoints.Serialize())
                        .Append(worldId.Serialize())
                        .Append(xPosition.Serialize())
                        .Append(zPosition.Serialize())
                        .Append(yPosition.Serialize())
                        .Append(unknown2.Serialize());
                }

                public PacketBytes Serialize() => bytes.Value;
            }

            public interface Hotkey {
                //public static CharacterInitialLoginInformation Read(PacketBytes packetBytes) {
                //    return
                //}
            }
        }
    }
}
