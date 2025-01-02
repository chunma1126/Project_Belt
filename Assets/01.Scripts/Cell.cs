using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Cell
{
    private Image image;
    private bool isOn;
    public Cell(Image _image, bool _isOn)
    {
        image = _image;
        isOn = _isOn;

        ChangeState(isOn);
    }
    
    public void ChangeState(bool _isOn)
    {
        isOn = _isOn;
        
        if (isOn)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.green;
        }


    }
    
    
}