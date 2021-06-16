using EQOAProto_C_Sharp.Repository;
using EQOAProto_C_Sharp.Enumeration;
using System;
using System.Collections.Generic;

namespace EQOAProto_C_Sharp.UnitTests.Packet {
    class MockCharacterRepository : CharacterRepository {
        private static readonly Lazy<List<CharacterRepository.ViewingModel>> playerList = new Lazy<List<CharacterRepository.ViewingModel>>(() => {
            return new List<CharacterRepository.ViewingModel>(){
                new Ferry(),
                new Daydrift(),
                new Lear(),
                new Kencade(),
                new Hymnofpower(),
                new Necnok(),
                new Dudderz(),
                new Corstensbank()
            };
        });
        public List<CharacterRepository.ViewingModel> ViewingModelFor(string userName) {
            return playerList.Value;
        }

        public bool DeleteCharacter(string userName, uint entityId) => true;

        public bool CreateCharacter(string userName, CharacterRepository.CreationModel creationModel) => true;
    }

    class Ferry : CharacterRepository.ViewingModel {
        public string CharacterName() => "Ferry";
        public long EntityId() => 1140744;
        public long ModelId() => 1640644319;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.BRD;
        public CharacterRace Race() => CharacterRace.BAR;
        public byte Level() => 60;
        public CharacterHairColor HairColor() => CharacterHairColor.BLACK;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_0;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_2;
        public CharacterFace Face() => CharacterFace.FACE_3;
        public byte Robe() => 0;
        public uint PrimaryHandGraphic() => 0xd8406c85;
        public uint SecondaryHandGraphic() => 0xbf7e7dcb;
        public uint ShieldGraphic() => 0;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.ONE_HAND_BLUNT;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 3;
        public byte BracerGraphic() => 0;
        public byte GloveGraphic() => 3;
        public byte PantsGraphic() => 3;
        public byte BootsGraphic() => 3;
        public byte HelmGraphic() => 7;
        public ushort Unknown2() => 0;
        public uint Unknown3() => 0;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xffffffff;
        public uint ChestColor() => 0xffffffff;
        public uint BracerColor() => 0xffffffff;
        public uint GloveColor() => 0xffffffff;
        public uint PantsColor() => 0xffffffff;
        public uint BootsColor() => 0xff008000;
        public uint HelmColor() => 0xff00f0f0;
        public uint RobeColor() => 0xff008000;
    }

    class Daydrift : CharacterRepository.ViewingModel {
        public string CharacterName() => "Daydrift";
        public long EntityId() => 1142499;
        public long ModelId() => -657100808;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.ENC;
        public CharacterRace Race() => CharacterRace.ELF;
        public byte Level() => 60;
        public CharacterHairColor HairColor() => CharacterHairColor.BLACK;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_2;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_1;
        public CharacterFace Face() => CharacterFace.FACE_1;
        public byte Robe() => 0;
        public uint PrimaryHandGraphic() => 0x7c7dbea9;
        public uint SecondaryHandGraphic() => 0;
        public uint ShieldGraphic() => 0x2ec56029;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.ONE_HAND_BLUNT;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 1;
        public byte BracerGraphic() => 0;
        public byte GloveGraphic() => 1;
        public byte PantsGraphic() => 1;
        public byte BootsGraphic() => 1;
        public byte HelmGraphic() => 1;
        public ushort Unknown2() => 0;
        public uint Unknown3() => 0x0302;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xff643920;
        public uint ChestColor() => 0xff800080;
        public uint BracerColor() => 0xffffffff;
        public uint GloveColor() => 0xff94763f;
        public uint PantsColor() => 0xff800080;
        public uint BootsColor() => 0xff750075;
        public uint HelmColor() => 0xff884444;
        public uint RobeColor() => 0xff0000ff;
    }

