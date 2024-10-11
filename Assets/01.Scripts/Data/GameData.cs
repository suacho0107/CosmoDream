using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class GameData
{
    public string name;

    public bool[] PuzzleProgress01 = new bool[4];
    public bool[] PuzzleProgress02 = new bool[3]; 
    public bool[] PuzzleProgress03 = new bool[3]; 
    public bool[] PuzzleProgress04 = new bool[3]; 
    public bool[] PuzzleProgress05 = new bool[3]; 
    public bool[] PuzzleProgress06 = new bool[4]; 

    //이 아래로 아이템 기록
    public GameData()
    {
        // 모든 배열의 값이 false로 기본 초기화됨
        InitializeProgress();
    }

    private void InitializeProgress()
    {
        name = "";
        for (int i = 0; i < PuzzleProgress01.Length; i++) PuzzleProgress01[i] = false;
        for (int i = 0; i < PuzzleProgress02.Length; i++) PuzzleProgress02[i] = false;
        for (int i = 0; i < PuzzleProgress03.Length; i++) PuzzleProgress03[i] = false;
        for (int i = 0; i < PuzzleProgress04.Length; i++) PuzzleProgress04[i] = false;
        for (int i = 0; i < PuzzleProgress05.Length; i++) PuzzleProgress05[i] = false;
        for (int i = 0; i < PuzzleProgress06.Length; i++) PuzzleProgress06[i] = false;
    }
}