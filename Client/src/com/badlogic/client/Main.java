package com.badlogic.client;

import com.badlogic.network.Connector;
import com.badlogic.util.Constants;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.WebSocket;
import java.util.concurrent.CountDownLatch;

public class Main {
    public static void main(String[] args) throws InterruptedException {
        // new Loop().start();
        // TODO: Ignore SSL.
        CountDownLatch latch = new CountDownLatch(1);
        Connector connector = new Connector(latch);
        WebSocket webSocket = HttpClient.newHttpClient()
                                        .newWebSocketBuilder()
                                        .buildAsync(URI.create(Constants.SOCKET_CONNECTION_STRING), connector)
                                        .join();
        webSocket.sendText("Hello", true);
        latch.await();
    }
}
