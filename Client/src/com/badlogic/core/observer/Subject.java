package com.badlogic.core.observer;

public interface Subject<T> {
    void attach(Observer observer);
    void detach(Observer observer);
    void notifyAllObservers(T data);
}
