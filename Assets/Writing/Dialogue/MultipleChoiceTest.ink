-> main

=== main ===
Can you deliver my package?
    + [Yes]
        -> chosen("Yes")
    + [No]
        -> chosen("No")
    + [Maybe]
        -> chosen("Maybe")
        
=== chosen(answer) ===
You chose {answer}!
-> END