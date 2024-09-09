using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Chest : MonoBehaviour
{
    public bool isOpened = false;
    public GameObject key;
    public GameObject door;
    public bool isPlayerInRange = false;
    public Animator animator;
    public Transform player;
    public Vector3 keyOffset = new Vector2(0, 2);

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        gameObject.SetActive(true);
        KeyDoorManager.Instance.AddKeyDoor(key, door);
    }

    private void Update()
    {
        key.transform.position = player.position - keyOffset;
    }

    public void OpenChest()
    {
        Debug.Log("Player is trying to open chest.");
        if (!isOpened)
        {
            isOpened = true;
            Debug.Log("Chest opened! Player received a key.");
            animator.SetTrigger("IsOpenChest");
            ShowKey();
            StartCoroutine(DelayInActiveChest());
        }
        else
        {
            Debug.Log("Chest is already opened.");
        }
    }

    private void ShowKey()
    {
        if (key != null && player != null)
        {
            key.transform.position = player.position - keyOffset;
            key.SetActive(true);
            PlayerInventory.Instance.keys.Add(key);
            Debug.Log("The key has appeared above the player!");
        }
    }

    private IEnumerator DelayInActiveChest()
    {
        yield return new WaitForSeconds(1.5f);
        key.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player is in range to open chest.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player is out of range from chest.");
        }
    }
}
