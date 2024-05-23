using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public NPCManager npcManager;
    public GameStateManager gameStateManager;
    private CharacterController characterController;
    private Vector3 movement;

    public float range = 2f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.TransformDirection(new Vector3(horizontal, 0f, vertical));
        movement = moveSpeed * Time.deltaTime * move;

        characterController.Move(movement);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("NPC"))
                {
                    Entity_Dialog.Param npcParam = npcManager.GetParamData(collider.GetComponent<NPCActor>().npcNumber, gameStateManager.gameState);

                    if (npcParam != null)
                    {
                        Debug.Log($"{npcParam.dialog}");

                        if (npcParam.changestate > 0)
                        {
                            gameStateManager.gameState = npcParam.changestate;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("해당하는 데이터가 없습니다.");
                    }
                }
            }
        }
    }
}
