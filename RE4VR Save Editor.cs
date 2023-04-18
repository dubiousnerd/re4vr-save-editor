using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace RE4VR_Save_Editor
{
    public partial class RE4VR_Save_Editor : Form
    {
        public RE4VR_Save_Editor(string FileName)
        {
            InitializeComponent();
            // If file is opened with association the path is passed into the rest of the program here.
            if (FileName.Length > 0)
            {
                inventoryList.Items.Clear();
                merchantList.Items.Clear();
                tuneupList.Items.Clear();
                npcList.Items.Clear();
                re4v.FileName = FileName;
                checkBox1.Enabled = false;
                label5.Enabled = false;
                // See the saveSlotSelect index changed event.
                saveSlotSelect.SelectedIndex = 0;
                this.Text = "RE4 VR Save Editor -" + FileName;
            }
            // Populate Weapon and Ammo Drop Down
            foreach (var item in weaponDict) { weaponList.Items.Add(item.Value.ToString()); }
            // Populate Item and Treasures Drop Down 
            foreach (var item in itemDict) { itemList.Items.Add(item.Value.ToString()); }
            // Groups all of the NPC byte editor textboxes to one event handler.
            foreach (Control maybeTextBox in groupBox10.Controls)
            {
                if (maybeTextBox is TextBox)
                {
                    maybeTextBox.TextChanged += new EventHandler(npcByte_TextChanged);
                }
            }
        }
        class RE4VRCRC
        {
            public RE4VRCRC()
            {
            }
            public uint[] RE4Table = {
                                     0, 79764919, 159529838, 222504665,
                                     319059676, 398814059, 445009330, 507990021,
                                     638119352, 583659535, 797628118, 726387553,
                                     890018660, 835552979, 1015980042, 944750013,
                                     1276238704, 1221641927, 1167319070, 1095957929,
                                     1595256236, 1540665371, 1452775106, 1381403509,
                                     1780037320, 1859660671, 1671105958, 1733955601,
                                     2031960084, 2111593891, 1889500026, 1952343757,
                                     2552477408, 2632100695, 2443283854, 2506133561,
                                     2334638140, 2414271883, 2191915858, 2254759653,
                                     3190512472, 3135915759, 3081330742, 3009969537,
                                     2905550212, 2850959411, 2762807018, 2691435357,
                                     3560074640, 3505614887, 3719321342, 3648080713,
                                     3342211916, 3287746299, 3467911202, 3396681109,
                                     4063920168, 4143685023, 4223187782, 4286162673,
                                     3779000052, 3858754371, 3904687514, 3967668269,
                                     881225847, 809987520, 1023691545, 969234094,
                                     662832811, 591600412, 771767749, 717299826,
                                     311336399, 374308984, 453813921, 533576470,
                                     25881363, 88864420, 134795389, 214552010,
                                     2023205639, 2086057648, 1897238633, 1976864222,
                                     1804852699, 1867694188, 1645340341, 1724971778,
                                     1587496639, 1516133128, 1461550545, 1406951526,
                                     1302016099, 1230646740, 1142491917, 1087903418,
                                     2896545431, 2825181984, 2770861561, 2716262478,
                                     3215044683, 3143675388, 3055782693, 3001194130,
                                     2326604591, 2389456536, 2200899649, 2280525302,
                                     2578013683, 2640855108, 2418763421, 2498394922,
                                     3769900519, 3832873040, 3912640137, 3992402750,
                                     4088425275, 4151408268, 4197601365, 4277358050,
                                     3334271071, 3263032808, 3476998961, 3422541446,
                                     3585640067, 3514407732, 3694837229, 3640369242,
                                     1762451694, 1842216281, 1619975040, 1682949687,
                                     2047383090, 2127137669, 1938468188, 2001449195,
                                     1325665622, 1271206113, 1183200824, 1111960463,
                                     1543535498, 1489069629, 1434599652, 1363369299,
                                     622672798, 568075817, 748617968, 677256519,
                                     907627842, 853037301, 1067152940, 995781531,
                                     51762726, 131386257, 177728840, 240578815,
                                     269590778, 349224269, 429104020, 491947555,
                                     4046411278, 4126034873, 4172115296, 4234965207,
                                     3794477266, 3874110821, 3953728444, 4016571915,
                                     3609705398, 3555108353, 3735388376, 3664026991,
                                     3290680682, 3236090077, 3449943556, 3378572211,
                                     3174993278, 3120533705, 3032266256, 2961025959,
                                     2923101090, 2868635157, 2813903052, 2742672763,
                                     2604032198, 2683796849, 2461293480, 2524268063,
                                     2284983834, 2364738477, 2175806836, 2238787779,
                                     1569362073, 1498123566, 1409854455, 1355396672,
                                     1317987909, 1246755826, 1192025387, 1137557660,
                                     2072149281, 2135122070, 1912620623, 1992383480,
                                     1753615357, 1816598090, 1627664531, 1707420964,
                                     295390185, 358241886, 404320391, 483945776,
                                     43990325, 106832002, 186451547, 266083308,
                                     932423249, 861060070, 1041341759, 986742920,
                                     613929101, 542559546, 756411363, 701822548,
                                     3316196985, 3244833742, 3425377559, 3370778784,
                                     3601682597, 3530312978, 3744426955, 3689838204,
                                     3819031489, 3881883254, 3928223919, 4007849240,
                                     4037393693, 4100235434, 4180117107, 4259748804,
                                     2310601993, 2373574846, 2151335527, 2231098320,
                                     2596047829, 2659030626, 2470359227, 2550115596,
                                     2947551409, 2876312838, 2788305887, 2733848168,
                                     3165939309, 3094707162, 3040238851, 2985771188
                                 };
            public uint Re4crc(byte[] data, int length)
            {
                uint crc = 0, A, B, C, D;
                for (int i = 0; i < length; i++)
                {
                    A = data[i];
                    B = crc >> 24;
                    C = crc << 8;
                    D = A ^ B;
                    crc = (D << 2) & 0x3FFFFF;
                    A = RE4Table[crc / 4];
                    crc = A ^ C;
                }
                return crc;
            }
            private string fileName;
            public string FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }

        }

        RE4VRCRC re4v = new RE4VRCRC();

        ///////////////////////////////////////////////////////////////////////////////
        /////////////////////////MAIN READ AND WRITE FUNCTIONS/////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        #region // Main Functions
        //
        // Read save file and fill form with data.
        // 
        public void read_saveGame(int slotOffset, string FileName, int inventorySlot, int merchantSlot, int tuneupSlot)
        {
            #region // Table of Values 
            /// Begin Table of Values
            Int32 Difficulty = 69 + slotOffset;
            Int32 numSaves = 72 + slotOffset;
            Int32 numRounds = 74 + slotOffset;
            Int32 tplayed = 80 + slotOffset;
            Int32 pesetas = 84 + slotOffset;
            Int32 roomID = 88 + slotOffset;
            Int32 LeonHP = 96 + slotOffset;
            Int32 LeonHPMax = 98 + slotOffset;
            Int32 AshleyHP = 100 + slotOffset;
            Int32 AshleyHPMax = 102 + slotOffset;
            Int32 Model = 116 + slotOffset;
            Int32 LeonCostume = 117 + slotOffset;
            Int32 AshleyCostume = 119 + slotOffset;
            Int32 playerCords = 124 + slotOffset;
            Int32 npcInventory = 1212 + slotOffset;
            Int32 nameSave = 13616 + slotOffset;
            Int32 numKills = 13588 + slotOffset;
            Int32 inventoryStart = 13948 + slotOffset;
            Int32 inventoryMerchant = 50976 + slotOffset;
            Int32 inventoryTuneup = 51488 + slotOffset;
            /// End Table of Values
            #endregion

            FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryReader reader = new BinaryReader(stream);

            ///////////////////////////////////////////////
            ////////////////GENERAL TAB////////////////////
            ///////////////////////////////////////////////
            #region //General Tab
            // Read Difficulty
            reader.BaseStream.Position = Difficulty;
            var diff = BitConverter.ToString(reader.ReadBytes(1), 0).Replace("-", "");
            if (diff == "02") { difficultyBox.SelectedIndex = 0; }
            if (diff == "03") { difficultyBox.SelectedIndex = 1; }
            if (diff == "04") { difficultyBox.SelectedIndex = 2; }
            // Read Pesetas
            reader.BaseStream.Position = pesetas;
            stats_pesetas.Text = BitConverter.ToInt32(reader.ReadBytes(0x4), 0).ToString();
            // Read Leon HP
            reader.BaseStream.Position = LeonHP;
            stats_leonHP.Text = BitConverter.ToInt16(reader.ReadBytes(0x2), 0).ToString();
            // Read Ahsley HP
            reader.BaseStream.Position = AshleyHP;
            stats_ashleyHP.Text = BitConverter.ToInt16(reader.ReadBytes(0x2), 0).ToString();
            // Read Leon Max HP
            reader.BaseStream.Position = LeonHPMax;
            stats_leonMaxHP.Text = BitConverter.ToInt16(reader.ReadBytes(0x2), 0).ToString();
            // Read Ashley Max HP
            reader.BaseStream.Position = AshleyHPMax;
            stats_ashleyMaxHP.Text = BitConverter.ToInt16(reader.ReadBytes(0x2), 0).ToString();
            // Read Selected Player
            reader.BaseStream.Position = Model;
            var psvalue = BitConverter.ToString(reader.ReadBytes(1), 0).Replace("-", "");
            playerSelect.SelectedIndex = Int32.Parse(psvalue);
            // Read Leon Costume
            reader.BaseStream.Position = LeonCostume;
            if (BitConverter.ToInt16(reader.ReadBytes(0x2), 0) == 0) { checkBox1.Checked = true; };
            if (BitConverter.ToInt16(reader.ReadBytes(0x2), 0) == 1) { checkBox1.Checked = false; };
            // Read Ashley Costume
            reader.BaseStream.Position = AshleyCostume;
            costumeSelect.SelectedIndex = -1;
            costumeSelect.SelectedIndex = BitConverter.ToInt16(reader.ReadBytes(0x2), 0);
            // Read Current Room ID
            reader.BaseStream.Position = roomID;
            var chap = BitConverter.ToString(reader.ReadBytes(2), 0).Replace("-", "");
            roomIDb_textbox.Text = chap;
            roomIDc_textbox.Text = chap;
            if (chapterDict.TryGetValue(chap, out var chapter))
            {
                roomID_textbox.Text = chapter;
            }
            else
            {
                roomID_textbox.Text = chap;
            }
            //Read Player Coordinates
            reader.BaseStream.Position = playerCords;
            var playercordsx = reader.ReadBytes(4);
            var playercordsy = reader.ReadBytes(4);
            var playercordsz = reader.ReadBytes(4);
            var playercordsa = reader.ReadBytes(4);
            pcords_textbox0.Text = BitConverter.ToSingle(playercordsx, 0).ToString();
            pcords_textbox1.Text = BitConverter.ToSingle(playercordsy, 0).ToString();
            pcords_textbox2.Text = BitConverter.ToSingle(playercordsz, 0).ToString();
            pcords_textbox3.Text = BitConverter.ToSingle(playercordsa, 0).ToString();
            // Read name of Save Game
            reader.BaseStream.Position = nameSave;
            slotinfo_name.Text = Encoding.Unicode.GetString(reader.ReadBytes(20));
            saveNameTextbox.Text = slotinfo_name.Text;
            // Read Number of Saves
            reader.BaseStream.Position = numSaves;
            slotinfo_numSaves.Text = BitConverter.ToInt16(reader.ReadBytes(2), 0).ToString();
            // Read Number of Rounds
            reader.BaseStream.Position = numRounds;
            slotinfo_numRounds.Text = slotinfo_numSaves.Text = BitConverter.ToInt16(reader.ReadBytes(2), 0).ToString();
            // Read Time Played (Seconds)
            reader.BaseStream.Position = tplayed;
            slotinfo_tplayed.Text = BitConverter.ToInt32(reader.ReadBytes(4), 0).ToString();
            // Read Number of Kills
            reader.BaseStream.Position = numKills;
            slotinfo_numKills.Text = BitConverter.ToInt16(reader.ReadBytes(2), 0).ToString();
            #endregion

            ///////////////////////////////////////////////
            ////////////////INVENTORY TAB//////////////////
            ///////////////////////////////////////////////
            #region //Inventory Tab
            // Build Inventory List
            inventoryList.Items.Clear();
            reader.BaseStream.Position = inventoryStart;
            for (int i = 0; i < 384; i++) // Player Inventory is 384 slots
            {
                var inv = reader.ReadBytes(1);
                reader.BaseStream.Seek(11, SeekOrigin.Current);
                if (invDict.TryGetValue(inv[0], out var invl))
                {
                    inventoryList.Items.Add(invl);
                }
                else
                {
                    inventoryList.Items.Add(inv);
                }
            }
            inventoryList.SelectedIndex = 0;
            #endregion

            ///////////////////////////////////////////////
            ////////////////MERCHANT TAB///////////////////
            ///////////////////////////////////////////////
            #region //Merchant Tab
            // Build Merchant Item List
            reader.BaseStream.Position = inventoryMerchant;
            merchantList.Items.Clear();
            for (int i = 0; i < 64; i++) // Merch inventory is 64 slots
            {
                var mil = reader.ReadBytes(1);
                reader.BaseStream.Seek(7, SeekOrigin.Current);
                if (invDict.TryGetValue(mil[0], out var minv))
                {
                    merchantList.Items.Add(minv);
                }
                else
                {
                    merchantList.Items.Add(mil[0]);
                }
            }
            // Build Merchant Tuneup List
            reader.BaseStream.Position = inventoryTuneup;
            tuneupList.Items.Clear();
            for (int i = 0; i < 32; i++) // Merch tuneup is 32 slots
            {
                var tul = reader.ReadBytes(1);
                reader.BaseStream.Seek(7, SeekOrigin.Current);
                if (invDict.TryGetValue(tul[0], out var tuneup))
                {
                    tuneupList.Items.Add(tuneup);
                }
                else
                {
                    tuneupList.Items.Add(tul[0]);
                }
            }
            #endregion

            ///////////////////////////////////////////////
            ///////////////////NPC TAB/////////////////////
            ///////////////////////////////////////////////
            #region //NPC Tab
            // Build NPC List
            reader.BaseStream.Position = npcInventory;
            npcList.Items.Clear();
            for (int i = 0; i < 255; i++) // NPC Inventory is 255 slots
            {
                reader.BaseStream.Seek(1, SeekOrigin.Current);
                var npc = BitConverter.ToString(reader.ReadBytes(2), 0).Replace("-", "");
                reader.BaseStream.Seek(29, SeekOrigin.Current);
                if (enemyDict.TryGetValue(npc, out var enemy))
                {
                    npcList.Items.Add(enemy);
                }
                else
                {
                    npcList.Items.Add(npc);
                }
            }
            npcList.SelectedIndex = 0;
            #endregion

            ///////////////////////////////////////////////
            ////////////////NPC TABLE TAB//////////////////
            ///////////////////////////////////////////////
            #region // NPC Table Tab
            DataTable edt = new DataTable();
            edt.Columns.Add("S-T", typeof(string));     // Spawn Type
            edt.Columns.Add("E-T", typeof(string));     // Enemy Type
            edt.Columns.Add("E-B", typeof(string));     // Enemy Behavior
            edt.Columns.Add("E-A", typeof(string));     // Enemy Agro
            edt.Columns.Add("E-1", typeof(string));     // Equipped S1
            edt.Columns.Add("E-2", typeof(string));     // Equipped S2
            edt.Columns.Add("E-W", typeof(string));     // Enemy Weapon
            edt.Columns.Add("E-HP", typeof(string));    // Enemy Health
            edt.Columns.Add("????", typeof(string));     // C??
            edt.Columns.Add("E-X", typeof(string));     // Enemy X
            edt.Columns.Add("E-Y", typeof(string));     // Enemy Y
            edt.Columns.Add("E-Z", typeof(string));     // Enemy Z
            edt.Columns.Add("E-XA", typeof(string));    // Enemy X Angle
            edt.Columns.Add("E-YA", typeof(string));    // Enemy Y Angle
            edt.Columns.Add("E-ZA", typeof(string));    // Enemy Z Angle
            edt.Columns.Add("E-R", typeof(string));     // Enemy Room
            edt.Columns.Add("ER?", typeof(string));     // ER?
            for (int i = 0; i < 255; i++)
            {
                reader.BaseStream.Position = npcInventory + 32 * i;
                // Spawn   | Column 0  |   1 Byte
                var cdata0 = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");//ST
                                                                                         // Type    | Column 1  |   2 Byte
                var cdata1 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");//ET
                if (enemyDict.TryGetValue(cdata1, out var cdata_1)) { }
                else { cdata_1 = cdata1; }
                // React   | Column 2  |   1 Byte
                var cdata2 = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");//EB
                                                                                         // Agro    | Column 3  |   1 Byte
                var cdata3 = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");//EA
                                                                                         // Acc 1   | Column 4  |   1 Byte
                var cdata4 = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");//E1
                                                                                         // Acc 2   | Column 5  |   1 Byte
                var cdata5 = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");//E2
                                                                                         // Weapon  | Column 6  |   1 Byte
                var cdata6 = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");//EW
                                                                                         // Health  | Column 7  |   2 Byte
                var cdata7 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");//EH
                var cdata_7 = BitConverter.ToInt16(ToBytes(cdata7), 0);
                // ????    | Column 8  |   2 Byte
                var cdata8 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");//??
                                                                                         // X Cord  | Column 9  |   2 Byte
                var cdata9 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");//EX
                var cdata_9 = BitConverter.ToInt16(ToBytes(cdata9), 0);
                // Y Cord  | Column 10 |   2 Byte
                var cdata10 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");//EY
                var cdata_10 = BitConverter.ToInt16(ToBytes(cdata10), 0);
                // Z Cord  | Column 11 |   2 Byte
                var cdata11 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");//EZ
                var cdata_11 = BitConverter.ToInt16(ToBytes(cdata11), 0);
                // X Angle | Column 12 |   2 Byte                                                              
                var cdata12 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");// X Angle
                var cdata_12 = BitConverter.ToInt16(ToBytes(cdata12), 0);
                // Y Angle | Column 13 |   2 Byte
                var cdata13 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");// Y Angle
                var cdata_13 = BitConverter.ToInt16(ToBytes(cdata13), 0);
                // Z Angle | Column 14 |   2 Byte
                var cdata14 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", ""); // Z Angle
                var cdata_14 = BitConverter.ToInt16(ToBytes(cdata14), 0);
                // Room ID | Column 15 |   2 Byte
                var cdata15 = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");//ERoom
                if (chapterDict.TryGetValue(cdata15, out var cdata_15)) { }
                else { cdata15 = cdata_15; }
                // Extraa  | Column 16 |   1 Byte
                var cdata16 = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");//ER?
                // Build the row
                edt.Rows.Add(cdata0, cdata_1, cdata2, cdata3, cdata4, cdata5, cdata6, cdata_7, cdata8, cdata_9, cdata_10, cdata_11, cdata_12, cdata_13, cdata_14, cdata_15, cdata16);
            }
            dataGridView1.DataSource = edt;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.AutoResizeColumn(i);
            }
            // Nohting below this line.
            reader.Close();
            #endregion

        }
        //
        // Read the save file and fill mini hex edit boxes.
        //
        public void read_Hex(int slotOffset, string FileName, int inventorySlot, int merchantSlot, int tuneupSlot, int npcSlot)
        {
            #region // Table of Values 
            /// Begin Table of Values
            Int32 Difficulty = 69 + slotOffset;
            Int32 numSaves = 72 + slotOffset;
            Int32 numRounds = 74 + slotOffset;
            Int32 tplayed = 80 + slotOffset;
            Int32 pesetas = 84 + slotOffset;
            Int32 roomID = 88 + slotOffset;
            Int32 LeonHP = 96 + slotOffset;
            Int32 LeonHPMax = 98 + slotOffset;
            Int32 AshleyHP = 100 + slotOffset;
            Int32 AshleyHPMax = 102 + slotOffset;
            Int32 Model = 116 + slotOffset;
            Int32 LeonCostume = 117 + slotOffset;
            Int32 AshleyCostume = 119 + slotOffset;
            Int32 playerCords = 124 + slotOffset;
            Int32 npcInventory = 1212 + slotOffset;
            Int32 nameSave = 13616 + slotOffset;
            Int32 numKills = 13588 + slotOffset;
            Int32 inventoryStart = 13948 + slotOffset;
            Int32 inventoryMerchant = 50976 + slotOffset;
            Int32 inventoryTuneup = 51488 + slotOffset;
            /// End Table of Values
            #endregion

            FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryReader reader = new BinaryReader(stream);

            ///////////////////////////////////////////////
            ////////////////INVENTORY TAB//////////////////
            ///////////////////////////////////////////////
            #region Inventory Tab
            reader.BaseStream.Position = inventoryStart + inventorySlot * 12;
            textBox1.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox2.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox3.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox4.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox5.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox6.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox12.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox11.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox10.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox9.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox8.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            textBox7.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");
            #endregion

            ///////////////////////////////////////////////
            ////////////////MERCHANT TAB///////////////////
            ///////////////////////////////////////////////
            #region Merchant Tab
            reader.BaseStream.Position = inventoryMerchant + merchantSlot * 8;
            merchHexBox.Text = BitConverter.ToString(reader.ReadBytes(8));
            reader.BaseStream.Position = inventoryTuneup + tuneupSlot * 8;
            tuneHexBox.Text = BitConverter.ToString(reader.ReadBytes(8));
            #endregion

            ///////////////////////////////////////////////
            ///////////////////NPC TAB/////////////////////
            ///////////////////////////////////////////////
            #region NPC Tab
            reader.BaseStream.Position = npcInventory + npcSlot * 32;
            npcBox0.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");  //  Spawn Type
            npcBox1.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");  //  Enemy Type 
            npcBox2.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");  //  Enemy Behavior 
            npcBox3.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");  //  Enemy Agro 
            npcBox4.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");  //  Enemy ACC 1
            npcBox5.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");  //  Enemy ACC 2
            npcBox6.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", "");  //  Enemy Weapon
            npcBox7.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");  //  Enemy Health 
            npcBox8.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");  //  ???? 
            //npcBox9.Text = BitConverter.ToInt16(reader.ReadBytes(2),0).ToString();     //  X  Cord 
            //npcBox10.Text = BitConverter.ToInt16(reader.ReadBytes(2), 0).ToString();   //  Y  Cord
            //npcBox11.Text = BitConverter.ToInt16(reader.ReadBytes(2), 0).ToString();   //  Z  Cord
            npcBox9.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", "");  //  X  Cord   
            npcBox10.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", ""); //  Y  Cord
            npcBox11.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", ""); //  Z  Cord
            npcBox12.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", ""); //  X  Angle
            npcBox13.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", ""); //  Y  Angle
            npcBox14.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", ""); //  Z  Angle 
            npcBox15.Text = BitConverter.ToString(reader.ReadBytes(2)).Replace("-", ""); // Enemy Room
            npcBox16.Text = BitConverter.ToString(reader.ReadBytes(1)).Replace("-", ""); // Extra
            npcHexbox.Text = npcBox0.Text + npcBox1.Text + npcBox2.Text + npcBox3.Text + npcBox4.Text + npcBox5.Text +
            npcBox6.Text + npcBox7.Text + npcBox8.Text + npcBox9.Text + npcBox10.Text + npcBox11.Text + npcBox12.Text + npcBox13.Text + npcBox14.Text + npcBox15.Text + npcBox16.Text;
            //
            // Close Reader
            // Nothing Below this comment
            //
            reader.Close();
            #endregion
        }
        //
        // Write save data to the file and checksum the file.
        //
        public void write_saveGame(int slotOffset, string FileName, int inventorySlot, int merchantSlot, int tuneupSlot)
        {
            #region // Table of Values 
            /// Begin Table of Values
            Int32 Difficulty = 69 + slotOffset;
            Int32 numSaves = 72 + slotOffset;
            Int32 numRounds = 74 + slotOffset;
            Int32 tplayed = 80 + slotOffset;
            Int32 pesetas = 84 + slotOffset;
            Int32 roomID = 88 + slotOffset;
            Int32 LeonHP = 96 + slotOffset;
            Int32 LeonHPMax = 98 + slotOffset;
            Int32 AshleyHP = 100 + slotOffset;
            Int32 AshleyHPMax = 102 + slotOffset;
            Int32 Model = 116 + slotOffset;
            Int32 LeonCostume = 117 + slotOffset;
            Int32 AshleyCostume = 119 + slotOffset;
            Int32 playerCords = 124 + slotOffset;
            Int32 npcInventory = 1212 + slotOffset;
            Int32 nameSave = 13616 + slotOffset;
            Int32 numKills = 13588 + slotOffset;
            Int32 inventoryStart = 13948 + slotOffset;
            Int32 inventoryMerchant = 50976 + slotOffset;
            Int32 inventoryTuneup = 51488 + slotOffset;
            /// End Table of Values
            #endregion

            FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            ///////////////////////////////////////////////
            ////////////////GENERAL TAB////////////////////
            ///////////////////////////////////////////////
            #region //General Tab
            // Save Game Name
            writer.BaseStream.Position = nameSave;
            foreach (char c in slotinfo_name.Text)
            {
                var tchar = Convert.ToByte(c).ToString("X");
                writer.Write(ToBytes(tchar));
                writer.Write(ToBytes("00"));
            }
            if (slotinfo_name.TextLength < 10)
            {
                for (int i = 0; i < 10 - slotinfo_name.TextLength; i++)
                { writer.Write(ToBytes("00")); }
            }
            // Number of Saves
            writer.BaseStream.Position = numSaves;
            writer.Write(Int16.Parse(slotinfo_numSaves.Text));
            // Time Played in Seconds
            writer.BaseStream.Position = tplayed;
            writer.Write(Int32.Parse(slotinfo_tplayed.Text));
            // Number of Rounds
            writer.BaseStream.Position = numRounds;
            writer.Write(Int16.Parse(slotinfo_numRounds.Text));
            writer.BaseStream.Position = numKills;
            writer.Write(Int32.Parse(slotinfo_numKills.Text));
            // Difficulty
            writer.BaseStream.Position = Difficulty;
            if (difficultyBox.SelectedIndex == 0) { writer.Write(ToBytes("02")); }
            if (difficultyBox.SelectedIndex == 1) { writer.Write(ToBytes("03")); }
            if (difficultyBox.SelectedIndex == 2) { writer.Write(ToBytes("04")); }
            // Player Model
            #region //Player Model
            writer.BaseStream.Position = Model;
            // Leon
            if (playerSelect.SelectedIndex == 0) { writer.Write(ToBytes("00")); }
            // Ashley
            if (playerSelect.SelectedIndex == 1) { writer.Write(ToBytes("01")); }
            // Ada
            if (playerSelect.SelectedIndex == 2) { writer.Write(ToBytes("02")); }
            // Hunk
            if (playerSelect.SelectedIndex == 3) { writer.Write(ToBytes("03")); }
            // Krauser
            if (playerSelect.SelectedIndex == 4) { writer.Write(ToBytes("04")); }
            // Wesker
            if (playerSelect.SelectedIndex == 5) { writer.Write(ToBytes("05")); }
            #endregion
            // Money Money Money
            writer.BaseStream.Position = pesetas;
            writer.Write(Int32.Parse(stats_pesetas.Text));
            // Health
            writer.BaseStream.Position = LeonHP;
            writer.Write(Int16.Parse(stats_leonHP.Text));
            writer.BaseStream.Position = AshleyHP;
            writer.Write(Int16.Parse(stats_ashleyHP.Text));
            writer.BaseStream.Position = LeonHPMax;
            writer.Write(Int16.Parse(stats_leonMaxHP.Text));
            writer.BaseStream.Position = AshleyHPMax;
            writer.Write(Int16.Parse(stats_ashleyMaxHP.Text));
            #region //Player Costume
            // Leon Costume 
            writer.BaseStream.Position = LeonCostume;
            // Leon with Jacket
            if (costumeSelect.SelectedIndex == 0 & checkBox1.Checked == true) { writer.Write(ToBytes("00")); }
            // Leon no Jacket
            if (costumeSelect.SelectedIndex == 0 & checkBox1.Checked == false) { writer.Write(ToBytes("01")); }
            // Leon in RPD
            if (costumeSelect.SelectedIndex == 1) { writer.Write(ToBytes("03")); }
            // Leon in Mafia
            if (costumeSelect.SelectedIndex == 2) { writer.Write(ToBytes("04")); }
            // Ashley Costume
            writer.BaseStream.Position = AshleyCostume;
            // Ashley with Skirt
            if (costumeSelect.SelectedIndex == 0) { writer.Write(ToBytes("00")); }
            // Ashley in Pop
            if (costumeSelect.SelectedIndex == 1) { writer.Write(ToBytes("01")); }
            // Ashley in Armor
            if (costumeSelect.SelectedIndex == 2) { writer.Write(ToBytes("02")); }
            // writer.BaseStream.Position = NumRounds;
            //writer.Write(numrounds);
            // Number of Kills
            #endregion
            //
            // Player Coordiantes
            //
            writer.BaseStream.Position = playerCords;
            var pcord0 = float.Parse(pcords_textbox0.Text);
            var pcord1 = float.Parse(pcords_textbox1.Text);
            var pcord2 = float.Parse(pcords_textbox2.Text);
            var pcord3 = float.Parse(pcords_textbox3.Text);
            writer.Write(pcord0); writer.Write(pcord1); writer.Write(pcord2); writer.Write(pcord3);
            //
            // Chapter Select
            //
            List<string> roomBytes = new List<string>(new string[]
 {
                 "0001" , "0401" , "0501" , "1B01" , "1701" ,
                 "1C01" , "0002" , "0402" , "0B02" , "0602" ,
                 "0602" , "2002" , "2502" , "2602" , "0003" ,
                 "0C03" , "1603" , "2003" , "3103" , "0004" ,
                 "0204" , "0304" , "0404" , "0D02" , "3303" ,
                 "2001" ,
 });
            List<string> pcordBytes = new List<string>(new string[]
            {
                "F3AB53C79A74BEC3D25367C6" , "A42957C500005EBB4AC94046" , "20B6B6457641C6450E880245" , "13A1F54742012FC341ED2748" ,
                "E20F0F4600B01CC576513445" , "6DA8D44760A9D7415FDB49C7" , "C193A14753181E46405849C7" , "923EF8420000943B79214AC7" ,
                "0C01FB4600007A45D2E9D944" , "A3D607C6FFFFF945C42815C6" , "80AA00C30000FA45C02495C6" , "419ACF446D429144DA63F545" ,
                "4B49F9C67A45F3440E464CC3" , "2E2B5BC5FE3F9C45B541A946" , "0966E24602D084C67ACD20C7" , "A049FE450000483B919A07C5" ,
                "98393F4400A827405A2942C5" , "DC66E646E601BD4503318547" , "8F3E12C77038C74590B51247" , "000000000000000000000000" ,
                "000000000000000000000000" , "000000000000000000000000" , "000000000000000000000000" , "6AA394C600008EBB358BDFC3" ,
                "80A1DB4800BC31C7C0582B48" , "000000000000000000000000"
            });
           for (int i = 0; i < roomBytes.Count; i++)
           {
               if (chapterSelect.SelectedIndex == i + 1)
               {
                   writer.BaseStream.Position = roomID;
                   writer.Write(ToBytes(roomBytes[i]));
                   writer.BaseStream.Position = playerCords;
                   writer.Write(ToBytes(pcordBytes[i]));
               }
           }
            if (roomIDb_textbox.Text.Length == 4)
            {
                writer.BaseStream.Position = roomID;
                writer.Write(ToBytes(roomIDb_textbox.Text));
            }
            #endregion

            ///////////////////////////////////////////////
            ////////////////INVENTORY TAB//////////////////
            ///////////////////////////////////////////////
            #region // Inventory Tab

            //
            // F_Trots inventory
            //
            string[] foxyBytes = {
                "7F000100000000000C060000" , "27004782FFFF000006030101" , "FE00FFFF0000000000000000" ,
                "3700E662000000000C040101" , "3F000101080000001C0F0201" , "2300E688FFFF000002030101" ,
                "94000682FFFF000014080101" , "2E000682FFFF0000090F0001" , "2F005582FFFF000010070101" ,
                "30000682FFFF0000180D0301" , "36000381FFFF000018050101" , "34000660A0000000070B0001" ,
                "43000000FFFF00001C0C0001" , "2D00F682FFFF00001C050101" , "0E00FFFF0000000006070301" ,
                "0200FFFF0000000009060001" , "0100FFFF0000000002070301" , "1500FFFF0000000009020001" ,
                "0400FFFF000000004C314131" , "1800FFFF000000004C314131" , "2000FFFF000000004C314131" ,
                "0700FFFF000000004C314131" , "0000FFFF000000004C314131" , "4600FFFF000000004C314131" ,
                };
            //
            // Foxy Loadout Checkbox
            //
            if (foxy_checkbox.Checked)
            {
                foreach (var weapon in foxyBytes)
                {
                    reader.BaseStream.Position = inventoryStart;
                    for (int i = 0; i < 384; i++) // Player Inventory is 384 slots
                    {
                        var inv = reader.ReadBytes(1);
                        reader.BaseStream.Seek(11, SeekOrigin.Current);
                        if (inv[0] == 0xFF)
                        {
                            writer.BaseStream.Position = inventoryStart + 12 * i;
                            writer.Write(ToBytes(weapon));
                            break;
                        }
                    }
                }
            }
            #endregion

            ///////////////////////////////////////////////
            ////////////////MERCHANT TAB///////////////////
            /////////////////////////////////////////////// 
            #region // Merchant Tab
            //
            // Merchant Data
            //
            #region //Merchant Data
            writer.BaseStream.Position = inventoryMerchant;
            var merchantBytes =
                "0500410000010000" + "2300010000000000" + "2F00010000000000" + "2E00010000000000" +
                "3000010000000000" + "4400010000010000" + "3500010000010000" + "7D00000000000000" +
                "A900010000000000" + "4300010000000000" + "2500010000000000" + "4200010000010000" +
                "2100010000000000" + "5400010000000000" + "7E00010000000000" + "2700010000000000" +
                "2900010000000000" + "2F00010000000000" + "3600010000000000" + "9400010000000000" +
                "4500010000000000" + "2C00010000000000" + "7F00010000000000" + "2D00010000000000" +
                "5500010000010000" + "2A00010000000000" + "FE00010000000000" + "0300010000000000" +
                "6D00010000000000" + "4100010000000000" + "3700010000000000" + "3400010000000000";
            #endregion
            //
            // Tune Up Data
            //
            #region //Tune Up Data
            var tuneupBytes =
                "2300070303060000" + "2C00070103060000" + "2E00070103060000" +
                "3000070103060000" + "2100070303060000" + "2500070303060000" +
                "2700070303060000" + "2900070103040000" + "2F00060203060000" +
                "9400070103060000" + "3600040102030000" + "2D00060103070000" +
                "2A00030103030000" + "0300060103070000" + "3700070103070000" ;
            var tuneupMaxBytes =
                "2300070F09090000" + "2C00070A03090000" + "2E00070F03090000" +
                "3000070103090000" + "2100070A03090000" + "2500070A03090000" +
                "2700080503090000" + "2900070103090000" + "2F00070603090000" +
                "9400070603090000" + "3600040102090000" + "2D00071003090000" +
                "2A00040103090000" + "0300070F03090000" + "3700070F03070000" ;
            var tuneupFspeedBytes =
                "2300070F03060000" + "2C00070A03060000" + "2E00070F03060000" +
                "3000070103090000" + "2100070A03060000" + "2500070A03060000" +
                "2700070503060000" + "2900070103040000" + "2F00060603060000" +
                "9400070603060000" + "3600040102030000" + "2D00061003070000" +
                "2A00030103030000" + "0300060F03070000" + "3700070F03070000" ;
            #endregion
            //
            // Max Default Tuneup
            //
            if (max_checkbox.Checked)
            {
                writer.BaseStream.Position = inventoryTuneup;
                writer.Write(ToBytes(tuneupBytes));
            }
            //
            // Max Modded Tuneup
            //
            if (modmax_checkbox.Checked)
            {
                writer.BaseStream.Position = inventoryTuneup;
                writer.Write(ToBytes(tuneupMaxBytes));
            }
            //
            // Max Default + Max Firing Speed
            //
            if (modfspeed_checkbox.Checked)
            {
                writer.BaseStream.Position = inventoryTuneup;
                writer.Write(ToBytes(tuneupFspeedBytes));
            }
            //
            // Unlock all wepons in the merchant.
            //
            if (unlockall_checkbox.Checked)
            {
                writer.BaseStream.Position = inventoryMerchant;
                writer.Write(ToBytes(merchantBytes));
            }
            #endregion
            // Signature
            writer.BaseStream.Position = 0x1EE0;
            writer.Write(ToBytes("01030307"));
            // Checksum 1
            reader.BaseStream.Position = slotOffset + 4;
            uint CRC = re4v.Re4crc(reader.ReadBytes(0x1FC), 0x1FC);
            writer.BaseStream.Position = slotOffset;
            writer.Write(CRC);
            writer.Flush();
            // Checksum 2
            reader.BaseStream.Position = slotOffset;
            CRC = re4v.Re4crc(reader.ReadBytes(0xCA44), 0xCA44);
            writer.BaseStream.Position = slotOffset + 51780;
            writer.Write(CRC);
            writer.Flush();
            reader.Close();
            read_saveGame(slotOffset, FileName, inventorySlot, merchantSlot, tuneupSlot);
        }
        //
        // Write the data found in the mini hex editors to the file does not checksum.
        //
        public void write_Hex(int slotOffset, string FileName, int inventorySlot, int npcSlot)
        {
            // Start Table of Values
            Int32 npcInventory = 1212 + slotOffset;
            Int32 inventoryStart = 13948 + slotOffset;
            // End Table of Values
            FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            ///////////////////////////////////////////////
            ////////////////INVENTORY TAB//////////////////
            ///////////////////////////////////////////////
            // Write data to the selected weapon list placeholder
            writer.BaseStream.Position = inventoryStart + inventorySlot * 12;
            writer.Write(ToBytes(
                textBox1.Text + textBox2.Text + textBox3.Text + textBox4.Text +
                textBox5.Text + textBox6.Text + textBox12.Text + textBox11.Text +
                textBox10.Text + textBox9.Text + textBox8.Text + textBox7.Text));
            // Refresh the inventory list after each write
            // Build Inventory List
            inventoryList.Items.Clear();
            reader.BaseStream.Position = inventoryStart;
            for (int i = 0; i < 384; i++) // Player Inventory is 384 slots
            {
                var inv = reader.ReadBytes(1);
                reader.BaseStream.Seek(11, SeekOrigin.Current);
                if (invDict.TryGetValue(inv[0], out var invl))
                {
                    inventoryList.Items.Add(invl);
                }
                else
                {
                    inventoryList.Items.Add(inv);
                }
            }
            if (tabbox.SelectedIndex == 1) { inventoryList.SelectedIndex = inventorySlot + 1; }

            ///////////////////////////////////////////////
            ///////////////////NPC TAB/////////////////////
            ///////////////////////////////////////////////
            writer.BaseStream.Position = npcInventory + npcSlot * 32;
            //writer.Write(ToBytes(
            //npcBox1.Text + npcBox2.Text + npcBox3.Text + npcBox4.Text + npcBox5.Text + npcBox6.Text +
            //npcBox7.Text + npcBox8.Text + npcBox9.Text + npcBox10.Text + npcBox11.Text + npcBox12.Text));
            writer.Write(ToBytes(npcHexbox.Text));
            // Refresh the NPC list after each write
            // Build NPC List
            npcList.Items.Clear();
            reader.BaseStream.Position = npcInventory;
            for (int i = 0; i < 255; i++) // NPC Inventory is 255 slots
            {
                reader.BaseStream.Seek(1, SeekOrigin.Current);
                var npc = BitConverter.ToString(reader.ReadBytes(2), 0).Replace("-", "");
                reader.BaseStream.Seek(29, SeekOrigin.Current);
                if (enemyDict.TryGetValue(npc, out var enemy))
                {
                    npcList.Items.Add(enemy);
                }
                else
                {
                    npcList.Items.Add(npc);
                }
            }
            if (tabbox.SelectedIndex == 3) { npcList.SelectedIndex = npcSlot + 1; }

            // Close out the reader and writer. Nothing below this line.
            writer.Flush();
            reader.Close();
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////TOOLS/////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////
        #region // Tools
        //
        //Cool way to convert hex string to bytes[]---------
        //
        public static byte[] ToBytes(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Hex cannot be null/empty/whitespace");
            if (hex.Length % 2 != 0)
                throw new FormatException("Hex must have an even number of characters");
            bool startsWithHexStart = hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase);
            if (startsWithHexStart && hex.Length == 2)
                throw new ArgumentException("There are no characters in the hex string");
            int startIndex = startsWithHexStart ? 2 : 0;
            byte[] bytesArr = new byte[(hex.Length - startIndex) / 2];
            char left;
            char right;
            try
            {
                int x = 0;
                for (int i = startIndex; i < hex.Length; i += 2, x++)
                {
                    left = hex[i];
                    right = hex[i + 1];
                    bytesArr[x] = (byte)((hexmap[left] << 4) | hexmap[right]);
                }
                return bytesArr;
            }
            catch (KeyNotFoundException)
            {
                throw new FormatException("Hex string has non-hex character");
            }
        }
        //
        // Used to get the file name and scope it to all other functions.
        //
        public string getFileName()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RE4 Save Game|*.sav" + "|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return null;
        }
        //
        // Import Slot to the Save File
        //
        public void import_Slot(int slotOffset, string FileName)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RE4 Slot Files|*.slot" + "|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream slotData = new FileStream(ofd.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryReader slotReader = new BinaryReader(slotData);
                FileStream saveData = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryWriter saveWriter = new BinaryWriter(saveData);
                slotReader.BaseStream.Position = 0;
                var slotbin = slotReader.ReadBytes(0xCA48);
                saveWriter.BaseStream.Position = slotOffset;
                saveWriter.Write(slotbin);
                slotReader.Close();
                saveWriter.Flush();
                saveWriter.Close();
                read_saveGame(slotOffset, FileName, 0, 0, 0);
            }
        }
        public void export_Slot(int slotOffset, string FileName)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var filepath = Path.Combine(folderBrowserDialog1.SelectedPath, "slot_" + (saveSlotSelect.SelectedIndex + 1) + ".slot");
                FileStream saveGame = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                FileStream slotData = new FileStream(filepath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(saveGame);
                BinaryWriter writer = new BinaryWriter(slotData);
                reader.BaseStream.Position = slotOffset;
                var slotbin = reader.ReadBytes(0xCA48);
                writer.Write(slotbin);
                slotData.Close();
            }
        }
        public void import_NPC(int slotOffset, string FileName)
        {
            #region // Table of Values 
            /// Begin Table of Values
            Int32 npcInventory = 1212 + slotOffset;
            /// End Table of Values
            #endregion

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RE4 Slot Files|*.npc" + "|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream npcData = new FileStream(ofd.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryReader npcReader = new BinaryReader(npcData);
                FileStream saveData = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryWriter saveWriter = new BinaryWriter(saveData);
                npcReader.BaseStream.Position = 0;
                var npcbin = npcReader.ReadBytes(0x1FE0);
                saveWriter.BaseStream.Position = npcInventory;
                saveWriter.Write(npcbin);
                npcReader.Close();
                saveWriter.Flush();
                saveWriter.Close();
                read_saveGame(slotOffset, FileName, 0, 0, 0);
            }
        }
        public void export_NPC(int slotOffset, string FileName)
        {
            /// Begin Table of Values
            Int32 npcInventory = 1212 + slotOffset;
            /// End Table of Values
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var filepath = Path.Combine(folderBrowserDialog1.SelectedPath, "slot_" + slotOffset + ".npc");
                FileStream saveGame = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                FileStream slotData = new FileStream(filepath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(saveGame);
                BinaryWriter writer = new BinaryWriter(slotData);
                reader.BaseStream.Position = npcInventory;
                var slotbin = reader.ReadBytes(0x1FE0);
                writer.Write(slotbin);
                slotData.Close();
            }
        }
        public void import_INV(int slotOffset, string FileName)
        {
            #region // Table of Values 
            /// Begin Table of Values
            Int32 inventoryStart = 13948 + slotOffset;
            /// End Table of Values
            #endregion

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RE4 Slot Files|*.inv" + "|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream invData = new FileStream(ofd.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryReader invReader = new BinaryReader(invData);
                FileStream saveData = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryWriter saveWriter = new BinaryWriter(saveData);
                invReader.BaseStream.Position = 0;
                var npcbin = invReader.ReadBytes(0x1200);
                saveWriter.BaseStream.Position = inventoryStart;
                saveWriter.Write(npcbin);
                invReader.Close();
                saveWriter.Flush();
                saveWriter.Close();
                read_saveGame(slotOffset, FileName, 0, 0, 0);
            }
        }
        public void export_INV(int slotOffset, string FileName)
        {
            #region // Table of Values 
            /// Begin Table of Values
            Int32 inventoryStart = 13948 + slotOffset;
            /// End Table of Values
            #endregion

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var filepath = Path.Combine(folderBrowserDialog1.SelectedPath, "slot_" + slotOffset + ".inv");
                FileStream saveGame = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                FileStream invData = new FileStream(filepath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(saveGame);
                BinaryWriter writer = new BinaryWriter(invData);
                reader.BaseStream.Position = inventoryStart;
                var invbin = reader.ReadBytes(0x1200);
                writer.Write(invbin);
                invData.Close();
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////DICTIONARYS/////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////
        #region //Dictionarys
        //
        // Iventory Dictionary Start // Dictionary stores all known inv items and they associate byte ID
        //
        private readonly static Dictionary<byte, string> invDict = new Dictionary<byte, string>()
        {
            { 0x0 , "Magnum Ammo" },
            { 0x1 , "Hand Grenade" },
            { 0x2 , "Incendiary Grenade" },
            { 0x3 , "Matilda" },
            { 0x4 , "Handgun Ammo" },
            { 0x5 , "First Aid Spray" },
            { 0x6 , "Green Herb" },
            { 0x7 , "Rifle Ammo" },
            { 0x8 , "White Egg" },
            { 0x9 , "Brown Egg" },
            { 0xA , "Gold  Egg" },
            { 0xB , "aaa 0xB Unkown" },
            { 0xC , "Plaga Sample" },
            { 0xD , "Krauser's Knife" },
            { 0xE , "Flash Grenade" },
            { 0xF , "Salazar Family Insignia" },
            { 0x10 , "Bowgun" },
            { 0x11 , "Bowgun Bolts" },
            { 0x12 , "Green Herb x2" },
            { 0x13 , "Green Herb x3" },
            { 0x14 , "Mixed Herbs (g+r)" },
            { 0x15 , "Mixed Herbs (g+r+y)" },
            { 0x16 , "Mixed Herbs (g+y)" },
            { 0x17 , "Rocket Launcher (Special)" },
            { 0x18 , "Shotgun Shells" },
            { 0x19 , "Red Herb" },
            { 0x1A , "Handcannon Ammo" },
            { 0x1B , "Hourglass w/ Gold Decor" },
            { 0x1C , "Yellow Herb" },
            { 0x1D , "Stone Tablet" },
            { 0x1E , "Lion Ornament" },
            { 0x1F , "Goat Ornament" },
            { 0x20 , "TMP Ammo" },
            { 0x21 , "Punisher" },
            { 0x22 , "Punisher w/ Silencer" },
            { 0x23 , "Handgun" },
            { 0x24 , "Handgun w/ Silencer" },
            { 0x25 , "Red9" },
            { 0x26 , "Red9 w/ Stock" },
            { 0x27 , "Blacktail" },
            { 0x28 , "Blacktail w/ Silencer" },
            { 0x29 , "Broken Butterfly" },
            { 0x2A , "Killer7" },
            { 0x2B , "Killer7 w/ Silencer" },
            { 0x2C , "Shotgun" },
            { 0x2D , "Striker" },
            { 0x2E , "Rifle" },
            { 0x2F , "Rifle (semi-auto)" },
            { 0x30 , "TMP" },
            { 0x31 , "Activation Key (Blue)" },
            { 0x32 , "TMP w/ Stock" },
            { 0x33 , "Activation Key (Red)" },
            { 0x34 , "Chicago Typewriter (infinite)" },
            { 0x35 , "Rocket Launcher" },
            { 0x36 , "Mine Thrower" },
            { 0x37 , "Handcannon" },
            { 0x38 , "Combat Knife" },
            { 0x39 , "Serpent Ornament" },
            { 0x3A , "Moonstone (right half)" },
            { 0x3B , "Insignia Key" },
            { 0x3C , "Round Insignia" },
            { 0x3D , "False eye" },
            { 0x3E , "Custom TMP" },
            { 0x3F , "Silencer (Handgun)" },
            { 0x40 , "Punisher" },
            { 0x41 , "P.R.L 412 Laser Cannon" },
            { 0x42 , "Stock (Red9)" },
            { 0x43 , "Stock (TMP)" },
            { 0x44 , "scope (Rifle)" },
            { 0x45 , "scope (Semi-Auto Fifle)" },
            { 0x46 , "Mine-Darts" },
            { 0x47 , "Shotgun**" },
            { 0x48 , "File - Capture Luis Sera" },
            { 0x49 , "File - Target Practice" },
            { 0x4A , "File - Luis' Memo" },
            { 0x4B , "File - Castellan Memo" },
            { 0x4C , "File - Female Intruder" },
            { 0x4D , "File - Butler's Memo" },
            { 0x4E , "File - Sample Retrieved" },
            { 0x4F , "File - Ritual Preparation" },
            { 0x50 , "File - Luis' Memo 2" },
            { 0x51 , "Rifle (Semi-Auto) w/ InfraRed Scope" },
            { 0x52 , "Krauser's Bow" },
            { 0x53 , "Chicago Typewriter (regular)" },
            { 0x54 , "File - Treasure Map (castle)" },
            { 0x55 , "File - Treasure Map (island)" },
            { 0x56 , "Velvet Blue" },
            { 0x57 , "Spinel" },
            { 0x58 , "Pearl Pendant" },
            { 0x59 , "Brass Pocket Watch" },
            { 0x5A , "Elegant Headdress" },
            { 0x5B , "Antique Pipe" },
            { 0x5C , "Gold Bangle w/ Pearls" },
            { 0x5D , "Amber Ring" },
            { 0x5E , "Beerstein" },
            { 0x5F , "Green Catseye" },
            { 0x60 , "Red Catseye" },
            { 0x61 , "Yellow Catseye" },
            { 0x62 , "Beerstein w/ (g)" },
            { 0x63 , "Beerstein w/ (r)" },
            { 0x64 , "Beerstein w/ (y)" },
            { 0x65 , "Beerstein w/ (g,r)" },
            { 0x66 , "Beerstein w/ (g,y)" },
            { 0x67 , "Beerstein w/ (r,y)" },
            { 0x68 , "Beerstein w/ (g,r,y)" },
            { 0x69 , "Moonstone (left half)" },
            { 0x6A , "Chicago Typewriter Ammo" },
            { 0x6B , "Rifle + scope" },
            { 0x6C , "Rifle (semi-auto) w/ Scope" },
            { 0x6D , "Infinite Launcher" },
            { 0x6E , "King's Grail" },
            { 0x6F , "Queen's Grail" },
            { 0x70 , "Staff of Royalty" },
            { 0x71 , "Gold Bars" },
            { 0x72 , "Arrows" },
            { 0x73 , "Bonus Time" },
            { 0x74 , "Emergency Lock Card Key" },
            { 0x75 , "Bonus Points" },
            { 0x76 , "Green Catseye" },
            { 0x77 , "Ruby" },
            { 0x78 , "Treasure Box (s)" },
            { 0x79 , "Treasure Box (l)" },
            { 0x7A , "Blue Moonstone" },
            { 0x7B , "Key to the Mine" },
            { 0x7C , "Attache Case S" },
            { 0x7D , "Attache Case M" },
            { 0x7E , "Attache Case L" },
            { 0x7F , "Attache Case XL" },
            { 0x80 , "Golden sword" },
            { 0x81 , "Iron Key" },
            { 0x82 , "Stone of Sacrifice" },
            { 0x83 , "Storage Room Card Key" },
            { 0x84 , "Freezer Card Key" },
            { 0x85 , "Piece of the Holy Beast, Panther" },
            { 0x86 , "Piece of the Holy Beast, Serpent" },
            { 0x87 , "Piece of the Holy Beast, Eagle" },
            { 0x88 , "Jet-Ski Key" },
            { 0x89 , "Dirty Pearl Pendant" },
            { 0x8A , "Dirty Brass Pocket Watch" },
            { 0x8B , "Old Key" },
            { 0x8C , "Camp Key" },
            { 0x8D , "Dynamite" },
            { 0x8E , "Lift Activation Key" },
            { 0x8F , "Gold Bangle" },
            { 0x90 , "Elegant Perfume Bottle" },
            { 0x91 , "Mirror w/ Pearls & Rubies" },
            { 0x92 , "Waste Disposal Card Key" },
            { 0x93 , "Elegant Chessboard" },
            { 0x94 , "Riot Gun" },
            { 0x95 , "Black Bass" },
            { 0x96 , "Hourglass w/ Gold Decor" },
            { 0x97 , "Black Bass (L)" },
            { 0x98 , "Illuminados Pendant" },
            { 0x99 , "Rifle w/ InfraRed Scope" },
            { 0x9A , "Crown" },
            { 0x9B , "Crown Jewel" },
            { 0x9C , "Royal Insignia" },
            { 0x9D , "Crown w/ Jewels" },
            { 0x9E , "Crown w/ Insignia" },
            { 0x9F , "Salazar Family Crown" },
            { 0xA0 , "Rifle Ammo (InfraRed)" },
            { 0xA1 , "Emerald" },
            { 0xA2 , "Bottle Caps" },
            { 0xA3 , "Gallery Key" },
            { 0xA4 , "Emblem (right half)" },
            { 0xA5 , "Emblem (left half)" },
            { 0xA6 , "Hexagonal Emblem" },
            { 0xA7 , "Castle Gate Key" },
            { 0xA8 , "Mixed Herbs (r+y)" },
            { 0xA9 , "Treasure Map (Village)" },
            { 0xAA , "Scope (mine-thrower)" },
            { 0xAB , "Mine-Thrower + Scope" },
            { 0xAC , "File - Playing Manual 1" },
            { 0xAD , "File - Info on Ashley" },
            { 0xAE , "File - Playing Manual 2" },
            { 0xAF , "File - Alert Order" },
            { 0xB0 , "File - About the Blue Medallions" },
            { 0xB1 , "File - Chief's Note" },
            { 0xB2 , "File - Closure of the Church" },
            { 0xB3 , "File - Anonymous Letter" },
            { 0xB4 , "File - Playing Manual 3" },
            { 0xB5 , "File - Sera and the 3rd Party" },
            { 0xB6 , "File - Two Routes" },
            { 0xB7 , "File - Village's Last Defense" },
            { 0xB8 , "Butterfly Lamp" },
            { 0xB9 , "Green Eye" },
            { 0xBA , "Red Eye" },
            { 0xBB , "Blue eye" },
            { 0xBC , "Butterfly Lamp w/ ( g )" },
            { 0xBD , "Butterfly Lamp w/ ( r )" },
            { 0xBE , "Butterfly Lamp w/ ( b )" },
            { 0xBF , "Butterfly Lamp w/ ( g, r )" },
            { 0xC0 , "Butterfly Lamp w/ ( g, b )" },
            { 0xC1 , "Butterfly Lamp w/ ( r, b )" },
            { 0xC2 , "Butterfly Lamp w/ ( r, g, b )" },
            { 0xC3 , "Prison Key" },
            { 0xC4 , "Platinum Sword" },
            { 0xC5 , "InfraRed Scope" },
            { 0xC6 , "Elegant Mask" },
            { 0xC7 , "Green Gem" },
            { 0xC8 , "Red Gem" },
            { 0xC9 , "Purple Gem" },
            { 0xCA , "Elegant Mask w/ ( g )" },
            { 0xCB , "Elegant Mask w/ ( r )" },
            { 0xCC , "Elegant Mask w/ ( p )" },
            { 0xCD , "Elegant Mask w/ ( g, r )" },
            { 0xCE , "Elegant Mask w/ ( g, p )" },
            { 0xCF , "Elegant Mask w/ ( r, p )" },
            { 0xD0 , "Elegant Mask w/ ( r, g, p )" },
            { 0xD1 , "Golden Lynx" },
            { 0xD2 , "Green Stone of Judgement" },
            { 0xD3 , "Red Stone of Faith" },
            { 0xD4 , "Blue Stone of Treason" },
            { 0xD5 , "Golden Lynx w/ ( g )" },
            { 0xD6 , "Golden Lynx w/ ( r )" },
            { 0xD7 , "Golden Lynx w/ ( b )" },
            { 0xD8 , "Golden Lynx w/ ( g, r )" },
            { 0xD9 , "Golden Lynx w/ ( g, b )" },
            { 0xDA , "Golden Lynx w/ ( r, b )" },
            { 0xDB , "Golden Lynx w/ ( g, r, b )" },
            { 0xDC , "Bottle Caps - Leon w/ rocket launcher" },
            { 0xDD , "Bottle Caps - Leon w/ Shotgun" },
            { 0xDE , "Bottle Caps - Leon w/ Handgun" },
            { 0xDF , "Bottle Caps - Ashley Graham" },
            { 0xE0 , "Bottle Caps - Luis Sera" },
            { 0xE1 , "Bottle Caps - Don Jose" },
            { 0xE2 , "Bottle Caps - Don Diego" },
            { 0xE3 , "Bottle Caps - Don Esteban" },
            { 0xE4 , "Bottle Caps - Don Manuel" },
            { 0xE5 , "Bottle Caps - Dr. Salvador" },
            { 0xE6 , "Bottle Caps - Merchant" },
            { 0xE7 , "Bottle Caps - Zealot w/ Scythe" },
            { 0xE8 , "Bottle Caps - Zealot w/ Shield" },
            { 0xE9 , "Bottle Caps - Zealot w/ Bowgun" },
            { 0xEA , "Bottle Caps - leader zealot" },
            { 0xEB , "Bottle Caps - Soldier w/ Dynamite" },
            { 0xEC , "Bottle Caps - Soldier w/ Stun-rod" },
            { 0xED , "Bottle Caps - Soldier w/ Hammer" },
            { 0xEE , "Bottle Caps - Isabel" },
            { 0xEF , "Bottle Caps - Maria" },
            { 0xF0 , "Bottle Caps - Ada Wong" },
            { 0xF1 , "Bottle Caps - Bella Sisters" },
            { 0xF2 , "Bottle Caps - Don Pedro" },
            { 0xF3 , "Bottle Caps - J.J." },
            { 0xF4 , "File - Letter From Ada" },
            { 0xF5 , "File - Luis' Memo 3" },
            { 0xF6 , "File - Paper Airplane" },
            { 0xF7 , "File - Our Plan" },
            { 0xF8 , "File - Luis' Memo 4" },
            { 0xF9 , "File - Krauser's Note" },
            { 0xFA , "File - Luis' Memo 5" },
            { 0xFB , "File - Our Mission" },
            { 0xFC , "aaa 0xFC Unknown" },
            { 0xFD , "aaa 0xFD Unknown " },
            { 0xFE , "Tactical Vest" },
            { 0xFF , "Empty Slot" },
        };
        //
        // Iventory Hex Dictionary Start // Dictionary stores out best weapon and items.
        //
        private readonly static Dictionary<string, string> weaponDict = new Dictionary<string, string>()
        {
            // Empty Slot
            { "FFFF00000000000000000000" , "Empty Slot" },
             // Recovery Items
			{ "060001000000000006030001" , "Recovery - Green Herb" },
            { "120001000000000006030001" , "Recovery - Green Herb x2" },
            { "130001000000000006030001" , "Recovery - Green Herb x3" },
            { "190001000000000006030001" , "Recovery - Red Herb" },
            { "1C0001000000000006030001" , "Recovery - Yellow Herb" },
            { "140001000000000006030001" , "Recovery - Mixed Herbs (g+r)" },
            { "160001000000000006030001" , "Recovery - Mixed Herbs (g+y)" },
            { "A80001000000000006030001" , "Recovery - Mixed Herbs (r+y)" },
            { "150001000000000006030001" , "Recovery - Mixed Herbs (g+r+y)" },
            { "050001000000000006030001" , "Recovery - First Aid Spray" },
            { "080001000000000006030001" , "Recovery - Chicken Egg" },
            { "090001000000000006030001" , "Recovery - Brown Chicken Egg" },
            { "0A0001000000000006030001" , "Recovery - Gold Chicken Egg" },
            { "0A00FFFF0000000006030001" , "Recovery - Gold Chicken Egg (65535)" },
            { "950001000000000006030001" , "Recovery - Black Bass" },
            { "970001000000000006030001" , "Recovery - Black Bass (L)" },
            // Grenade
            { "0100FF000000000019020001" , "Grenade - Hand Grenade (255)" },
            { "0100FFFF0000000019020001" , "Grenade - Hand Grenade (65535)" },
            { "0200FF0000000000190A0001" , "Grenade - Incendiary Grenade (255)" },
            { "0200FFFF00000000190A0001" , "Grenade - Incendiary Grenade (65535)" },
            { "0E00FF000000000019060001" , "Grenade - Flash Grenade (255)" },
            { "0E00FFFF0000000019060001" , "Grenade - Flash Grenade (65535)" },
			// Ammo
            { "0400FF000000000000000000" , "Ammo - Handgun Ammo x255" },
            { "0400FFFF0000000000000000" , "Ammo - Handgun Ammo x65535" },
            { "0700FF000000000000000000" , "Ammo - Rifle Ammo x255" },
            { "0700FFFF0000000000000000" , "Ammo - Rifle Ammo x65535" },
            { "1800FF000000000000000000" , "Ammo - Shotgun Shells x255" },
            { "1800FFFF0000000000000000" , "Ammo - Shotgun Shells x65535" },
            { "2000FF000000000000000000" , "Ammo - TMP Ammo x255" },
            { "2000FFFF0000000000000000" , "Ammo - TMP Ammo x65535" },
            { "4600FF000000000000000000" , "Ammo - Mine-Darts x255" },
            { "4600FFFF0000000000000000" , "Ammo - Mine-Darts x65535" },
            { "0000FF000000000000000000" , "Ammo - Magnum Ammo x255" },
            { "0000FFFF0000000000000000" , "Ammo - Magnum Ammo x65535" },
            // Handguns
            { "23002652C800000003020001" , "Handgun - Handgun" },
            { "21002652E000000013020001" , "Handgun - Punisher" },
            { "25002652B00000000C020001" , "Handgun - Red9" },
            { "270026521801000019020001" , "Handgun - Blacktail" },
            { "03000562200300001C090101" , "Handgun - Matilda" },
			// Magnums
            { "290006326000000004060001" , "Magnum - Broken Butterfly" },
            { "2A000220700000000C060001" , "Magnum - Killer7" },
            { "3700066200000000140E0001" , "Magnum - Handcannon" },
			//Machine Guns
            { "30000652D007000017060001" , "Machine Gun - TMP" },
			// Rilfles
			{ "2E00065290000000090F0001" , "Rifle - Rifle" },
            { "2F001552C0000000090C0001" , "Rifle - Rifle (Semi-Auto)" },
			// Shotgun
			{ "2C00065290000000080E0001" , "Shotgun - Shotgun" },
            { "9400065288000000080A0001" , "Shotgun - Riot Gun" },
            { "2D00056220030000150A0001" , "Shotgun - Striker" },
			// Special
			{ "410000000000000002010001" , "Special - P.R.L 412 Laser Cannon" },
            { "340016620000000002010001" , "Special - Ifinite - Chicago Typewriter" },
            { "350001000000000002010001" , "Rocket Launcher - (Single)" },
            { "170001000000000002010001" , "Rocket Launcher - (Special)" },
            { "6D0000000000000002010001" , "Rocket Launcher - (Infinite)" },
            { "3600032150000000170E0001" , "Explosive - Mine Thrower" },
			// Parts
            { "420001000000000002010001" , "Stock - (Red9)" },
            { "430001000000000002010001" , "Stock - (TMP)" },
            { "440001000000000002010001" , "Scope - (Rifle)" },
            { "450001000000000002010001" , "Scope - (Semi-Auto Rifle)" },
            { "C50001000000000002010001" , "Scope IR - (Semi-Auto Rifle)" },
            { "AA0001000000000002010001" , "Scope - (Mine-Thrower)" },
            { "3F0001000000000002010001" , "Silencer - (Handgun)" },
            // Testing
            { "510000000000000002010001" , "Testing - Rifle (Semi-Auto) w/ InfraRed Scope" },
            { "520000000000000002010001" , "Testing - Krauser's Bow" },
            { "7200FFFF000000001B000001" , "Testing - Arrows" },
            { "990000000000000002010001" , "Testing - Rifle w/ Infrared Scope" },
            { "320000000000000002010001" , "Testing - TMP w/ Stock" },
            { "2B0000000000000002010001" , "Testing - Killer7 w/ Silencer" },
            { "280000000000000002010001" , "Testing - Blacktail w/ Silencer" },
            { "260000000000000002010001" , "Testing - Handgun - Red9 w/ Stock" },
            { "220000000000000002010001" , "Testing - Punisher w/ Silencer" },
            { "240000000000000002010001" , "Testing - Handgun w/ Silencer" },
            { "AB0000000000000002010001" , "Testing - Mine Thrower w/ Scope" },
            // Modded Weapons
            { "27004782FFFF000006030101"  , "Modded Blacktail" },
            { "3700E662000000000C040101"  , "Modded Handcaanon" },
            { "2300E688FFFF000002030101"  , "Modded Handgun" },
            { "94000682FFFF000014080101"  , "Modded Riot Gun" },
            { "2E000682FFFF0000090F0001"  , "Modded Rifle" },
            { "2F005582FFFF000010070101"  , "Modded Auto Rilfe" },
            { "30000682FFFF0000180D0301"  , "Modded TMP" },
            { "36000381FFFF000018050101"  , "Modded Mine Thrower" },
            { "34000760A0000000070B0001"  , "Modded Chicago Typewriter" },
            { "2D00F682FFFF00001C050101"  , "Modded Striker" },
        };
        //
        // Iventory Hex Dictionary Start // Dictionary stores all known treasures, files, and bottle caps.
        //
        private readonly static Dictionary<string, string> itemDict = new Dictionary<string, string>()
        {
            // Empty Slot
            { "FFFF00000000000000000000" , "Empty Slot" },
            // Misc Items
			{ "FE0001000000000000000000" , "Misc - Tactical Vest" },
            { "7C0001000000000000000000" , "Misc - Attache Case S" },
            { "7D0001000000000000000000" , "Misc - Attache Case M" },
            { "7E0001000000000000000000" , "Misc - Attache Case L" },
            { "7F0001000000000000000000" , "Misc - Attache Case XL" },
            // Treasure
			{ "570001000000000000000000" , "Treasure - Spinel" },
            { "560001000000000000000000" , "Treasure - Velvet Blue" },
            { "A10001000000000000000000" , "Treasure - Emerald" },
            { "770001000000000000000000" , "Treasure - Ruby" },
            { "580001000000000000000000" , "Treasure - Pearl Pendant" },
            { "890001000000000000000000" , "Treasure - Dirty Pearl Pendant" },
            { "590001000000000000000000" , "Treasure - Brass Pocket Watch" },
            { "8A0001000000000000000000" , "Treasure - Dirty Brass Pocket Watch" },
            { "5A0001000000000000000000" , "Treasure - Elegant Headdress" },
            { "5B0001000000000000000000" , "Treasure - Antique Pipe" },
            { "5C0001000000000000000000" , "Treasure - Gold Bangle w/ Pearls" },
            { "5D0001000000000000000000" , "Treasure - Amber Ring" },
            { "5F0001000000000000000000" , "Treasure - Green Catseye" },
            { "600001000000000000000000" , "Treasure - Red Catseye" },
            { "610001000000000000000000" , "Treasure - Yellow Catseye" },
            { "5E0001000000000000000000" , "Treasure - Beerstein" },
            { "620001000000000000000000" , "Treasure - Beerstein w/ (g)" },
            { "630001000000000000000000" , "Treasure - Beerstein w/ (r)" },
            { "640001000000000000000000" , "Treasure - Beerstein w/ (y)" },
            { "650001000000000000000000" , "Treasure - Beerstein w/ (g,r)" },
            { "660001000000000000000000" , "Treasure - Beerstein w/ (g,y)" },
            { "670001000000000000000000" , "Treasure - Beerstein w/ (r,y)" },
            { "680001000000000000000000" , "Treasure - Beerstein w/ (g,r,y)" },
            { "B90001000000000000000000" , "Treasure - Green Eye" },
            { "BA0001000000000000000000" , "Treasure - Red Eye" },
            { "BB0001000000000000000000" , "Treasure - Blue eye" },
            { "B80001000000000000000000" , "Treasure - Butterfly Lamp" },
            { "BC0001000000000000000000" , "Treasure - Butterfly Lamp w/ ( g )" },
            { "BD0001000000000000000000" , "Treasure - Butterfly Lamp w/ ( r )" },
            { "BE0001000000000000000000" , "Treasure - Butterfly Lamp w/ ( b )" },
            { "BF0001000000000000000000" , "Treasure - Butterfly Lamp w/ ( g, r )" },
            { "C00001000000000000000000" , "Treasure - Butterfly Lamp w/ ( g, b )" },
            { "C10001000000000000000000" , "Treasure - Butterfly Lamp w/ ( r, b )" },
            { "C20001000000000000000000" , "Treasure - Butterfly Lamp w/ ( r, g, b )" },
            { "C70001000000000000000000" , "Treasure - Green Gem" },
            { "C80001000000000000000000" , "Treasure - Red Gem" },
            { "C90001000000000000000000" , "Treasure - Purple Gem" },
            { "C60001000000000000000000" , "Treasure - Elegant Mask" },
            { "CA0001000000000000000000" , "Treasure - Elegant Mask w/ ( g )" },
            { "CB0001000000000000000000" , "Treasure - Elegant Mask w/ ( r )" },
            { "CC0001000000000000000000" , "Treasure - Elegant Mask w/ ( p )" },
            { "CD0001000000000000000000" , "Treasure - Elegant Mask w/ ( g, r )" },
            { "CE0001000000000000000000" , "Treasure - Elegant Mask w/ ( g, p )" },
            { "CF0001000000000000000000" , "Treasure - Elegant Mask w/ ( r, p )" },
            { "D00001000000000000000000" , "Treasure - Elegant Mask w/ ( r, g, p )" },
            { "8F0001000000000000000000" , "Treasure - Gold Bangle" },
            { "980001000000000000000000" , "Treasure - Illuminados Pendant" },
            { "700001000000000000000000" , "Treasure - Staff of Royalty" },
            { "930001000000000000000000" , "Treasure - Elegant Chessboard" },
            { "900001000000000000000000" , "Treasure - Elegant Perfume Bottle" },
            { "910001000000000000000000" , "Treasure - Mirror w/ Pearls & Rubies" },
            { "1B0001000000000000000000" , "Treasure - Hourglass w/ Gold Decor" },
            { "9B0001000000000000000000" , "Treasure - Crown Jewel" },
            { "9C0001000000000000000000" , "Treasure - Royal Insignia" },
            { "9A0001000000000000000000" , "Treasure - Crown" },
            { "9D0001000000000000000000" , "Treasure - Crown w/ Jewels" },
            { "9E0001000000000000000000" , "Treasure - Crown w/ Insignia" },
            { "9F0001000000000000000000" , "Treasure - Salazar Family Crown" },
            { "D20001000000000000000000" , "Treasure - Green Stone of Judgement" },
            { "D30001000000000000000000" , "Treasure - Red Stone of Faith" },
            { "D40001000000000000000000" , "Treasure - Blue Stone of Treason" },
            { "D10001000000000000000000" , "Treasure - Golden Lynx" },
            { "760001000000000000000000" , "Treasure - Green Catseye" },
            { "D50001000000000000000000" , "Treasure - Golden Lynx w/ ( g )" },
            { "D60001000000000000000000" , "Treasure - Golden Lynx w/ ( r )" },
            { "D70001000000000000000000" , "Treasure - Golden Lynx w/ ( b )" },
            { "D80001000000000000000000" , "Treasure - Golden Lynx w/ ( g, r )" },
            { "D90001000000000000000000" , "Treasure - Golden Lynx w/ ( g, b )" },
            { "DA0001000000000000000000" , "Treasure - Golden Lynx w/ ( r, b )" },
            { "DB0001000000000000000000" , "Treasure - Golden Lynx w/ ( g, r, b )" },
			// Treasure Maps
			{ "540001000000000000000000" , "Treasure Map - Castle" },
            { "550001000000000000000000" , "Treasure Map - Island" },
            { "A90001000000000000000000" , "Treasure Map - Village" },
            // Files
			{ "AC0001000100000000000000" , "File - Playing Manual 1" },
            { "AD0001000200000000000000" , "File - Info on Ashley" },
            { "AE0001000300000000000000" , "File - Playing Manual 2" },
            { "AF0001000400000000000000" , "File - Alert Order" },
            { "B00001000500000000000000" , "File - About the Blue Medallions" },
            { "B10001000600000000000000" , "File - Chief's Note" },
            { "B20001000700000000000000" , "File - Closure of the Church" },
            { "B30001000800000000000000" , "File - Anonymous Letter" },
            { "B40001000900000000000000" , "File - Playing Manual 3" },
            { "B50001000A00000000000000" , "File - Sera and the 3rd Party" },
            { "B60001000B00000000000000" , "File - Two Routes" },
            { "B70001000C00000000000000" , "File - Village's Last Defense" },
            { "480001000D00000000000000" , "File - Capture Luis Sera" },
            { "490001000E00000000000000" , "File - Target Practice" },
            { "4A0001000F00000000000000" , "File - Luis' Memo" },
            { "4B0001001000000000000000" , "File - Castellan Memo" },
            { "4D0001001100000000000000" , "File - Butler's Memo" },
            { "4E0001001200000000000000" , "File - Sample Retrieved" },
            { "4F0001001300000000000000" , "File - Ritual Preparation" },
            { "500001001400000000000000" , "File - Luis' Memo 2" },
            { "F40001001500000000000000" , "File - Letter From Ada" },
            { "4C0001001600000000000000" , "File - Female Intruder" },
            { "F50001001700000000000000" , "File - Luis' Memo 3" },
            { "F60001001800000000000000" , "File - Paper Airplane" },
            { "F70001001900000000000000" , "File - Our Plan" },
            { "F80001001A00000000000000" , "File - Luis' Memo 4" },
            { "F90001001B00000000000000" , "File - Krauser's Note" },
            { "FA0001001C00000000000000" , "File - Luis' Memo 5" },
            { "FB0001001D00000000000000" , "File - Our Mission" },
            // Bottle Caps
            { "DC0001000000000000000000" , "Bottle Caps - Leon w/ rocket launcher" },
            { "DD0001000000000000000000" , "Bottle Caps - Leon w/ Shotgun" },
            { "DE0001000000000000000000" , "Bottle Caps - Leon w/ Handgun" },
            { "DF0001000000000000000000" , "Bottle Caps - Ashley Graham" },
            { "E00001000000000000000000" , "Bottle Caps - Luis Sera" },
            { "F00001000000000000000000" , "Bottle Caps - Ada Wong" },
            { "E10001000000000000000000" , "Bottle Caps - Don Jose" },
            { "E20001000000000000000000" , "Bottle Caps - Don Diego" },
            { "E30001000000000000000000" , "Bottle Caps - Don Esteban" },
            { "E40001000000000000000000" , "Bottle Caps - Don Manuel" },
            { "E50001000000000000000000" , "Bottle Caps - Dr. Salvador" },
            { "F10001000000000000000000" , "Bottle Caps - Bella Sisters" },
            { "E60001000000000000000000" , "Bottle Caps - Merchant" },
            { "E70001000000000000000000" , "Bottle Caps - Zealot w/ Scythe" },
            { "E80001000000000000000000" , "Bottle Caps - Zealot w/ Shield" },
            { "E90001000000000000000000" , "Bottle Caps - Zealot w/ Bowgun" },
            { "EA0001000000000000000000" , "Bottle Caps - Leader Zealot" },
            { "F20001000000000000000000" , "Bottle Caps - Don Pedro" },
            { "EB0001000000000000000000" , "Bottle Caps - Soldier w/ Dynamite" },
            { "EC0001000000000000000000" , "Bottle Caps - Soldier w/ Stun-rod" },
            { "ED0001000000000000000000" , "Bottle Caps - Soldier w/ Hammer" },
            { "EE0001000000000000000000" , "Bottle Caps - Isabel" },
            { "EF0001000000000000000000" , "Bottle Caps - Maria" },
            { "F30001000000000000000000" , "Bottle Caps - J.J." },
            // Key Items
            { "A40001000000000000000000" , "Key Item - Emblem (right half)" },
            { "A50001000000000000000000" , "Key Item - Emblem (left half)" },
            { "A60001000000000000000000" , "Key Item - Hexagonal Emblem" },
            { "3B0001000000000000000000" , "Key Item - Insignia Key" },
            { "3C0001000000000000000000" , "Key Item - Round Insignia" },
            { "8B0001000000000000000000" , "Key Item - Old Key" },
            { "8C0001000000000000000000" , "Key Item - Camp Key" },
            { "3D0001000000000000000000" , "Key Item - False eye" },
            { "C40001000000000000000000" , "Key Item - Platinum Sword" },
            { "800001000000000000000000" , "Key Item - Golden sword" },
            { "A70001000000000000000000" , "Key Item - Castle Gate Key" },
            { "C30001000000000000000000" , "Key Item - Prison Key" },
            { "A30001000000000000000000" , "Key Item - Gallery Key" },
            { "1F0001000000000000000000" , "Key Item - Goat Ornament" },
            { "1E0001000000000000000000" , "Key Item - Lion Ornament" },
            { "390001000000000000000000" , "Key Item - Serpent Ornament" },
            { "690001000000000000000000" , "Key Item - Moonstone (left half)" },
            { "3A0001000000000000000000" , "Key Item - Moonstone (right half)" },
            { "7A0001000000000000000000" , "Key Item - Blue Moonstone" },
            { "1D0001000000000000000000" , "Key Item - Stone Tablet" },
            { "0F0001000000000000000000" , "Key Item - Salazar Family Insignia" },
            { "6E0001000000000000000000" , "Key Item - King's Grail" },
            { "6F0001000000000000000000" , "Key Item - Queen's Grail" },
            { "8D0001000000000000000000" , "Key Item - Dynamite" },
            { "7B0001000000000000000000" , "Key Item - Key to the Mine" },
            { "820001000000000000000000" , "Key Item - Stone of Sacrifice" },
            { "840001000000000000000000" , "Key Item - Freezer Card Key" },
            { "920001000000000000000000" , "Key Item - Waste Disposal Card Key" },
            { "830001000000000000000000" , "Key Item - Storage Room Card Key" },
            { "850001000000000000000000" , "Key Item - Piece of the Holy Beast, Panther" },
            { "870001000000000000000000" , "Key Item - Piece of the Holy Beast, Eagle" },
            { "860001000000000000000000" , "Key Item - Piece of the Holy Beast, Serpent" },
            { "740001000000000000000000" , "Key Item - Emergency Lock Card Key" },
            { "880001000000000000000000" , "Key Item - Jet-Ski Key" },
        };
        //
        // Chapter dictionary with some descriptions.
        //
        private readonly static Dictionary<string, string> chapterDict = new Dictionary<string, string>()
        {
            { "0001" , "Chapter 1 > Path to the Village" },
            { "0101" , "Chapter 1 > The Village" },
            { "0301" , "Chapter 1 > The Farm" },
            { "0601" , "Chapter 1 > Beyond the Farm" },
            { "0401" , "Chapter 1 > Valley" },
            { "0701" , "Chapter 1 > Village Storage" },
            { "0501" , "Chapter 1 > Chief's House" },
            { "0201" , "Chapter 1 > The Locked Building" },
            { "0801" , "Chapter 1 > Cemetery" },
            { "0901" , "Chapter 1 > Holding Area" },
            { "0A01" , "Chapter 1 > Swamp" },
            { "0B01" , "Chapter 1 > The Lake" },
            { "1B01" , "Chapter 2 > The Lake (night time)" },
            { "0C01" , "Chapter 2 > Waterfall" },
            { "0D01" , "Chapter 2 > Mechant Cave" },
            { "1A01" , "Chapter 2 > Swamp (night time)" },
            { "0E01" , "Chapter 2 > Undergroun Dock" },
            { "1901" , "Chapter 2 > Holding Area (night time)" },
            { "0F01" , "Chapter 2 > Gondola" },
            { "1101" , "Chapter 2 > The Village (night time)" },
            { "1201" , "Chapter 2 > The Locked Building (night time)" },
            { "1301" , "Chapter 2 > The Farm (night time)" },
            { "1701" , "Chapter 2 > Church" },
            { "1801" , "Chapter 2 > Cemetery (night time)" },
            { "1C01" , "Chapter 2 > Cabin" },
            { "1D01" , "Chapter 2 > Bella Sisters (Left)" },
            { "1E01" , "Chapter 2 > El Gigante (Right Path)" },
            { "1F01" , "Chapter 2 > Burning Barn (Mendez)" },
            { "0002" , "Chapter 2 > Road to Castle" },
            { "0202" , "Chapter 3 > Castle Begining Catapults & Cannons" },
            { "0302" , "Chapter 3 > Barracks" },
            { "0102" , "Chapter 3 > Castle Entrance" },
            { "0402" , "Chapter 3 > Ceremonial Chamber (Before sewer)" },
            { "0502" , "Chapter 3 > Sewers" },
            { "0602" , "Chapter 3 > Giant Knight Hall" },
            { "0702" , "Chapter 3 > Castle Sword Swap Room" },
            { "0802" , "Chapter 3 > Castle Water Room" },
            { "0902" , "Chapter 3 > Castle Gallery)" },
            { "0A02" , "Chapter 3 > Castle Fountain" },
            { "0B02" , "Chapter 3 > Hedge Garden Maze" },
            { "0C02" , "Chapter 3 > Castle Dining Hall" },
            { "0D02" , "Chapter 3 > Ashley Library" },
            { "0E02" , "Chapter 3 > Ashley Puzzle" },
            { "0F02" , "Chapter 3 > Lifting Bridge Room" },
            { "1002" , "Chapter 4 > Lava Trolley" },
            { "2202" , "Chapter 4 > Dragon room" },
            { "1102" , "Chapter 4 > Grail Offering Hallway" },
            { "1202" , "Chapter 4 > Queens Grail" },
            { "1302" , "Chapter 4 > Novistador Hive" },
            { "1402" , "Chapter 4 > Path to Clock Tower" },
            { "1502" , "Chapter 4 > Castle Above Pit" },
            { "2902" , "Chapter 4 > Castle Pit" },
            { "1602" , "Chapter 4 > Kings Grail" },
            { "1702" , "Chapter 4 > Gear Clock-Tower" },
            { "1802" , "Chapter 4 > Dual Garrador Tower" },
            { "1902" , "Chapter 4 > Castle Trolley Cart" },
            { "1A02" , "Chapter 4 > Underground Mines" },
            { "1B02" , "Chapter 4 > Mines (Mine cart ride)" },
            { "1B03" , "Chapter 4 > U3 Fight Area" },
            { "1D02" , "Chapter 4 > Novistador Cave" },
            { "1A03" , "Chapter 4 > Island before U3 Foght" },
            { "1D03" , "Chapter 4 > Island after U3 Fight" },
            { "2002" , "Chapter 4 > Save point after Verdugo" },
            { "2102" , "Chapter 4 > Verdugo Area" },
            { "2302" , "Chapter 4 > Mines with Boulder" },
            { "2402" , "Chapter 4 > Furnace Dual El Gigante" },
            { "2502" , "Chapter 4 > Outdoor Campfire Area" },
            { "2602" , "Chapter 4 > Giant Salazar Statue Room" },
            { "2702" , "Chapter 4 > Tower of Barrells" },
            { "2802" , "Chapter 4 > Salazar Boss room" },
            { "2A02" , "Chapter 4 > Final area (Ada waits for Leon)" },
            { "2C02" , "Shooting Range" },
            { "0003" , "Chapter 5 > First Island" },
            { "0103" , "Chapter 5 > Upper First Island" },
            { "0303" , "Chapter 5 > Lab" },
            { "0403" , "Chapter 5 > CCTV Room" },
            { "0503" , "Chapter 5 > Garage Door & Archers" },
            { "0603" , "Chapter 5 > Lab Hallway" },
            { "0703" , "Chapter 5 > Testing Room with RGB Puzzle" },
            { "0803" , "Chapter 5 > Freezer Room" },
            { "0903" , "Chapter 5 > Lab w/ Iron Maiden" },
            { "0A03" , "Chapter 5 > Communications Tower" },
            { "0B03" , "Chapter 5 > Claw Machine" },
            { "0C03" , "Chapter 5 > Ashley Prison" },
            { "0D03" , "Chapter 5 > Tandem Door" },
            { "0E03" , "Chapter 5 > Breakroom" },
            { "0F03" , "Chapter 5 > Truck on the Lift" },
            { "1003" , "Chapter 5 > Below the Claw Machine" },
            { "1103" , "Chapter 5 > Wrecking Ball" },
            { "1203" , "Chapter 5 > Merchant before Truck" },
            { "1503" , "Chapter 5 > Room after Ahsley wrecks Truck" },
            { "1603" , "Chapter 5 > Saddler steals Ashley" },
            { "1703" , "Chapter 5 > Leon vs. Krauser" },
            { "1803" , "Chapter 5 > Laser Room" },
            { "1C03" , "Chapter 5 > Krauser Area" },
            { "2003" , "Chapter 5 > Big Battle Area" },
            { "2103" , "Chapter 5 > Mikes Helicopter" },
            { "2503" , "Chapter 5 > Leon attacks Ada." },
            { "2603" , "Chapter 5 > Jail House w/Reginerator" },
            { "2703" , "Chapter 5 > Keycards and JJ" },
            { "2903" , "Chapter 5 > Ada vs Saddler" },
            { "3003" , "Chapter 5 > Parasite Removal" },
            { "3103" , "Chapter 5 > Final Merchant" },
            { "3203" , "Chapter 5 > Saddler Boss Fight" },
            { "3303" , "Chapter 5 > Jet Ski." },
            { "3403" , "Chapter 5 > Second Truck" },
            { "0004" , "Mercenaries - Village" },
            { "0204" , "Mercenaries - Castle" },
            { "0304" , "Mercenaries - Island" },
            { "0404" , "Mercenaries - Waterworld" }
        };
        //
        // Enemy Dict with most of the known enemies.
        //
        private readonly static Dictionary<string, string> enemyDict = new Dictionary<string, string>()
        {
            { "000F" , "Unknown 000F" },
            { "0002" , "Unknown 0002" },
            { "0000" , "Unknown 0000" },
            { "0100" , "Unknown 0100" },
            { "0200" , "Leon with TMP" },
            { "0300" , "Ashley (auto-follow with HUD)" },
            { "0400" , "Luis Sera with Red9" },
            { "0500" , "Ashley (auto-follow, no HUD) 0" },
            { "0C00" , "Ashley (auto-follow, no HUD) 1" },
            { "0600" , "Don Jose  Invincible 0600" },
            { "0700" , "Don Jose  Invincible 0700" },
            { "0800" , "Don Jose  Invincible 0800" },
            { "0900" , "Don Jose  Invincible 0900" },
            { "0A00" , "Don Jose  Invincible 0A00" },
            { "0B00" , "Don Jose  Invincible 0B00" },
            { "0D00" , "Don Jose  Invincible 0D00" },
            { "0E00" , "Jet-ski" },
            { "0F00" , "Boat Lake wooden boat 0" },
            { "0F01" , "Boat Lake wooden boat 1" },
            { "0F02" , "Boat Lake wooden boat 2" },
            { "0F03" , "Boat Lake wooden boat 3" },
            { "0F04" , "Boat Lake wooden boat 4" },
            { "0F05" , "Boat Lake wooden boat 5" },
            { "1107" , "Black Zealot Regular 1107" },
            { "1108" , "Red Zealot Regular 1108" },
            { "1200" , "Don Jose  1200" },
            { "1201" , "Don Manuel  1201" },
            { "1203" , "Don Esteban  1203" },
            { "1204" , "Don Diego  1204" },
            { "120B" , "Maria  Regular" },
            { "120C" , "Isabell  Regular" },
            { "1300" , "Don Jose  1300" },
            { "1301" , "Don Manuel  1301" },
            { "1303" , "Don Esteban  1303" },
            { "1304" , "Don Diego  1304" },
            { "1306" , "Merchant 1306" },
            { "1406" , "Merchant 1406" },
            { "1407" , "Black Zealot 1407" },
            { "1408" , "Red Zealot 1408" },
            { "1500" , "Don Jose  1500" },
            { "1503" , "Don Esteban  1503" },
            { "1504" , "Don Diego  1504" },
            { "150B" , "Maria  150B" },
            { "1603" , "Don Esteban  1603" },
            { "1604" , "Don Diego  1604" },
            { "160B" , "Maria  160B" },
            { "160C" , "Isabell  160C" },
            { "1700" , "Don Jose " },
            { "1701" , "Don Manuel " },
            { "1703" , "Don Esteban " },
            { "1704" , "Don Diego " },
            { "170C" , "Isabell " },
            { "1800" , "Merchant 1800" },
            { "1801" , "Merchant 1801" },
            { "1806" , "Merchant Merchant" },
            { "1807" , "Merchant Merchant" },
            { "1907" , "Black Zealot" },
            { "1A07" , "Black Zealot" },
            { "1A08" , "Red Zealot" },
            { "1A09" , "Blue Zealot" },
            { "1B07" , "Black Zealot 1B07" },
            { "1B0A" , "Regular Garrador -" },
            { "1C09" , "Blue Zealot 1C09" },
            { "1C0A" , "Regular Garrador" },
            { "1C0D" , "Armored Garrador" },
            { "1D02" , "Captain J.J." },
            { "1D0E" , "Soldier E" },
            { "1D0F" , "Soldier F" },
            { "1D10" , "Soldier 10" },
            { "1D11" , "Soldier 11" },
            { "1D12" , "Soldier 12" },
            { "1D13" , "Soldier 13" },
            { "1D14" , "Soldier 14" },
            { "1D15" , "Soldier 15" },
            { "1E00" , "Soldier 1E00" },
            { "1E06" , "Merchant 1E06" },
            { "1E0E" , "Soldier 1E0E" },
            { "1E0F" , "Soldier 1E0F" },
            { "1E17" , "Soldier 1E17" },
            { "1E19" , "Soldier 1E19" },
            { "1F0E" , "Soldier 1F0E" },
            { "1F0F" , "Soldier 1F0F" },
            { "1F10" , "Soldier 1F10" },
            { "1F18" , "Soldier 1F18" },
            { "200E" , "Soldier Waterworld and SW " },
            { "200F" , "Soldier Waterworld and SW " },
            { "2010" , "Soldier Waterworld and SW " },
            { "2011" , "Soldier Waterworld and SW " },
            { "2012" , "Soldier Waterworld and SW " },
            { "2013" , "Soldier Waterworld and SW " },
            { "2014" , "Soldier Waterworld and SW " },
            { "2015" , "Soldier Waterworld and SW " },
            { "2016" , "Waterworld Boss" },
            { "2100" , "Farm Dog" },
            { "2101" , "Injured Dog" },
            { "2200" , "Evil Wolf" },
            { "2300" , "Crow" },
            { "2400" , "Snake" },
            { "2500" , "Parasite type IV Parasite 0" },
            { "2507" , "Parasite type IV Parasite 7" },
            { "2509" , "Parasite type IV Parasite 9" },
            { "2600" , "Black & White Cow" },
            { "2601" , "Brown Cow" },
            { "2700" , "Bass" },
            { "2701" , "Large Bass" },
            { "2800" , "Bright Chicken" },
            { "2801" , "Dark Chicken" },
            { "2900" , "Bat" },
            { "2A00" , "Bear Trap" },
            { "2A02" , "Mine Trap String" },
            { "2B00" , "First El Gigante" },
            { "2B01" , "El Gigante w/ Mask" },
            { "2B02" , "Second El Gigante" },
            { "2B03" , "Chained El Gigante" },
            { "2C00" , "Verdugo Working" },
            { "2C01" , "Verdugo Celing Attack" },
            { "2D00" , "Invisible Novistador 0" },
            { "2D02" , "Invisible Novistador 1" },
            { "2D04" , "Flying Novistador" },
            { "2E00" , "Spider" },
            { "2F00" , "Del Lago" },
            { "3100" , "Saddler Monster 0 //Sorta spawned" },
            { "3101" , "Saddler Monster 1" },
            { "3200" , "Project U3//WORKS" },
            { "3500" , "Mendez Full body monsternw" },
            { "3501" , "Mendez Half body monster" },
            { "3502" , "Mendez Legs Only" },
            { "3600" , "Common regenerator 0" },
            { "3601" , "Common regenerator 1" },
            { "3602" , "Iron Maiden 2" },
            { "3603" , "Iron Maiden 3 //Works" },
            { "3607" , "Blind regenerator" },
            { "3800" , "Salazar Monster 0nw" },
            { "3801" , "Salazar Tentacle 1" },
            { "3802" , "Salazar Tentacle 2" },
            { "3803" , "Salazar" },
            { "3900" , "Krasuer Unmutated 0// Does not work" },
            { "3901" , "Krauser Unmutated // Does not spawn" },
            { "3902" , "Krauser Mutated //WORKS" },
            { "3903" , "Krauser Mutated(SW //NOT WORK)" },
            { "3A00" , "Flying Robot 0" },
            { "3A01" , "Flying Robot 1" },
            { "3A02" , "Robot Standing" },
            { "3B00" , "Truck Driven by " },
            { "3B01" , "Ammo Wagon moving after shot" },
            { "3B02" , "Ammo Wagon exploding after shot" },
            { "3C00" , "Rusty Knight 3C00 //Doesnt Move" },
            { "3C01" , "Rusty Knight 3C01 //Doesnt Move"},
            { "3C02" , "Feathered Knight 3C02" },
            { "3C03" , "Feathered Knight 3C03" },
            { "3D00" , "Mike's Helicopter" },
            // Extra Stuff
            { "3F00" , "SW Saddler Saddler Unmatated Saddler in Separate Ways" },
            { "4200" , "Don Jose SW  In the same room with Bella Sister" },
            { "4201" , "Don Manuel SW  In the same room with Bella Sister" },
            { "4203" , "Don Esteban SW  In the same room with Bella Sister" },
            { "420B" , "Maria SW  In the same room with Bella Sister" },
            { "420C" , "Isabell SW  In the same room with Bella Sister" },
            { "4300" , "SW soldier Voice of attacking soldiers" },
            { "4400" , "Don Jose SW  In the same room with 441A" },
            { "4403" , "Don Esteban SW  In the same room with 441A" },
            { "4404" , "Don Diego SW  In the same room with 441A" },
            { "440B" , "Maria SW  In the same room with 441A" },
            { "441A" , "Don Manuel  in Leon's jacket" },
            { "4E00" , "Ship cannon Cannon Ship cannon attacking Ada" },
            { "4E01" , "Ship cannon Cannon Ship cannon attacking Ada" },
        };
        //
        //Cool way to convert hex string to bytes[]---------
        //
        private readonly static Dictionary<char, byte> hexmap = new Dictionary<char, byte>()
            {
                { 'a', 0xA },{ 'b', 0xB },{ 'c', 0xC },{ 'd', 0xD },
                { 'e', 0xE },{ 'f', 0xF },{ 'A', 0xA },{ 'B', 0xB },
                { 'C', 0xC },{ 'D', 0xD },{ 'E', 0xE },{ 'F', 0xF },
                { '0', 0x0 },{ '1', 0x1 },{ '2', 0x2 },{ '3', 0x3 },
                { '4', 0x4 },{ '5', 0x5 },{ '6', 0x6 },{ '7', 0x7 },
                { '8', 0x8 },{ '9', 0x9 }
            };
        #endregion

        ///////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////FRONT END STUFF////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////
        #region //Front End Stuff
        ///////////////////////////////////////////////
        /////////////////TOP GROUP/////////////////////
        ///////////////////////////////////////////////
        #region //Top Group Box
        //
        // Open Button Clicked
        //
        private void saveOpenButton_Click(object sender, EventArgs e)
        {
            inventoryList.Items.Clear();
            merchantList.Items.Clear();
            tuneupList.Items.Clear();
            re4v.FileName = getFileName();
            checkBox1.Enabled = false;
            label5.Enabled = false;
            // See the saveSlotSelect index changed event.
            saveSlotSelect.SelectedIndex = 0;
            this.Text = "RE4 VR Save Editor -" + re4v.FileName;
        }
        //
        // Save Slot Select Changed
        //
        private void saveSlotSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; read_saveGame(slotOffset, re4v.FileName, inventoryList.SelectedIndex, merchantList.SelectedIndex, tuneupList.SelectedIndex); }

            }
        }
        //
        // Save Button Clicked
        //
        private void saveSaveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; write_saveGame(slotOffset, re4v.FileName, inventoryList.SelectedIndex, merchantList.SelectedIndex, tuneupList.SelectedIndex); }

            }
            MessageBox.Show("Saved and Checksumed!");
        }
        #endregion

        ///////////////////////////////////////////////
        ////////////////GENERAL TAB////////////////////
        ///////////////////////////////////////////////
        #region // General Tab
        // Costume Changed
        private void stats_costume_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (costumeSelect.SelectedIndex == 0) { checkBox1.Enabled = true; label5.Enabled = true; }
            if (costumeSelect.SelectedIndex == 1) { checkBox1.Enabled = false; label5.Enabled = false; }
            if (costumeSelect.SelectedIndex == 2) { checkBox1.Enabled = false; label5.Enabled = false; }
        }
        #endregion

        ///////////////////////////////////////////////
        ////////////////INVENTORY TAB//////////////////
        ///////////////////////////////////////////////
        #region // Inventory Tab
        //
        // Inventory Selection Changed
        //
        private void inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; read_Hex(slotOffset, re4v.FileName, inventoryList.SelectedIndex, merchantList.SelectedIndex, tuneupList.SelectedIndex, npcList.SelectedIndex); }
            }
            textBox1.Enabled = true; textBox2.Enabled = true; textBox3.Enabled = true; textBox4.Enabled = true; textBox5.Enabled = true; textBox6.Enabled = true;
            textBox12.Enabled = true; textBox11.Enabled = true; textBox10.Enabled = true; textBox9.Enabled = true; textBox8.Enabled = true; textBox7.Enabled = true;
        }
        //
        // Weapon List Changed
        //
        private void weaponList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var pair in weaponDict)
            {
                if (pair.Value == weaponList.Text)
                {
                    textBox1.Text = pair.Key.Substring(0, 2);
                    textBox2.Text = pair.Key.Substring(2, 2);
                    textBox3.Text = pair.Key.Substring(4, 2);
                    textBox4.Text = pair.Key.Substring(6, 2);
                    textBox5.Text = pair.Key.Substring(8, 2);
                    textBox6.Text = pair.Key.Substring(10, 2);
                    textBox12.Text = pair.Key.Substring(12, 2);
                    textBox11.Text = pair.Key.Substring(14, 2);
                    textBox10.Text = pair.Key.Substring(16, 2);
                    textBox9.Text = pair.Key.Substring(18, 2);
                    textBox8.Text = pair.Key.Substring(20, 2);
                    textBox7.Text = pair.Key.Substring(22, 2);
                }
            }
            textBox1.Enabled = true; textBox2.Enabled = true; textBox3.Enabled = true; textBox4.Enabled = true; textBox5.Enabled = true; textBox6.Enabled = true;
            textBox12.Enabled = true; textBox11.Enabled = true; textBox10.Enabled = true; textBox9.Enabled = true; textBox8.Enabled = true; textBox7.Enabled = true;
        }
        //
        // Key Item and Treasure List Changed
        //
        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var pair in itemDict)
            {
                if (pair.Value == itemList.Text)
                {
                    textBox1.Text = pair.Key.Substring(0, 2);
                    textBox2.Text = pair.Key.Substring(2, 2);
                    textBox3.Text = pair.Key.Substring(4, 2);
                    textBox4.Text = pair.Key.Substring(6, 2);
                    textBox5.Text = pair.Key.Substring(8, 2);
                    textBox6.Text = pair.Key.Substring(10, 2);
                    textBox12.Text = pair.Key.Substring(12, 2);
                    textBox11.Text = pair.Key.Substring(14, 2);
                    textBox10.Text = pair.Key.Substring(16, 2);
                    textBox9.Text = pair.Key.Substring(18, 2);
                    textBox8.Text = pair.Key.Substring(20, 2);
                    textBox7.Text = pair.Key.Substring(22, 2);
                }
            }
            // Some front end stuff
            // Disable unused textboxes
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox12.Enabled = false;
            textBox11.Enabled = false;
            textBox10.Enabled = false;
            textBox9.Enabled = false;
            textBox8.Enabled = false;
            textBox7.Enabled = false;
            label16.Text = "QTY";
        }
        //
        // Inventory Save Button Clicked
        //
        private void saveInventoryButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; write_Hex(slotOffset, re4v.FileName, inventoryList.SelectedIndex, npcList.SelectedIndex); }

            }
            label16.Text = "CC";
        }
        #endregion

        ///////////////////////////////////////////////
        ////////////////MERCHANT TAB///////////////////
        ///////////////////////////////////////////////
        #region // Merchant Tab
        //
        // Merchant Item Select
        //
        private void merchantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i)
                {
                    Int32 slotOffset = 0x1F30 + 0xCA48 * i; read_Hex(slotOffset, re4v.FileName, inventoryList.SelectedIndex, merchantList.SelectedIndex, tuneupList.SelectedIndex, npcList.SelectedIndex);
                }
            }
        }
        //
        // Tuneup Select
        //
        private void tuneupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; read_Hex(slotOffset, re4v.FileName, inventoryList.SelectedIndex, merchantList.SelectedIndex, tuneupList.SelectedIndex, npcList.SelectedIndex); }
            }
        }
        // Default Max Checkbox
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            modmax_checkbox.Enabled = true;
            modfspeed_checkbox.Enabled = true;
            if (max_checkbox.Checked) { modmax_checkbox.Enabled = false; modfspeed_checkbox.Enabled = false; unlockall_checkbox.Checked = true; }
        }
        // Max Modded Checkbox
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            max_checkbox.Enabled = true;
            modfspeed_checkbox.Enabled = true;
            if (modmax_checkbox.Checked) { max_checkbox.Enabled = false; modfspeed_checkbox.Enabled = false; unlockall_checkbox.Checked = true; }
        }
        // Max Firiing Speed Checkbox
        private void modfspeed_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            modmax_checkbox.Enabled = true;
            max_checkbox.Enabled = true;
            if (modfspeed_checkbox.Checked) { max_checkbox.Enabled = false; modmax_checkbox.Enabled = false; unlockall_checkbox.Checked = true; }
        }
        #endregion

        ///////////////////////////////////////////////
        ///////////////////NPC TAB/////////////////////
        ///////////////////////////////////////////////
        #region //NPC Tab
        //
        // Enemy Selection Changed
        //
        private void npcList_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; read_Hex(slotOffset, re4v.FileName,
                    inventoryList.SelectedIndex, merchantList.SelectedIndex, tuneupList.SelectedIndex, npcList.SelectedIndex); }
            }
        }
        //
        // Npc Save Button Clicked
        //
        private void saveNpcButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; write_Hex(slotOffset, re4v.FileName, inventoryList.SelectedIndex, npcList.SelectedIndex); }

            }
        }
        //
        // NPC Text auto updates the big textbox.
        //
        private void npcByte_TextChanged(object sender, EventArgs e)
        {
            npcHexbox.Text = npcBox0.Text + npcBox1.Text + npcBox2.Text + npcBox3.Text + npcBox4.Text + npcBox5.Text +
            npcBox6.Text + npcBox7.Text + npcBox8.Text + npcBox9.Text + npcBox10.Text + npcBox11.Text + npcBox12.Text + npcBox13.Text + npcBox14.Text + npcBox15.Text + npcBox16.Text;
        }
        #endregion

        ///////////////////////////////////////////////
        //////////////////TOOLS TAB////////////////////
        ///////////////////////////////////////////////
        #region // Tools Tab
        private void importSlot_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; import_Slot(slotOffset, re4v.FileName); }
            }
        }
        private void exportSlot_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; export_Slot(slotOffset, re4v.FileName); }
            }
        }
        private void importNPC_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; import_NPC(slotOffset, re4v.FileName); }
            }
        }
        private void exportNPC_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; export_NPC(slotOffset, re4v.FileName); }
            }
        }
        private void importINV_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; import_INV(slotOffset, re4v.FileName); }
            }
        }
        private void exportINV_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                if (saveSlotSelect.SelectedIndex == i) { Int32 slotOffset = 0x1F30 + 0xCA48 * i; export_INV(slotOffset, re4v.FileName); }
            }
        }
        #endregion

        ///////////////////////////////////////////////
        //////////////////MISC STUFF///////////////////
        ///////////////////////////////////////////////
        #region // Misc Stuff
        //Hyper Link
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
        // HyperLink
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel2.AccessibleDescription);
        }
        // Dynamic form size change

        private void tabbox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabbox.SelectedIndex == 0) { tabbox.Width = 400; tabbox.Height = 350; groupBox12.Visible = true; }
            if (tabbox.SelectedIndex == 1) { tabbox.Width = 400; tabbox.Height = 350; groupBox12.Visible = true; }
            if (tabbox.SelectedIndex == 2) { tabbox.Width = 400; tabbox.Height = 350; groupBox12.Visible = true; }
            if (tabbox.SelectedIndex == 3) { tabbox.Width = 400; tabbox.Height = 350; groupBox12.Visible = true; }
            if (tabbox.SelectedIndex == 4)
            {
                tabbox.Width = 1200;
                tabbox.Height = 800;
                groupBox12.Visible = false;
            }
            if (tabbox.SelectedIndex == 5) { tabbox.Width = 400; tabbox.Height = 350; }
        #endregion

#endregion
        }
    }
}
