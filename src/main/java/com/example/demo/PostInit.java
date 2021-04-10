package com.example.demo;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;

@Component
public class PostInit {

    @Autowired
    UserRepository userRepository;

    @PostConstruct
    public void init(){
        User user =new User();
        user.setEmail("MiloDupcia@chybTy");
        user.setName("Sriłosz");
        userRepository.save(user);

        User user1 =new User();
        user1.setEmail("cisnijSpringa@Kubsztal");
        user1.setName("Kubsztal");
        userRepository.save(user1);
    }
}

// z automatu dodaje wartosci do bazy