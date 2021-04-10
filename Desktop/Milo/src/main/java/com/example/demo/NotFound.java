package com.example.demo;

public class NotFound extends RuntimeException {

    NotFound(Long id){
        super("Nie znaleziono " + id);
    }
}
