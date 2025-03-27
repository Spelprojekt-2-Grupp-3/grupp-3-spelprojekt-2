INCLUDE ../globals.ink

-> fillerBengt

=== fillerBengt ===

Hey again! #Speaker:Bengt

Something on your mind? #Speaker:Bengt

-> questions

= questions

    + [I heard something...]
    
    I heard something interesting... #Speaker:Cleo
    
    Something interesting? Well, now I'm curious! #Speaker:Bengt
    
    -> activeQuestions

    + [About you...]
    
    I've got some questions about you. #Speaker:Cleo
    
    Oh, wondering about me? #Speaker:Bengt
    
    I'm an open book! Ask me anything you'd like. #Speaker:Bengt
    
    -> bengtQuestions

        Hmm... I can't say I have much else to tell you! #Speaker:Bengt
        
        How about we move on from that? #Speaker:Bengt
        
        -> questions

    + [About the other islanders...]
    
    What are your thoughts on the others in the archipelago? #Speaker:Cleo
    
    I get along pretty well with all of them. #Speaker:Bengt
    
    Is there anyone in particular you're wondering about? #Speaker:Bengt
    
    -> characters1

    * [Bye.]
    
    I need to get going. #Speaker:Cleo
    
    Alright. Have a good one! #Speaker:Bengt
    
    ->END
    
    
