using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFlipper : MonoBehaviour
{
    //store the card front and back sprites, located in the inspector
    public Sprite CardFront;
    public Sprite CardBack;

    public void Flip()
    {
        //when Flip() is called, store the value of the current sprite attached to this gameobject
        Sprite currentSprite = gameObject.GetComponent<Image>().sprite;

        //conditional logic to determine whether to display the card front or back sprite
        if (currentSprite == CardFront)
        {
            gameObject.GetComponent<Image>().sprite = CardBack;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = CardFront;
        }
    }
}
