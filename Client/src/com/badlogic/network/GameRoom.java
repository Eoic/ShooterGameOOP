package com.badlogic.network;

import com.badlogic.game.Player;

import java.util.ArrayList;

public class GameRoom {
    private ArrayList<Player> players = new ArrayList<>();

    public void addPlayer(Player player) {
        players.add(player);
    }
    
    public ArrayList<Player> getPlayers() {
        return players;
    }
}
