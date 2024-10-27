using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class GameData
{
    public string name;

    //맵 진행도를 enum으로 기록?
    //힘들어요... 걍 마지막 스테이지 씬만 기록해주세요 껐다켜도 눈치못챔


    //퍼즐 진행도
    public bool[] PuzzleProgress01 = new bool[4];
    public bool[] PuzzleProgress02 = new bool[3]; 
    public bool[] PuzzleProgress03 = new bool[3]; 
    public bool[] PuzzleProgress04 = new bool[3]; 
    public bool[] PuzzleProgress05 = new bool[3]; 
    public bool[] PuzzleProgress06 = new bool[4];

    //스테이지 별 퍼즐아이템 획득상태
    public bool[] PuzzleItem01 = new bool[4];
    public bool[] PuzzleItem02 = new bool[3];
    public bool[] PuzzleItem03 = new bool[3];
    public bool[] PuzzleItem04 = new bool[3];
    public bool[] PuzzleItem05 = new bool[3];
    public bool[] PuzzleItem06 = new bool[4];

    //엔딩용 아이템 배열(간이 인벤토리)
    public bool[] Item = new bool[4];
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

        for (int i = 0; i < PuzzleItem01.Length; i++) PuzzleItem01[i] = false;
        for (int i = 0; i < PuzzleItem02.Length; i++) PuzzleItem02[i] = false;
        for (int i = 0; i < PuzzleItem03.Length; i++) PuzzleItem03[i] = false;
        for (int i = 0; i < PuzzleItem04.Length; i++) PuzzleItem04[i] = false;
        for (int i = 0; i < PuzzleItem05.Length; i++) PuzzleItem05[i] = false;
        for (int i = 0; i < PuzzleItem06.Length; i++) PuzzleItem06[i] = false;

        for (int i = 0; i < Item.Length; i++) Item[i] = false;
    }
}