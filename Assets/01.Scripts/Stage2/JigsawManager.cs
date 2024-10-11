using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JigsawManager : MonoBehaviour
{
    public bool isPuzzle1Complete = false;
    public bool isPuzzle2Complete = false;
    public bool isPuzzle3Complete = false;
    public bool isPuzzle4Complete = false;

    public bool stage2AllClear()
    {
        return isPuzzle1Complete && isPuzzle2Complete && isPuzzle3Complete;
    }

    public void CompletePuzzle(int puzzleNumber)
    {
        switch (puzzleNumber)
        {
            case 1:
                isPuzzle1Complete = true;
                break;
            case 2:
                isPuzzle2Complete = true;
                break;
            case 3:
                isPuzzle3Complete = true;
                break;
            case 4:
                {
                    isPuzzle4Complete = true;
                    SceneManager.LoadScene(5);
                    break;
                }
        }

        if (stage2AllClear())
        {
            GoNextStage();
        }
        else
        {
            ReturnStage();
        }
    }

    public void ReturnStage()
    {
        SceneManager.LoadScene(7);  // 스테이지 2로 돌아가기
    }

    public void GoNextStage()
    {
        SceneManager.LoadScene(11);  // 스테이지 3으로 이동
    }
}
