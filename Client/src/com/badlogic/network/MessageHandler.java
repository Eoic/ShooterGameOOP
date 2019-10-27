package com.badlogic.network;

import com.badlogic.game.GameManager;
import com.badlogic.game.Player;
import com.badlogic.serializables.SerializablePlayer;
import com.badlogic.util.JsonParser;
import com.badlogic.util.Point;
import com.badlogic.util.Vector;

public class MessageHandler {
    private JsonParser jsonParser = new JsonParser();
    private MessageEmitter messageEmitter;
    private GameManager gameManager;
    private GameRoom gameRoom;

    public MessageHandler(MessageEmitter messageEmitter, GameManager gameManager, GameRoom gameRoom) {
        this.messageEmitter = messageEmitter;
        this.gameManager = gameManager;
        this.gameRoom = gameRoom;
    }

    public void OnGameCreated(Message message, Player player) {
        var position = jsonParser.deserialize(message.getPayload(), Point.class);
        player = new Player(gameManager, messageEmitter);
        player.getPosition().set(new Vector(position.getX(), position.getY()));
        gameRoom.addPlayer(player);
    }

    public void OnPositionUpdated(Message message) {
        var players = jsonParser.deserializeList(message.getPayload(), SerializablePlayer.class);
        players.forEach(serializablePlayer -> {
            if (serializablePlayer.getType() == 10) {
                var position = serializablePlayer.getPosition();
                this.gameRoom.getPlayers().get(0).position.set(new Vector(position.getX(), position.getY()));
            } else {
                // var p = serializablePlayer.getPosition();
                // remotePlayer.position = new Vector(p.getX(), p.getY());
            }
        });
    }
}
