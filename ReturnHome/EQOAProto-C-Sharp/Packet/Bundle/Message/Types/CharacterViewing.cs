using ReturnHome;
using ReturnHome.Enumeration;
using ReturnHome.Packet.Support;
using ReturnHome.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface CharacterViewing : OpcodeMessage {
        public static readonly ushort OPCODE = 0x002c;
        IList<CharacterToView> Characters();

        public static OpcodeMessage Read(PacketBytes packetBytes) {
            VariableLengthEncodedInt characterCount = VariableLengthEncodedInt.Read(packetBytes);
            List<CharacterToView> characters = new List<CharacterToView>();
            for (long i = 0; i < characterCount.ToLong(); i++) {
                characters.Add(CharacterToView.Read(packetBytes));
            }
           return new CharacterViewing.Impl(characterCount, characters);
        }

        public static CharacterViewing Of(List<CharacterRepository.ViewingModel> characters) => new CharacterViewing.Impl(
            characterCount: VariableLengthEncodedInt.Of(characters.Count),
            characters: characters.Select(c => c.ToBinaryRecord()).ToList());

        private class Impl : CharacterViewing {
            readonly VariableLengthEncodedInt characterCount;
            readonly IList<CharacterToView> characters;
            readonly Lazy<PacketBytes> bytes;

            public IList<CharacterToView> Characters() => characters;
            public PacketBytes Serialize() => bytes.Value;
            public void Accept(HandleMessage handleMessage) => handleMessage.ProcessCharacterViewing(this);
            public OpcodeAndMessage ToOpcodeAndMessage() => OpcodeAndMessage.Of(opcode: CharacterViewing.OPCODE, opcodeMessage: this);

            public Impl(VariableLengthEncodedInt characterCount, List<CharacterToView> characters) {
                this.characterCount = characterCount;
                this.characters = characters.AsReadOnly();
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() {
                PacketBytes packetBytes = this.characterCount.Serialize();
                foreach (CharacterToView character in characters) {
                    packetBytes = packetBytes.Append(character.Serialize());
                }
                return packetBytes;
            }
        }

        public interface CharacterToView : BinaryRecord, CharacterRepository.ViewingModel {
            public static CharacterToView Read(PacketBytes packetBytes) {
                Uint32Le lengthOfCharacterName = Uint32Le.Read(packetBytes.PopFirst(bytes: 4));
                return new CharacterToView.Impl(
                    lengthOfCharacterName: lengthOfCharacterName,
                    characterName: ASCIIString.Read(packetBytes.PopFirst(bytes: (int)lengthOfCharacterName.ToUint())),
                    entityId: VariableLengthEncodedInt.Read(packetBytes),
                    modelId: VariableLengthEncodedInt.Read(packetBytes),
                    characterClass: VariableLengthEncodedInt.Read(packetBytes),
                    race: VariableLengthEncodedInt.Read(packetBytes),
                    level: VariableLengthEncodedInt.Read(packetBytes),
                    hairColor: VariableLengthEncodedInt.Read(packetBytes),
                    hairLength: VariableLengthEncodedInt.Read(packetBytes),
                    hairStyle: VariableLengthEncodedInt.Read(packetBytes),
                    face: VariableLengthEncodedInt.Read(packetBytes),
                    robe: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    primaryHandGraphic: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    secondaryHandGraphic: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    shieldGraphic: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    toonAnimation: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),
                    unknown: Uint8.Read(packetBytes.PopFirst(bytes: 1)),
                    chestGraphic: Uint8.Read(packetBytes.PopFirst(bytes: 1)),
                    bracerGraphic: Uint8.Read(packetBytes.PopFirst(bytes: 1)),
                    gloveGraphic: Uint8.Read(packetBytes.PopFirst(bytes: 1)),
                    pantsGraphic: Uint8.Read(packetBytes.PopFirst(bytes: 1)),
                    bootsGraphic: Uint8.Read(packetBytes.PopFirst(bytes: 1)),
                    helmGraphic: Uint8.Read(packetBytes.PopFirst(bytes: 1)),
                    unknown2: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),
                    unknown3: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    unknownColor1: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    unknownColor2: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    unknownColor3: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    chestColor: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    bracerColor: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    gloveColor: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    pantsColor: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    bootsColor: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    helmColor: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),
                    robeColor: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)));
            }

            public static CharacterToView Of(string characterName, long entityId, long modelId, byte characterClass, byte race, byte level, byte hairColor, byte hairLength, byte hairStyle, byte face, byte robe, uint primaryHandGraphic, uint secondaryHandGraphic, uint shieldGraphic, ushort toonAnimation, byte unknown, byte chestGraphic, byte bracerGraphic, byte gloveGraphic, byte pantsGraphic, byte bootsGraphic, byte helmGraphic, ushort unknown2, uint unknown3, uint unknownColor1, uint unknownColor2, uint unknownColor3, uint chestColor, uint bracerColor, uint gloveColor, uint pantsColor, uint bootsColor, uint helmColor, uint robeColor) {
                return new CharacterToView.Impl(
                    lengthOfCharacterName: Uint32Le.Of((uint)characterName.Length),
                    characterName: ASCIIString.Of(characterName),
                    entityId: VariableLengthEncodedInt.Of(entityId),
                    modelId: VariableLengthEncodedInt.Of(modelId),
                    characterClass: VariableLengthEncodedInt.Of(characterClass),
                    race: VariableLengthEncodedInt.Of(race),
                    level: VariableLengthEncodedInt.Of(level),
                    hairColor: VariableLengthEncodedInt.Of(hairColor),
                    hairLength: VariableLengthEncodedInt.Of(hairLength),
                    hairStyle: VariableLengthEncodedInt.Of(hairStyle),
                    face: VariableLengthEncodedInt.Of(face),
                    robe: Uint32Le.Of(robe == 0xff ? 0xffffffff : robe),
                    primaryHandGraphic: Uint32Le.Of(primaryHandGraphic),
                    secondaryHandGraphic: Uint32Le.Of(secondaryHandGraphic),
                    shieldGraphic: Uint32Le.Of(shieldGraphic),
                    toonAnimation: Uint16Le.Of(toonAnimation),
                    unknown: Uint8.Of(unknown),
                    chestGraphic: Uint8.Of(chestGraphic),
                    bracerGraphic: Uint8.Of(bracerGraphic),
                    gloveGraphic: Uint8.Of(gloveGraphic),
                    pantsGraphic: Uint8.Of(pantsGraphic),
                    bootsGraphic: Uint8.Of(bootsGraphic),
                    helmGraphic: Uint8.Of(helmGraphic),
                    unknown2: Uint16Le.Of(unknown2),
                    unknown3: Uint32Le.Of(unknown3),
                    unknownColor1: Uint32Le.Of(unknownColor1),
                    unknownColor2: Uint32Le.Of(unknownColor2),
                    unknownColor3: Uint32Le.Of(unknownColor3),
                    chestColor: Uint32Le.Of(chestColor),
                    bracerColor: Uint32Le.Of(bracerColor),
                    gloveColor: Uint32Le.Of(gloveColor),
                    pantsColor: Uint32Le.Of(pantsColor),
                    bootsColor: Uint32Le.Of(bootsColor),
                    helmColor: Uint32Le.Of(helmColor),
                    robeColor: Uint32Le.Of(robeColor));
            }

            private class Impl : CharacterToView {
                readonly Uint32Le lengthOfCharacterName;
                readonly ASCIIString characterName;
                readonly VariableLengthEncodedInt entityId;
                readonly VariableLengthEncodedInt modelId;
                readonly VariableLengthEncodedInt characterClass;
                readonly VariableLengthEncodedInt race;
                readonly VariableLengthEncodedInt level;
                readonly VariableLengthEncodedInt hairColor;
                readonly VariableLengthEncodedInt hairLength;
                readonly VariableLengthEncodedInt hairStyle;
                readonly VariableLengthEncodedInt face;
                readonly Uint32Le robe;
                readonly Uint32Le primaryHandGraphic;
                readonly Uint32Le secondaryHandGraphic;
                readonly Uint32Le shieldGraphic;
                readonly Uint16Le toonAnimation;
                readonly Uint8 unknown;
                readonly Uint8 chestGraphic;
                readonly Uint8 bracerGraphic;
                readonly Uint8 gloveGraphic;
                readonly Uint8 pantsGraphic;
                readonly Uint8 bootsGraphic;
                readonly Uint8 helmGraphic;
                readonly Uint16Le unknown2;
                readonly Uint32Le unknown3;
                readonly Uint32Le unknownColor1;
                readonly Uint32Le unknownColor2;
                readonly Uint32Le unknownColor3;
                readonly Uint32Le chestColor;
                readonly Uint32Le bracerColor;
                readonly Uint32Le gloveColor;
                readonly Uint32Le pantsColor;
                readonly Uint32Le bootsColor;
                readonly Uint32Le helmColor;
                readonly Uint32Le robeColor;
                readonly Lazy<PacketBytes> bytes;

                public string CharacterName() => characterName.ToString();
                public long EntityId() => entityId.ToLong();
                public long ModelId() => modelId.ToLong();
                public ReturnHome.Enumeration.CharacterClass CharacterClass() => ReturnHome.Enumeration.CharacterClass.Of((byte)characterClass.ToLong());
                public CharacterRace Race() => CharacterRace.Of((byte)race.ToLong());
                public byte Level() => (byte)level.ToLong();
                public CharacterHairColor HairColor() => CharacterHairColor.Of((byte)hairColor.ToLong());
                public CharacterHairLength HairLength() => CharacterHairLength.Of((byte)hairLength.ToLong());
                public CharacterHairStyle HairStyle() => CharacterHairStyle.Of((byte)hairStyle.ToLong());
                public CharacterFace Face() => CharacterFace.Of((byte)face.ToLong());
                public byte Robe() => (byte)robe.ToUint();
                public uint PrimaryHandGraphic() => primaryHandGraphic.ToUint();
                public uint SecondaryHandGraphic() => secondaryHandGraphic.ToUint();
                public uint ShieldGraphic() => shieldGraphic.ToUint();
                public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.Of(toonAnimation.ToUshort());
                public byte Unknown() => unknown.ToByte();
                public byte ChestGraphic() => chestGraphic.ToByte();
                public byte BracerGraphic() => bracerGraphic.ToByte();
                public byte GloveGraphic() => gloveGraphic.ToByte();
                public byte PantsGraphic() => pantsGraphic.ToByte();
                public byte BootsGraphic() => bootsGraphic.ToByte();
                public byte HelmGraphic() => helmGraphic.ToByte();
                public ushort Unknown2() => unknown2.ToUshort();
                public uint Unknown3() => unknown3.ToUint();
                public uint UnknownColor1() => unknownColor1.ToUint();
                public uint UnknownColor2() => unknownColor2.ToUint();
                public uint UnknownColor3() => unknownColor3.ToUint();
                public uint ChestColor() => chestColor.ToUint();
                public uint BracerColor() => bracerColor.ToUint();
                public uint GloveColor() => gloveColor.ToUint();
                public uint PantsColor() => pantsColor.ToUint();
                public uint BootsColor() => bootsColor.ToUint();
                public uint HelmColor() => helmColor.ToUint();
                public uint RobeColor() => robeColor.ToUint();
                public PacketBytes Serialize() => bytes.Value;
                public CharacterToView ToBinaryRecord() => this;

                public Impl(Uint32Le lengthOfCharacterName, ASCIIString characterName, VariableLengthEncodedInt entityId, VariableLengthEncodedInt modelId, VariableLengthEncodedInt characterClass, VariableLengthEncodedInt race, VariableLengthEncodedInt level, VariableLengthEncodedInt hairColor, VariableLengthEncodedInt hairLength, VariableLengthEncodedInt hairStyle, VariableLengthEncodedInt face, Uint32Le robe, Uint32Le primaryHandGraphic, Uint32Le secondaryHandGraphic, Uint32Le shieldGraphic, Uint16Le toonAnimation, Uint8 unknown, Uint8 chestGraphic, Uint8 bracerGraphic, Uint8 gloveGraphic, Uint8 pantsGraphic, Uint8 bootsGraphic, Uint8 helmGraphic, Uint16Le unknown2, Uint32Le unknown3, Uint32Le unknownColor1, Uint32Le unknownColor2, Uint32Le unknownColor3, Uint32Le chestColor, Uint32Le bracerColor, Uint32Le gloveColor, Uint32Le pantsColor, Uint32Le bootsColor, Uint32Le helmColor, Uint32Le robeColor) {
                    this.lengthOfCharacterName = lengthOfCharacterName;
                    this.characterName = characterName;
                    this.entityId = entityId;
                    this.modelId = modelId;
                    this.characterClass = characterClass;
                    this.race = race;
                    this.level = level;
                    this.hairColor = hairColor;
                    this.hairLength = hairLength;
                    this.hairStyle = hairStyle;
                    this.face = face;
                    this.robe = robe;
                    this.primaryHandGraphic = primaryHandGraphic;
                    this.secondaryHandGraphic = secondaryHandGraphic;
                    this.shieldGraphic = shieldGraphic;
                    this.toonAnimation = toonAnimation;
                    this.unknown = unknown;
                    this.chestGraphic = chestGraphic;
                    this.bracerGraphic = bracerGraphic;
                    this.gloveGraphic = gloveGraphic;
                    this.pantsGraphic = pantsGraphic;
                    this.bootsGraphic = bootsGraphic;
                    this.helmGraphic = helmGraphic;
                    this.unknown2 = unknown2;
                    this.unknown3 = unknown3;
                    this.unknownColor1 = unknownColor1;
                    this.unknownColor2 = unknownColor2;
                    this.unknownColor3 = unknownColor3;
                    this.chestColor = chestColor;
                    this.bracerColor = bracerColor;
                    this.gloveColor = gloveColor;
                    this.pantsColor = pantsColor;
                    this.bootsColor = bootsColor;
                    this.helmColor = helmColor;
                    this.robeColor = robeColor;
                    this.bytes = new Lazy<PacketBytes>(() => Bytes());
                }

                private PacketBytes Bytes() {
                    return lengthOfCharacterName.Serialize()
                        .Append(characterName.Serialize())
                        .Append(entityId.Serialize())
                        .Append(modelId.Serialize())
                        .Append(characterClass.Serialize())
                        .Append(race.Serialize())
                        .Append(level.Serialize())
                        .Append(hairColor.Serialize())
                        .Append(hairLength.Serialize())
                        .Append(hairStyle.Serialize())
                        .Append(face.Serialize())
                        .Append(robe.Serialize())
                        .Append(primaryHandGraphic.Serialize())
                        .Append(secondaryHandGraphic.Serialize())
                        .Append(shieldGraphic.Serialize())
                        .Append(toonAnimation.Serialize())
                        .Append(unknown.Serialize())
                        .Append(chestGraphic.Serialize())
                        .Append(bracerGraphic.Serialize())
                        .Append(gloveGraphic.Serialize())
                        .Append(pantsGraphic.Serialize())
                        .Append(bootsGraphic.Serialize())
                        .Append(helmGraphic.Serialize())
                        .Append(unknown2.Serialize())
                        .Append(unknown3.Serialize())
                        .Append(unknownColor1.Serialize())
                        .Append(unknownColor2.Serialize())
                        .Append(unknownColor3.Serialize())
                        .Append(chestColor.Serialize())
                        .Append(bracerColor.Serialize())
                        .Append(gloveColor.Serialize())
                        .Append(pantsColor.Serialize())
                        .Append(bootsColor.Serialize())
                        .Append(helmColor.Serialize())
                        .Append(robeColor.Serialize());
                }
            }
        }
    }
}
