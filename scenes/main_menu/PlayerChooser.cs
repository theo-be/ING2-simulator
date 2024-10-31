using Godot;
using System;

public partial class PlayerChooser : Control
{
	private Button startButton;
	private Button quitButton;
	private Button _1PlayerButton;
	private Button _2PlayersButton;
	private Button _3PlayersButton;

	[Export]
	public PackedScene StartLevel { get; set; }

	[Export]
	public PackedScene StartMenu { get; set; }

	[Export]
	public PackedScene sceneToAdd; // Scène à ajouter

	[Export]
	public NodePath scenesContainerPath; // Chemin vers le conteneur des scènes

	private Node scenesContainer; // Nœud où les scènes seront ajoutées

	public override void _Ready()
	{
		InitializeButtons();
		ConnectSignals();

	
		scenesContainer = GetNode<Node>(scenesContainerPath);
		if (scenesContainer == null)
		{
			GD.PrintErr("Scenes container not found!");
		}
	}

	private void InitializeButtons()
	{
		startButton = GetNode<Button>("MarginContainer/Start_Button");
		quitButton = GetNode<Button>("MarginContainer/VBoxContainer/Quit_Button");
		_1PlayerButton = GetNode<Button>("MarginContainer/VBoxContainer/1Player");
		_2PlayersButton = GetNode<Button>("MarginContainer/VBoxContainer/2Players");
		_3PlayersButton = GetNode<Button>("MarginContainer/VBoxContainer/3Players");

		if (startButton == null)
			GD.PrintErr("Start button not found!");
		if (quitButton == null)
			GD.PrintErr("Quit button not found!");
	}

	private void ConnectSignals()
	{
		if (startButton != null)
		{
			startButton.Pressed += OnStartPressed;
			GD.Print("Start button connected.");
		}
		if (quitButton != null)
		{
			quitButton.Pressed += OnQuitPressed;
			GD.Print("Quit button connected.");
		}
		if (_1PlayerButton != null)
		{
			_1PlayerButton.Pressed += () => {
				GD.Print("1 Player button pressed.");
				AddScenes(1);
			};
		}
		if (_2PlayersButton != null)
		{
			_2PlayersButton.Pressed += () => {
				GD.Print("2 Players button pressed.");
				AddScenes(2);
			};
		}
		if (_3PlayersButton != null)
		{
			_3PlayersButton.Pressed += () => {
				GD.Print("3 Players button pressed.");
				AddScenes(3);
			};
		}
	}

	private void OnStartPressed()
	{
		if (StartLevel == null)
		{
			GD.PrintErr("Start level scene not assigned!");
			return;
		}
		GD.Print("Start button pressed");
		GetTree().ChangeSceneToPacked(StartLevel);
	}

	private void OnQuitPressed()
	{
		if (StartMenu == null)
		{
			GD.PrintErr("Start menu scene not assigned!");
			return;
		}
		GD.Print("Quit button pressed");
		GetTree().ChangeSceneToPacked(StartMenu);
	}

	private void AddScenes(int count)
	{
		GD.Print($"Adding {count} scenes to {scenesContainer?.Name}");

		
		if (scenesContainer == null)
		{
			GD.PrintErr("Scenes container is null!");
			return; 
		}

		
		GD.Print("Clearing existing scenes...");
		foreach (Node child in scenesContainer.GetChildren())
		{
			child.QueueFree();
		}

		
		for (int i = 0; i < count; i++)
		{
			if (sceneToAdd != null)
			{
				Node newSceneInstance = sceneToAdd.Instantiate();
				GD.Print($"Adding new scene instance: {newSceneInstance.Name}");
				scenesContainer.AddChild(newSceneInstance);
				GD.Print($"Scene added: {newSceneInstance.Name}");
			}
			else
			{
				GD.Print("No scene to add!");
				break;
			}
		}
	}
}
