package com.badlogic.gfx;

import com.badlogic.game.RemotePlayer;
import com.badlogic.util.Constants;

import java.awt.*;
import java.util.HashMap;
import java.util.concurrent.atomic.AtomicInteger;

public class HeadUpDisplay {
    private Window window;

    public HeadUpDisplay(Window window) {
        this.window = window;
    }

    public void render(Graphics graphics, HashMap<String, RemotePlayer> players, int hostTeam) {
        AtomicInteger index = new AtomicInteger();
        var nextRowOffset = 30;
        var originX = 30;
        var originY = 30;
        var gap = 30;

        players.forEach((playerKey, playerValue) -> {
            if (playerValue.getTeam() == hostTeam) {
                graphics.drawString(playerValue.getName(), originX, originY + index.get() * gap);
                graphics.drawString(playerValue.getHealth() + " / " + Constants.HEALTH_MAX, originX, originY + index.get() * gap + nextRowOffset);
                index.getAndIncrement();
                index.getAndIncrement();
            }
        });
    }
}
