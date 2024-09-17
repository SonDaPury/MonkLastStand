using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isPlayerInDoorRange = false;
    public bool isDoorOpened = false;
    public int doorId;
    public float openTime = 2f;
    public float openHeight = 5f;
    public Vector3 closedPosition;
    public Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);
        doorId = gameObject.GetInstanceID();
    }

    public void OpenDoor()
    {
        if (!isDoorOpened)
        {
            isDoorOpened = true;
            foreach (GameObject key in PlayerInventory.Instance.keys)
            {
                if (KeyDoorManager.Instance.CompareKeyDoor(key, this))
                {
                    PlayerInventory.Instance.keys.Remove(key);
                    StartCoroutine(MoveDoor(closedPosition, openPosition));
                    break;
                }
            }
        }
    }

    public IEnumerator MoveDoor(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < openTime)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / openTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        isPlayerInDoorRange = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isPlayerInDoorRange = false;
    }
}
