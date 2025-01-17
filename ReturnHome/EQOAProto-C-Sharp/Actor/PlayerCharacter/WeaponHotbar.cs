﻿using System;
using System.Collections.Generic;
using System.Text;
using ReturnHome.Utilities;

namespace ReturnHome.Playercharacter.Actor
{
    //Memory dump always expects 4 weapon hotbars, even if it's just "null"
    public class WeaponHotbar
    {
        public string HotbarName { get; private set; }
        public int PrimaryHandID { get; private set; }
        public int SecondaryHandID { get; private set; }

        private List<byte> ourMessage = new List<byte> { };

        //Default constructor
        //Even if not hotbar data, these must be -1 (Techniqued is 1)
        public WeaponHotbar()
        {
            HotbarName = "";
            PrimaryHandID = -1;
            SecondaryHandID = -1;
        }

        public WeaponHotbar(string thisHotBarName, int thisPrimaryHandID, int thisSecondaryHandID)
        {
            HotbarName = thisHotBarName;
            PrimaryHandID = thisPrimaryHandID;
            SecondaryHandID = thisSecondaryHandID;
        }

        public byte[] PullWeaponHotbar()
        {
            //Ensure this is empty
            ourMessage.Clear();

            ourMessage.AddRange(Utility_Funcs.Technique(PrimaryHandID));
            ourMessage.AddRange(Utility_Funcs.Technique(SecondaryHandID));
            ourMessage.AddRange(BitConverter.GetBytes(HotbarName.Length));
            ourMessage.AddRange(Encoding.Unicode.GetBytes(HotbarName));

            return ourMessage.ToArray();
        }
    }
}
