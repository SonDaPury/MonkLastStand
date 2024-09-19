using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int level = 1;
    public float exp = 0;
    public int expToLevelUp = 100;
    public int health = 100;
    public int stamina = 100;
    public int def = 5;
    public int attackDamage = 10;
    public int skillPoints = 0;
    public PlayerStamina playerStamina;
    public PlayerHealthBar playerHealthBar;
    private int healthPointsAllocated = 0;
    private int staminaPointsAllocated = 0;
    private int attackDamagePointsAllocated = 0;
    public int fireballDamage = 50;
    public int baseHealth = 100;
    public int baseStamina = 100;
    public int baseAttackPower = 10;
    public int baseDef = 5;

    public static PlayerStats Instance { get; private set; }

    void Awake()
    {
        playerStamina = FindAnyObjectByType<PlayerStamina>();
        playerHealthBar = FindAnyObjectByType<PlayerHealthBar>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
            //Debug.LogError("Another instance of PlayerStats already exists");
        }
    }

    private void Update()
    {
        if (exp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        exp -= expToLevelUp;
        expToLevelUp = CalculateExperienceToLevelUp();

        skillPoints += 2;
    }

    public int CalculateExperienceToLevelUp()
    {
        return (int)(100 * Mathf.Pow(1.5f, level - 1));
    }

    public void AddExperience(int amount)
    {
        exp += amount * Time.deltaTime;
    }

    public void AllocatePoints(string stat)
    {
        if (skillPoints > 0)
        {
            switch (stat)
            {
                case "health":
                    health += 10;
                    PlayerManager.Instance.currentHealth = health;
                    playerHealthBar.slider.value = health;
                    healthPointsAllocated++;
                    break;
                case "stamina":
                    stamina += 10;
                    staminaPointsAllocated++;
                    playerStamina.staminaSlider.value = stamina;
                    playerStamina.currentStamina = stamina;
                    break;
                case "attack":
                    attackDamage += 2;
                    attackDamagePointsAllocated++;
                    break;
                case "defense":
                    def += 1;
                    attackDamagePointsAllocated++;
                    break;
            }

            skillPoints--;
        }
    }

    public void ResetPoints()
    {
        // Reset các chỉ số về lại mức ban đầu
        health = baseHealth;
        stamina = baseStamina;
        attackDamage = baseAttackPower;
        def = baseDef;

        // Trả lại toàn bộ điểm cộng cho người chơi
        skillPoints += healthPointsAllocated + staminaPointsAllocated + attackDamagePointsAllocated;

        // Reset các điểm đã cộng cho từng chỉ số
        healthPointsAllocated = 0;
        staminaPointsAllocated = 0;
        attackDamagePointsAllocated = 0;

        PlayerManager.Instance.currentHealth = health;
        playerStamina.currentStamina = stamina;
        playerHealthBar.slider.value = health;
        playerStamina.staminaSlider.value = stamina;
    }

    public void IncreaseHealth()
    {
        AllocatePoints("health");
    }

    public void IncreaseStamina()
    {
        AllocatePoints("stamina");
    }

    public void IncreaseAttack()
    {
        AllocatePoints("attack");
    }

    public void IncreaseDefense()
    {
        AllocatePoints("defense");
    }
}
