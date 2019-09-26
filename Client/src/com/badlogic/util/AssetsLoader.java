package com.badlogic.util;
import com.badlogic.gfx.SpriteSheet;

import java.awt.image.BufferedImage;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.HashMap;

public class AssetsLoader {
    public static HashMap<String, BufferedImage> load(SpriteSheet spriteSheet, String spriteSheetDataPath, int width, int height) {
        var textureMap = new HashMap<String, BufferedImage>();
        var spriteLabels = readSpriteSheetData(spriteSheetDataPath);
        var sheetSize = spriteSheet.getRows() * spriteSheet.getColumns();

        if (spriteLabels.size() != sheetSize) {
            System.out.println("Each sprite should be labeled.");
            return null;
        }

        for (int i = 0; i < spriteSheet.getRows(); i++) {
            for (int j = 0; j < spriteSheet.getColumns(); j++) {
                var texture = spriteSheet.parseSpriteSheet(j * width, i * height, width, height);
                var textureName = spriteLabels.get(i * spriteSheet.getColumns() + j);
                textureMap.put(textureName, texture);
            }
        }

        return textureMap;
    }

    private static ArrayList<String> readSpriteSheetData(String path) {
        var spriteLabels = new ArrayList<String>();
        var currentLine = "";

        try (var inputStream = AssetsLoader.class.getResourceAsStream(path)) {
            if (inputStream == null)
                throw new FileNotFoundException(path);

            try (var bufferedReader = new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8))) {
                while ((currentLine = bufferedReader.readLine()) != null)
                    spriteLabels.add(currentLine);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        return spriteLabels;
    }
}
