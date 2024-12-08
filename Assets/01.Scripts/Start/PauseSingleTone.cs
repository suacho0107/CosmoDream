using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSingleTone : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 정적 변수
    public static PauseSingleTone pauseInstance { get; private set; }

    void Awake()
    {
        // 인스턴스가 이미 존재하면 자신을 파괴
        if (pauseInstance != null && pauseInstance != this)
        {
            Debug.Log($"중복된 싱글톤 발견: {gameObject.name}. 기존 오브젝트를 유지하고 자신을 파괴합니다.");
            Destroy(gameObject);
            return;
        }

        // 싱글톤 인스턴스 설정
        pauseInstance = this;

        // 씬 변경 시에도 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }
}
