extends Node2D


var peer =ENetMultiplayerPeer.new()
@export var player_scene: PackedScene
var player_count=0

func _on_host_pressed() -> void:
	if player_count<1:
		peer.create_server(135,4)
		multiplayer.multiplayer_peer = peer
		multiplayer.peer_connected.connect(_add_player)
		_add_player()
		
func _add_player(id=1):
	if player_count<3:
		player_count+=1
		var player = player_scene.instantiate()
		player.name = str(id)
		call_deferred("add_child",player)
		player.set("player_id", player_count)
		player.position = Vector2(0,95+(70 * player_count))
		
func _on_join_pressed() -> void:
	player_count+=1
	peer.create_client("localhost",135)
	multiplayer.multiplayer_peer = peer
		
func _on_add_player_pressed() -> void:
	if player_count<3:
		player_count+=1
		var player = player_scene.instantiate()
		player.set("player_id", player_count)
		print(player_count)
		player.position = Vector2(0,110+(70 * player_count))
		add_child(player)