= bengtQuestions

    * [About your time here.]
    
    What's your story? How did you come to live here? #Speaker:Cleo

    Ah, I don't have a very interesting story, I'm afraid! Nothing as exciting as yours. #Speaker:Bengt
    
    I've lived here my whole life, all 35 years, born and raised! #Speaker:Bengt
    
    So did my parents, and my grandparents, and so on. #Speaker:Bengt
    
    Actually, my workshop has been inherited for generations! #Speaker:Bengt
    
    Lots of mechanics in my family tree... #Speaker:Bengt
    
    -> bengtQuestions
    
    * [Do you want to leave?]
    
    Do you ever want to leave this place? #Speaker:Cleo
    
    Not in the least! #Speaker:Bengt
    
    I'm not a very adventurous guy, and what I've heard about the outside world... #Speaker:Bengt
    
    Well, it doesn't align with me at all! #Speaker:Bengt
    
    Plus, I have my workshop, all these interesting birds, a bunch of friends... #Speaker:Bengt
    
    I'm content staying here. #Speaker:Bengt
    
    I even get the occasional outsider looking for help, like you! That's enough excitement. #Speaker:Bengt
    
    Anyway, was there anything else? #Speaker:Bengt
    
    -> bengtQuestions
    
    + [That's all.]
    
    That's all I wanted to know. #Speaker:Cleo
    
    Alright. Anything else you wondered about? #Speaker:Bengt

    -> questions
    
    
= characters1
    
    * [Sigrid?]
    
    What about Sigrid? #Speaker:Cleo
    
    When I was a kid, I was super interested in how the lighthouse spotlight kept rotating... #Speaker:Bengt
    
    I nagged and nagged and nagged for my parents to take me there so I could check it out... #Speaker:Bengt
    
    ... and they did! #Speaker:Bengt
    
    Sigrid seemed... not too pleased, at first. I thought she'd scold us, honestly. #Speaker:Bengt
    
    Even so, she showed me up to the top floor and let me look at that light. #Speaker:Bengt
    
    Anyway, what I'm trying to get across here is that she might seem standoffish at times... #Speaker:Bengt
    
    But she's a good sort, and will help you out when you need it. #Speaker:Bengt
    
    I'd say we've become decent friends since then as well. #Speaker:Bengt
    
    Granted, I do bring her coffee. #Speaker:Bengt
    
    Anyone else? #Speaker:Bengt
    
    -> characters1
    
    * [Irma?]
    
    What about Irma? #Speaker:Cleo
    
    Irma came here not too long ago... #Speaker:Bengt
    
    Well, it's been over a decade, but that's much shorter than anyone else here. #Speaker:Bengt
    
    After her sister disappeared, she was pretty closed off for a while. #Speaker:Bengt
    
    It was hard to really get to know her then. #Speaker:Bengt
    
    Fortunately, our jobs here wound up colliding every now and then. #Speaker:Bengt
    
    We had to work together on a few projects. That let me talk to her some more. #Speaker:Bengt
    
    She's quick-witted and smart! And passionate about her craft! I respect her a lot. #Speaker:Bengt
    
    Anyone else? #Speaker:Bengt
    
    -> characters1
    
    + [Someone else...]
    
    -> characters2
    
    + [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Alright. Anything else you wondered about? #Speaker:Bengt
    
    -> questions
    
    
= characters2
    
    * [Ulrich?]
    
    What about Ulrich? #Speaker:Cleo
    
    Ulrich is... pretty hard to understand at times. #Speaker:Bengt
    
    He's constantly congested! I can hardly hear him anytime I visit. #Speaker:Bengt
    
    The first time I spoke with him, I thought he was mad at me! He sounded so gruff! #Speaker:Bengt
    
    Luckily he's a lot easier to understand when he takes his allergy medicine... #Speaker:Bengt
    
    Turns out he's a total sweetheart! I never would've guessed! #Speaker:Bengt
    
    He gave me some great tips on how to attract birds with different kinds of flowers... #Speaker:Bengt
    
    ...and I do maintenance on his gardening tools from time to time. #Speaker:Bengt
    
    We get along amicably if I say so myself! #Speaker:Bengt
    
    Anyway, don't take it personally if he seems off. Poor guy just needs a hit of his nasal spray. #Speaker:Bengt
    
    Anyone else? #Speaker:Bengt
    
    -> characters2
    
    * [Vera?]
    
    What about Vera? #Speaker:Cleo
    
    Nobody cares more for the ecosystem here than Vera! #Speaker:Bengt
    
    I'm honestly a little lost on all that, myself. #Speaker:Bengt
    
    One time, I wanted to test out a mechanical doodad of mine in the water... #Speaker:Bengt 
    
    ...and she scolded me like no tomorrow! #Speaker:Bengt
    
    She said it was too loud and would scare away all the fish. I never even considered that! #Speaker:Bengt
    
    Oh, but I felt no ill will toward her whatsoever! She was right, after all. #Speaker:Bengt 
    
    Anytime I want to take something like that into the water again, I double check with Vera first. #Speaker:Bengt
    
    I just hope she doesn't think of me as a threat to the ecosystem, ha ha... I'd hate to be her enemy. #Speaker:Bengt
    
    Anyone else? #Speaker:Bengt
    
    -> characters2
    
    + [Someone else...]
    
    -> characters1
    
    + [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Alright. Anything else you wondered about? #Speaker:Bengt
    
    -> questions
    


= activeQuestions

+ [Nevermind.]
    
    Actually, nevermind! #Speaker:Cleo
    
    Alrighty! #Speaker:Bengt
    
-> questions

* {BengtYapping == "active"} [You talk a lot...]

I had a little chat with Ulrich, and he says you talk a lot. #Speaker:Cleo

Bengt... have you been harassing that poor man? #Speaker:Cleo

~ BengtYapping = "solved"

Ha ha! I do like talking! #Speaker:Bengt

But between you and me, I bother Ulrich a little extra! #Speaker:Bengt

    ** [Why?]
    
    But why? #Speaker:Cleo
    
    He visits so rarely! And it's not often I can actually understand him! #Speaker:Bengt
    
    When there's an opportunity to really chat, I gotta take it! #Speaker:Bengt
    
    Plus, between you and me... #Speaker:Bengt
    
    He's so grumpy all the time, I can't help but tease him. #Speaker:Bengt
    
    *** [Stop that!]
    
    Hey, that's an old dude you're pestering! #Speaker:Cleo
    
    Respect your elders, man! #Speaker:Cleo
    
    I should, shouldn't I? Ha ha! #Speaker:Bengt
    
    Anyway, what else is up? #Speaker:Bengt
    
    -> activeQuestions
    
    *** [That's funny!]
    
    {UlrichRadioShow == "solved":
    
    I've teased him too, it actually is pretty fun! #Speaker:Cleo
    
    - else: 
    
        Okay, that actually sounds pretty fun. #Speaker:Cleo

    }
    
    Right? Oh, it feels so mean, but I just can't help myself! #Speaker:Bengt
    
    Anyway, what else is up? #Speaker:Bengt

    -> activeQuestions
    
