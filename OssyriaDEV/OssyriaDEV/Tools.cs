using System;
using System.Collections.Generic;

namespace OssyriaDEV
{
    public class Tools
    {
        private static Random r = new Random(DateTime.Now.Millisecond);
        private const string secret = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()-={}[]<>?,.";

        private static readonly List<int> classSet = new List<int>()
        {
            0,
            100,
            110, 111, 112,
            120, 121, 122,
            130, 131, 132,
            200,
            210, 211, 212,
            220, 221, 222,
            230, 231, 232,
            300,
            310, 311, 312,
            320, 321, 322,
            400,
            410, 411, 412,
            420, 421, 422,
            500,
            510, 511, 512,
            520, 521, 522,
            900,
            910
        };
        private static readonly Dictionary<int, int> playerExpSet = new Dictionary<int, int>()
        {
            [1] = 15, [2] = 34, [3] = 57, [4] = 92, [5] = 135, [6] = 372, [7] = 560, [8] = 840, [9] = 1242, [10] = 1716,

            [11] = 2360, [12] = 3216, [13] = 4200, [14] = 5460, [15] = 7050, [16] = 8840, [17] = 11040, [18] = 13716, [19] = 16680, [20] = 20216,

            [21] = 24402, [22] = 28980, [23] = 34320, [24] = 40512, [25] = 47216, [26] = 54900, [27] = 63666, [28] = 73080, [29] = 83720, [30] = 95700,

            [31] = 108480, [32] = 122760, [33] = 138666, [34] = 155540, [35] = 174216, [36] = 194832, [37] = 216600, [38] = 240500, [39] = 266682, [40] = 294216,

            [41] = 324240, [42] = 356916, [43] = 391160, [44] = 428280, [45] = 468450, [46] = 510420, [47] = 555680, [48] = 604416, [49] = 655200, [50] = 709716,

            [51] = 748608, [52] = 789631, [53] = 832902, [54] = 878545, [55] = 926689, [56] = 977471, [57] = 1031036, [58] = 1087536, [59] = 1147132, [60] = 1209994,

            [61] = 1276301, [62] = 1346242, [63] = 1420016, [64] = 1497832, [65] = 1579913, [66] = 1666492, [67] = 1757815, [68] = 1854143, [69] = 1955750, [70] = 2062925,

            [71] = 2175973, [72] = 2295216, [73] = 2420993, [74] = 2553663, [75] = 2693603, [76] = 2841212, [77] = 2996910, [78] = 3161140, [79] = 3334370, [80] = 3517093,

            [81] = 3709829, [82] = 3913127, [83] = 4127566, [84] = 4353756, [85] = 4592341, [86] = 4844001, [87] = 5109452, [88] = 5389449, [89] = 5684790, [90] = 5996316,

            [91] = 6324914, [92] = 6671519, [93] = 7037118, [94] = 7422752, [95] = 7829518, [96] = 8258575, [97] = 8711144, [98] = 9188514, [99] = 9692044, [100] = 10223168,

            [101] = 10783397, [102] = 11374327, [103] = 11997640, [104] = 12655110, [105] = 13348610, [106] = 14080113, [107] = 14851703, [108] = 15665576, [109] = 16524049, [110] = 17429566,

            [111] = 18384706, [112] = 19392187, [113] = 20454878, [114] = 21575805, [115] = 22758159, [116] = 24005306, [117] = 25320796, [118] = 26708375, [119] = 28171993, [120] = 29715818,

            [121] = 31344244, [122] = 33061908, [123] = 34873700, [124] = 36784778, [125] = 38800583, [126] = 40926854, [127] = 43169645, [128] = 45535341, [129] = 48030677, [130] = 50662758,

            [131] = 53439077, [132] = 56367538, [133] = 59456479, [134] = 62714694, [135] = 66151459, [136] = 69776558, [137] = 73600313, [138] = 77633610, [139] = 81887931, [140] = 86375389,

            [141] = 91108760, [142] = 96101520, [143] = 101367883, [144] = 106922842, [145] = 112782213, [146] = 118962678, [147] = 125481832, [148] = 132358236, [149] = 139611467, [150] = 147262175,

            [151] = 155332142, [152] = 163844343, [153] = 172823012, [154] = 182293713, [155] = 192283408, [156] = 202820538, [157] = 213935103, [158] = 225658746, [159] = 238024845, [160] = 251068606,

            [161] = 264827165, [162] = 279339693, [163] = 294647508, [164] = 310794191, [165] = 327825712, [166] = 345790561, [167] = 364739883, [168] = 384727628, [169] = 405810702, [170] = 428049128,

            [171] = 451506220, [172] = 476248760, [173] = 502347192, [174] = 529875818, [175] = 558913012, [176] = 589541445, [177] = 621848316, [178] = 655925603, [179] = 691870326, [180] = 729784819,

            [181] = 769777027, [182] = 811960808, [183] = 856456260, [184] = 903390063, [185] = 952895838, [186] = 1005114529, [187] = 1060194805, [188] = 1118293480, [189] = 1179575962, [190] = 1244216724,

            [191] = 1312399800, [192] = 1384319309, [193] = 1460180007, [194] = 1540197871, [195] = 1624600714, [196] = 1713628833, [197] = 1807535693, [198] = 1906588648, [199] = 2011069705, [200] = 0,
        };

