using UnityEngine;
using System.Collections;

public class NGUIButtonClick : MonoBehaviour {


    public void OnLabelButtonClick() {
        print("Label button be click!");
        Application.LoadLevel("B");
    }
    public void OnSpriteButtonClick() {
        print("Sprite button be click!");
    }

}
