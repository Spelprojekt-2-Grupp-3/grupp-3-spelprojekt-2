EXTERNAL EditQuest(int ID, int step)
EXTERNAL Give()
INCLUDE ../globals.ink
EXTERNAL Fade(int time)

-> VeraDialogueUlrich

=== VeraDialogueUlrich ===

I see you took care of those shells for me! #Speaker:Vera

Now then, I'll hand you that nasal spray. #Speaker:Vera

~ Fade(3.5)

Here ya go! #Speaker:Vera

~ Give()

    * [Thanks.]
    
-Thanks for the help. #Speaker:Cleo

~ EditQuest(3, 3)

Back at ya! #Speaker:Vera

I gotta say, I'm shocked you could figure out what Ulrich needed! #Speaker:Vera

That man can be harder to figure out than one of Irma's riddles! #Speaker:Vera

    * [Irma's riddles?]
    
She loves those little brain teasers! #Speaker:Vera

~ IrmaRiddles = "active"

She challenges me to solve 'em every so often, and they have me stumped every time! #Speaker:Vera

If you like riddles, ask her to dish one out to ya. Maybe you'd do better at it than me! #Speaker:Vera

    * [I bet.]

~ IrmaRiddles = "standby"

No kidding, it was a challenge for sure! #Speaker:Cleo

-You best be off now. Don't leave poor Ulrich sniffling too much longer! #Speaker:Vera

-> END