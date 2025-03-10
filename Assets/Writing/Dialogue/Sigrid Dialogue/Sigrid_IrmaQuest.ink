EXTERNAL EditQuest(questTitle, questInfo, questIndex)
EXTERNAL InsertItem(packageIndex)

-> SigridDialogueIrma

=== SigridDialogueIrma ===

What's up? #Speaker:Sigrid

    * [About a cog...]
    
-Irma needs a cog for a project she's working on. She asked me to come get it for her. #Speaker:Cleo

Right, I have a bunch of those. #Speaker:Sigrid

Let me get one. #Speaker:Sigrid

[Sigrid starts rummaging through a whole bunch of boxes.] #Speaker:Sigrid

[She then returns and hands you a very, very heavy package.] #Speaker:Sigrid

I don't know what size she needs, so I packed them all in there. #Speaker:Cleo

    * [Thanks.]
    
-Oh, gee! That's heavy. #Speaker:Cleo

Anyway, thanks! I'll be going now. #Speaker:Cleo

Bye. #Speaker:Sigrid

~ EditQuest("The tech expert", "Deliver the cogs to Irma", 2)
~ InsertItem(1)

-> END