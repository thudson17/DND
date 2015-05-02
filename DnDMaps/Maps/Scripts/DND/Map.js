﻿var squares_wide = 0; //width of each square to be rendered on the map
var squares_high = 0; //height of each square to be renfered on the map

var squares_border_offset = 2; //need to account for the border size of square dom elemenets..

var client_width = 0; //width of the browser window
var client_height = 0; //height of the browser window

var $map; //jquery variable of the actual map
var $body; //the body of the map with all layers (this one is draggable)

var $startblock; //the top/left most square on the map, all other squares are simply clones of this guy

var blocks; //array of the blocks on the map;

//initializes the map size from server, etc.
function map_init(_squares_wide, _squares_high) {

    //initialize the map size
    squares_wide = parseInt(_squares_wide);
    squares_high = parseInt(_squares_high);

    //initialize the client sizing (re-sizing window will require refresh for now)
    client_height = parseInt($(window).height());
    client_width = parseInt($(document).width());

    $map = $(".map");
    $startblock = $(".map_block");
    $body = $(".map_body");

    $startblock.css("width", squares_wide.toString() + "px");
    $startblock.css("height", squares_high.toString() + "px");


    //log the map sizes for debug
    console.log("Map Width Squares : " + parseInt(squares_wide));
    console.log("Map Height Squares : " + parseInt(squares_high));
    console.log("Window Height : " + client_height);
    console.log("Window Width : " + client_width);

    buildMapGrid();

}


function buildMapGrid() {

    var row_width = 0;
    var row_height = squares_high;

    blocks = new Array();

//while we still have verticle height left on page, loop and add boxes 
    while (row_height < client_height) {
        //while we still have width left on page, loop and add boxes to current row
        while (row_width < client_width) {

            var $new_block = $startblock.clone();// create a new "clone" square, which we will append the our map img..
            $new_block.css("display", "inline-block"); //setup the display of the block, since the original will be hidden 

            $new_block.appendTo($map);

            blocks.push($new_block); //store the newly created block in our global array

     
            //need to determine if we can actually fit another full block on the edge of the screen, and render a new block accordingly...
            if ((row_width + squares_wide + squares_border_offset) > client_width) {
                var diff = client_width - row_width - squares_border_offset;

                $new_block.css("width", diff.toString() + "px");
                row_width += diff;


                break;

            }
            else {
                row_width += squares_wide + squares_border_offset;
            }


        }

        row_width = 0;
        row_height += squares_high;

    }

  
}

//view will pass an object array of the characters over to the map here
function addCharactersToMap(_characters, _avatarpath) {
   
    //loop through each character provided and add them into our map!
    $.each(_characters, function (index, character) {
        
        //now we use the array of blocks to position each character in a starting area.

        var $avatar = $("<div>", { id: character.Character_ID, class: "character_avatar", title: character.Name });
      
        $avatar.css("height", (squares_high /2).toString() + "px");
        $avatar.css("width", (squares_wide / 2).toString() + "px");

        $avatar.html("<img src='" + _avatarpath + '/' + character.Avatar + "' style='height:100%;width:100%;'/>");

        $avatar.draggable();

        $($avatar).appendTo($body);

    });

}



function blockDrop() {
    alert();
}
