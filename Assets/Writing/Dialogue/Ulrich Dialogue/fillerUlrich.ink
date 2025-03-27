INCLUDE ../globals.ink

{UlrichQuest == "complete":

-> fillerUlrich

- else:

-> fillerUlrichCongested

}

=== fillerUlrichCongested ===

Mugh. #Speaker:Ulrich

Mhnurhm hurmm? #Speaker:Ulrich

-> questionsCongested

= questionsCongested

    + [I heard something...]
    
    I heard something interesting... #Speaker:Cleo
    
    Hrmh? #Speaker:Ulrich
    
    [It might be best to ask him when he's... decipherable.] #Speaker:Cleo
    
    Y'know what? Nevermind! #Speaker:Cleo
    
    Hmrnm... #Speaker:Ulrich
    
    -> questionsCongested

    + [About you...]
    
    I've got some questions about you. #Speaker:Cleo

    Hmnh. #Speaker:Ulrich
    
    -> ulrichQuestionsCongested
    
    + [About the other islanders...]
    
    What are your thoughts on the others in the archipelago? #Speaker:Cleo
    
    Hhnrm? Hmrnnuhm hm. #Speaker:Ulrich
    
    -> characters1Congested
    
    * [Bye.]
    
    I need to get going. #Speaker:Cleo
    
    Hrm. #Speaker:Ulrich

-> END


= ulrichQuestionsCongested

    * [About your time here.]
    
    What's your story? How did you come to live here? #Speaker:Cleo
    
    Hmrhnm Huhhm. Herrmn. Humghhm. Hm. #Speaker:Ulrich
    
    -> ulrichQuestionsCongested
    
    * [Do you want to leave?]
    
    Do you ever want to leave this place? #Speaker:Cleo
    
    Hmehm. Mhmmgrrnm. Hrrm... #Speaker:Ulrich
    
    -> ulrichQuestionsCongested
    
    + [That's all.]
    
    That's all I wanted to know. #Speaker:Cleo
    
    Hm. #Speaker:Ulrich
    
    -> questionsCongested


= characters1Congested

    * [Bengt?]
    
    What about Bengt? #Speaker:Cleo
    
    Hmernmm. Hgnrm. Hmnghm. #Speaker:Ulrich
    
    -> characters1Congested
    
    * [Sigrid?]
    
    What about Sigrid? #Speaker:Cleo
    
    Hrrmmm heernm Hngrnm. Hmrmm. #Speaker:Ulrich
    
    -> characters1Congested
    
    + [Someone else...]
    
    -> characters2Congested
    
    + [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Hm. #Speaker:Ulrich

-> questionsCongested


= characters2Congested

    * [Irma?]
    
    What about Irma? #Speaker:Cleo
    
    Hrmm hrrghm hurgnmm. Hermnh. #Speaker:Ulrich
    
    -> characters2Congested
    
    * [Vera?]
    
    What about Vera? #Speaker:Cleo
    
    Hrmn. Hehrher. Mumgh. #Speaker:Ulrich
    
    -> characters2Congested
    
    + [Someone else...]
    
    -> characters1Congested
    
    + [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Hm. #Speaker:Ulrich

-> questionsCongested



=== fillerUlrich ===

[Ulrich clears his throat.] #Speaker:Ulrich

What now? #Speaker:Ulrich

-> questions

= questions

    + [I heard something...]
    
    I heard something interesting... #Speaker:Cleo
    
    Well? Out with it. #Speaker:Ulrich
    
    -> activeQuestions

    + [About you...]
    
    I've got some questions about you. #Speaker:Cleo

    Out with it. #Speaker:Ulrich
    
    -> ulrichQuestions
    
    + [About the other islanders...]
    
    What are your thoughts on the others in the archipelago? #Speaker:Cleo
    
    What, you expect me to prepare a list for you? Narrow it down for me. #Speaker:Ulrich
    
    -> characters1
    
    * [Bye.]
    
    I need to get going. #Speaker:Cleo
    
    Tschüss. #Speaker:Ulrich

-> END


= ulrichQuestions

    * [About your time here.]
    
    What's your story? How did you come to live here? #Speaker:Cleo
    
    I was born here. I continued living here. End of story. #Speaker:Ulrich
    
    What about the german accent? #Speaker:Cleo
    
    What accent? What are you talking about?! #Speaker:Ulrich
    
    Oh, nevermind then...? #Speaker:Cleo
    
    Now, enough about that. #Speaker:Ulrich
    
    -> ulrichQuestions
    
    * [Do you want to leave?]
    
    Do you ever want to leave this place? #Speaker:Cleo
    
    Nein! #Speaker:Ulrich
    
    Don't you think I would have left while I was still in my prime, if I didn't like it here? #Speaker:Ulrich
    
    Pfui, not that it would've been easy. Darn Kraken. #Speaker:Ulrich
    
    This island will be my grave, you hear me?! #Speaker:Ulrich
    
    Okay! Okay... Geez. #Speaker:Cleo
    
    What else is it? #Speaker:Ulrich
    
    -> ulrichQuestions
    
    + [That's all.]
    
    That's all I wanted to know. #Speaker:Cleo
    
    Hm. #Speaker:Ulrich
    
    -> questions


= characters1

    * [Bengt?]
    
    What about Bengt? #Speaker:Cleo
    
    Bengt is friendly and does important work around here, ja... #Speaker:Ulrich
    
    But, scheiße! He is TOO friendly! Darn chatterbox talks my ear off... #Speaker:Ulrich
    
    [Ulrich is obviously complaining, but there's something soft, fond even, about his tone.] #Speaker:Ulrich
    
    Now, who else? #Speaker:Ulrich
    
    -> characters1
    
    * [Sigrid?]
    
    What about Sigrid? #Speaker:Cleo
    
    Sigrid keeps things short and direct. Doesn't blabber on like some others. #Speaker:Ulrich
    
    Every time I run into Bengt, he INSISTS on "chatting it up" for hours and hours... #Speaker:Ulrich
    
    You know what Sigrid does? She nods and lets me be on my way! #Speaker:Ulrich
    
    Let me tell you, we need more of that around here! #Speaker:Ulrich
    
    Now, who else? #Speaker:Ulrich
    
    -> characters1
    
    + [Someone else...]
    
    -> characters2
    
    + [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Hm. #Speaker:Ulrich

-> questions


= characters2

    * [Irma?]
    
    What about Irma? #Speaker:Cleo
    
    Irma is a smart girl, but all that tech mumbo jumbo means nothing to me! #Speaker:Ulrich
    
    Youngsters need to be out and about, not staying inside poking at... thingamajigs! #Speaker:Ulrich
    
    It's fine for an old coot like me to stick to myself, but Irma... #Speaker:Ulrich
    
    Pfui, just do me a favor and tell that girl to go out more! #Speaker:Ulrich
    
    Now, who else? #Speaker:Ulrich
    
    -> characters2
    
    * [Vera?]
    
    What about Vera? #Speaker:Cleo
    
    Ja, we get along well. And I appreciate the nasal spray she gets me. #Speaker:Ulrich
    
    If only she wasn't so darn stubborn! #Speaker:Ulrich
    
    I tell her to let me help in exchange for medicine and she refuses! She pities me! #Speaker:Ulrich
    
    And she slaps me on my back and tells me to "not worry about it". #Speaker:Ulrich
    
    The nerve! Pfui! #Speaker:Ulrich
    
    Now, who else? #Speaker:Ulrich
    
    -> characters2
    
    + [Someone else...]
    
    -> characters1
    
    + [That's all.]
    
    That's all I wanted to know! #Speaker:Cleo
    
    Hm. #Speaker:Ulrich

-> questions



= activeQuestions

+ [Nevermind.]
    
    Actually, nevermind! #Speaker:Cleo
    
    [Ulrich grumbles.] #Speaker:Ulrich
    
-> questions

* {UlrichRadioShow == "active"} [The radio show...]

Irma let me in on some very interesting information... #Speaker:Cleo

That sappy radio show... what's it about? #Speaker:Cleo

~ UlrichRadioShow = "solved"

Hä?! Verdammt! That is none of your business! #Speaker:Ulrich

Aw, c'mon... I'm so curious! She said it even made you cry! #Speaker:Cleo

What I listen to is none of your business! #Speaker:Ulrich

-> convinceUlrich

= convinceUlrich

[Seems like you'll need to convince him to spill the beans...] #Speaker:Ulrich

[But how?] #Speaker:Ulrich

    * [Barter!] 
    
    How about I trade you something to loosen up those lips, huh? #Speaker:Cleo
    
    Whaddaya want? I'll hook you up! #Speaker:Cleo
    
    Pfui, you think I'll let up that easy?! This is a matter of pride! #Speaker:Ulrich
    
    -> convinceUlrich
    
    * [Beg!]
    
    Please, please, please! Tell me! I so badly want to know! #Speaker:Cleo
    
    Didn't I help you with your allergies?! C'mon... #Speaker:Cleo
    
    Nein! I already repaid that favor, remember?! #Speaker:Ulrich
    
    Don't you try to trick me now! #Speaker:Ulrich
    
    -> convinceUlrich
    
    * [Sympathize!]
    
    Y'know, it's okay! I love that sappy stuff too! #Speaker:Cleo
    
    One time, I watched this movie that had me full-blown weeping at the end! #Speaker:Cleo
    
    You wanna know why? Because the love interest proposed to the main character... #Speaker:Cleo
    
    ... at her DEATHBED! #Speaker:Cleo
    
    Hrnmm... #Speaker:Ulrich
    
    [Ulrich grumbles for a moment, and then a look of defeat sets in.] #Speaker:Ulrich
    
    Fine! I'll tell you... #Speaker:Ulrich
    
    It's a tragedy. About a fisherwoman that falls in love with a shipwrecked sailor. #Speaker:Ulrich
    
    And...? #Speaker:Cleo
    
    ... the fisherwoman is married to the sailor's captain. #Speaker:Ulrich
    
    Gasp! Ulrich, that's scandalous! I didn't take you for a drama hound! #Speaker:Cleo
    
    Are you making fun of me?! #Speaker:Ulrich
    
    ** [Yes.]
    
    Well, maybe a little. #Speaker:Cleo
    
    Pfui! Darn brat! #Speaker:Ulrich
    
    ** [No.]
    
    No, no! I might actually give that program a try! #Speaker:Cleo
    
    Forbidden romance, AND a tragedy? I'll be crying for days! #Speaker:Cleo
    
    ... they broadcast every sunday. #Speaker:Ulrich
    
    Oh, I'll be tuning in for sure. #Speaker:Cleo
    
    -No more about this! What else do you want?! #Speaker:Ulrich
    
    -> questions

