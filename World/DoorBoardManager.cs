using UnityEngine;

public class DoorBoardManager : MonoBehaviour
{
    public int totalBoards;
    int removedBoards = 0;

    public PlayerInventory inventory;

    public void OnBoardRemoved()
    {
        removedBoards++;

        if (removedBoards >= totalBoards)
        {
            inventory.ConsumePipe(); // pipa rusak
            Debug.Log("All boards removed. Pipe broken.");
        }
    }
}
