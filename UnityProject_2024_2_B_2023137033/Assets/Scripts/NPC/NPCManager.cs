using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Entity_Dialog dialog;

    public void Start()
    {
        
    }

    public Entity_Dialog.Param GetParamData(int npc, int gameState)
    {
        foreach (Entity_Dialog.Param param in dialog.sheets[0].list)
        {
            if (param.npc == npc && param.gamestate == gameState)
            {
                return param;
            }
        }

        return null;
    }
}
