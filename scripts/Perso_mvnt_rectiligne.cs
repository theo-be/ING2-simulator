using Godot;
using System;

//host join probleme le personnage qui join sur le meme ecran bouge pas sil rejoint apres le join

//voir camera mouvement suivre le 1er / voir si possible bouger la camera toute seule si prsonne bouge / voir si posssible un chemin predefini (path2D)

//voir les touches changer dynamiquement

public partial class Perso_mvnt_rectiligne : CharacterBody2D
{
	// Constantes pour la vitesse et la vélocité du saut
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450f;
	public const float Friction = 1000f;

	// Variables pour le double saut
	private int jumpCount = 0;
	private const int MaxJumps = 2;
	private float gravity = 1000f;

	// ID du joueur
	[Export]
	public int player_id = 1;

	// Multiplayer authority
	public override void _EnterTree()
	{
		// On définit l'autorité du joueur selon son nom ou ID.
		SetMultiplayerAuthority(int.Parse(Name));
	}

	public override void _PhysicsProcess(double delta)
	{
		// On vérifie si le joueur a l'autorité sur ce personnage avant de gérer ses mouvements.
		if (!IsMultiplayerAuthority())
		{
			return;
		}

		// Récupérer la vélocité actuelle
		Vector2 velocity = Velocity;

		// Ajouter la gravité si le joueur n'est pas au sol
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		// Réinitialiser le compteur de sauts si le joueur est au sol
		if (IsOnFloor())
		{
			jumpCount = 0; // On est au sol, donc on réinitialise les sauts
		}

		// Gérer le saut ou le double saut
		if (Input.IsActionJustPressed("ui_accept_" + player_id) && jumpCount < MaxJumps)
		{
			velocity.Y = JumpVelocity;
			jumpCount++; // Incrémente le nombre de sauts
		}

		// Récupérer la direction des entrées et gérer le mouvement/la décélération
		Vector2 direction = Vector2.Zero;
		if (Input.IsActionPressed("ui_left_" + player_id))
		{
			direction.X -= 1;
		}
		if (Input.IsActionPressed("ui_right_" + player_id))
		{
			direction.X += 1;
		}

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed ;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Friction * (float)delta);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	// Vous pouvez définir la méthode GetGravity() si elle n'est pas déjà définie.
	private float GetGravity()
	{
		return ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	}
}
