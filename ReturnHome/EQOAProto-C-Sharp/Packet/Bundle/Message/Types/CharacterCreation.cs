using ReturnHome.Enumeration;
using ReturnHome.Packet.Support;
using ReturnHome.Repository;
using System;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface CharacterCreation : OpcodeMessage, CharacterRepository.CreationModel {
        public static ushort OPCODE = 0x002b;
        public static CharacterCreation Read(PacketBytes packetBytes) {
            Uint32Le lengthOfCharacterName = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
            return new CharacterCreation.Impl(
                lengthOfCharacterName: lengthOfCharacterName,
                characterName: ASCIIString.Read(packetBytes.PopFirst(bytes: (int)lengthOfCharacterName.ToUint())),
                level: VariableLengthEncodedInt.Read(packetBytes),
                race: VariableLengthEncodedInt.Read(packetBytes),
                characterClass: VariableLengthEncodedInt.Read(packetBytes),
                gender: VariableLengthEncodedInt.Read(packetBytes),
                hairColor: VariableLengthEncodedInt.Read(packetBytes),
                hairLength: VariableLengthEncodedInt.Read(packetBytes),
                hairStyle: VariableLengthEncodedInt.Read(packetBytes),
                face: VariableLengthEncodedInt.Read(packetBytes),
                humanType: VariableLengthEncodedInt.Read(packetBytes),
                pointsAppliedToStrength: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                pointsAppliedToStamina: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                pointsAppliedToAgility: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                pointsAppliedToDexterity: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                pointsAppliedToWisdom: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                pointsAppliedToIntelligence: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                pointsAppliedToCharisma: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)));
        }

        private class Impl : CharacterCreation {
            readonly Uint32Le lengthOfCharacterName;
            readonly ASCIIString characterName;
            readonly VariableLengthEncodedInt level;
            readonly VariableLengthEncodedInt race;
            readonly VariableLengthEncodedInt characterClass;
            readonly VariableLengthEncodedInt gender;
            readonly VariableLengthEncodedInt hairColor;
            readonly VariableLengthEncodedInt hairLength;
            readonly VariableLengthEncodedInt hairStyle;
            readonly VariableLengthEncodedInt face;
            readonly VariableLengthEncodedInt humanType;
            readonly Uint32Le pointsAppliedToStrength;
            readonly Uint32Le pointsAppliedToStamina;
            readonly Uint32Le pointsAppliedToAgility;
            readonly Uint32Le pointsAppliedToDexterity;
            readonly Uint32Le pointsAppliedToWisdom;
            readonly Uint32Le pointsAppliedToIntelligence;
            readonly Uint32Le pointsAppliedToCharisma;
            readonly Lazy<PacketBytes> bytes;

            public string CharacterName() => characterName.ToString();
            public byte Level() => (byte)level.ToLong();
            public CharacterRace Race() => CharacterRace.Of((byte)race.ToLong());
            public ReturnHome.Enumeration.CharacterClass CharacterClass() => ReturnHome.Enumeration.CharacterClass.Of((byte)characterClass.ToLong());
            public CharacterGender Gender() => CharacterGender.Of((byte)gender.ToLong());
            public CharacterHairColor HairColor() => CharacterHairColor.Of((byte)hairColor.ToLong());
            public CharacterHairLength HairLength() => CharacterHairLength.Of((byte)hairLength.ToLong());
            public CharacterHairStyle HairStyle() => CharacterHairStyle.Of((byte)hairStyle.ToLong());
            public CharacterFace Face() => CharacterFace.Of((byte)face.ToLong());
            public CharacterHumanType HumanType() => CharacterHumanType.Of((byte)humanType.ToLong());
            public uint PointsAppliedToStrength() => pointsAppliedToStrength.ToUint();
            public uint PointsAppliedToStamina() => pointsAppliedToStamina.ToUint();
            public uint PointsAppliedToAgility() => pointsAppliedToAgility.ToUint();
            public uint PointsAppliedToDexterity() => pointsAppliedToDexterity.ToUint();
            public uint PointsAppliedToWisdom() => pointsAppliedToWisdom.ToUint();
            public uint PointsAppliedToIntelligence() => pointsAppliedToIntelligence.ToUint();
            public uint PointsAppliedToCharisma() => pointsAppliedToCharisma.ToUint();
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessCharacterCreation(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: CharacterCreation.OPCODE, opcodeMessage: this);

            public Impl(Uint32Le lengthOfCharacterName, ASCIIString characterName, VariableLengthEncodedInt level, VariableLengthEncodedInt race, VariableLengthEncodedInt characterClass, VariableLengthEncodedInt gender, VariableLengthEncodedInt hairColor, VariableLengthEncodedInt hairLength, VariableLengthEncodedInt hairStyle, VariableLengthEncodedInt face, VariableLengthEncodedInt humanType, Uint32Le pointsAppliedToStrength, Uint32Le pointsAppliedToStamina, Uint32Le pointsAppliedToAgility, Uint32Le pointsAppliedToDexterity, Uint32Le pointsAppliedToWisdom, Uint32Le pointsAppliedToIntelligence, Uint32Le pointsAppliedToCharisma) {
                this.lengthOfCharacterName = lengthOfCharacterName;
                this.characterName = characterName;
                this.level = level;
                this.race = race;
                this.characterClass = characterClass;
                this.gender = gender;
                this.hairColor = hairColor;
                this.hairLength = hairLength;
                this.hairStyle = hairStyle;
                this.face = face;
                this.humanType = humanType;
                this.pointsAppliedToStrength = pointsAppliedToStrength;
                this.pointsAppliedToStamina = pointsAppliedToStamina;
                this.pointsAppliedToAgility = pointsAppliedToAgility;
                this.pointsAppliedToDexterity = pointsAppliedToDexterity;
                this.pointsAppliedToWisdom = pointsAppliedToWisdom;
                this.pointsAppliedToIntelligence = pointsAppliedToIntelligence;
                this.pointsAppliedToCharisma = pointsAppliedToCharisma;
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                return lengthOfCharacterName.Serialize()
                    .Append(characterName.Serialize())
                    .Append(level.Serialize())
                    .Append(race.Serialize())
                    .Append(characterClass.Serialize())
                    .Append(gender.Serialize())
                    .Append(hairColor.Serialize())
                    .Append(hairLength.Serialize())
                    .Append(hairStyle.Serialize())
                    .Append(face.Serialize())
                    .Append(humanType.Serialize())
                    .Append(pointsAppliedToStrength.Serialize())
                    .Append(pointsAppliedToStamina.Serialize())
                    .Append(pointsAppliedToAgility.Serialize())
                    .Append(pointsAppliedToDexterity.Serialize())
                    .Append(pointsAppliedToWisdom.Serialize())
                    .Append(pointsAppliedToIntelligence.Serialize())
                    .Append(pointsAppliedToCharisma.Serialize());
            }
        }
    }
}
