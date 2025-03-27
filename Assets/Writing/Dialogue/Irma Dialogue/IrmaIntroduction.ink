EXTERNAL EditQuest(int ID, int step)
INCLUDE ../globals.ink

-> IrmaDialogue1

=== IrmaDialogue1 ===

Hi, I'm looking for... #Speaker:Cleo 

One sec! #Speaker:Irma

... #Speaker:Cleo

..... #Speaker:Cleo

....... #Speaker:Cleo

    * [Hello?]
    
-Uh, hello? I'm here for- #Speaker:Cleo

Shoot- I'll be right with you! #Speaker:Irma

... #Speaker:Cleo

Sorry, sorry, I was busy doing something and... #Speaker:Irma

... Do I know you? #Speaker:Irma

    * [I'm Cleo.]

-I'm Cleo. I came here just recently. #Speaker:Cleo
    
Cool, cool. What brings you to this old shack, then? #Speaker:Irma

    * [Bengt sent me.]
    
-I was sent here by Bengt. He told me to speak to someone called Irma. #Speaker:Cleo
    
You're speaking to her right now! How can I help you? #Speaker:Irma

    * [My boat...]
    
-I need to get some upgrades for my boat. I was told you could help me with that. #Speaker:Cleo

You've definitely come to the right place, then! #Speaker:Irma

There's just one teeny problem. #Speaker:Irma

I'm in desperate need of a cog! It's for a project of mine. #Speaker:Irma

Sigrid has a bunch of spare cogs for her lighthouse. #Speaker:Irma

If you're up for it, I'd like you to go and borrow one for me. #Speaker:Irma

If I don't get this tower up and running again, poor Ulrich is gonna miss his radio program! #Speaker:Irma

    * [What program?]
    
Ulrich might miss his radio show? What show? #Speaker:Cleo

[Irma looks around and then leans in close, like she's about to spill a secret.] #Speaker:Irma

There's only one program our radios can pick up on from all the way out here... #Speaker:Irma

It's this out-loud reading of a really, REALLY sappy book series. #Speaker:Irma

I caught Ulrich listening to it once, and I'm pretty sure I saw tears in his eyes! Like, actual tears! #Speaker:Irma

I don't have the guts to bring it up to him, myself! Gosh, that would be scary! #Speaker:Irma

But if you're ever feeling brave enough to do it, try not to tease him too hard. #Speaker:Irma

~ UlrichRadioShow = "active"

Anyway, are you going to help me? #Speaker:Irma

    * [I'm in.]
    
~ UlrichRadioShow = "standby"
    
-I'll get you a cog! #Speaker:Cleo

~ EditQuest(2, 0)
    
Perfect! I'll be waiting here. #Speaker:Irma

-> END
