using System;
using TMPro;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _holaMovilText;
    private float _initialTime;


    void Update()
    {
        //Escape es la flecha hacia atrás en móvil
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                _initialTime = Time.realtimeSinceStartup;

                //Ended touch
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                float finalTime = Time.realtimeSinceStartup - _initialTime;
                _holaMovilText.text = Input.GetTouch(0).position.ToString() + "\n" + finalTime + " seg";
                _holaMovilText.color = Color.white;

            }

            //Moving touch
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                _holaMovilText.text = "Moving in " + Input.GetTouch(0).position.ToString();
                _holaMovilText.color = Color.blue;
            }


            //Stationary touch
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                _holaMovilText.text = "Stationary";
                _holaMovilText.color = Color.green;
            }
        }
        
    }
}
