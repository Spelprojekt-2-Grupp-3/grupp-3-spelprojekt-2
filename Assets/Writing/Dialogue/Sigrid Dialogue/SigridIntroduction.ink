EXTERNAL EditQuest(string questTitle, string questInfo, int questIndex)

-> SigridDialogue1

=== SigridDialogue1 ===

[You prepare to knock on the lighthouse entrance...] #Speaker:Cleo

    * [<color=\#29c445>Skip (testing)</color>] 
    
    ~ EditQuest("The lighthouse keeper", "Get coffee from Bengt", 1)
    
    -> END

    * [Continue dialogue]

-[...but someone opens the door before your fist can reach it.] #Speaker:Cleo

Hey. #Speaker:Sigrid

Don't look so alarmed, I saw you approaching from the top of the lighthouse. #Speaker:Sigrid

I don't recognize you at all. You new here? #Speaker:Sigrid

    * [I'm Cleo.]
    
-I'm Cleo. I'm... sort of just passing by, really. #Speaker:Cleo
    
Alright then. I'm Sigrid. #Speaker:Sigrid

What brings you here? I'm not usually one to receive guests. #Speaker:Sigrid

    * [My boat...]
    
-I need to upgrade my boat. Bengt told me to come see you. #Speaker:Cleo

I gotta admit, I'm a little hesitant to deal with a total stranger. #Speaker:Sigrid

Nothing personal, of course. Just not used to outsiders is all. #Speaker:Sigrid

    * [A favor for a favor?]
    
-Bengt told me you all trade favors here. What if I helped you out with something first?

Hmm... yeah, that works. #Speaker:Sigrid

Lucky you, I do need help with something right now. #Speaker:Sigrid

I stay up late a lot, and for me to do that, I need coffee. #Speaker:Sigrid

And I'm fresh out. #Speaker:Sigrid

Bengt has the best coffee around here. I refuse to drink anything else. #Speaker:Sigrid

I want you to head to Bengt and get me some from him. Think you're up for it? #Speaker:Sigrid

    * [Yes.]
    
- I'll get you that coffee! #Speaker:Cleo

'Preciate it. #Speaker:Sigrid

~ EditQuest("The lighthouse keeper", "Get coffee from Bengt", 1)

-> END
