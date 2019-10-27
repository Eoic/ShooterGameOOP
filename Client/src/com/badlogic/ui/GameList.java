package com.badlogic.ui;

import com.badlogic.network.Message;
import com.badlogic.network.MessageEmitter;
import com.badlogic.network.RequestCode;
import com.badlogic.serializables.SerializableGame;
import com.badlogic.serializables.SerializableGameId;
import com.badlogic.util.JsonParser;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.ActionListener;
import java.util.ArrayList;

public class GameList extends JPanel {
    public GameList() {
        this.setBackground(Color.WHITE);
        this.setLayout(new GridLayout(0, 3));

        // List header.
        this.setHeader();
        this.setBorder(BorderFactory.createEmptyBorder(50, 10, 0, 15));

        // Initial value.
        this.addNoGamesEntry();
    }
    
    public void updateList(ArrayList<SerializableGame> gameList, MessageEmitter messageEmitter, JsonParser jsonParser) {
        if (gameList.size() == 0) {
            this.addNoGamesEntry();
            return;
        }

        this.removeAll();
        this.revalidate();
        this.repaint();

        this.setHeader();

        gameList.forEach((game) -> addEntry(game.getRoomId(), game.getJoinedPlayers(), game.getMaxPlayers(), (event) -> {
            var gameId = new SerializableGameId(game.getRoomId());
            var message = new Message(RequestCode.JoinGame, jsonParser.serialize(gameId));
            messageEmitter.send(jsonParser.serialize(message));
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
