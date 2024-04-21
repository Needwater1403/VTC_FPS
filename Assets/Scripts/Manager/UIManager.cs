using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Title("Player UI")] 
    [SerializeField] private Slider healthSlider;
    private float lastHealth;
    [Title("Panel")] 
    [SerializeField] private GameObject YouDiedPanel;
    [SerializeField] private GameObject YouWinPanel;
    [Title("Text")] 
    public TextMeshProUGUI txt_coin;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.Player.GetCurrentHP() - lastHealth == 0) return;
        lastHealth = GameManager.Instance.Player.GetCurrentHP();
        UpdateHealthUI(GameManager.Instance.Player.GetMaxHP());
    }

    private void UpdateHealthUI(float maxHP)
    {
        var percentage = lastHealth / maxHP ;
        healthSlider.value = (percentage);
    }

    public void ShowLosePanel(bool show)
    {
        YouDiedPanel.SetActive(show);
    }
    
    public void ShowWinPanel(bool show)
    {
        YouWinPanel.SetActive(show);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
    
    public void OnClickReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
