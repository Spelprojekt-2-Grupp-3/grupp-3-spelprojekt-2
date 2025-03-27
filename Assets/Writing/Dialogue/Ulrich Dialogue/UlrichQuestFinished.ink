EXTERNAL EditQuest(int ID, int step)
EXTERNAL Fade(int time)
INCLUDE ../globals.ink

-> UlrichDialogue2

=== UlrichDialogue2 ===

[You hand Ulrich the nasal spray, and he doesn't waste a second before taking a hit of it.] #Speaker:Ulrich

[A few coughs and sniffs later, he clears his throat.] #Speaker:Ulrich

Scheiße! Good riddance! #Speaker:Ulrich

Any longer and I would've suffocated to death like some sad sack of- #Speaker:Ulrich

And who are you? Do I know you? #Speaker:Ulrich

    * [I'm Cleo.]
    
-You wouldn't recognize me, I'm new here. The name's Cleo. #Speaker:Cleo

Well, Cleo... I appreciate the medicine. #Speaker:Ulrich

I guess I owe you now, so what do you want? #Speaker:Ulrich

    * [My boat...]
    
-Again, it's about my boat... #Speaker:Cleo

Right, right. You're here about the upgrade. I'm on it. #Speaker:Ulrich

Wait here, will you? #Speaker:Ulrich

~ Fade(3.5)

//[Ulrich shuffles over to your boat and installs the upgrade.] #Speaker:Ulrich 

..... There. #Speaker:Ulrich

I have work to get to. Whatever else you got to say, make it quick. #Speaker:Ulrich

    * [My parents...]
    
I was told my parents passed through this archipelago a long time ago. #Speaker:Cleo

{parentsAskedAbout == "":

~ parentsAskedAbout = "true"

}

Have you seen them at all? Do you know where they might've gone? #Speaker:Cleo

Listen, kid. My memory ain't what it used to be. I can't remember everyone that passed by. #Speaker:Ulrich

[While speaking harshly, there's something about Ulrich's eyes that soften.]

I can't help you. #Speaker:Ulrich

Now, move along. #Speaker:Ulrich

That's too bad... I'll just keep looking. #Speaker:Cleo

    * [That's all.]
    
Nothing comes to mind. I'll take my leave now! #Speaker:Cleo

-[Ulrich mumbles something under his breath before coughing once again.] #Speaker:Ulrich

~ EditQuest(3, 4)
~ UlrichQuest = "complete"
{
- SigridQuest == "complete" and IrmaQuest == "complete" and VeraQuest == "complete":
~ EditQuest(0, 0)
}

Off with you, then. #Speaker:Ulrich

-> END