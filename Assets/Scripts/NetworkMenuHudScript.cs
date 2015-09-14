using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NetworkMenuHudScript : NetworkBehaviour {

	public Button startServerButton;
	public Button joinServerButton;
	public Button killServerButton;

	public Button connectedButton;

	public InputField ipInputField;
	
	public ARGameNetworkManager networkManager;

	void Start () 
	{
		killServerButton.gameObject.SetActive (false);
		ipInputField.text = networkManager.ip;

		startServerButton.onClick.AddListener(() => { 
			networkManager.autoCreatePlayer = false;

			if (connectedButton.image.color == Color.green) 
			{
				if (networkManager.StartHost() != null) 
				{
					print ("Started server");
					FlipMenu();
				}
			}
		});

		joinServerButton.onClick.AddListener(() => { 
			networkManager.autoCreatePlayer = true;

			networkManager.networkAddress = ipInputField.text;
			NetworkClient networkClient = networkManager.StartClient();
			killServerButton.GetComponent<Text>().text = "Leave server";
			print ("Joined server: " + networkManager.ip);
			FlipMenu();
		});

		killServerButton.onClick.AddListener (() => {
			networkManager.StopHost();
			print ("Leave server");
			FlipMenu ();
		});
	}

	void FlipMenu()
	{
		bool flip = startServerButton.IsActive();

		startServerButton.gameObject.SetActive(!flip);
		joinServerButton.gameObject.SetActive(!flip);
		ipInputField.gameObject.SetActive(!flip);

		killServerButton.gameObject.SetActive (flip);
	}
}