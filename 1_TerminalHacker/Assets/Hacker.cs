using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
	string[,] levelPasswords = { 
		{ "donkey", "books", "island" },
		{ "incognito", "insomnia", "atmospheare" },
		{ "greatness", "homeland", "satelite" }
	};

	string[] keywords = { "/menu", "/cls", "/help" };

	enum Screen { MainMenu, Password, Win };
	Screen currentScreen = Screen.MainMenu;

	string password;
	string anagram;

	// private variables are visible in the Unity editor but first we have to chose debug mode
	int level;


	// Use this for initialization
	void Start () {
		ShowMainMenu("Radek");
	}
	
	void ShowMainMenu(string name)
	{
		Terminal.ClearScreen();

		if (!string.IsNullOrEmpty(name))
			Terminal.WriteLine("Hello " + name);

		Terminal.WriteLine("What would you like to hack to?");
		Terminal.WriteLine("");
		Terminal.WriteLine("Press 0 for the local library");
		Terminal.WriteLine("Press 1 for the police station");
		Terminal.WriteLine("Press 2 for NASA");
		Terminal.WriteLine("");
		Terminal.WriteLine("Enter your selection: ");
	}

	void ShowMainMenu()
	{
		ShowMainMenu(null);
	}

	void OnUserInput(string input)
	{
		if (input.ToLower() == "/menu")
		{
			currentScreen = Screen.MainMenu;
			ShowMainMenu();
		}
		else if (input.ToLower() == "/cls")
		{
			Terminal.ClearScreen();
		}
		else if (input.ToLower() == "/help")
		{
			Terminal.WriteLine("Avaiable commands:");
			Terminal.WriteLine("/menu - goes back to main menu");
			Terminal.WriteLine("/cls - clears screen");
			Terminal.WriteLine("/help - shows help");
			Terminal.WriteLine("/quit - turns off terminal");
		}
		else if (input.ToLower() == "/cls")
		{
			Application.Quit();
			Terminal.WriteLine("If on browser - please close the tab");
		}
		else if(currentScreen == Screen.MainMenu)
		{
			int level;
			if (int.TryParse(input, out level) && level >= 0 && level <= 2)
			{
				StartLevel(level);
			}
		}
		else if(currentScreen == Screen.Password)
		{
			CheckPassword(input);
		}
	}

	void CheckPassword(string input)
	{
		if(input == password)
		{
			Terminal.WriteLine("Well done!");
			DisplayWinScreen();
		}
		else
		{
			Terminal.WriteLine("Wrong password, hint: " + anagram);
		}
	}

	void DisplayWinScreen()
	{
		currentScreen = Screen.Win;
		Terminal.ClearScreen();
		ShowLevelReward();
	}

	void ShowLevelReward()
	{
		switch(level)
		{
			case 0:
				Terminal.WriteLine(
					@"Have an apple.
			\/.--,
			//_.'
	   .-""-/""-.
	  /       __ \
	 /        \\\ \
	 |         || |
	 \            /
	 \  \         /
	  \  '-      /
	   '-.__.__.'     ");
				break;
			case 1:
				Terminal.WriteLine(
					@"Have a chocolate.
	___  ___  ___  ___.---------------.
  .'\__\'\__\'\__\'\__,`   .  ____ ___ \
  |\/ __\/ __\/ __\/ _:\   |`.  \  \___ \
   \\'\__\'\__\'\__\'\_`.__|""`. \  \___ \
	\\/ __\/ __\/ __\/ __:                \
	 \\'\__\'\__\'\__\'\_;-----------------`
	  \\/   \/   \/   \/ :               hh|
	   \|_________________;________________| ");
				break;
			case 2:
				Terminal.WriteLine("Have a book...");
				break;
		}
	}

	void StartLevel(int input)
	{
		level = input;
		currentScreen = Screen.Password;
		print("levelPasswords.GetUpperBound(1): " + levelPasswords.GetUpperBound(1));
		password = levelPasswords[input, UnityEngine.Random.Range(0, levelPasswords.GetUpperBound(1))];
		Terminal.WriteLine("hacking to LEVEL " + input + " started");

		anagram = password.Anagram();
		Terminal.WriteLine("please enter password, hint: " + anagram);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
