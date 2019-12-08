package com.badlogic.ui;

import com.badlogic.gfx.Window;
import com.badlogic.gfx.ui.panels.TeamSelection;
import com.badlogic.network.Message;
import com.badlogic.network.MessageEmitter;
import com.badlogic.network.RequestCode;
import com.badlogic.serializables.SerializableGame;
import com.badlogic.serializables.SerializableGameId;
import com.badlogic.util.Constants;
import com.badlogic.util.JsonParser;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

public class GameList extends JPanel {

    public GameList() {
        int gridLayoutRows = 0;
        int gridLayoutCols = 3;
        int borderMarginTop = 50;
        int borderMarginLeft = 10;
        int borderMarginBottom = 0;
        int borderMarginRight = 15;
        this.setBackground(Color.WHITE);
        this.setLayout(new GridLayout(gridLayoutRows, gridLayoutCols));
        this.setHeader();
        this.setBorder(BorderFactory.createEmptyBorder(borderMarginTop, borderMarginLeft, borderMarginBottom, borderMarginRight));
        this.addNoGamesEntry();
    }

    // Update list of games available
    public void updateList(ArrayList<SerializableGame> gameList, MessageEmitter messageEmitter, JsonParser jsonParser, Window window) {
        this.removeAll();
        this.setHeader();

        if (gameList.size() == 0) {
            this.addNoGamesEntry();
            this.revalidate();
            return;
        }

        gameList.forEach((game) -> addEntry(game.getRoomId(), game.getJoinedPlayers(), game.getMaxPlayers(), (event) -> {
            // Show team selection window and add actions to selection.
            window.showTeamSelectionWindow();
            ArrayList<ActionListener> events = new ArrayList<>();

            for (var i = 0; i < Constants.TEAM_COUNT; i++) {
                int finalI = i;
                events.add(actionEvent -> {
                    var gameId = new SerializableGameId(game.getRoomId(), finalI);
                    var message = new Message(RequestCode.JoinGame, jsonParser.serialize(gameId));
                    messageEmitter.send(jsonParser.serialize(message));
                });
            }

            window.setTeamSelectionEvents(messageEmitter, events);
        }));

        this.revalidate();
    }

    private void addNoGamesEntry() {
        this.add(createLabel("No games available"));
        this.add(createLabel(""));
        this.add(createLabel(""));
    }

    private void addEntry(String gameId, int playersCurrent, int playersMax, ActionListener actionListener) {
        this.add(createLabel(gameId));
        this.add(createLabel(playersCurrent + " / " + playersMax));
        var joinGameBtn = createButton(actionListener);
        this.add(joinGameBtn);
    }

    private JLabel createLabel(String content) {
        var label = new JLabel(content, SwingConstants.CENTER);
        label.setBorder(new LineBorder(Color.GRAY));
        return label;
    }

    private JButton createButton(ActionListener actionListener) {
        var button = new JButton("JOIN");
        button.setBorder(new LineBorder(Color.GRAY));
        button.setBackground(Color.GREEN);
        button.addActionListener(actionListener);
        return button;
    }

    private void setHeader() {
        this.add(createLabel("ROOM ID"));
        this.add(createLabel("PLAYERS"));
        this.add(createLabel("ACTIONS"));
    }
}
