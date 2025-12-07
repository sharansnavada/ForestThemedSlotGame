using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PaylineData : MonoBehaviour
{
    List<string> threeMatch1 = new List<string> { "SCATTER", "SCATTER", "WILD", "L5_10", "H1_Bear", "H2_Deer", "BONUS", "H1_Bear", "L1_A", "H3_Fox", "H1_Bear", "H2_Deer", "L3_Q", "BONUS", "L1_A" }; // 5,6,7 (H1_Bear)
    List<string> threeMatch2 = new List<string> { "L5_10", "L5_10", "WILD", "H4_Wolf", "H3_Fox", "WILD", "H4_Wolf", "L1_A", "SCATTER", "H4_Wolf", "H2_Deer", "L5_10", "L2_K", "L3_Q", "L1_A" }; //1,2,3 (H4_Wolf)
    List<string> threeMatch3 = new List<string> { "L2_K", "L4_J", "H3_Fox", "L2_K", "H4_Wolf", "BONUS", "L3_Q", "H3_Fox", "L1_A", "L5_10", "BONUS", "H3_Fox", "H4_Wolf", "H3_Fox", "L1_A" }; //7,13,9 (H3_Fox)
    List<string> threeMatch4 = new List<string> { "H4_Wolf", "H3_Fox", "L5_10", "L4_J", "L2_K", "WILD", "L2_K", "H3_Fox", "L1_A", "L4_J", "L2_K", "WILD", "SCATTER", "H4_Wolf", "L1_A" }; //6,2,8 (L2_K)

    List<string> fourMatch1 = new List<string> { "L5_10", "H4_Wolf", "SCATTER", "L4_J", "WILD", "H1_Bear", "L4_J", "L1_A", "BONUS", "L4_J", "SCATTER", "L3_Q", "L4_J", "H1_Bear", "L1_A" }; // 1,2,3,4 (L4_J)
    List<string> fourMatch2 = new List<string> { "H4_Wolf", "L5_10", "SCATTER", "L3_Q", "L1_A", "H2_Deer", "L2_K", "H3_Fox", "H2_Deer", "L2_K", "SCATTER", "H2_Deer", "L4_J", "H2_Deer", "L1_A" }; // 11,12,13,9 (H2_Deer)
    List<string> fourMatch3 = new List<string> { "L5_10", "L5_10", "WILD", "H4_Wolf", "L1_A", "WILD", "H4_Wolf", "L1_A", "SCATTER", "H4_Wolf", "L1_A", "L5_10", "L1_A", "L3_Q", "L1_A" }; // 6,7,8,4 (L1_A)
    List<string> fourMatch4 = new List<string> { "H4_Wolf", "L5_10", "SCATTER", "L3_Q", "L3_Q", "H2_Deer", "L2_K", "H3_Fox", "L3_Q", "L2_K", "L3_Q", "H2_Deer", "L4_J", "L3_Q", "L1_A" }; // 6,12,8,9 (L3_Q)

    List<string> fiveMatch1 = new List<string> { "H4_Wolf", "WILD", "H2_Deer", "L4_J", "H2_Deer", "H2_Deer", "L4_J", "L1_A", "H2_Deer", "L4_J", "SCATTER", "H2_Deer", "L4_J", "H1_Bear", "H2_Deer" }; // 10,11,12,13,14 (H2_Deer)
    List<string> fiveMatch2 = new List<string> { "H4_Wolf", "L5_10", "SCATTER", "H4_Wolf", "L1_A", "H2_Deer", "L2_K", "H4_Wolf", "H2_Deer", "L2_K", "SCATTER", "H4_Wolf", "L4_J", "H2_Deer", "H4_Wolf" }; // 0,1,7,13,14 (H4_Wolf)
    List<string> fiveMatch3 = new List<string> { "L5_10", "L5_10", "L1_A", "H4_Wolf", "L1_A", "WILD", "H4_Wolf", "L1_A", "SCATTER", "H4_Wolf", "L1_A", "L5_10", "BONUS", "L3_Q", "L1_A" }; // 10,6,7,8,14 (L1_A)
    List<string> fiveMatch4 = new List<string> { "H2_Deer", "L5_10", "SCATTER", "H2_Deer", "L1_A", "H2_Deer", "L2_K", "H3_Fox", "H2_Deer", "H2_Deer", "SCATTER", "BONUS", "H2_Deer", "H1_Bear", "L1_A" }; // 0,1,12,3,4 (H2_Deer)

    List<string> threeWilMatch1 = new List<string> { "L5_10", "H4_Wolf", "WILD", "L1_A", "L4_J", "H1_Bear", "L4_J", "L1_A", "BONUS", "BONUS", "L4_J", "L3_Q", "BONUS", "H1_Bear", "L4_J" }; // 10,6,2,8,14 (L4_J)(WILD)
    List<string> threeWildMatch2 = new List<string> { "H4_Wolf", "H2_Deer", "SCATTER", "L3_Q", "L1_A", "H2_Deer", "L2_K", "WILD", "L1_A", "H2_Deer", "SCATTER", "H2_Deer", "L4_J", "H2_Deer", "L1_A" }; // 5,11,7,4,9 (H2_Deer)
    List<string> bonusMatch1 = new List<string> { "BONUS", "L5_10", "WILD", "BONUS", "L1_A", "WILD", "BONUS", "L1_A", "SCATTER", "H4_Wolf", "L1_A", "L5_10", "L1_A", "L3_Q", "L1_A" }; // 0,1,2(bonus)
    List<string> bonusMatch2 = new List<string> { "H4_Wolf", "L5_10", "SCATTER", "L3_Q", "L3_Q", "BONUS", "L2_K", "H3_Fox", "BONUS", "L2_K", "L3_Q", "BONUS", "L4_J", "L3_Q", "L1_A" }; // 11,12,13(bonus)

    public Dictionary<List<string>, float> lineWins = new Dictionary<List<string>, float>();

    public PaylineData()
    {
        lineWins[threeMatch1] = 1;
        lineWins[threeMatch2] = 0.8f;
        lineWins[threeMatch3] = 0.8f;
        lineWins[threeMatch4] = 0.3f;

        lineWins[fourMatch1] = 1.2f;
        lineWins[fourMatch2] = 6;
        lineWins[fourMatch3] = 1.2f;
        lineWins[fourMatch4] = 0.3f;

        lineWins[fiveMatch1] = 16;
        lineWins[fiveMatch2] = 12;
        lineWins[fiveMatch3] = 3;
        lineWins[fiveMatch4] = 16;

        lineWins[threeWilMatch1] = 0.3f;
        lineWins[threeWildMatch2] = 0.8f;
        lineWins[bonusMatch1] = 3;
        lineWins[bonusMatch2] = 3;
    }
}