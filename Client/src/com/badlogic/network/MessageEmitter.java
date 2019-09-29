package com.badlogic.network;

import com.badlogic.core.observer.Observer;
import com.badlogic.util.Constants;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.WebSocket;
import java.util.concurrent.CompletionException;
import java.util.concurrent.CountDownLatch;

public class MessageEmitter {
    private CountDownLatch latch;
    private Connector connector;
    private WebSocket webSocket;

    public MessageEmitter() {
        latch = new CountDownLatch(Constants.COUNTDOWN_LATCH_COUNT);
        connector = new Connector(latch);

        try {
            webSocket = HttpClient.newHttpClient()
                    .newWebSocketBuilder()
                    .buildAsync(URI.create(Constants.SOCKET_CONNECTION_STRING), connector)
                    .join();
        } catch (CompletionException ex) {
            ex.printStackTrace();
        }
    }

    // TODO: Make async.
    // TODO: Check if connection was made before sending
    public void send(String message) {
        webSocket.sendText(message, true);
        try {
            latch.await();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    public void addListener(Observer observer) {
        connector.addListener(observer);
    }
}
