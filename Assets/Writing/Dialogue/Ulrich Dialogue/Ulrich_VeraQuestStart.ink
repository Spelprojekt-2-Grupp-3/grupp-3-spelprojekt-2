EXTERNAL EditQuest(int ID, int step)
EXTERNAL MinigameQuest(int ID, int step)
INCLUDE ../globals.ink

{UlrichQuest == "complete":

-> UlrichDialogueVera

- else:

-> UlrichDialogueVeraCongested

}


=== UlrichDialogueVeraCongested ===

Hrnemrm? #Speaker:Ulrich

    * [About Vera...]
    
-Uh, Vera was stung by... something. In the water. #Speaker:Cleo 

She says she's gonna puke like crazy if she doesn't get treatment! #Speaker:Cleo

Do you have anything that could help? #Speaker:Cleo

Hejrm mrmrnh ghrhn... Hrmn!! #Speaker:Ulrich

[Ulrich points at the flower pots by his house.] #Speaker:Ulrich

[He appears a little confused, grumbling and scratching his head.] #Speaker:Ulrich

    * [What's up?]
    
-What's going on? Anything I can help with? #Speaker:Cleo

Hrmhrm... Hremn! #Speaker:Ulrich

[It looks like his pots are out of order.] #Speaker:Cleo

[Maybe organizing them could jog his memory?] #Speaker:Cleo

    * [I'll help.]
    
-I'll sort those pots for you, if it'll help? #Speaker:Cleo

Hrmn! #Speaker:Ulrich

~ EditQuest(4, 1)
~ MinigameQuest(4, 2)

[Ulrich nods.] #Speaker:Ulrich

-> END



=== UlrichDialogueVera ===

What now?! #Speaker:Ulrich

    * [About Vera...]
    
-Uh, Vera was stung by... something. In the water. #Speaker:Cleo 

She says she's gonna puke like crazy if she doesn't get treatment! #Speaker:Cleo

Do you have anything that could help? #Speaker:Cleo

Scheiße, that woman has no self-preservation! #Speaker:Ulrich

I have some flowers that can help with... #Speaker:Ulrich

[Ulrich trails off and appears a little confused, grumbling and scratching his head.] #Speaker:Ulrich

    * [What's up?]
    
-What's going on? #Speaker:Cleo

Pfui, all that pollen must've made my head foggy! #Speaker:Ulrich

My darn pots are all out of order! Look at the mess! Bah, I need to label them next time. #Speaker:Ulrich

    * [I'll help.]
    
-I'll sort those pots for you, if it'll help clear things up for you again...? #Speaker:Cleo

~ EditQuest(4, 1)
~ MinigameQuest(4, 2)

Yes, yes, do that! #Speaker:Ulrich

-> END