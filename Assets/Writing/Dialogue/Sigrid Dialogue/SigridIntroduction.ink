EXTERNAL EditQuest(int ID, int step)
INCLUDE ../globals.ink

-> SigridDialogue1

=== SigridDialogue1 ===

[You prepare to knock on the lighthouse entrance...] #Speaker:Cleo

[...but someone opens the door before your fist can reach it.] #Speaker:Cleo

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
    
-You trade favors around here, right? #Speaker:Cleo

What if I helped you out with something first? #Speaker:Cleo

Hmm... yeah, that works. #Speaker:Sigrid

Lucky you, I do need help with something right now. #Speaker:Sigrid

I stay up late a lot, and for me to do that, I need coffee. #Speaker:Sigrid

And I'm fresh out. #Speaker:Sigrid

Bengt has the best coffee around here. I refuse to drink anything else. #Speaker:Sigrid

I want you to head to Bengt and get me some. Think you're up for it? #Speaker:Sigrid

    * [Why only Bengts'?]
    
Why only Bengt's coffee? Can't I ask anyone else? #Speaker:Cleo
    
Ulrich only drinks tea, Irma's coffee is too weak, and Vera's is too... eccentric. #Speaker:Sigrid

Eccentric...? #Speaker:Cleo

She puts some weird... fish goo in it or whatever. #Speaker:Sigrid

She says it's for nutrition, but it tastes real funky. And slimey. It's gross. #Speaker:Sigrid

Oh, ew. #Speaker:Cleo

Yeah. Ask her if you want to know what on earth she puts in there. #Speaker:Sigrid

~ VeraCoffeeMystery = "active"

Anyway... #Speaker:Cleo

    * [Yes.]
    
- I'll get you that coffee! #Speaker:Cleo

~ EditQuest(1, 0)

'Preciate it. #Speaker:Sigrid

-> END
