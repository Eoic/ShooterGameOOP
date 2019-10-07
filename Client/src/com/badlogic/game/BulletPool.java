package com.badlogic.game;

import com.badlogic.util.Vector;

import java.util.ArrayList;
import java.util.List;

public class BulletPool {
    private ArrayList<Bullet> bullets;
    private GameManager gameManager;

    public BulletPool(int bulletCount, GameManager gameManager) {
        this.gameManager = gameManager;
        this.bullets = new ArrayList<>();

        for (int i = 0; i < bulletCount; i++) {
            bullets.add(new Bullet(10, gameManager.getWindow(), gameManager.getCamera()));
        }
    }

    public void launch(Vector target) {
        for (var i = 0; i < bullets.size(); i++) {
            if (!bullets.get(i).getActiveState()) {
                bullets.get(i).launch(target);
                break;
            }
        }
    }

    public List<Bullet> getBullets() {
        return this.bullets;
    }

    public void cleanup() {

    }
}
