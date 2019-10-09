package com.badlogic.game;

import com.badlogic.util.Constants;
import com.badlogic.util.Vector;

import java.util.ArrayList;
import java.util.List;

public class BulletPool {
    private ArrayList<Bullet> bullets;

    public BulletPool(int bulletCount, GameManager gameManager, String bulletType) {
        this.bullets = new ArrayList<>();

        for (int i = 0; i < bulletCount; i++)
            bullets.add(new Bullet(Constants.DEFAULT_BULLET_SPEED, gameManager.getWindow(), gameManager.getCamera(), bulletType));
    }

    void launch(Vector target, Vector origin) {
        for (Bullet bullet : bullets) {
            if (!bullet.getActiveState()) {
                bullet.launch(target, origin);
                break;
            }
        }
    }

    List<Bullet> getBullets() {
        return this.bullets;
    }

    public void cleanup() {
        for (Bullet bullet : bullets) {
            if (bullet.getPosition().getX() < -Constants.SPRITE_WIDTH_HALF ||
                bullet.getPosition().getX() > Constants.MAP_PIXEL_WIDTH - Constants.SPRITE_WIDTH_HALF ||
                bullet.getPosition().getY() < - Constants.SPRITE_HEIGHT_HALF ||
                bullet.getPosition().getY() > Constants.MAP_PIXEL_HEIGHT - Constants.SPRITE_WIDTH_HALF)
                bullet.dispose();
        }
    }
}
