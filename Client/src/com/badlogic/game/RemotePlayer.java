package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Camera;
import com.badlogic.serializables.SerializableBullet;
import com.badlogic.util.Constants;
import com.badlogic.util.SpriteKeys;
import com.badlogic.util.Vector;

import java.awt.*;
import java.util.ArrayList;

public class RemotePlayer extends GameObject {
    private ArrayList<RemoteBullet> bullets;
    private Vector direction;
    private Window window;
    private Camera camera;
    private String name;
    private String id;
    private int team;
    private int health;
    private int speed;

    public RemotePlayer(Window window, Camera camera, boolean isFriendly) {
        this.window = window;
        this.camera = camera;
        this.direction = new Vector(0, 0);
        this.speed = Constants.DEFAULT_PLAYER_SPEED;
        this.name = Constants.DEFAULT_PLAYER_NAME;
        this.health = Constants.HEALTH_MAX;

        if (!isFriendly) {
            this.sprite = Assets.getSprite(SpriteKeys.ENEMY_PLAYER);
        } else {
            this.sprite = Assets.getSprite(SpriteKeys.FRIENDLY_PLAYER);
        }

        bullets = new ArrayList<>();
    }

    @Override
    public void render(Graphics graphics) {
        int posX = (int)position.getX() - (int)camera.getOffset().getX() - Constants.SPRITE_WIDTH_HALF;
        int posY = (int)position.getY() - (int)camera.getOffset().getY() - Constants.SPRITE_WIDTH_HALF;
        var nameOffset = Constants.SPRITE_WIDTH / name.length();
        graphics.drawString(name, posX + (int)(nameOffset - name.length() / 2.0f), posY - 5);
        graphics.drawImage(this.sprite, posX, posY, null);
        this.renderBullets(graphics, camera);
    }

    @Override
    public void update(int delta) {
        var change = direction.multiply(delta * speed);
        var newPos = change.sum(this.position);

        if (newPos.getX() >= 0 && newPos.getX() < Constants.MAP_PIXEL_WIDTH - Constants.MAP_TILE_SIZE &&
            newPos.getY() >= 0 && newPos.getY() < Constants.MAP_PIXEL_HEIGHT - Constants.MAP_TILE_SIZE) {
            this.position.add(change);
        }

        this.updateBullets(delta);
    }

    // Revalidate bullets.
    public void parseBullets(ArrayList<SerializableBullet> bullets) {
        this.bullets = new ArrayList<>();
        bullets.forEach((bullet) -> this.bullets.add(new RemoteBullet(bullet.getPosition(), bullet.getDirection())));
    }

    public void renderBullets(Graphics graphics, Camera camera) {
        bullets.forEach((bullet) -> {
            bullet.render(graphics, camera);
        });
    }

    public void updateBullets(int delta) {
        bullets.forEach((bullet) -> {
            bullet.update(delta);
        });
    }

    public void setDirection(Vector direction) {
        this.direction = direction;
    }

    public void setPosition(Vector position) {
        this.position = position;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public int getHealth() {
        return health;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
        this.name = "[" + ((team == 0) ? "A" : "B") + "]PLAYER_" + id.substring(0, 5);
    }

    public int getTeam() {
        return team;
    }

    public void setTeam(int team) {
        this.team = team;
    }
}
