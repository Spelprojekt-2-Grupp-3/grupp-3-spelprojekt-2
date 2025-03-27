EXTERNAL EditQuest(int ID, int step)
EXTERNAL Fade(float time)
INCLUDE ../globals.ink

-> SigridDialogue2

=== SigridDialogue2 ===

I got your coffee for you! #Speaker:Cleo

That solves my problem. Thanks. #Speaker:Sigrid

I'll get you your upgrade. #Speaker:Sigrid

~ Fade(3.5)

//[Sigrid gets straight to work on installing your new upgrade.] #Speaker:Sigrid

//[When she finishes, she dusts her palms off and looks at you.] #Speaker:Sigrid

..... Is there anything else? #Speaker:Sigrid

    * [My parents...]

I'm trying to find out what happened to my parents. They were last seen at this archipelago. #Speaker:Cleo

{parentsAskedAbout == "":

~ parentsAskedAbout = "true"

}


If a pair of outsiders passed by here, I would've noticed. You sure you're at the right place? #Speaker:Sigrid

I received this letter from someone here. #Speaker:Cleo
    
[You hand Sigrid the letter.]

[She glances at your boat in the harbor.] #Speaker:Sigrid 

That boat was your parents'? #Speaker:Sigrid

Sorry to say, I have no clue where your parents went. #Speaker:Sigrid

I'm sure Bengt already told you, but we found that boat a very long time ago. #Speaker:Sigrid 

Even if we did know something, we might not remember it. #Speaker:Sigrid

Apologies. #Speaker:Sigrid

That's alright. I'll keep looking. #Speaker:Cleo
    
[Sigrid shifts awkwardly.] #Speaker:Sigrid

    * [Nope.]
    
No, that's it! #Speaker:Cleo

-Shouldn't you get going? #Speaker:Sigrid

I don't have much else for you. #Speaker:Sigrid

    * [Bye.]
    
-I'll be heading out then. Thanks again. #Speaker:Cleo

~ EditQuest(1, 4)
~ SigridQuest = "complete"
{
- IrmaQuest == "complete" and UlrichQuest == "complete" and VeraQuest == "complete":
~ EditQuest(0, 0)
}

No problem. #Speaker:Sigrid

-> END