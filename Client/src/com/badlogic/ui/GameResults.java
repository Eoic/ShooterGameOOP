package com.badlogic.ui;

import com.badlogic.gfx.Window;
import com.badlogic.serializables.SerializablePlayerState;
import com.badlogic.util.Constants;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.util.ArrayList;
import java.util.concurrent.atomic.AtomicInteger;

public class GameResults extends JPanel {
    private ArrayList<SerializablePlayerState> data = new ArrayList<SerializablePlayerState>();
    private Window window;
    private JPanel gridContainer;

    public GameResults() {
        int gridLayoutRows = 0;
        int gridLayoutCols = 4;
        int borderMarginTop = 50;
        int borderMarginLeft = 10;
        int borderMarginBottom = 0;
        int borderMarginRight = 15;
        this.gridContainer = new JPanel(new GridLayout(gridLayoutRows, gridLayoutCols));
        //this.gridContainer.setBorder(BorderFactory.createEmptyBorder(borderMarginTop, borderMarginLeft, borderMarginBottom, borderMarginRight));
        this.add(gridContainer);
        this.setBorder(BorderFactory.createEmptyBorder(borderMarginTop, borderMarginLeft, borderMarginBottom, borderMarginRight));
        this.setVisible(false);
        this.setBackground(Color.WHITE);
    }

    // Yeah, whatever ...
    public void createDataList(ArrayList<SerializablePlayerState> data) {
        AtomicInteger aliveTeamA = new AtomicInteger();
        AtomicInteger aliveTeamB = new AtomicInteger();

        data.forEach((player) -> {
            if (player.getHealth() > 0 && player.getTeam() == 0)
                aliveTeamA.getAndIncrement();
            else aliveTeamB.getAndIncrement();
        });

        var matchResult = "DRAW";

        if (aliveTeamA.get() > aliveTeamB.get())
            matchResult = "TEAM A WON THE GAME";
        else if (aliveTeamA.get() < aliveTeamB.get())
            matchResult = "TEAM B WON THE GAME";

        this.add(createLabel(matchResult), BorderLayout.PAGE_START);
        createRow("TEAM A", "", "", "");
        createRow("Nr.","Name", "Health", "Status");

        for (var i = 0; i < data.size(); i++) {
            var player = data.get(i);
            if (player.getTeam() == 0) {
                var health = player.getHealth() + " / " + Constants.HEALTH_MAX;
                var status = (player.getHealth() > 0) ? "Alive" : "Dead";
                createRow(Integer.toString(i + 1), player.getName(), health, status);
            }
        }

        createRow("", "", "", "");
        createRow("TEAM B", "", "", "");

        for (var i = 0; i < data.size(); i++) {
            var player = data.get(i);
            if (player.getTeam() == 1) {
                var health = player.getHealth() + " / " + Constants.HEALTH_MAX;
                var status = (player.getHealth() > 0) ? "Alive" : "Dead";
                createRow(Integer.toString(i + 1), player.getName(), health, status);
            }
        }

        this.revalidate();
    }

    private void createRow(String... text) {
        for (String item : text)
            this.gridContainer.add(createLabel(item));
    }

    private JLabel createLabel(String content) {
        var label = new JLabel(content, SwingConstants.CENTER);
        label.setBorder(new LineBorder(Color.GRAY));
        return label;
    }
}
