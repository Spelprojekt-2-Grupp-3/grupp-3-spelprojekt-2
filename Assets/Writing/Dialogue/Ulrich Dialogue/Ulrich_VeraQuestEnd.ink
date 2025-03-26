EXTERNAL EditQuest(int ID, int step)
INCLUDE ../globals.ink

{UlrichQuest == "complete":

-> Ulrich_VeraQuestEnd

- else:

-> Ulrich_VeraQuestEndCongested

}

=== Ulrich_VeraQuestEndCongested ===

[Ulrich walks over to the pots and picks a few flowers.] #Speaker:Ulrich

Hrrgnmm! #Speaker:Ulrich

[He makes a crushing motion with his fist, and then a... drinking gesture?] #Speaker:Ulrich

    * [Tea?]
    
-Should we make tea of these...? #Speaker:Cleo

[Ulrich nods.] #Speaker:Ulrich

    * [Got it.]
    
-Alright, I'll let Vera know! #Speaker:Cleo

Thanks, Ulrich! #Speaker:Cleo

~ EditQuest(4, 3)

Hrnm. #Speaker:Ulrich

-> END




=== Ulrich_VeraQuestEnd ===

[Ulrich walks over to the pots and picks a few flowers.] #Speaker:Ulrich

There, make tea out of these and it should dissolve the toxin! #Speaker:Ulrich

    * [Got it.]
    
-Alright, I'll let Vera know! #Speaker:Cleo

Thanks, Ulrich! #Speaker:Cleo

~ EditQuest(4, 3)

Hrnm. #Speaker:Ulrich

-> END