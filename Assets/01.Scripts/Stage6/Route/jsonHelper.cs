using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class jsonHelper : MonoBehaviour
{
    public static T[] FromJson<T>(string json)
    {
        // JSON �����͸� �迭�� ����
        string wrappedJson = $"{{\"items\":{json}}}";
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
        return wrapper.items;
    }

    // �迭�� JSON ���ڿ��� ��ȯ�ϴ� �Լ�
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T> { items = array };
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    // ���� ���� �������� ��ȯ�ϴ� ToJson �Լ�
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T> { items = array };
        return UnityEngine.JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}
