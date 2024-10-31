using Godot;
using System;

public partial class MainMenu : Control
{
   private Button start_button;
   private Button quit_button;
   [Export] private PackedScene player_chooser;


   public override void _Ready()
   {
	   InitializeButtons();
	   ConnectSignals();
	   ValidateStartLevel();
   }

   private void InitializeButtons()
   {
	   start_button = GetNode<Button>("MarginContainer/HBoxContainer/VBoxContainer/Start_Button");
	   quit_button = GetNode<Button>("MarginContainer/HBoxContainer/VBoxContainer/Quit_Button");
   }

   private void ConnectSignals()
   {
	   if (start_button != null)
	   {
		   start_button.Connect("pressed", new Callable(this, nameof(OnStartPressed)));
	   }

	   if (quit_button != null)
	   {
		   quit_button.Connect("pressed", new Callable(this, nameof(OnQuitPressed)));
	   }
   }

   private void ValidateStartLevel()
   {
	   if (player_chooser == null)
	   {
		   player_chooser = GD.Load<PackedScene>("res://scenes/main_menu/player_chooser.tscn");
	   }
   }

   private void OnStartPressed()
   {
	   if (player_chooser == null)
	   {
		   return;
	   }
	   GetTree().ChangeSceneToPacked(player_chooser);
   }

   private void OnQuitPressed()
   {
	   GetTree().Quit();
   }
}
