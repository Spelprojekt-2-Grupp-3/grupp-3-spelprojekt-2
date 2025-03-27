INCLUDE ../globals.ink

-> fillerIrma

=== fillerIrma ===

... Oh, hey there! #Speaker:Irma

Something on your mind? #Speaker:Irma

-> questions

= questions

    + [I heard something...]
    
    I heard something interesting... #Speaker:Cleo
    
    Ooh, what is it? #Speaker:Irma
    
    -> activeQuestions

    + [About you...]
    
    I've got some questions about you. #Speaker:Cleo

    And I've got answers! Go ahead. #Speaker:Irma
    
    -> irmaQuestions
    
    + [About the other islanders...]
    
    What are your thoughts on the others in the archipelago? #Speaker:Cleo
    
    Mind narrowing it down for me? #Speaker:Irma
    
    -> characters1
    
    * [Bye.]
    
    I need to get going. #Speaker:Cleo
    
    Don't be a stranger! #Speaker:Irma

-> END


= irmaQuestions

    * [About your time here.]
    
    What's your story? How did you come to live here? #Speaker:Cleo
    
    I came here with my sister around ten years ago. No, longer than that. #Speaker:Irma
    
    It's not much of a story, if I'm being honest. #Speaker:Irma
    
    We stumbled upon this place, thought it was interesting, and decided to stay for a while. #Speaker:Irma
    
    If I'm being honest, I only stayed here for her sake. She's the explorer between the two of us. #Speaker:Irma
    
    As you can probably imagine, I prefer to stay inside with my electronics. #Speaker:Irma
    
    And then, y'know, some things happened and now I'm here without her. #Speaker:Irma
    
    Anyway, let's move on. Got any other questions? #Speaker:Irma

    -> irmaQuestions
    
    * [Do you want to leave?]
    
    Do you ever want to leave this place? #Speaker:Cleo
    
    No! #Speaker:Irma
    
    I mean, I do! Maybe. #Speaker:Irma
    
    Just not without my sister. I can't. #Speaker:Irma
    
    That's all I have to say about it. Next question! #Speaker:Irma
    
    -> irmaQuestions
    
    * [That's all.]
    
    That's all I wanted to know. #Speaker:Cleo
    
    Roger that. What else do you want to talk about? #Speaker:Irma
    
    -> questions


= characters1

    * [Bengt?]
    
    What about Bengt? #Speaker:Cleo
    
    I'm not sure if he realizes it, but Bengt has been a huge help to me! #Speaker:Irma
    
    It was hard for me to stay positive after my sister went missing. I just wanted to keep to myself. #Speaker:Irma
    
    But then he came to me looking for help with a project of his. #Speaker:Irma
    
    I swear, I hadn't left my house in weeks at that point. It was nice to be around another person again. #Speaker:Irma
    
    I didn't realize I needed that. It made me feel... a little more at home here. #Speaker:Irma
    
    Since then, each day has felt a little easier. I opened up more, made some friends, had some laughs. #Speaker:Irma
    
    I owe him for that. #Speaker:Irma
    
    But enough about that! Is there anyone else you're curious about? #Speaker:Irma
    
    -> characters1
    
    * [Sigrid?]
    
    What about Sigrid? #Speaker:Cleo
    
    Sigrid keeps to herself a lot. It's hard to get a read on her. #Speaker:Irma
    
    Plus, she seems kinda awkward anytime I talk to her... #Speaker:Irma
    
    She doesn't mince her words around anyone else, I've noticed. Only me. #Speaker:Irma
    
    At first I figured she's just closed off to outsiders, but it's been over a decade! #Speaker:Irma
    
    Wouldn't she consider me a fellow resident at this point? Agh, it's frustrating! #Speaker:Irma
    
    I wish I had more to say. Is there anyone else you're curious about? #Speaker:Irma
    
    -> characters1
    
    + [Someone else...]
    
    -> characters2
    
    * [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Roger that. What else do you want to talk about? #Speaker:Irma

-> questions


= characters2

    * [Ulrich?]
    
    What about Ulrich? #Speaker:Cleo
    
    Ulrich seems to be as allergic to electronics as he is to his own plants! #Speaker:Irma
    
    I have no common ground with him! And I can hardly understand him in the first place. #Speaker:Irma
    
    It doesn't seem like the archipelago was very technologically advanced before I got here. #Speaker:Irma
    
    From what I know, Ulrich has lived here a very long time. He must prefer to stick to his old ways. #Speaker:Irma
    
    That's not to say there's any ill will between us! #Speaker:Irma
    
    He even brought me a bunch of flowers as condolences after my sister disappeared. #Speaker:Irma
    
    At least, I think that's why he brought them? I, uh, couldn't decipher what he was saying. #Speaker:Irma
    
    Is there anyone else you're curious about? #Speaker:Irma
    
    -> characters2
    
    * [Vera?]
    
    What about Vera? #Speaker:Cleo
    
    I'm totally lost on all this ecosystem stuff she's so passionate about... #Speaker:Irma
    
    I know it's important, and I make sure to dispose of my electronic scraps safely so I don't mess it up! #Speaker:Irma
    
    But to dedicate so much time and energy into a bunch of water... I mean, wow. #Speaker:Irma
    
    That was a good kind of "wow", by the way! Her work is impressive, really! #Speaker:Irma
    
    She's an avid explorer, as well. Ah, my sister used to chat with her day and night! #Speaker:Irma
    
    Honestly, my sister has much more in common with her than I do. #Speaker:Irma
    
    Still, Vera and I get along well enough on our own. #Speaker:Irma
    
    Is there anyone else you're curious about? #Speaker:Irma
    
    -> characters2
    
    + [Someone else...]
    
    -> characters1
    
    * [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Roger that. What else do you want to talk about? #Speaker:Irma

    -> questions
    
    
= activeQuestions

+ [Nevermind.]
    
    Actually, nevermind! #Speaker:Cleo
    
    Roger that. What else do you want to talk about? #Speaker:Irma
    
-> questions

* {UlrichRadioShow == "standby"} [Ulrich's radio show...]

    What was it you said, that Ulrich listens to some radio show...? #Speaker:Cleo

    Oh, that! #Speaker:Irma

    [Irma looks around and then leans in close, like she's about to spill a secret.] #Speaker:Irma

    There's only one program our radios can pick up on from all the way out here... #Speaker:Irma

    It's this out-loud reading of a really, REALLY sappy book series. #Speaker:Irma

    I caught Ulrich listening to it once, and I'm pretty sure I caught tears in his eyes! Like, actual tears! #Speaker:Irma

    I don't have the guts to bring it up to him, myself! Gosh, that would be scary! #Speaker:Irma

    But if you're ever feeling brave enough to do it, don't tease him too hard. #Speaker:Irma

    ~ UlrichRadioShow = "active"

Anyway, what else is going on? #Speaker:Irma

-> activeQuestions

+ {IrmaRiddles == "solved" and riddlesSolved != "true"} [I want to solve riddles.]

    I'd like to take a crack at those riddles! #Speaker:Cleo
    
    Now you're speaking my language! How about... #Speaker:Irma
    
    {riddlesSolved == "": -> riddle1 }
    
    {riddlesSolved == "1": -> riddle2 }
    
    {riddlesSolved == "2": -> riddle3 }
    

* {IrmaRiddles == "active"} [You like riddles?]

    Vera mentioned you love riddles. #Speaker:Cleo
    
    ~ IrmaRiddles = "solved"
    
    Oh, do I ever! #Speaker:Irma
    
    Then, how about you answer this... #Speaker:Cleo
    
    Where do you take a sick boat? #Speaker:Cleo
    
    Pff, the dock! #Speaker:Irma
    
    Aw, okay that one was easy! How about... #Speaker:Cleo
    
    What's a vampire's favorite boat to travel on? #Speaker:Cleo
    
    Hmm... #Speaker:Irma
    
    Aha! A blood vessel! #Speaker:Irma
    
    Are all your riddles boat-related, by the way? #Speaker:Irma
    
    My riddle arsenal is a bit limited, I admit. #Speaker:Cleo
    
    How about I challenge you to a few? You game? #Speaker:Irma
    
        ** [Nah.]
        
    -> exitRiddles
    
        ** [Sure!]
        
    Oh! Totally, yes! #Speaker:Cleo
    
    Sweet! How about... #Speaker:Irma
    
    -> riddle1
    
    = riddle1
    
    A girl has as many brothers as sisters, but each brother has only half as many brothers as sisters... #Speaker:Irma
    
    How many brothers and sisters are there in the family?
    
    * [4 sisters, 3 brothers.]
    
    Is there... four sisters, three brothers? #Speaker:Cleo
    
    Ding ding ding! That's right! #Speaker:Irma
    
    ~ riddlesSolved = "1"
    
    Are you ready for another one? #Speaker:Irma
    
    ** [Nah.]
        
        -> exitRiddles
        
    ** [Yes!]
    
        Hit me! #Speaker:Cleo
        
        That's the spirit! #Speaker:Irma
        
        -> riddle2
        
    * [2 sisters, 4 brothers.]
    
    Is there... two sisters, four brothers? #Speaker:Cleo

    Oof, nope! I'll repeat it for you... #Speaker:Irma
    
    -> riddle1

    * [5 sisters, 2 brothers.]
    
    Is there... five sisters, two brothers? #Speaker:Cleo
    
    Oof, nope! I'll repeat it for you... #Speaker:Irma
    
    -> riddle1
    
    + [Repeat that.]
    
    Hmm... could you repeat the riddle for me? #Speaker:Cleo
    
    Of course! #Speaker:Irma
    
    -> riddle1
        
        = riddle2
        
        A is the brother of B. B is the brother of C. C is the father of D. #Speaker:Irma
        
        How is D related to A? #Speaker:Irma
        
        * [A's father.]
        
        D is A's father! #Speaker:Cleo
        
        No, no! I'll repeat it for you... #Speaker:Irma
    
        -> riddle2
        
        * [A's uncle.]
        
        D is A's uncle! #Speaker:Cleo

        That's correct! #Speaker:Irma
        
        ~ riddlesSolved = "2"
        
        How about just one more? #Speaker:Irma
        
            ** [Nah.]
        
        -> exitRiddles
    
            ** [Yes!]
    
            Hit me! #Speaker:Cleo
        
            That's the spirit! #Speaker:Irma
        
            -> riddle3
            
            * [A's brother.]
        
        D is A's brother! #Speaker:Cleo
        
        No, no! I'll repeat it for you... #Speaker:Irma
    
        -> riddle2
        
        + [Repeat that.]
        
        Hmm... could you repeat the riddle for me? #Speaker:Cleo
    
        Of course! #Speaker:Irma
    
        -> riddle2
            
            = riddle3
            
            I dance in the wind and I bow to a storm... #Speaker:Irma
            
            I grow best in temperatures that are warm... #Speaker:Irma
            
            By the riverbeds, I like to grow, where the people in boats like to row... #Speaker:Irma
            
            What am I? #Speaker:Irma
            
            * [Seaweed!]
            
            You're seaweed! #Speaker:Cleo
            
            That I am not! I'll repeat it for you... #Speaker:Irma
    
            -> riddle3
            
            * [A reed!]
            
            You're a reed! #Speaker:Cleo
            
            That I am! You got me all figured out! #Speaker:Irma
            
            I'm all out of riddles now, so how about you give that brain of yours a rest? #Speaker:Irma
            
            Phew, I sure will! #Speaker:Cleo
            
            ~ riddlesSolved = "true"
            
            Right, was there anything else you wondered about? #Speaker:Irma
            
            -> activeQuestions
            
            * [A water lily!]
            
            You're a water lily! #Speaker:Cleo
            
            That I am not! I'll repeat it for you... #Speaker:Irma
            
            -> riddle3
            
            + [Repeat that.]
            
            Hmm... could you repeat the riddle for me? #Speaker:Cleo
    
            Of course! #Speaker:Irma
    
        -> riddle3
        
        
    ** [No.]
    
    -> exitRiddles
    
    
    = exitRiddles
        
    Maybe another time? #Speaker:Cleo
    
    Bummer! That's alright, let me know whenever you'd like to work your brain out with me! #Speaker:Irma
    
    Sure, I'll do that. #Speaker:Cleo
    
    Was there anything else, though? #Speaker:Irma

-> questions