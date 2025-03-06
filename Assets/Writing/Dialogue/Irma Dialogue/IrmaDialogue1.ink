EXTERNAL UpdateQuest(questIndex, Title, info, step)

-> IrmaDialogue1

=== IrmaDialogue1 ===

[You approach the hut on the island and knock on the door.] #Speaker:Cleo

    * [<color=\#29c445>Skip (testing)</color>] 
    
    ~ UpdateQuest(3, "The tech expert", "Help Irma", 1)
    
    -> END

    * [Continue dialogue]

-[You hear a voice coming from inside.] #Speaker:Cleo

One sec! #Speaker:Irma

[...] #Speaker:Cleo

[.....] #Speaker:Cleo

[.......] #Speaker:Cleo

    * [Hello?]
    
-Hello? Is anyone in there? #Speaker:Cleo

[You knock again.] #Speaker:Cleo

Shoot- I'll be right there! #Speaker:Irma

[...] #Speaker:Cleo

[A few more seconds pass until the door finally opens.] #Speaker:Cleo

Sorry, sorry, I was busy doing something and... #Speaker:Irma

... Do I know you? #Speaker:Irma

    * [I'm Cleo.]

-I'm Cleo. I came here just recently. #Speaker:Cleo
    
Cool, cool. What brings you to this old shack, then? #Speaker:Irma

    * [Bengt sent me.]
    
-I was sent here by Bengt. He told me to speak to someone called Irma. #Speaker:Cleo
    
You're speaking to her right now! How can I help you? #Speaker:Irma

    * [My boat...]
    
-I need to get some upgrades for my boat. I was told you could help me fix my radio. #Speaker:Cleo

You've definitely come to the right place, then! #Speaker:Irma

There's just one teeny problem. #Speaker:Irma

[Blah blah minigame and quest stuff.] #Speaker:Irma

I'm gonna need you to help me out before I can fix that radio of yours. #Speaker:Irma

If you're up for it, that is. #Speaker:Irma

    * [I am.]
    
-I'll help you with the minigame. #Speaker:Cleo
    
Perfect! Meet me there. #Speaker:Irma

~ UpdateQuest(2, "The tech expert", "Help Irma", 1)

-> END
