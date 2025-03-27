EXTERNAL EditQuest(int ID, int step)
EXTERNAL Fade(int time)

-> SigridDialogueIrma

=== SigridDialogueIrma ===

I worked out that issue for you. #Speaker:Cleo

I appreciate the help. I'll get you a cog. #Speaker:Sigrid

~ Fade(3.5)

//[Sigrid starts rummaging through a whole bunch of boxes.] #Speaker:Sigrid

//[She then returns and hands you a very, very heavy package.] #Speaker:Sigrid

..... I don't know what size she needs, so I packed them all in this box. #Speaker:Sigrid

    * [Thanks.]
    
-Oh, gee! That's heavy! #Speaker:Cleo

Anyway, thanks! I'll be going now. #Speaker:Cleo

~ EditQuest(2, 3)

Bye. #Speaker:Sigrid

-> END