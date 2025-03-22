INCLUDE globals.ink

{ person_name == "": -> main | -> already_chose }

=== main ===
Which person do you hate the most?
+ [Irma]
    -> chosen("Irma")
+ [Sigrid]
    -> chosen("Sigrid")
+ [Ulrich]
    -> chosen("Ulrich")
+ [Vera]
    -> chosen("Vera")

=== chosen(person) ===
~ person_name = person
You chose {person}!
-> END

=== already_chose ===
You already chose {person_name}!
-> END
