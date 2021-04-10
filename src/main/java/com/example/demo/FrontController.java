package com.example.demo;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

import java.util.ArrayList;
import java.util.List;


@Controller
public class FrontController {

    @GetMapping("/")
    public String home(){
        return "home";
    }

}

// to mi wyświetla HTMLa !!!!
