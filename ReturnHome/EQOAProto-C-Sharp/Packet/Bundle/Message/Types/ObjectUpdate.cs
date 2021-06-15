using ReturnHome.Packet.Support;
using System;

namespace ReturnHome.Packet.Bundle.Message.Types {
    public interface ObjectUpdate : UncompressedUpdateMessage {
        string Name();
        public static ObjectUpdate Read(PacketBytes packetBytes, ObjectUpdate xorUsing) => null;

        public static ObjectUpdate Read(PacketBytes packetBytes) => new ObjectUpdate.Impl(
            unknown: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//1
            entityId: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//5
            unknown2: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//6
            x: Uint24Le.Read(packetBytes.PopFirst(bytes: 3)),//9
            z: Uint24Le.Read(packetBytes.PopFirst(bytes: 3)),//12
            y: Uint24Le.Read(packetBytes.PopFirst(bytes: 3)),//15
            facing: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//16
            firstPlayerVerticalFacing: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//17
            worldId: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//18
            timeOfKill1: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//22
            timeOfKill2: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//26
            hpShowFlag: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//27
            hpPercentage: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//28
            model: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//32
            upgradedModel: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//36
            size: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//40
            xCoordinatesOffset: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),//42
            yCoordinatesOffset: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),//44
            unknown3: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//45
            eastToWestSpeed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//46
            lateralMovement: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//47
            northToSouthSpeed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//48
            turning: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//49
            spinUpAndDownRate: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//50
            zCoord: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//54
            facingDetail: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//58
            animation: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//59
            targetEntityId: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//63
            unknown4: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//67
            mainHandWeapon: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//71
            offHandWeapon: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//75
            shield: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//79
            primaryMainHandWeapon: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//83
            primaryOffHandWeapon: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//87
            primaryShield: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//91
            unknown5: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),//93
            torsoModel: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//94
            forearmsModel: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//95
            glovesModel: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//96
            legsModel: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//97
            feetModel: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//98
            headModel: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//99
            chestVanillaColor: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//100
            unknown6: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//101
            bracerVanillaColor: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//102
            unknown7: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//103
            glovesVanillaColor: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//104
            unknown8: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//105
            pantsVanillaColor: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//106
            unknown9: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//107
            bootsVanillaColor: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//108
            unknown10: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//109
            helmVanillaColor: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//110
            unknown11: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//111
            chestColorRed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//112
            chestColorBlue: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//113
            chestColorGreen: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//114
            chestColorAlpha: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//115
            unknown12: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//116
            bracerColorRed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//117
            bracerColorBlue: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//118
            bracerColorGreen: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//119
            bracerColorAlpha: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//120
            unknown13: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//121
            glovesColorRed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//122
            glovesColorBlue: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//123
            glovesColorGreen: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//124
            glovesColorAlpha: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//125
            unknown14: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//126
            pantsColorRed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//127
            pantsColorBlue: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//128
            pantsColorGreen: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//129
            pantsColorAlpha: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//130
            unknown15: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//131
            bootsColorRed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//132
            bootsColorBlue: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//133
            bootsColorGreen: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//134
            bootsColorAlpha: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//135
            unknown16: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//136
            helmColorRed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//137
            helmColorBlue: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//138
            helmColorGreen: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//139
            helmColorAlpha: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//140
            unknown17: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//141
            robeColorRed: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//142
            robeColorBlue: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//143
            robeColorGreen: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//144
            robeColorAlpha: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//145
            hairColor: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//146
            hairLength: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//147
            hairStyle: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//148
            face: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//149
            robeStyle: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//150
            unknown18: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//154
            unknown19: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//155
            name: ASCIIString.Read(packetBytes.PopFirst(bytes: 24)),//179
            level: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//180
            hasMovement: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//181
            conColorFlag: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//182
            targetNameplate: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//183
            race: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//184
            characterClass: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//185
            npcType: Uint16Le.Read(packetBytes.PopFirst(bytes: 2)),//187
            npcPatternId: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)),//191
            unknown20: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//192
            unknown21: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//193
            invisPoisonDiseaseFlags: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//194
            unknown22: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//195
            unknown23: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//196
            unknown24: Uint8.Read(packetBytes.PopFirst(bytes: 1)),//197
            messageEnd: Uint32Le.Read(packetBytes.PopFirst(bytes: 4)));//201

        private class Impl : ObjectUpdate, UncompressedUpdateMessage {
            readonly Uint8 unknown;
            readonly Uint32Le entityId;
            readonly Uint8 unknown2;
            readonly Uint24Le x;
            readonly Uint24Le z;
            readonly Uint24Le y;
            readonly Uint8 facing;
            readonly Uint8 firstPlayerVerticalFacing;
            readonly Uint8 worldId;
            readonly Uint32Le timeOfKill1;
            readonly Uint32Le timeOfKill2;
            readonly Uint8 hpShowFlag;
            readonly Uint8 hpPercentage;
            readonly Uint32Le model;
            readonly Uint32Le upgradedModel;
            readonly Uint32Le size;
            readonly Uint16Le xCoordinatesOffset;
            readonly Uint16Le yCoordinatesOffset;
            readonly Uint8 unknown3;
            readonly Uint8 eastToWestSpeed;
            readonly Uint8 lateralMovement;
            readonly Uint8 northToSouthSpeed;
            readonly Uint8 turning;
            readonly Uint8 spinUpAndDownRate;
            readonly Uint32Le zCoord;
            readonly Uint32Le facingDetail;
            readonly Uint8 animation;
            readonly Uint32Le targetEntityId;
            readonly Uint32Le unknown4;
            readonly Uint32Le mainHandWeapon;
            readonly Uint32Le offHandWeapon;
            readonly Uint32Le shield;
            readonly Uint32Le primaryMainHandWeapon;
            readonly Uint32Le primaryOffHandWeapon;
            readonly Uint32Le primaryShield;
            readonly Uint16Le unknown5;
            readonly Uint8 torsoModel;
            readonly Uint8 forearmsModel;
            readonly Uint8 glovesModel;
            readonly Uint8 legsModel;
            readonly Uint8 feetModel;
            readonly Uint8 headModel;
            readonly Uint8 chestVanillaColor;
            readonly Uint8 unknown6;
            readonly Uint8 bracerVanillaColor;
            readonly Uint8 unknown7;
            readonly Uint8 glovesVanillaColor;
            readonly Uint8 unknown8;
            readonly Uint8 pantsVanillaColor;
            readonly Uint8 unknown9;
            readonly Uint8 bootsVanillaColor;
            readonly Uint8 unknown10;
            readonly Uint8 helmVanillaColor;
            readonly Uint8 unknown11;
            readonly Uint8 chestColorRed;
            readonly Uint8 chestColorBlue;
            readonly Uint8 chestColorGreen;
            readonly Uint8 chestColorAlpha;
            readonly Uint8 unknown12;
            readonly Uint8 bracerColorRed;
            readonly Uint8 bracerColorBlue;
            readonly Uint8 bracerColorGreen;
            readonly Uint8 bracerColorAlpha;
            readonly Uint8 unknown13;
            readonly Uint8 glovesColorRed;
            readonly Uint8 glovesColorBlue;
            readonly Uint8 glovesColorGreen;
            readonly Uint8 glovesColorAlpha;
            readonly Uint8 unknown14;
            readonly Uint8 pantsColorRed;
            readonly Uint8 pantsColorBlue;
            readonly Uint8 pantsColorGreen;
            readonly Uint8 pantsColorAlpha;
            readonly Uint8 unknown15;
            readonly Uint8 bootsColorRed;
            readonly Uint8 bootsColorBlue;
            readonly Uint8 bootsColorGreen;
            readonly Uint8 bootsColorAlpha;
            readonly Uint8 unknown16;
            readonly Uint8 helmColorRed;
            readonly Uint8 helmColorBlue;
            readonly Uint8 helmColorGreen;
            readonly Uint8 helmColorAlpha;
            readonly Uint8 unknown17;
            readonly Uint8 robeColorRed;
            readonly Uint8 robeColorBlue;
            readonly Uint8 robeColorGreen;
            readonly Uint8 robeColorAlpha;
            readonly Uint8 hairColor;
            readonly Uint8 hairLength;
            readonly Uint8 hairStyle;
            readonly Uint8 face;
            readonly Uint8 robeStyle;
            readonly Uint32Le unknown18;
            readonly Uint8 unknown19;
            readonly ASCIIString name;
            readonly Uint8 level;
            readonly Uint8 hasMovement;
            readonly Uint8 conColorFlag;
            readonly Uint8 targetNameplate;
            readonly Uint8 race;
            readonly Uint8 characterClass;
            readonly Uint16Le npcType;
            readonly Uint32Le npcPatternId;
            readonly Uint8 unknown20;
            readonly Uint8 unknown21;
            readonly Uint8 invisPoisonDiseaseFlags;
            readonly Uint8 unknown22;
            readonly Uint8 unknown23;
            readonly Uint8 unknown24;
            readonly Uint32Le messageEnd;
            readonly Lazy<PacketBytes> bytes;

            public string Name() => name.ToString().TrimEnd(new char[]{(char)0x00});
            public PacketBytes Serialize() => bytes.Value;

            public Impl(Uint8 unknown, Uint32Le entityId, Uint8 unknown2, Uint24Le x, Uint24Le z, Uint24Le y, Uint8 facing, Uint8 firstPlayerVerticalFacing, Uint8 worldId, Uint32Le timeOfKill1, Uint32Le timeOfKill2, Uint8 hpShowFlag, Uint8 hpPercentage, Uint32Le model, Uint32Le upgradedModel, Uint32Le size, Uint16Le xCoordinatesOffset, Uint16Le yCoordinatesOffset, Uint8 unknown3, Uint8 eastToWestSpeed, Uint8 lateralMovement, Uint8 northToSouthSpeed, Uint8 turning, Uint8 spinUpAndDownRate, Uint32Le zCoord, Uint32Le facingDetail, Uint8 animation, Uint32Le targetEntityId, Uint32Le unknown4, Uint32Le mainHandWeapon, Uint32Le offHandWeapon, Uint32Le shield, Uint32Le primaryMainHandWeapon, Uint32Le primaryOffHandWeapon, Uint32Le primaryShield, Uint16Le unknown5, Uint8 torsoModel, Uint8 forearmsModel, Uint8 glovesModel, Uint8 legsModel, Uint8 feetModel, Uint8 headModel, Uint8 chestVanillaColor, Uint8 unknown6, Uint8 bracerVanillaColor, Uint8 unknown7, Uint8 glovesVanillaColor, Uint8 unknown8, Uint8 pantsVanillaColor, Uint8 unknown9, Uint8 bootsVanillaColor, Uint8 unknown10, Uint8 helmVanillaColor, Uint8 unknown11, Uint8 chestColorRed, Uint8 chestColorBlue, Uint8 chestColorGreen, Uint8 chestColorAlpha, Uint8 unknown12, Uint8 bracerColorRed, Uint8 bracerColorBlue, Uint8 bracerColorGreen, Uint8 bracerColorAlpha, Uint8 unknown13, Uint8 glovesColorRed, Uint8 glovesColorBlue, Uint8 glovesColorGreen, Uint8 glovesColorAlpha, Uint8 unknown14, Uint8 pantsColorRed, Uint8 pantsColorBlue, Uint8 pantsColorGreen, Uint8 pantsColorAlpha, Uint8 unknown15, Uint8 bootsColorRed, Uint8 bootsColorBlue, Uint8 bootsColorGreen, Uint8 bootsColorAlpha, Uint8 unknown16, Uint8 helmColorRed, Uint8 helmColorBlue, Uint8 helmColorGreen, Uint8 helmColorAlpha, Uint8 unknown17, Uint8 robeColorRed, Uint8 robeColorBlue, Uint8 robeColorGreen, Uint8 robeColorAlpha, Uint8 hairColor, Uint8 hairLength, Uint8 hairStyle, Uint8 face, Uint8 robeStyle, Uint32Le unknown18, Uint8 unknown19, ASCIIString name, Uint8 level, Uint8 hasMovement, Uint8 conColorFlag, Uint8 targetNameplate, Uint8 race, Uint8 characterClass, Uint16Le npcType, Uint32Le npcPatternId, Uint8 unknown20, Uint8 unknown21, Uint8 invisPoisonDiseaseFlags, Uint8 unknown22, Uint8 unknown23, Uint8 unknown24, Uint32Le messageEnd) {
                this.unknown = unknown;
                this.entityId = entityId;
                this.unknown2 = unknown2;
                this.x = x;
                this.z = z;
                this.y = y;
                this.facing = facing;
                this.firstPlayerVerticalFacing = firstPlayerVerticalFacing;
                this.worldId = worldId;
                this.timeOfKill1 = timeOfKill1;
                this.timeOfKill2 = timeOfKill2;
                this.hpShowFlag = hpShowFlag;
                this.hpPercentage = hpPercentage;
                this.model = model;
                this.upgradedModel = upgradedModel;
                this.size = size;
                this.xCoordinatesOffset = xCoordinatesOffset;
                this.yCoordinatesOffset = yCoordinatesOffset;
                this.unknown3 = unknown3;
                this.eastToWestSpeed = eastToWestSpeed;
                this.lateralMovement = lateralMovement;
                this.northToSouthSpeed = northToSouthSpeed;
                this.turning = turning;
                this.spinUpAndDownRate = spinUpAndDownRate;
                this.zCoord = zCoord;
                this.facingDetail = facingDetail;
                this.animation = animation;
                this.targetEntityId = targetEntityId;
                this.unknown4 = unknown4;
                this.mainHandWeapon = mainHandWeapon;
                this.offHandWeapon = offHandWeapon;
                this.shield = shield;
                this.primaryMainHandWeapon = primaryMainHandWeapon;
                this.primaryOffHandWeapon = primaryOffHandWeapon;
                this.primaryShield = primaryShield;
                this.unknown5 = unknown5;
                this.torsoModel = torsoModel;
                this.forearmsModel = forearmsModel;
                this.glovesModel = glovesModel;
                this.legsModel = legsModel;
                this.feetModel = feetModel;
                this.headModel = headModel;
                this.chestVanillaColor = chestVanillaColor;
                this.unknown6 = unknown6;
                this.bracerVanillaColor = bracerVanillaColor;
                this.unknown7 = unknown7;
                this.glovesVanillaColor = glovesVanillaColor;
                this.unknown8 = unknown8;
                this.pantsVanillaColor = pantsVanillaColor;
                this.unknown9 = unknown9;
                this.bootsVanillaColor = bootsVanillaColor;
                this.unknown10 = unknown10;
                this.helmVanillaColor = helmVanillaColor;
                this.unknown11 = unknown11;
                this.chestColorRed = chestColorRed;
                this.chestColorBlue = chestColorBlue;
                this.chestColorGreen = chestColorGreen;
                this.chestColorAlpha = chestColorAlpha;
                this.unknown12 = unknown12;
                this.bracerColorRed = bracerColorRed;
                this.bracerColorBlue = bracerColorBlue;
                this.bracerColorGreen = bracerColorGreen;
                this.bracerColorAlpha = bracerColorAlpha;
                this.unknown13 = unknown13;
                this.glovesColorRed = glovesColorRed;
                this.glovesColorBlue = glovesColorBlue;
                this.glovesColorGreen = glovesColorGreen;
                this.glovesColorAlpha = glovesColorAlpha;
                this.unknown14 = unknown14;
                this.pantsColorRed = pantsColorRed;
                this.pantsColorBlue = pantsColorBlue;
                this.pantsColorGreen = pantsColorGreen;
                this.pantsColorAlpha = pantsColorAlpha;
                this.unknown15 = unknown15;
                this.bootsColorRed = bootsColorRed;
                this.bootsColorBlue = bootsColorBlue;
                this.bootsColorGreen = bootsColorGreen;
                this.bootsColorAlpha = bootsColorAlpha;
                this.unknown16 = unknown16;
                this.helmColorRed = helmColorRed;
                this.helmColorBlue = helmColorBlue;
                this.helmColorGreen = helmColorGreen;
                this.helmColorAlpha = helmColorAlpha;
                this.unknown17 = unknown17;
                this.robeColorRed = robeColorRed;
                this.robeColorBlue = robeColorBlue;
                this.robeColorGreen = robeColorGreen;
                this.robeColorAlpha = robeColorAlpha;
                this.hairColor = hairColor;
                this.hairLength = hairLength;
                this.hairStyle = hairStyle;
                this.face = face;
                this.robeStyle = robeStyle;
                this.unknown18 = unknown18;
                this.unknown19 = unknown19;
                this.name = name;
                this.level = level;
                this.hasMovement = hasMovement;
                this.conColorFlag = conColorFlag;
                this.targetNameplate = targetNameplate;
                this.race = race;
                this.characterClass = characterClass;
                this.npcType = npcType;
                this.npcPatternId = npcPatternId;
                this.unknown20 = unknown20;
                this.unknown21 = unknown21;
                this.invisPoisonDiseaseFlags = invisPoisonDiseaseFlags;
                this.unknown22 = unknown22;
                this.unknown23 = unknown23;
                this.unknown24 = unknown24;
                this.messageEnd = messageEnd;
                this.bytes = new Lazy<PacketBytes>(() => Bytes());
            }

            private PacketBytes Bytes() => this.unknown.Serialize()
                .Append(this.entityId.Serialize())
                .Append(this.unknown2.Serialize())
                .Append(this.x.Serialize())
                .Append(this.z.Serialize())
                .Append(this.y.Serialize())
                .Append(this.facing.Serialize())
                .Append(this.firstPlayerVerticalFacing.Serialize())
                .Append(this.worldId.Serialize())
                .Append(this.timeOfKill1.Serialize())
                .Append(this.timeOfKill2.Serialize())
                .Append(this.hpShowFlag.Serialize())
                .Append(this.hpPercentage.Serialize())
                .Append(this.model.Serialize())
                .Append(this.upgradedModel.Serialize())
                .Append(this.size.Serialize())
                .Append(this.xCoordinatesOffset.Serialize())
                .Append(this.yCoordinatesOffset.Serialize())
                .Append(this.unknown3.Serialize())
                .Append(this.eastToWestSpeed.Serialize())
                .Append(this.lateralMovement.Serialize())
                .Append(this.northToSouthSpeed.Serialize())
                .Append(this.turning.Serialize())
                .Append(this.spinUpAndDownRate.Serialize())
                .Append(this.zCoord.Serialize())
                .Append(this.facingDetail.Serialize())
                .Append(this.animation.Serialize())
                .Append(this.targetEntityId.Serialize())
                .Append(this.unknown4.Serialize())
                .Append(this.mainHandWeapon.Serialize())
                .Append(this.offHandWeapon.Serialize())
                .Append(this.shield.Serialize())
                .Append(this.primaryMainHandWeapon.Serialize())
                .Append(this.primaryOffHandWeapon.Serialize())
                .Append(this.primaryShield.Serialize())
                .Append(this.unknown5.Serialize())
                .Append(this.torsoModel.Serialize())
                .Append(this.forearmsModel.Serialize())
                .Append(this.glovesModel.Serialize())
                .Append(this.legsModel.Serialize())
                .Append(this.feetModel.Serialize())
                .Append(this.headModel.Serialize())
                .Append(this.chestVanillaColor.Serialize())
                .Append(this.unknown6.Serialize())
                .Append(this.bracerVanillaColor.Serialize())
                .Append(this.unknown7.Serialize())
                .Append(this.glovesVanillaColor.Serialize())
                .Append(this.unknown8.Serialize())
                .Append(this.pantsVanillaColor.Serialize())
                .Append(this.unknown9.Serialize())
                .Append(this.bootsVanillaColor.Serialize())
                .Append(this.unknown10.Serialize())
                .Append(this.helmVanillaColor.Serialize())
                .Append(this.unknown11.Serialize())
                .Append(this.chestColorRed.Serialize())
                .Append(this.chestColorBlue.Serialize())
                .Append(this.chestColorGreen.Serialize())
                .Append(this.chestColorAlpha.Serialize())
                .Append(this.unknown12.Serialize())
                .Append(this.bracerColorRed.Serialize())
                .Append(this.bracerColorBlue.Serialize())
                .Append(this.bracerColorGreen.Serialize())
                .Append(this.bracerColorAlpha.Serialize())
                .Append(this.unknown13.Serialize())
                .Append(this.glovesColorRed.Serialize())
                .Append(this.glovesColorBlue.Serialize())
                .Append(this.glovesColorGreen.Serialize())
                .Append(this.glovesColorAlpha.Serialize())
                .Append(this.unknown14.Serialize())
                .Append(this.pantsColorRed.Serialize())
                .Append(this.pantsColorBlue.Serialize())
                .Append(this.pantsColorGreen.Serialize())
                .Append(this.pantsColorAlpha.Serialize())
                .Append(this.unknown15.Serialize())
                .Append(this.bootsColorRed.Serialize())
                .Append(this.bootsColorBlue.Serialize())
                .Append(this.bootsColorGreen.Serialize())
                .Append(this.bootsColorAlpha.Serialize())
                .Append(this.unknown16.Serialize())
                .Append(this.helmColorRed.Serialize())
                .Append(this.helmColorBlue.Serialize())
                .Append(this.helmColorGreen.Serialize())
                .Append(this.helmColorAlpha.Serialize())
                .Append(this.unknown17.Serialize())
                .Append(this.robeColorRed.Serialize())
                .Append(this.robeColorBlue.Serialize())
                .Append(this.robeColorGreen.Serialize())
                .Append(this.robeColorAlpha.Serialize())
                .Append(this.hairColor.Serialize())
                .Append(this.hairLength.Serialize())
                .Append(this.hairStyle.Serialize())
                .Append(this.face.Serialize())
                .Append(this.robeStyle.Serialize())
                .Append(this.unknown18.Serialize())
                .Append(this.unknown19.Serialize())
                .Append(this.name.Serialize())
                .Append(this.level.Serialize())
                .Append(this.hasMovement.Serialize())
                .Append(this.conColorFlag.Serialize())
                .Append(this.targetNameplate.Serialize())
                .Append(this.race.Serialize())
                .Append(this.characterClass.Serialize())
                .Append(this.npcType.Serialize())
                .Append(this.npcPatternId.Serialize())
                .Append(this.unknown20.Serialize())
                .Append(this.unknown21.Serialize())
                .Append(this.invisPoisonDiseaseFlags.Serialize())
                .Append(this.unknown22.Serialize())
                .Append(this.unknown23.Serialize())
                .Append(this.unknown24.Serialize())
                .Append(this.messageEnd.Serialize());
        }
    }
}
