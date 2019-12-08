package com.badlogic.util;

import java.awt.*;

public class Constants {
    public static final boolean
            IS_RESIZEABLE = true;

    public static final Color CANVAS_COLOR =
            new Color(64, 67, 78);

    public static final String
            SPRITE_SHEET = "spritesheet.png",
            TEXTURES = "/textures",
            MAPS = "/maps",
            TITLE = "BadLogic",
            SPRITE_SHEET_INFO = "/textures/spritesheet.txt",
            SPRITE_SHEET_PATH = "/textures/spritesheet.png",
            SOCKET_CONNECTION_STRING = "ws://localhost:44327/api/messages",
            DEFAULT_PLAYER_NAME = "UNNAMED_PLAYER";

    public static final int
            WIDTH = 1000,
            HEIGHT = 800,
            BUFFER_COUNT = 3,
            SPRITE_WIDTH = 128,
            SPRITE_HEIGHT = 128,
            SPRITE_WIDTH_HALF = 64,
            SPRITE_HEIGHT_HALF = 64,
            SHEET_ROWS = 5,
            SHEET_COLUMNS = 3,
            FPS = 60,
            COUNTDOWN_LATCH_COUNT = 1,
            DEFAULT_PLAYER_SPEED = 1,
            DEFAULT_PLAYER_BULLET_COUNT = 100,
            DEFAULT_BULLET_SPEED = 1,
            TEAM_COUNT = 2,
            DEFAULT_AMMO = 100;

    public static final int
            MAP_WIDTH = 20,
            MAP_HEIGHT = 15,
            MAP_TILE_SIZE = 128,
            MAP_PIXEL_WIDTH = MAP_WIDTH * MAP_TILE_SIZE,
            MAP_PIXEL_HEIGHT = MAP_HEIGHT * MAP_TILE_SIZE,
            HEALTH_MAX = 100;
}
