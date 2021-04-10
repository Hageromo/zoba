package com.example.demo;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;


@CrossOrigin
@RestController
public class UserController {

    @Autowired
    private UserRepository userRepository;

    @GetMapping("/api/users")
    public Iterable<User> getUser(){

        return userRepository.findAll();
    }



    @PostMapping("/employees")
    User newUser(@RequestBody User user) {
        return userRepository.save(user);
    }


}
