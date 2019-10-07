package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Assets;
import com.badlogic.network.Message;
import com.badlogic.network.MessageEmitter;
import com.badlogic.network.MessageType;
import com.badlogic.util.Constants;
import com.badlogic.util.JsonParser;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Player extends GameObject {
    private BulletPool bulletPool;
    private BufferedImage sprite;
    private MessageEmitter messageEmitter;
    private JsonParser jsonParser;
    private Vector direction;
    private int speed;

    private Vector bulletDir = new Vector(0, 0);
    private Vector bulletPos = new Vector(0, 0);
    private int ii = 0;

    public Player(GameManager gameManager, MessageEmitter messageEmitter) {
        this.jsonParser = new JsonParser();
        this.messageEmitter = messageEmitter;
        this.direction = new Vector(0, 0);
        this.gameManager = gameManager;
        this.sprite = Assets.getSprite("player");
        this.speed = 1;
        this.position.setX(100); // ?
        this.bulletPool = new BulletPool(100, gameManager);
    }

    @Override
    public String getId() {
        return id;
    }

    @Override
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

        var change = direction.multiply(delta * speed);
        var newPos = change.sum(this.position);

        if (newPos.getX() >= 0 && newPos.getX() < Constants.MAP_PIXEL_WIDTH - Constants.MAP_TILE_SIZE && newPos.getY() >= 0 && newPos.getY() < Constants.MAP_PIXEL_HEIGHT - Constants.MAP_TILE_SIZE) {
            this.position.add(change);
            gameManager.getCamera().getOffset().add(change);
        }

        // Launch and update bullets.
        if (gameManager.getInputManager().lmb) {
            bulletPool.launch(gameManager.getInputManager().getMouseClickPoint());
            gameManager.getInputManager().lmb = false;
        }

        bulletPool.getBullets().forEach(bullet -> {
            bullet.update(delta);
        });
    }

    @Override
    public void render(Graphics graphics) {
        var windowSize = gameManager.getWindow().getSize();
        int posX = (windowSize.width / 2 - Constants.SPRITE_WIDTH / 2);
        int posY = (windowSize.height / 2 - Constants.SPRITE_HEIGHT / 2);
        graphics.drawImage(sprite, posX, posY, null);

        bulletPool.getBullets().forEach(bullet -> {
            bullet.render(graphics);
        });
        /*
        // Get shot direction.
        var a = new Vector(windowSize.width / 2.0, windowSize.height / 2.0);

        // --
        var dir = gameManager.getInputManager().getMouseClickPoint().difference(a);
        // --

        var c = a.sum(dir.getNormalized().multiply(100 * ii));
        if (gameManager.getInputManager().lmb) {
            ii++;
        }
        */
    }
}
