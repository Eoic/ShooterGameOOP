package com.badlogic.network;

public class MessageType {
    // Sent to server
    public static int CreateGame = 4;
    public static int DirectionUpdate = 8;

    // Received from server
    public static int GameCreated = 10;
    public static int PositionUpdate = 11;
    public static int InstantiateBonuses = 12;
}


