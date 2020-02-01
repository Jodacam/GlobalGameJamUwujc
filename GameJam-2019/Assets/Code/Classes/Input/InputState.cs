

using UnityEngine;

public class InputState
{

    public InputState(string input){
        inputName = input;
    }
    private string inputName = "";


    public bool down;

    public bool press;

    public bool release;



    public void Process(){
        this.down = Input.GetButtonDown(inputName);
        this.press = Input.GetButton(inputName);
        this.release = Input.GetButtonUp(inputName);
    }

}