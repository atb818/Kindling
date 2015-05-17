using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fading : MonoBehaviour {
	
	public Text titleText;
	public float fadeSpeed = 5f;
	public bool entrance;
	public GameObject titleObject;
	
	void Awake () 
		
	{
		
		titleText = titleObject.GetComponentInChildren<Text> ();
		titleText.color = Color.clear;
		
	}
	
	void Update () 
		
	{
		ColorChange();
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			entrance = true;
		} 
		
	}
	
	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			entrance = false;                       
			
		} 
		
	}
	
	void ColorChange () 
		
	{
		
		if (entrance)
		{
			titleText.color = Color.Lerp (titleText.color, Color.white, fadeSpeed * Time.deltaTime);
			
		}
		
		if (!entrance)
		{
			titleText.color = Color.Lerp (titleText.color, Color.clear, fadeSpeed * Time.deltaTime);
		}
		
	}
}