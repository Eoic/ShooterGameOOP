package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Assets;
import com.badlogic.network.Message;
import com.badlogic.network.MessageEmitter;
import com.badlogic.network.RequestCode;
import com.badlogic.util.Constants;
import com.badlogic.util.JsonParser;
import com.badlogic.util.SpriteKeys;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.util.Random;

public class Player extends GameObject {
    private MessageEmitter messageEmitter;
    private BulletPool bulletPool;
    private JsonParser jsonParser;
    private BufferedImage sprite;
    private Vector direction;
    private String name;
    private int ammo;
    private int speed;
    private int team;

    public Player(GameManager gameManager, MessageEmitter messageEmitter) {
        this.bulletPool = new BulletPool(Constants.DEFAULT_PLAYER_BULLET_COUNT, gameManager, SpriteKeys.BULLET_TYPE_TWO);
        this.sprite = Assets.getSprite(SpriteKeys.PLAYER);
        this.speed = Constants.DEFAULT_PLAYER_SPEED;
        this.name = "PLAYER_" + id.substring(0, 5);
        this.messageEmitter = messageEmitter;
        this.jsonParser = new JsonParser();
        this.direction = new Vector();
        this.gameManager = gameManager;
        this.ammo = Constants.DEFAULT_AMMO;
    }

    @Override
    public String getId() {
        return id;
    }

    @Override
    public void update(int delta) {
        var newDirection = new Vector();

        if (gameManager.getInputManager().left)
            newDirection.add(Vector.LEFT);

        if (gameManager.getInputManager().right)
            newDirection.add(Vector.RIGHT);

        if (gameManager.getInputManager().up)
            newDirection.add(Vector.UP);

        if (gameManager.getInputManager().down)
            newDirection.add(Vector.DOWN);

        // Notify server about direction change.
        if (!direction.equals(newDirection)) {
            var message = new Message(RequestCode.UpdateDirection, jsonParser.serialize(newDirection.getSerializable()));
            messageEmitter.send(jsonParser.serialize(message));
            direction.set(newDirection);
        }

        // Calculate constraints.
        var change = direction.multiply(delta * speed);
        var newPos = change.sum(this.position);

        if (newPos.getX() >= 0 && newPos.getX() < Constants.MAP_PIXEL_WIDTH - Constants.MAP_TILE_SIZE &&
            newPos.getY() >= 0 && newPos.getY() < Constants.MAP_PIXEL_HEIGHT - Constants.MAP_TILE_SIZE) {
            this.position.add(change);
            gameManager.getCamera().getOffset().add(change);
        }

        // Follow the player.
        gameManager.getCamera().follow(this, gameManager.getWindow().getSize());

        // Launch bullet on mouse click.
        if (gameManager.getInputManager().lmb && ammo > 0) {
            Vector direction = bulletPool.launch(gameManager.getInputManager().getMouseClickPoint(), this.position);
            messageEmitter.send(jsonParser.serialize(new Message(RequestCode.Shoot, jsonParser.serialize(direction.getSerializable()))));
            gameManager.getInputManager().lmb = false;
            ammo--;
        }

        // Update bullets.
        bulletPool.getBullets().forEach(bullet -> bullet.update(delta));
        bulletPool.cleanup();
    }

    @Override
    public void render(Graphics graphics) {
        var offset = gameManager.getCamera().getOffset();
        int posX = (int) (this.position.getX() - offset.getX()) - Constants.SPRITE_WIDTH_HALF;
        int posY = (int) (this.position.getY() - offset.getY()) - Constants.SPRITE_HEIGHT_HALF;
        var nameOffset = Constants.SPRITE_WIDTH / name.length();
        var window = gameManager.getWindow();
        graphics.drawString("AMMO: " + ammo, window.getWidth() / 2 - 375, window.getHeight() - 80);
        graphics.drawString(name, posX + (int)(nameOffset - name.length() / 2.0f), posY - 5);
        graphics.drawImage(sprite, posX, posY, null);
        bulletPool.getBullets().forEach(bullet -> bullet.render(graphics));
    }

    public int getTeam() {
        return this.team;
    }

    public void setTeam(int team) {
        this.team = team;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAmmo() {
        return ammo;
    }

    public void setAmmo(int ammo) {
        this.ammo = ammo;
    }
}
