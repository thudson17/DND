var squares_wide = 0; //width of each square to be rendered on the map
var squares_high = 0; //height of each square to be renfered on the map

var client_width = 0; //width of the browser window
var client_height = 0; //height of the browser window

var $map; //jquery variable of the actual map
var $startblock; //the top/left most square on the map, all other squares are simply clones of this guy

//initializes the map size from server, etc.
function map_init(_squares_wide, _squares_high) {

    //initialize the map size
    squares_wide = parseInt(_squares_wide);
    squares_high = parseInt(_squares_high);

    //initialize the client sizing (re-sizing window will require refresh for now)
    client_height = parseInt($(window).height());
    client_width = parseInt($(window).width());

    $map = $(".map");
    $startblock = $(".map_block");

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
    blocks = new Array(); //re-init the array of blocks

    while (row_width < client_width) {

        var $new_block = $startblock.clone();// create a new "clone" square, which we will append the our map img..

        //need to determine if we can actually fit another full block on the edge of the screen, and render a new block accordingly...
        if ((row_width + squares_wide) > client_width) {
            var diff = (row_width + squares_wide) - client_width;
            $new_block.css("width", diff.toString() + "px");
            row_width += diff;
        }
        else {
            row_width += squares_wide;
        }

        
        $new_block.appendTo($map);

        

       

        console.log(row_width);
    }

  
}




