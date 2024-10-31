using Godot;
using System;
using System.Collections.Generic;

public partial class SkinCarousel : HBoxContainer
{
	private List<string> skins = new List<string>();
	private int currentPage = 0;

	[Export] private TextureRect currentSkinDisplay;
	[Export] private Button nextButton;
	[Export] private Button prevButton;
	[Export] private Button selectButton;
	private AnimationPlayer animationPlayer;
	private LineEdit pseudoInput; // Ligne pour entrer le pseudo

	public override void _Ready()
	{
		// Initialize skins
		skins.Add("res://images/2915003117.webp");
		skins.Add("res://images/3898723029.webp");

		// Initialize display
		pseudoInput = GetNode<LineEdit>("Player_Catalog/PseudoInput");
		if (pseudoInput == null)
		{
			GD.PrintErr("PseudoInput not found!");
		}
		UpdateDisplay();

		// Connect button signals
		nextButton.Pressed += OnNextButtonPressed;
		prevButton.Pressed += OnPrevButtonPressed;
		selectButton.Pressed += OnSelectButtonPressed; 
		animationPlayer = GetNode<AnimationPlayer>("Player_Catalog/ColorRect/MarginContainer/SkinDisplay/AnimationPlayer"); 
	}

	private void UpdateDisplay()
	{
		if (skins.Count > 0)
		{
			int currentIndex = currentPage;
			if (currentIndex < skins.Count)
			{
				currentSkinDisplay.Texture = GD.Load<Texture2D>(skins[currentIndex]);
			}
		}

		int totalPages = skins.Count;

		prevButton.Disabled = currentPage <= 0;
		nextButton.Disabled = currentPage >= totalPages - 1;
	}

	private void OnPrevButtonPressed()
	{
		if (currentPage > 0)
		{
			currentPage--;
			UpdateDisplay();
		}
	}

	private void OnNextButtonPressed()
	{
		if (currentPage < skins.Count - 1)
		{
			currentPage++;
			UpdateDisplay();
		}
	}

	private void OnSelectButtonPressed()
	{
	

		if (animationPlayer != null)
		{
			animationPlayer.Play("SelectAnimation");
		}
		string pseudo = pseudoInput.Text;
		GD.Print("Pseudo entered: " + pseudo);
		ApplySelectedSkinToPlayer();
	}

	public string GetSelectedSkin()
	{
		int currentIndex = currentPage;
		return currentIndex < skins.Count ? skins[currentIndex] : "";
	}

	public void ApplySelectedSkinToPlayer()
	{
		GD.Print("Le skin appliqué est " + GetSelectedSkin());
	}
}
