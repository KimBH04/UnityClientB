using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAccessControl : MonoBehaviour
{
    /// <summary> 모든 인스턴스에서 접근가능 </summary>
    public int publicValue;
    /// <summary> 인스턴스 내에서만 접근가능 </summary>
    private int privateValue;
    /// <summary> 자기 자신과 자식 클래스내에서만 접근 가능 </summary>
    protected int protectedValue;
    /// <summary> 같은 어셈블리 내에서 접근 가능 </summary>
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