    class Lear : CharacterRepository.ViewingModel {
        public string CharacterName() => "Lear";
        public long EntityId() => 1144189;
        public long ModelId() => -657100808;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.DRD;
        public CharacterRace Race() => CharacterRace.ELF;
        public byte Level() => 60;
        public CharacterHairColor HairColor() => CharacterHairColor.BROWN;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_3;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_1;
        public CharacterFace Face() => CharacterFace.FACE_1;
        public byte Robe() => 0;
        public uint PrimaryHandGraphic() => 0xd8406c85;
        public uint SecondaryHandGraphic() => 0;
        public uint ShieldGraphic() => 0x0aa6e873;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.ONE_HAND_BLUNT;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 2;
        public byte BracerGraphic() => 0;
        public byte GloveGraphic() => 2;
        public byte PantsGraphic() => 2;
        public byte BootsGraphic() => 2;
        public byte HelmGraphic() => 2;
        public ushort Unknown2() => 0;
        public uint Unknown3() => 0x00030303;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xff006450;
        public uint ChestColor() => 0xff404000;
        public uint BracerColor() => 0xffffffff;
        public uint GloveColor() => 0xff404000;
        public uint PantsColor() => 0xff404000;
        public uint BootsColor() => 0xff404000;
        public uint HelmColor() => 0xff006450;
        public uint RobeColor() => 0xff008000;
    }

     class Kencade : CharacterRepository.ViewingModel {
        public string CharacterName() => "Kencade";
        public long EntityId() => 1155682;
        public long ModelId() => -657100808;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.CL;
        public CharacterRace Race() => CharacterRace.ELF;
        public byte Level() => 60;
        public CharacterHairColor HairColor() => CharacterHairColor.BLACK;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_3;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_3;
        public CharacterFace Face() => CharacterFace.FACE_1;
        public byte Robe() => 0;
        public uint PrimaryHandGraphic() => 0xd8406c85;
        public uint SecondaryHandGraphic() => 0x992ce4a8;
        public uint ShieldGraphic() => 0;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.ONE_HAND_BLUNT;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 4;
        public byte BracerGraphic() => 0;
        public byte GloveGraphic() => 4;
        public byte PantsGraphic() => 4;
        public byte BootsGraphic() => 4;
        public byte HelmGraphic() => 4;
        public ushort Unknown2() => 1;
        public uint Unknown3() => 0x00030003;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xffffffff;
        public uint ChestColor() => 0xff59ed1b;
        public uint BracerColor() => 0xffffffff;
        public uint GloveColor() => 0xffbd7979;
        public uint PantsColor() => 0xff3c842d;
        public uint BootsColor() => 0xffbd7979;
        public uint HelmColor() => 0xff389c9e;
        public uint RobeColor() => 0xff008000;
    }

     class Hymnofpower : CharacterRepository.ViewingModel {
        public string CharacterName() => "Hymnofpower";
        public long EntityId() => 1204345;
        public long ModelId() => -657100808;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.BRD;
        public CharacterRace Race() => CharacterRace.ELF;
        public byte Level() => 60;
        public CharacterHairColor HairColor() => CharacterHairColor.BLACK;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_3;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_2;
        public CharacterFace Face() => CharacterFace.FACE_1;
        public byte Robe() => 1;
        public uint PrimaryHandGraphic() => 0x17f9870b;
        public uint SecondaryHandGraphic() => 0xf4dd991a;
        public uint ShieldGraphic() => 0;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.OFF_HAND_PIERCE;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 3;
        public byte BracerGraphic() => 3;
        public byte GloveGraphic() => 3;
        public byte PantsGraphic() => 3;
        public byte BootsGraphic() => 3;
        public byte HelmGraphic() => 0;
        public ushort Unknown2() => 3;
        public uint Unknown3() => 0x00030302;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xffffffff;
        public uint ChestColor() => 0xffd6ff80;
        public uint BracerColor() => 0xff9f9f00;
        public uint GloveColor() => 0xff824100;
        public uint PantsColor() => 0xff9f9f00;
        public uint BootsColor() => 0xffc08000;
        public uint HelmColor() => 0xffffffff;
        public uint RobeColor() => 0xff000015;
    }

