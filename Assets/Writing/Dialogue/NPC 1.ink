You finally arrived. #speaker:NPC1
-> start

=== start ===
What do you want? #speaker:NPC1
+ [I need information.]
    That depends... <color=\#30FF87>what kind</color> of information? #speaker:NPC1
+ [Just passing through.]
    Then keep moving. <color=\#FF9930>Strangers</color> tend to disappear around here. #speaker:NPC1

- "I don’t trust you. <b><color=\#FF1E35>Who are you really?</color></b>" #speaker:Player

Anything else? #speaker:NPC1
+ [Yes.]
    -> start
+ [No.]
    Then we’re done here. #speaker:NPC1
    -> END
