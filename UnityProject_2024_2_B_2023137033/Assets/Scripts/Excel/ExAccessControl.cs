using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAccessControl : MonoBehaviour
{
    /// <summary> ��� �ν��Ͻ����� ���ٰ��� </summary>
    public int publicValue;
    /// <summary> �ν��Ͻ� �������� ���ٰ��� </summary>
    private int privateValue;
    /// <summary> �ڱ� �ڽŰ� �ڽ� Ŭ������������ ���� ���� </summary>
    protected int protectedValue;
    /// <summary> ���� ����� ������ ���� ���� </summary>
    internal int internalValue;


}

public class ParentClass : MonoBehaviour
{
    protected int protectedValueParent;
}

public class ChildClass : ParentClass
{
    private void Start()
    {
        Debug.Log(protectedValueParent);
    }
}