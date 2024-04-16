extends Node2D

export (int) var start_x;
export (int) var rows;
export (int) var columns;
export (int) var start_y;
export (int) var offset;

var all_pieces=[];

var possible_pieces=[
preload("res://scenes/Piece_blue.tscn"),
preload("res://scenes/Piece_green.tscn"),
preload("res://scenes/Piece_purple.tscn"),
preload("res://scenes/Piece_red.tscn"),
preload("res://scenes/Piece_yellow.tscn")
];
# Declare member variables here. Examples:
var first_touch = Vector2(0,0);
var final_touch= Vector2(0,0);
var controlling=false;
var grid_position_init;
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	print("Hola Ready");
	all_pieces=make_2d_array();
	spawn_pieces();	

func make_2d_array():
	var array=[];
	for i  in columns:
		array.append([]);
		for j in rows:
			array[i].append(null);
	return array;   
			

func spawn_pieces():
	print("spawn pieces");
	for i in columns:
		for j in rows:
			var rand=floor(rand_range(0,possible_pieces.size()));
			var piece=possible_pieces[rand].instance();
			add_child(piece);
			piece.position=grid_to_pixel(i,j);
			all_pieces[i][j]=piece;

func is_in_grid(column,row):
	if column>=0 && column < columns:
		if row>=0 && row< rows:
			return true;
		return false;
		
func grid_to_pixel(column,row):
	var new_x=start_x+offset*column;	
	var new_y=start_y-offset*row;
	return Vector2(new_x,new_y);
	
func pixel_to_grid(pixel_x,pixel_y):
	var new_x=round((pixel_x-start_x)/offset);	
	var new_y=round((pixel_y-start_y)/-offset);		
	return Vector2(new_x,new_y);

func touch_input():
	
	if Input.is_action_just_pressed("ui_touch"):
		first_touch=get_global_mouse_position();
		grid_position_init=pixel_to_grid(first_touch.x,first_touch.y);
		if is_in_grid(grid_position_init.x,grid_position_init.y):
			controlling=true;			
	if Input.is_action_just_released("ui_touch"):		
		final_touch=get_global_mouse_position();		
		var grid_position_final=pixel_to_grid(final_touch.x,final_touch.y);
		if is_in_grid(grid_position_final.x,grid_position_final.y) && controlling:
			touch_difference(grid_position_init,grid_position_final);
			controlling=false;
			
			
			
func swap_pieces(column,row,direction):
	var first_piece=all_pieces[column][row];
	var aux_position=first_piece.position;
	var other_piece=all_pieces[column+direction.x][row+direction.y];
	all_pieces[column][row]=other_piece;
	all_pieces[column+direction.x][row+direction.y]=first_piece;
	first_piece.position=other_piece.position;
	other_piece.position=aux_position;	

func touch_difference(grid_1,grid_2):
	var difference=grid_2-grid_1;
	if abs(difference.x)>abs(difference.y):
		if difference.x >0:
			swap_pieces(grid_1.x,grid_1.y,Vector2(1,0));
		elif difference.x<0:
			swap_pieces(grid_1.x,grid_1.y,Vector2(-1,0));	
	elif abs(difference.y)>abs(difference.x):
		if difference.y>0:
			swap_pieces(grid_1.x,grid_1.y,Vector2(0,1));	
		elif difference.y<0:
			swap_pieces(grid_1.x,grid_1.y,Vector2(0,-1));		
				
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	touch_input();
#	pass
