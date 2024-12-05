using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class GameData
{
    public string name;

    public int nowStg;

    public bool puzzle1Clear = false;
    public bool puzzle2Clear = false;
    public bool puzzle3Clear = false;

    public static bool puzzle1_1 = false;
    public static bool puzzle1_2 = false;
    public static bool puzzle1_3 = false;
    public static bool puzzle1_4 = false;


    //퍼즐2~5 삭제 예정, puzzle1Clear~puzzle3Clear 대신 사용하기
    public static bool puzzle2_1 = false;
    public static bool puzzle2_2 = false;
    public static bool puzzle2_3 = false;

    public bool puzzle3_1 = false;
    public bool puzzle3_2 = false;
    public bool puzzle3_3 = false;

    public bool puzzle4_1 = false;
    public bool puzzle4_2 = false;
    public bool puzzle4_3 = false;

    public bool puzzle5_1 = false;
    public bool puzzle5_2 = false;
    public bool puzzle5_3 = false;

    //엔딩용 아이템
    public bool _scissors = false;
    public bool _white = false;
    public bool _awl = false;
    public bool _hammer = false;
}