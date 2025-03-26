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
    
    Youngsters need to be out and about, not stay inside poking at... thingamajigs! #Speaker:Ulrich
    
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