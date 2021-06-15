using ReturnHome.Enumeration;
using System.Collections.Generic;
using ReturnHome.Packet.Bundle.Message.Types;

namespace ReturnHome.Repository {
    public interface CharacterRepository {
        List<ViewingModel> ViewingModelFor(string userName);

        //MemoryDumpModel GetMemoryDumpModelFor(long entityId);

        bool DeleteCharacter(string userName, uint entityId);

        bool CreateCharacter(string userName, CreationModel creationModel);

        public interface ViewingModel {
            string CharacterName();
            long EntityId();
            long ModelId();
            CharacterClass CharacterClass();
            CharacterRace Race();
            byte Level();
            CharacterHairColor HairColor();
            CharacterHairLength HairLength();
            CharacterHairStyle HairStyle();
            CharacterFace Face();
            byte Robe();
            uint PrimaryHandGraphic();
            uint SecondaryHandGraphic();
            uint ShieldGraphic();
            CharacterSelectAnimation ToonAnimation();
            byte Unknown();
            byte ChestGraphic();
            byte BracerGraphic();
            byte GloveGraphic();
            byte PantsGraphic();
            byte BootsGraphic();
            byte HelmGraphic();
            ushort Unknown2();
            uint Unknown3();
            uint UnknownColor1();
            uint UnknownColor2();
            uint UnknownColor3();
            uint ChestColor();
            uint BracerColor();
            uint GloveColor();
            uint PantsColor();
            uint BootsColor();
            uint HelmColor();
            uint RobeColor();
            CharacterViewing.CharacterToView ToBinaryRecord() => CharacterViewing.CharacterToView.Of(
                characterName: CharacterName(),
                entityId: EntityId(),
                modelId: ModelId(),
                characterClass: CharacterClass().ToByte(),
                race: Race().ToByte(),
                level: Level(),
                hairColor: HairColor().ToByte(),
                hairLength: HairLength().ToByte(),
                hairStyle: HairStyle().ToByte(),
                face: Face().ToByte(),
                robe: Robe(),
                primaryHandGraphic: PrimaryHandGraphic(),
                secondaryHandGraphic: SecondaryHandGraphic(),
                shieldGraphic: ShieldGraphic(),
                toonAnimation: ToonAnimation().ToUshort(),
                unknown: Unknown(),
                chestGraphic: ChestGraphic(),
                bracerGraphic: BracerGraphic(),
                gloveGraphic: GloveGraphic(),
                pantsGraphic: PantsGraphic(),
                bootsGraphic: BootsGraphic(),
                helmGraphic: HelmGraphic(),
                unknown2: Unknown2(),
                unknown3: Unknown3(),
                unknownColor1: UnknownColor1(),
                unknownColor2: UnknownColor2(),
                unknownColor3: UnknownColor3(),
                chestColor: ChestColor(),
                bracerColor: BracerColor(),
                gloveColor: GloveColor(),
                pantsColor: PantsColor(),
                bootsColor: BootsColor(),
                helmColor: HelmColor(),
                robeColor: RobeColor());
        }

        public interface CreationModel {
            string CharacterName();
            byte Level();
            CharacterRace Race();
            CharacterClass CharacterClass();
            CharacterGender Gender();
            CharacterHairColor HairColor();
            CharacterHairLength HairLength();
            CharacterHairStyle HairStyle();
            CharacterFace Face();
            CharacterHumanType HumanType();
            uint PointsAppliedToStrength();
            uint PointsAppliedToStamina();
            uint PointsAppliedToAgility();
            uint PointsAppliedToDexterity();
            uint PointsAppliedToWisdom();
            uint PointsAppliedToIntelligence();
            uint PointsAppliedToCharisma();
        }

        public interface MemoryDumpModel {
            string FileReference();
            int EntityId();
            string Name();

        }
    }
}
