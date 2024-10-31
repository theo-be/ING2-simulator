using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	private Control pauseMenuContainer;
	private Button resume_button;
	private Button mainmenu_button;
	private Button quit_button;

	[Export] private PackedScene startmenu;

	public override void _Ready()
	{
		pauseMenuContainer = GetNode<Control>("PauseMenuContainer");
		pauseMenuContainer.Hide();  // Cache le menu au démarrage

		InitializeButtons();
		ConnectSignals();
	}

	private void InitializeButtons()
	{
		resume_button = GetNode<Button>("PauseMenuContainer/MarginContainer/VBoxContainer/ResumeButton");
		mainmenu_button = GetNode<Button>("PauseMenuContainer/MarginContainer/VBoxContainer/MainMenuButton");
		quit_button = GetNode<Button>("PauseMenuContainer/MarginContainer/VBoxContainer/QuitButton");
	}

	private void ConnectSignals()
	{
		resume_button.Connect("pressed", new Callable(this, nameof(OnResumePressed)));
		mainmenu_button.Connect("pressed", new Callable(this, nameof(OnMainMenuPressed)));
		quit_button.Connect("pressed", new Callable(this, nameof(OnQuitPressed)));
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel")) 
		{
			TogglePauseMenu();
		}
	}

	private void TogglePauseMenu()
	{
		bool isPaused = !pauseMenuContainer.Visible;
		pauseMenuContainer.Visible = isPaused;
		GetTree().Paused = isPaused;
	}

	private void OnResumePressed()
	{
		TogglePauseMenu();
	}

	private void OnMainMenuPressed()
	{
		if (startmenu == null) return;
		
		GetTree().Paused = false;
		GetTree().ChangeSceneToPacked(startmenu);
	}

	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
