package com.badlogic.util;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public class JsonParser {
    private ObjectMapper objectMapper;

    public JsonParser() {
        objectMapper = new ObjectMapper();
    }

    public <T> String serialize(T value) {
        try {
            return objectMapper.writeValueAsString(value);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
            return null;
        }
    }

    public <T> T deserialize(String value, Class<T> valueType) {
        try {
            return objectMapper.readValue(value, valueType);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
            return null;
        }
    }
}
