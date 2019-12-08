package com.badlogic.gfx;

import com.badlogic.game.RemotePlayer;
import com.badlogic.gfx.ui.panels.Timer;
import com.badlogic.serializables.SerializableTimer;
import com.badlogic.util.Constants;

import java.awt.*;
import java.util.HashMap;
import java.util.concurrent.atomic.AtomicInteger;

public class HeadUpDisplay {
    private Timer timer;
    private Window window;

    public HeadUpDisplay(Window window) {
        this.timer = new Timer("Game starts in", 0);
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

        var offset = timer.getTimeString().length() * 10 / 2;
        graphics.drawString(timer.getTimeString(), window.getWidth() / 2 - offset, 30);
    }

    public void updateTimer(SerializableTimer timer) {
        this.timer = new Timer(timer.getLabel(), timer.getValue());
    }
}
