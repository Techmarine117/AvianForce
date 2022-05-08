using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public InputField nameText;
    
   
    
    public void UpdateText()
    {
        GetPlayerName.pname = nameText.text;
        Debug.Log(GetPlayerName.pname);
    }
    public void ChooseCharacter()
    {
        GetPlayerName.CharacterType = "Player";
    }
   
    public void PlayGame()
    {
        SceneManager.LoadScene("MovementTest");
    }

    public void Quit()
    {
        Application.Quit();
        
    }


    
}
