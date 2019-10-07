package com.badlogic.util;

public class Constants {
    public static final boolean
            IS_RESIZEABLE = true;

    public static final String
            SPRITE_SHEET = "spritesheet.png",
            TEXTURES = "/textures",
            TITLE = "BadLogic",
            SPRITE_SHEET_INFO = "/textures/spritesheet.txt",
            SPRITE_SHEET_PATH = "/textures/spritesheet.png",
            SOCKET_CONNECTION_STRING = "ws://localhost:44327/api/messages";

    public static final int
            WIDTH = 800,
            HEIGHT = 600,
            BUFFER_COUNT = 3,
            SPRITE_WIDTH = 128,
            SPRITE_HEIGHT = 128,
            SHEET_ROWS = 4,
            SHEET_COLUMNS = 3,
            FPS = 60,
            COUNTDOWN_LATCH_COUNT = 1;

    public static final int
            MAP_WIDTH = 20,
            MAP_HEIGHT = 10,
            MAP_TILE_SIZE = 128,
            MAP_PIXEL_WIDTH = MAP_WIDTH * MAP_TILE_SIZE,
            MAP_PIXEL_HEIGHT = MAP_HEIGHT * MAP_TILE_SIZE;
}
