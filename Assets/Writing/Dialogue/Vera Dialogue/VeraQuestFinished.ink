EXTERNAL EditQuest(int ID, int step)
EXTERNAL Fade(int time)
INCLUDE ../globals.ink

-> VeraDialogue2

=== VeraDialogue2 ===

Phew, you're back! #Speaker:Vera

So, what did Ulrich say? #Speaker:Vera

    * [These flowers...]
    
-He gave me these flowers. #Speaker:Cleo

I'm pretty sure he wants us to make tea out of them...? #Speaker:Cleo

[Vera snatches the flowers out of your hand.] #Speaker:Vera

Make tea, got it! Roger that! Thanks! #Speaker:Vera

[She hurries into her hut.] #Speaker:Vera

~ Fade(2.5)

[....A few moments later, she comes back with a cup of tea in hand.] #Speaker:Vera

Lemme tell ya, I got nervous for a sec there! #Speaker:Vera

I appreciate the help! You're a good sort. #Speaker:Vera

So, what was it you needed? A boat upgrade, right? #Speaker:Vera 

Give me a sec and I'll hook you up. #Speaker:Vera

~ Fade(3.5)

//[Vera downs her tea before she gets to work on installing your upgrade.] #Speaker:Vera

.... All done! #Speaker:Vera

Are you heading out or is there anything else I could help you with? #Speaker:Vera

    * [My parents...]
    
I'm looking for my parents. Or at least, any trace of them... This was their boat. #Speaker:Cleo

{parentsAskedAbout == "":

~ parentsAskedAbout = "true"

}

You wouldn't happen to know anything, would you? #Speaker:Cleo

I thought I recognized that old thing. #Speaker:Vera

I'm sorry, kiddo, but I can't help you with that. I've no clue where your folks might've gone. #Speaker:Vera

I see... #Speaker:Cleo

    * [Nope.]
    
Nah, I'm good! #Speaker:Cleo

-Anyway, thanks for the upgrade. #Speaker:Cleo

No problem! Let me know if there's anything else I could help you with. #Speaker:Vera

    * [Bye.]
    
-I need to get going. #Speaker:Cleo

~ EditQuest(4, 4)
~ VeraQuest = "complete"
{
- SigridQuest == "complete" and UlrichQuest == "complete" and IrmaQuest == "complete":
~ EditQuest(0, 0)
}

Don't be a stranger! #Speaker:Vera

-> END