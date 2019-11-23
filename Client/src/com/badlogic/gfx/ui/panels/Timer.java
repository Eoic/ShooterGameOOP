package com.badlogic.gfx.ui.panels;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public class Timer {
    private int time;
    private String label;
    private ScheduledExecutorService executorService;

    public Timer(String label, int time) {
        this.executorService = Executors.newSingleThreadScheduledExecutor();
        this.label = label;
        this.time = time;
        executorService.scheduleAtFixedRate(this::updateTime, 0,1, TimeUnit.SECONDS);
    }

    public String getTimeString() {
        var timeString = "00:00";

        if (time > 0) {
            var minutes = time / 60;
            var seconds = time % 60;
            timeString = ((minutes < 10) ? "0" : "") + minutes + ":";
            timeString += ((seconds < 10) ? "0" : "") + seconds;
        }

        return label + " " + timeString;
    }

    public void setTime(int time) {
        this.time = time;
    }

    public void setLabel(String label) {
        this.label = label;
    }

    private void updateTime() {
        if (time > 0)
            time--;
    }
}