        private static readonly Dictionary<int, int> mountExpSet = new Dictionary<int, int>()
        {
            [1] = 252, [2] = 370, [3] = 417, [4] = 433, [5] = 544, [6] = 617, [7] = 619, [8] = 622, [9] = 623, [10] = 631,
			
            [11] = 640, [12] = 737, [13] = 743, [14] = 789, [15] = 841, [16] = 898, [17] = 964, [18] = 980, [19] = 994, [20] = 1004,
			
            [21] = 1206, [22] = 1346, [23] = 1351, [24] = 1408, [25] = 1451, [26] = 1558, [27] = 1724, [28] = 1742, [29] = 1808, [30] = 1829,
			
            [31] = 1876, [32] = 1888, [33] = 1913, [34] = 2057, [35] = 2196, [36] = 2222, [37] = 2424, [38] = 2499, [39] = 2518, [40] = 2523,
			
            [41] = 2551, [42] = 2615, [43] = 2669, [44] = 2980, [45] = 3068, [46] = 3259, [47] = 3307, [48] = 3321, [49] = 3379, [50] = 3488,
			
            [51] = 3606, [52] = 3673, [53] = 3691, [54] = 4142, [55] = 4303, [56] = 4319, [57] = 4408, [58] = 4471, [59] = 4555, [60] = 4571,
			
            [61] = 4597, [62] = 4682, [63] = 4685, [64] = 4795, [65] = 4811, [66] = 4928, [67] = 5176, [68] = 5241, [69] = 5347, [70] = 5564,
			
            [71] = 5634, [72] = 5768, [73] = 5888, [74] = 5962, [75] = 6020, [76] = 6027, [77] = 6161, [78] = 6209, [79] = 6221, [80] = 6282,
			
            [81] = 6801, [82] = 6804, [83] = 7149, [84] = 7152, [85] = 7398, [86] = 7441, [87] = 7879, [88] = 7946, [89] = 7948, [90] = 8420,
			
            [91] = 8599, [92] = 8980, [93] = 8990, [94] = 9199, [95] = 9497, [96] = 9649, [97] = 9683, [98] = 9959, [99] = 9985, [100] = 10022,
			
            [101] = 10045, [102] = 10063, [103] = 10177, [104] = 10255, [105] = 10264, [106] = 10450, [107] = 10522, [108] = 10676, [109] = 10781, [110] = 10799,
			
            [111] = 11010, [112] = 11133, [113] = 11184, [114] = 11218, [115] = 11439, [116] = 11524, [117] = 11683, [118] = 12061, [119] = 12491, [120] = 12979,
			
            [121] = 13079, [122] = 14250, [123] = 14316, [124] = 14393, [125] = 14612, [126] = 14676, [127] = 15130, [128] = 15140, [129] = 16159, [130] = 16507,
			
            [131] = 16550, [132] = 17512, [133] = 17862, [134] = 17887, [135] = 18037, [136] = 18417, [137] = 18823, [138] = 18864, [139] = 20285, [140] = 20352,
			
            [141] = 20437, [142] = 21179, [143] = 21362, [144] = 21389, [145] = 21749, [146] = 21764, [147] = 22144, [148] = 23039, [149] = 23190, [150] = 23200,
			
            [151] = 23311, [152] = 23368, [153] = 23609, [154] = 23759, [155] = 24052, [156] = 24322, [157] = 25483, [158] = 26272, [159] = 26575, [160] = 26590,
			
            [161] = 27564, [162] = 27637, [163] = 27892, [164] = 28422, [165] = 28640, [166] = 29972, [167] = 30485, [168] = 31615, [169] = 31617, [170] = 31628,
			
            [171] = 32487, [172] = 32905, [173] = 35709, [174] = 36162, [175] = 36483, [176] = 37186, [177] = 37453, [178] = 37673, [179] = 38222, [180] = 39126,
			
            [181] = 39264, [182] = 40093, [183] = 40177, [184] = 40420, [185] = 40864, [186] = 42023, [187] = 43453, [188] = 44710, [189] = 48268, [190] = 48770,
			
            [191] = 49752, [192] = 50679, [193] = 50796, [194] = 52183, [195] = 52406, [196] = 53624, [197] = 54617, [198] = 56740, [199] = 57109, [200] = 57132,
			
            [201] = 57240, [202] = 57757, [203] = 57833, [204] = 62804, [205] = 63431, [206] = 63719, [207] = 64254, [208] = 64278, [209] = 66347, [210] = 67382,
			
            [211] = 70325, [212] = 72300, [213] = 73741, [214] = 76596, [215] = 76653, [216] = 79656, [217] = 79956, [218] = 80138, [219] = 80160, [220] = 81236,
			
            [221] = 81434, [222] = 83508, [223] = 86305, [224] = 86459, [225] = 87736, [226] = 89933, [227] = 92187, [228] = 94505, [229] = 96207, [230] = 96247,
			
            [231] = 98054, [232] = 100790, [233] = 102809, [234] = 102865, [235] = 103748, [236] = 106335, [237] = 106914, [238] = 109531, [239] = 109982, [240] = 110901,
			
            [241] = 114879, [242] = 115560, [243] = 117519, [244] = 118247, [245] = 119212, [246] = 120502, [247] = 122509, [248] = 123097, [249] = 125179, [250] = 125547,
			
            [251] = 126448, [252] = 128355, [253] = 128991, [254] = 131784, [255] = 133121, [256] = 137875, [257] = 138289, [258] = 141749, [259] = 143593, [260] = 144865,
			
            [261] = 151600, [262] = 153454, [263] = 154826, [264] = 155007, [265] = 159788, [266] = 163238, [267] = 163680, [268] = 164197, [269] = 164966, [270] = 166681,
			
            [271] = 170107, [272] = 172338, [273] = 178074, [274] = 181282, [275] = 182533, [276] = 182986, [277] = 186463, [278] = 190931, [279] = 192140, [280] = 197125,
			
            [281] = 197326, [282] = 199169, [283] = 203612, [284] = 203682, [285] = 204996, [286] = 205051, [287] = 206189, [288] = 207168, [289] = 209274, [290] = 209387,
			
            [291] = 210559, [292] = 213880, [293] = 214396, [294] = 217097, [295] = 219340, [296] = 222602, [297] = 222935, [298] = 224811, [299] = 225140, [300] = 226999,
			
            [301] = 227595, [302] = 227921, [303] = 231151, [304] = 236203, [305] = 236601, [306] = 241050, [307] = 250127, [308] = 259346, [309] = 269631, [310] = 273961,
			
            [311] = 277043, [312] = 280095, [313] = 282504, [314] = 282856, [315] = 283680, [316] = 288671, [317] = 293271, [318] = 293710, [319] = 301303, [320] = 304577,
			
            [321] = 307913, [322] = 309506, [323] = 311866, [324] = 318758, [325] = 319825, [326] = 321783, [327] = 330355, [328] = 334657, [329] = 337211, [330] = 339121,
			
            [331] = 339623, [332] = 342194, [333] = 343922, [334] = 355059, [335] = 356351, [336] = 358273, [337] = 359461, [338] = 359584, [339] = 359916, [340] = 360265,
			
            [341] = 366792, [342] = 367961, [343] = 367964, [344] = 371093, [345] = 371638, [346] = 372218, [347] = 373882, [348] = 380597, [349] = 381233, [350] = 390848,
			
            [351] = 392235, [352] = 392492, [353] = 393392, [354] = 404005, [355] = 408695, [356] = 408809, [357] = 410512, [358] = 413370, [359] = 413670, [360] = 415278,
			
            [361] = 417764, [362] = 425503, [363] = 426844, [364] = 428484, [365] = 430046, [366] = 436923, [367] = 439783, [368] = 441728, [369] = 446020, [370] = 446436,
			
            [371] = 465110, [372] = 483245, [373] = 500469, [374] = 511917, [375] = 523722, [376] = 526784, [377] = 540779, [378] = 546092, [379] = 547699, [380] = 548839,
			
            [381] = 564524, [382] = 577068, [383] = 580162, [384] = 594312, [385] = 596659, [386] = 598232, [387] = 603619, [388] = 610302, [389] = 610835, [390] = 611282,
			
            [391] = 616956, [392] = 620721, [393] = 621499, [394] = 634875, [395] = 635804, [396] = 655989, [397] = 664048, [398] = 670689, [399] = 681669, [400] = 683317,
			
            [401] = 696548, [402] = 697308, [403] = 718805, [404] = 720008, [405] = 720635, [406] = 724120, [407] = 740956, [408] = 761978, [409] = 767910, [410] = 770539,
			
            [411] = 774652, [412] = 789641, [413] = 825857, [414] = 834096, [415] = 837322, [416] = 853637, [417] = 854945, [418] = 871497, [419] = 885084, [420] = 892406,
			
            [421] = 896410, [422] = 900166, [423] = 902551, [424] = 902795, [425] = 928389, [426] = 947190, [427] = 948222, [428] = 963313, [429] = 1011997, [430] = 1017408,
			
            [431] = 1017638, [432] = 1022981, [433] = 1029202, [434] = 1085131, [435] = 1089642, [436] = 1091107, [437] = 1106175, [438] = 1106534, [439] = 1119558, [440] = 1131386,
            
			[441] = 1131440, [442] = 1138121, [443] = 1138153, [444] = 1159551, [445] = 1165266, [446] = 1208769, [447] = 1220216, [448] = 1221990, [449] = 1270800, [450] = 1279913,
            
			[451] = 1296082, [452] = 1310144, [453] = 1311133, [454] = 1319368, [455] = 1320988, [456] = 1325471, [457] = 1346776, [458] = 1348063, [459] = 1355290, [460] = 1365475,
            
			[461] = 1370547, [462] = 1378195, [463] = 1387804, [464] = 1397611, [465] = 1410910, [466] = 1429798, [467] = 1454975, [468] = 1502104, [469] = 1506963, [470] = 1535271,
            
			[471] = 1549822, [472] = 1557911, [473] = 1567061, [474] = 1568967, [475] = 1582135, [476] = 1601839, [477] = 1615974, [478] = 1625186, [479] = 1632360, [480] = 1635816,
            
			[481] = 1641628, [482] = 1645290, [483] = 1653427, [484] = 1662521, [485] = 1676607, [486] = 1686287, [487] = 1688205, [488] = 1732674, [489] = 1765865, [490] = 1812988,
            
			[491] = 1819748, [492] = 1847890, [493] = 1852698, [494] = 1876517, [495] = 1945319, [496] = 2027149, [497] = 2047626, [498] = 2052520, [499] = 2097570, [500] = 2109777,
            
			[501] = 2170516, [502] = 2189105, [503] = 2214669, [504] = 2246634, [505] = 2275963, [506] = 2301104, [507] = 2351735, [508] = 2357485, [509] = 2363623, [510] = 2409888,
            
			[511] = 2435261, [512] = 2440177, [513] = 2515412, [514] = 2541394, [515] = 2583012, [516] = 2616703, [517] = 2648636, [518] = 2655239, [519] = 2657878, [520] = 2695594,
            
			[521] = 2713534, [522] = 2741472, [523] = 2781697, [524] = 2805123, [525] = 2813010, [526] = 2828003, [527] = 2848650, [528] = 2853425, [529] = 2963494, [530] = 2966400,
            
			[531] = 2998385, [532] = 3028177, [533] = 3029675, [534] = 3054810, [535] = 3081883, [536] = 3085999, [537] = 3122330, [538] = 3126895, [539] = 3135867, [540] = 3160866,
            
			[541] = 3176509, [542] = 3196393, [543] = 3200787, [544] = 3210912, [545] = 3267719, [546] = 3305622, [547] = 3305890, [548] = 3318203, [549] = 3327530, [550] = 3577334,
            
			[551] = 3651240, [552] = 3658255, [553] = 3764010, [554] = 3777915, [555] = 3792054, [556] = 3849240, [557] = 3861667, [558] = 3866646, [559] = 3882512, [560] = 3918337,
            
			[561] = 3942921, [562] = 3953996, [563] = 3969346, [564] = 4026682, [565] = 4039875, [566] = 4094694, [567] = 4104638, [568] = 4205233, [569] = 4228961, [570] = 4239590,
            
			[571] = 4264113, [572] = 4305724, [573] = 4418278, [574] = 4534622, [575] = 4564494, [576] = 4566887, [577] = 4580117, [578] = 4692978, [579] = 4880155, [580] = 4911680,
            
			[581] = 4926771, [582] = 4931960, [583] = 4958109, [584] = 4959106, [585] = 5008886, [586] = 5028592, [587] = 5035653, [588] = 5060004, [589] = 5105781, [590] = 5146950,
            
			[591] = 5166341, [592] = 5182892, [593] = 5287140, [594] = 5315853, [595] = 5403642, [596] = 5445191, [597] = 5445538, [598] = 5447234, [599] = 5477691, [600] = 5510831,
            
			[601] = 5561545, [602] = 5738713, [603] = 5822944, [604] = 5824521, [605] = 5955911, [606] = 6053524, [607] = 6093272, [608] = 6131800, [609] = 6198660, [610] = 6205185,
            
			[611] = 6290064, [612] = 6429332, [613] = 6571576, [614] = 6591452, [615] = 6656739, [616] = 6704454, [617] = 6815996, [618] = 6863462, [619] = 6974454, [620] = 6992406,
            
			[621] = 7082729, [622] = 7113441, [623] = 7362783, [624] = 7385364, [625] = 7527079, [626] = 7578519, [627] = 7599691, [628] = 7673676, [629] = 7742712, [630] = 7856500,
            
			[631] = 7984683, [632] = 8100218, [633] = 8117613, [634] = 8180602, [635] = 8226154, [636] = 8281144, [637] = 8376110, [638] = 8392851, [639] = 8632051, [640] = 8721750,
            
