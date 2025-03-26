INCLUDE ../globals.ink
EXTERNAL Fade(int time)
EXTERNAL Give()

-> fillerVera

=== fillerVera ===

Good to see ya! #Speaker:Vera

How's it hanging? #Speaker:Vera

-> questions

= questions

    + [I heard something...]
    
    I heard something interesting... #Speaker:Cleo
    
    I'm all ears! What's up? #Speaker:Vera
    
    -> activeQuestions

    + [About you...]
    
    I've got some questions about you. #Speaker:Cleo

    Yeah? Let's hear it. #Speaker:Vera
    
    -> veraQuestions
    
    * [About the other islanders...]
    
    What are your thoughts on the others in the archipelago? #Speaker:Cleo
    
    That depends. Who are we talking 'bout? #Speaker:Vera
    
    -> characters1
    
    * [Bye.]
    
    I need to get going. #Speaker:Cleo
    
    Have a good one! #Speaker:Vera

-> END


= veraQuestions

    * [About your time here.]
    
    What's your story? How did you come to live here? #Speaker:Cleo
    
    I happened upon this place some... twenty years ago? #Speaker:Vera
    
    I was exploring, you see. Wasn't expecting to be smacked outta the waters by that mean ol' Kraken. #Speaker:Vera
    
    Sodding beast caught my eye, though! Got me thinking of what other creatures this archipelago houses. #Speaker:Vera
    
    Figured I could stay a while, check things out. #Speaker:Vera
    
    I can't say I intended on sticking around this long. Got a little attached in the process, you see. #Speaker:Vera
    
    The ecosystem here is unlike anything I've ever seen! I've been keeping an eye on it ever since. #Speaker:Vera
    
    Not much else to say, really. Anything else you're curious 'bout? #Speaker:Vera
    
    -> veraQuestions
    
    * [Do you want to leave?]
    
    Do you ever want to leave this place? #Speaker:Cleo
    
    Only way I'd ever leave is if you drag me out of here. And even then, I wouldn't make it easy for you! #Speaker:Vera
    
    I have no intention of leaving all this behind. Sure, there are some downsides to living here... #Speaker:Vera
    
    ...but I've made this place my home, and these fish won't look after themselves! #Speaker:Vera
    
    So, no. I plan on staying. #Speaker:Vera
    
    Anything else? #Speaker:Vera

    -> veraQuestions
    
    * [That's all.]
    
    That's all I wanted to know. #Speaker:Cleo
    
    Moving on, then? #Speaker:Vera
    
    -> questions


= characters1

    * [Bengt?]
    
    What about Bengt? #Speaker:Cleo
    
    Bengt is a good sort! And having a boat mechanic around does wonders for our fishing practices. #Speaker:Vera
    
    Anytime one of our fishers bumps into a boulder or what have you, I send them out to Bengt for a fix-up. #Speaker:Vera
    
    Sometimes they even come back with a brand new motor, or a spiffed up hull. #Speaker:Vera
    
    Agh, but I keep telling him not to do that! #Speaker:Vera
    
    Can't have our fishers getting all careless out there thinking they'll get free upgrades should they crash. #Speaker:Vera
    
    "I got ahead of myself, sorry!" he says, like it's nothing! #Speaker:Vera
    
    Anyway, got anyone else in mind? #Speaker:Vera
    
    -> characters1
    
    * [Sigrid?]
    
    What about Sigrid? #Speaker:Cleo
    
    Ha! Has she badmouthed me? #Speaker:Vera
    
    In case you didn't know already, her and I are... at slight odds. #Speaker:Vera
    
    Rather one-sided on her end, if you ask me. #Speaker:Vera
    
    She seems to think I don't care for the people living here, that I put the wildlife above them. #Speaker:Vera
    
    Let me tell ya, that couldn't be further from the truth! #Speaker:Vera
    
    The Kraken does pose a threat to our boaters, yes, but I train them to avoid that darned thing! #Speaker:Vera
    
    Hell, part of her job is to keep an eye out for them. #Speaker:Vera
    
    Should push come to shove, I trust that she'll keep them safe. #Speaker:Vera
    
    But enough about that whole debate. Got anyone else in mind? #Speaker:Vera
    
    -> characters1
    
    + [Someone else...]
    
    -> characters2
    
    * [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Moving on, then? #Speaker:Vera

-> questions


= characters2

    * [Irma?]
    
    What about Irma? #Speaker:Cleo
    
    She was a total shut-in when she first came here... #Speaker:Vera
    
    Any contact I had with her was always through her sister! #Speaker:Vera
    
    I didn't get to know Irma herself until after... well, ya know. #Speaker:Vera
    
    Ah, but we've become friends since then! Had a couple beers with her and we kicked the shit. #Speaker:Vera
    
    I taught her how to properly dispose of electronics without it harming the ecosystem... #Speaker:Vera
    
    ...and she tried to teach me a thing or two about radio what-have-yous. #Speaker:Vera
    
    Frankly, it all went in one ear and out the other. #Speaker:Vera
    
    Got anyone else in mind? #Speaker:Vera
    
    -> characters2
    
    * [Ulrich?]
    
    What about Ulrich? #Speaker:Cleo
    
    Ulrich and I get along about as well as sharks and remoras! #Speaker:Vera
    
    Oh... #Speaker:Cleo
    
    Wait, is that bad or good? #Cleo
    
    Ha! It means we get along great. #Speaker:Vera
    
    He shows a great deal of care for his plants. Same way I care for our ocean life. #Speaker:Vera
    
    We both have ecosystems to tend to, in a way. One just happens to be dryer than the other. #Speaker:Vera
    
    They're not all that separate, either. #Speaker:Vera
    
    Flora and fauna affects the environment, and the environment affects the waters. #Speaker:Vera
    
    Now, if only those plants didn't mess so much with his sinuses. That's some bad luck he has. #Speaker:Vera
    
    Got anyone else in mind? #Speaker:Vera
    
    -> characters2
    
    + [Someone else...]
    
    -> characters1
    
    * [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Moving on, then? #Speaker:Vera

    -> questions
    
    
= activeQuestions 

+ [Nevermind.]
    
    Actually, nevermind! #Speaker:Cleo
    
    Moving on, then? #Speaker:Vera
    
-> questions

* {VeraCoffeeMystery == "solved" and coffeeTasted == ""} [Could I try your coffee?]

    Okay, the curiosity is killing me! Can I try your coffee? #Speaker:Cleo
    
    Ha! Of course you can! I'll fetch you a cup. #Speaker:Vera 
    
    ~ Fade(2.0)
    
    All done! Have a sip, why don't you? #Speaker:Vera
    
    ~ Give()
    
    [You take a whiff of the coffee.] #Speaker:Cleo
    
    [Aside from a faint fishy odor, it seems... relatively normal.] #Speaker:Cleo
    
    [When you take a sip, however, you notice the coffee is gooey and thick...] #Speaker:Cleo
    
    [The taste is downright rotten! You can't swallow this... concoction! Your eyes are tearing up!] #Speaker:Cleo
    
    [You spit the coffee back out into the cup.] #Speaker:Cleo
    
    Blehh! Oh, geez! That's so not happening! #Speaker:Cleo
    
    I warned you! It's got a real kick, doesn't it? #Speaker:Vera
    
    That's an understatement! Gosh... You seriously drink this every morning? #Speaker:Cleo
    
    A cup of fish goo a day keeps the doctor away! #Speaker:Vera
    
    I don't think that's how the saying goes. #Speaker:Cleo
    
    Ha! Anyway, was that all? #Speaker:Vera
    
    ~ coffeeTasted = "true"

    -> activeQuestions

* {VeraCoffeeMystery == "active"} [Your coffee...]

    Sigrid told me you put something weird in your coffee. #Speaker:Cleo
    
    Ha! Of course she'd say that. #Speaker:Vera
    
    She's not wrong, though. I do include a secret ingredient. #Speaker:Vera
    
    What is it? #Speaker:Cleo
    
    Glyco-protein! #Speaker:Vera
    
    ~ VeraCoffeeMystery = "solved"

    Or, well, slime. I scrape it off the fish we catch. #Speaker:Vera
    
    ** [Wait, what?]
    
    You put... slime... in your coffee...? Hold on, is it even safe?! #Speaker:Cleo
    
    Did I mention the ecosystem here is unique to this archipelago? #Speaker:Vera
    
    There's this fish around here that produces that slime, and it's just teeming with nutrition! #Speaker:Vera
    
    Sure, the first few attempts gave me the runs... #Speaker:Vera
    
    Ew. #Speaker:Cleo
    
    But then I figured out how to brew out any icky bacteria without losing what makes the slime healthy. #Speaker:Vera
    
    I've been chugging it every morning since then! Really, the taste is fine when you're used to it. #Speaker:Vera
    
    *** [Cool!]
    
    Y'know... that's actually kinda cool! #Speaker:Cleo
    
    Right?! #Speaker:Vera
    
    There's a ton of interesting things you can discover if you buckle down and commit to trying them. #Speaker:Vera
    
    Wanna try a cup? #Speaker:Vera
    
    **** [Nope!]
    
    Uh, I wouldn't go that far..! #Speaker:Cleo
    
    **** [Sure!]
    
    Yeah, totally! #Speaker:Cleo
    
    ~ coffeeTasted = "true"
    
    That's the spirit! I'll brew you a cup. #Speaker:Vera
    
    ~ Fade(2.0)
    
    All done! Have a sip, why don't you? #Speaker:Vera
    
    ~ Give()
    
    [You take a whiff of the coffee.] #Speaker:Cleo
    
    [Aside from a faint fishy odor, it seems... relatively normal.] #Speaker:Cleo
    
    [When you take a sip, however, you notice the coffee is gooey and thick...] #Speaker:Cleo
    
    [The taste is downright rotten! You can't swallow this... concoction! Your eyes are tearing up!] #Speaker:Cleo
    
    [You spit the coffee back out into the cup.] #Speaker:Cleo
    
    Blehh! Oh, geez! That's so not happening! #Speaker:Cleo
    
    I warned you! It's got a real kick, doesn't it? #Speaker:Vera
    
    That's an understatement! Gosh... You seriously drink this every morning? #Speaker:Cleo
    
    A cup of fish goo a day keeps the doctor away! #Speaker:Vera
    
    I don't think that's how the saying goes. #Speaker:Cleo
    
    *** [Ew!]
    
    Urgh, I regret asking about this... #Speaker:Cleo
    
    ** [I'm done asking...]
    
    I'm already regretting this topic! The less I know, the better. #Speaker:Cleo
    
    -Ha! Anyway, was that all? #Speaker:Vera
    
    -> activeQuestions
    
    