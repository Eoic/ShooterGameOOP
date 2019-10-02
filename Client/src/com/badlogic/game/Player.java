package com.badlogic.game;

import com.badlogic.core.Entity;
import com.badlogic.gfx.Assets;
import com.badlogic.network.Message;
import com.badlogic.network.MessageEmitter;
import com.badlogic.network.MessageType;
import com.badlogic.util.Constants;
import com.badlogic.util.JsonParser;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Player extends Entity {
    private MessageEmitter messageEmitter;
    private JsonParser jsonParser;
    private GameManager gameManager;
    private BufferedImage sprite;
    private Vector direction;
    private int speed;

    public Player(GameManager gameManager, MessageEmitter messageEmitter) {
        this.jsonParser = new JsonParser();
        this.messageEmitter = messageEmitter;
        this.direction = new Vector(0, 0);
        this.gameManager = gameManager;
        this.sprite = Assets.getSprite("player");
        this.speed = 1;
    }

    public Vector getPosition() {
        return position;
    }

    @Override
    public String getId() {
        return id;
    }

    public void render(Graphics graphics) {
        var camOffset = gameManager.getCamera().getOffset();
        var windowSize = gameManager.getWindow().getSize();
        int posX = ((int)position.getX() - (int)camOffset.getX()) + (windowSize.width / 2 - Constants.SPRITE_WIDTH / 2);
        int posY = ((int)position.getY() - (int)camOffset.getY()) + (windowSize.height / 2 - Constants.SPRITE_HEIGHT / 2);
        graphics.drawImage(sprite, posX, posY, null);
    }

    public void update(int delta) {
        var newDirection = new Vector(0, 0);

        if (gameManager.getInputManager().left) {
            newDirection.add(-1, 0);
        }
        if (gameManager.getInputManager().right) {
            newDirection.add(1, 0);
        }
        if (gameManager.getInputManager().up) {
            newDirection.add(0, -1);
        }
        if (gameManager.getInputManager().down) {
            newDirection.add(0, 1);
        }

        // Notify server about direction change.
        if (!direction.equals(newDirection)) {
            var message = new Message(MessageType.DirectionUpdate, jsonParser.serialize(newDirection.dump()));
            messageEmitter.send(jsonParser.serialize(message));
            direction.set(newDirection);
        }

        gameManager.getCamera().getOffset().add(direction);
        position.add(direction.multiply(delta * speed));
    }
}
