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
        if (!isOpened)
        {
            isOpened = true;
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
