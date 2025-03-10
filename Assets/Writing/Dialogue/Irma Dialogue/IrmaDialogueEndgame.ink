EXTERNAL EditQuest(string questTitle, string questInfo, int questIndex)
EXTERNAL DeliverPackage(recipient)

-> IrmaDialogueEndgame

=== IrmaDialogueEndgame ===

~ DeliverPackage("Irma")

Cleo! Found anything out? #Speaker:Irma

    * [I did.]
    
-Yeah! There's this whirlpool thing, and it, like, pulls people in, and... #Speaker:Cleo

Ah, whatever! Read this letter! #Speaker:Cleo

[You hand Irma Bengt's letter.] #Speaker:Cleo

It's from Bengt? #Speaker:Irma

[Irma reads the letter, her eyebrows raising further up along her forehead with each second.] #Speaker:Irma

What the-! #Speaker:Irma

I knew they were hiding something! Agh, this whole time! #Speaker:Irma

I'll have to give them a piece of my mind some other time. I can't believe this! #Speaker:Irma

Are you... y'know, going to check it out...? #Speaker:Irma

I know I shouldn't ask you to, but we both need this, right? #Speaker:Irma

    * [Yeah, I'm going.]
    
-I'm already on the case. I haven't found the whirlpool yet, but something tells me it's out there now. #Speaker:Cleo

Whew! Okay, okay... #Speaker:Irma

I'm assuming it's safer without a second passenger onboard, so I'll stay back. #Speaker:Irma

And honestly, I'm terrified of what you're about to do! #Speaker:Irma

So just stay safe, yeah? I don't know what I'd do if yet another person went missing. #Speaker:Irma

    * [I will.]
    
-Don't worry, my boat is so sturdy now, I'm sure I'll be okay! #Speaker:Cleo

Gosh, your confidence is rubbing off on me... I'll be cheering you on from here! #Speaker:Irma

~ EditQuest("Windward", "Find the whirlpool", 0)

-> END