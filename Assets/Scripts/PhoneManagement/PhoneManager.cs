using UnityEngine;


public class PhoneManager : MonoBehaviour
{
    public GameObject phoneCanvas;
    public GameObject mainMenu;
    public GameObject groceries;
    public GameObject kittyBank;
    public GameObject kitchenWarehouse;
    public GameObject renovationsWarehouse;
    public GameObject recipes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePhone()
    {
        if(phoneCanvas != null)
        {
            phoneCanvas.SetActive(!phoneCanvas.activeSelf);
        }
        if(mainMenu != null)
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
    public void OpenGroceries()
    {
        if (groceries != null)
        {
            groceries.SetActive(!groceries.activeSelf);
        }
        if (mainMenu != null)
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
    public void OpenKittyBank()
    {
        if (kittyBank != null)
        {
            kittyBank.SetActive(!kittyBank.activeSelf);
        }
        if (mainMenu != null)
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
    public void OpenKitchenWarehouse()
    {
        if (kitchenWarehouse != null)
        {
            kitchenWarehouse.SetActive(!kitchenWarehouse.activeSelf);
        }
        if (mainMenu != null)
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
    public void OpenRenovationsWarehouse()
    {
        if (renovationsWarehouse != null)
        {
            renovationsWarehouse.SetActive(!renovationsWarehouse.activeSelf);
        }
        if (mainMenu != null)
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
    public void OpenRecipes()
    {
        if (recipes != null)
        {
            recipes.SetActive(!recipes.activeSelf);
        }
        if (mainMenu != null)
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }

    public void OpenMainMenu()
    {
        if (mainMenu != null)
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
        if (groceries != null)
        {
            groceries.SetActive(false);
        }
        if (recipes != null)
        {
            recipes.SetActive(false);
        }
        if (kittyBank != null)
        {
            kittyBank.SetActive(false);
        }
        if (kitchenWarehouse != null)
        {
            kitchenWarehouse.SetActive(false);
        }
        if (renovationsWarehouse != null)
        {
            renovationsWarehouse.SetActive(false);
        }
        
    }
}
