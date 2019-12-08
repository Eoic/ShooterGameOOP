package com.badlogic.gfx.ui.panels;

import com.badlogic.gfx.Window;
import com.badlogic.gfx.ui.CanvasElement;
import com.badlogic.network.MessageEmitter;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionListener;
import java.util.ArrayList;

public class TeamSelection implements CanvasElement {
    private JLabel teamSelectionLabel;
    private ArrayList<JButton> teams;
    private Window window;

    public TeamSelection(Window window, String[] teamNames) {
        this.window = window;
        this.teams = new ArrayList<>();
        this.teamSelectionLabel = new JLabel("SELECT TEAM", SwingConstants.CENTER);
        this.teamSelectionLabel.setFont(new Font("Courier New", Font.BOLD, 24));

        for (String name : teamNames) {
            var button = new JButton(name);
            teams.add(button);
        }

        this.updatePositions();
    }

    @Override
    public void update() {
        this.updatePositions();
    }

    private void updatePositions() {
        int labelYPos = this.window.getHeight() / 2 - 200;

        if (labelYPos < 0)
            labelYPos = 0;

        this.teamSelectionLabel.setBounds(0, labelYPos, this.window.getWidth(), 100);

        for (int i = 0; i < teams.size(); i++) {
            int buttonBoxSize = 100;
            int gap = 10;
            int offsetX = i * (buttonBoxSize + gap) - teams.size() * buttonBoxSize / 2 - gap;
            int offsetY = -buttonBoxSize / 2;
            teams.get(i).setBounds((this.window.getWidth() / 2) + offsetX, this.window.getHeight() / 2 + offsetY, buttonBoxSize, buttonBoxSize);
        }
    }

    @Override
    public void addToFrame(JFrame frame) {
        frame.add(teamSelectionLabel);

        for (JButton teamSelectionButton : this.teams) {
            frame.add(teamSelectionButton);
        }
    }

    public void setVisible(boolean visible) {
        teamSelectionLabel.setVisible(visible);

        for (JButton teamSelectionButton : this.teams) {
            teamSelectionButton.setVisible(visible);
        }
    }

    // Add given listeners to selection buttons
    public void setEventForSelection(MessageEmitter messageEmitter, ArrayList<ActionListener> listeners) {
        for (int i = 0; i < this.teams.size(); i++) {
            teams.get(i).addActionListener(listeners.get(i));
        }
    }
}
