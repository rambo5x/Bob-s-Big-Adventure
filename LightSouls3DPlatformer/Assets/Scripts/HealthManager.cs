using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currentHealth, maxHealth;

    public float invincibleLength = 2f;
    private float invincCounter;

    public Sprite[] healthbarImages; //collect non inputted sprites 

    public int soundToPlay;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0) //multiply counter to be bigger and convert to whole number and check if its even. Creates a flashing effect
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }

                if(invincCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt()
    {
        if (invincCounter <= 0) //check if invincibility timer is still going
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.KnockBack();
                invincCounter = invincibleLength;
                AudioManager.instance.PlaySFX(soundToPlay);

            }

            UpdateUI();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        UIManager.instance.healthImage.enabled = true;

        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        
        currentHealth += amountToHeal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();

        switch (currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthbarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthbarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthbarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthbarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthbarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }

    public void PlayerKilled()
    {
        currentHealth = 0;
        UpdateUI();
    }
}
