using TMPro;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public int bankBalance = 100;
    public int energy = 100;
    public int workIncome = 100;
    public int workEnergy = 80;
    public int pastureChickenCost = 21;
    public int grainChickenCost = 12;
    public int ingredientQuantity;
    public int recipeQuantity;
    public int recipeQuantityTest;
    public int recipeEnergy;
    public int recipeCostLow;
    public int recipeCostHigh;
    public int houseValue;


    public TextMeshProUGUI recipeQuantityText;
    public TextMeshProUGUI chickenNeededText;
    public TextMeshProUGUI chickenNeededTextUI;
    public TextMeshProUGUI bankBalanceUI;
    public TextMeshProUGUI bankBalanceApp;
    public TextMeshProUGUI energyUI;
    public TextMeshProUGUI recipeCostLowText;
    public TextMeshProUGUI recipeCostHighText;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //recipeEnergy = recipeEnergy * recipeQuantity;
        recipeQuantityText.text = recipeQuantityTest.ToString();
        chickenNeededText.text = recipeQuantityText.text;
        chickenNeededTextUI.text = chickenNeededText.text;
        bankBalanceApp.text = bankBalance.ToString();
        bankBalanceUI.text = bankBalance.ToString();
        energyUI.text = energy.ToString();

        recipeCostLowText.text = (recipeQuantityTest * grainChickenCost).ToString();
        recipeCostHighText.text = (recipeQuantityTest * pastureChickenCost).ToString();

    }

    public void AddRecipeQuantity()
    {
        recipeQuantityTest = recipeQuantityTest + 1;
    }
    public void MinusRecipeQuantity()
    {
        recipeQuantityTest = recipeQuantityTest - 1;
    }

   public void ChooseRecipe()
    {
        recipeQuantity = recipeQuantityTest;
        recipeQuantityTest = 0;
    }
    
    public void CookAndEat()
    {
        if(ingredientQuantity > 0)
        {
            ingredientQuantity = ingredientQuantity - 1;
            energy = energy + 50;
        }
    }

    public void GoToJob()
    {
        if(energy > 80)
        {
            bankBalance = bankBalance + workIncome;
            energy = energy - workEnergy;
        }
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}
