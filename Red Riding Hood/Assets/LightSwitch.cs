using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
   private bool light;
   public GameObject _gameObject;
   
   private void Update()
   {
	   CheckInput();
   }
   
   private void CheckInput()
   {
	   if(Input.GetKeyDown(KeyCode.F))
	   {
		   OnKeyF();
	   }
   }
   
   private void OnKeyF()
   {
	   light = ! light;
	   if(light)
	   {
		   TurnOffLight();
	   }
	   else
	   {
		   TurnOnLight();
	   }
   }


	void TurnOnLight()
	{
		_gameObject.SetActive(true);
	}


	void TurnOffLight()
	{
		_gameObject.SetActive(false);
	}
}
 