     class Necnok : CharacterRepository.ViewingModel {
        public string CharacterName() => "Necnok";
        public long EntityId() => 1216726;
        public long ModelId() => -1449366763;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.NEC;
        public CharacterRace Race() => CharacterRace.GNO;
        public byte Level() => 60;
        public CharacterHairColor HairColor() => CharacterHairColor.GRAY;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_3;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_1;
        public CharacterFace Face() => CharacterFace.FACE_0;
        public byte Robe() => 0;
        public uint PrimaryHandGraphic() => 0x7c7dbea9;
        public uint SecondaryHandGraphic() => 0x0c088b7c;
        public uint ShieldGraphic() => 0;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.ONE_HAND_BLUNT;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 1;
        public byte BracerGraphic() => 0;
        public byte GloveGraphic() => 1;
        public byte PantsGraphic() => 1;
        public byte BootsGraphic() => 1;
        public byte HelmGraphic() => 1;
        public ushort Unknown2() => 0;
        public uint Unknown3() => 0;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xffffffff;
        public uint ChestColor() => 0xff3c0096;
        public uint BracerColor() => 0xffffffff;
        public uint GloveColor() => 0xff3c0096;
        public uint PantsColor() => 0xff3c0096;
        public uint BootsColor() => 0xff626231;
        public uint HelmColor() => 0xff000015;
        public uint RobeColor() => 0xff0000ff;
    }

     class Dudderz : CharacterRepository.ViewingModel {
        public string CharacterName() => "Dudderz";
        public long EntityId() => 1224955;
        public long ModelId() => 1282385202;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.WAR;
        public CharacterRace Race() => CharacterRace.TRL;
        public byte Level() => 60;
        public CharacterHairColor HairColor() => CharacterHairColor.BLACK;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_3;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_0;
        public CharacterFace Face() => CharacterFace.FACE_2;
        public byte Robe() => 0;
        public uint PrimaryHandGraphic() => 0x6eb7224d;
        public uint SecondaryHandGraphic() => 0x17f9870b;
        public uint ShieldGraphic() => 0;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.OFF_HAND_SLASH;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 4;
        public byte BracerGraphic() => 0;
        public byte GloveGraphic() => 4;
        public byte PantsGraphic() => 4;
        public byte BootsGraphic() => 4;
        public byte HelmGraphic() => 4;
        public ushort Unknown2() => 0;
        public uint Unknown3() => 0;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xffffffff;
        public uint ChestColor() => 0xff000062;
        public uint BracerColor() => 0xffffffff;
        public uint GloveColor() => 0xff800040;
        public uint PantsColor() => 0xffb4aaa0;
        public uint BootsColor() => 0xff800040;
        public uint HelmColor() => 0xff800040;
        public uint RobeColor() => 0xffff0000;
    }

     class Corstensbank : CharacterRepository.ViewingModel {
        public string CharacterName() => "Corstensbank";
        public long EntityId() => 1260509;
        public long ModelId() => 1893243078;
        public EQOAProto_C_Sharp.Enumeration.CharacterClass CharacterClass() => EQOAProto_C_Sharp.Enumeration.CharacterClass.MAG;
        public CharacterRace Race() => CharacterRace.HUM;
        public byte Level() => 1;
        public CharacterHairColor HairColor() => CharacterHairColor.BROWN;
        public CharacterHairLength HairLength() => CharacterHairLength.LENGTH_2;
        public CharacterHairStyle HairStyle() => CharacterHairStyle.STYLE_2;
        public CharacterFace Face() => CharacterFace.FACE_2;
        public byte Robe() => 0xff;
        public uint PrimaryHandGraphic() => 0;
        public uint SecondaryHandGraphic() => 0;
        public uint ShieldGraphic() => 0;
        public CharacterSelectAnimation ToonAnimation() => CharacterSelectAnimation.STANDING;
        public byte Unknown() => 0;
        public byte ChestGraphic() => 0;
        public byte BracerGraphic() => 0;
        public byte GloveGraphic() => 0;
        public byte PantsGraphic() => 0;
        public byte BootsGraphic() => 0;
        public byte HelmGraphic() => 0;
        public ushort Unknown2() => 0;
        public uint Unknown3() => 0;
        public uint UnknownColor1() => 0xffffffff;
        public uint UnknownColor2() => 0xffffffff;
        public uint UnknownColor3() => 0xffffffff;
        public uint ChestColor() => 0xffffffff;
        public uint BracerColor() => 0xffffffff;
        public uint GloveColor() => 0xffffffff;
        public uint PantsColor() => 0xffffffff;
        public uint BootsColor() => 0xffffffff;
        public uint HelmColor() => 0xffffffff;
        public uint RobeColor() => 0xffffffff;
    }
}
